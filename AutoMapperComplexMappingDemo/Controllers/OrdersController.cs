using AutoMapper;
using AutoMapperComplexMappingDemo.Data;
using AutoMapperComplexMappingDemo.DTOs;
using AutoMapperComplexMappingDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoMapperComplexMappingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ECommerceDBContext _context;
        private readonly IMapper _mapper;

        public OrdersController(ECommerceDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //This method is responsible for creating a new order.
        //It accepts an OrderCreateDTO from the client,
        //validates the input data (e.g., checks if the customer exists and verifies the products),
        //maps the DTO to the Order entity,
        //calculates the total order amount based on the selected products and their quantities,
        //and saves the new order to the database.
        // POST: api/order
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody] OrderCreateDTO orderCreateDTO)
        {
            if(orderCreateDTO == null)
            {
                return BadRequest("Order data is null.");
            }
            try
            {
                bool customerExists = await _context.Customers.AnyAsync(c => c.Id == orderCreateDTO.CustomerId);

                if(!customerExists)
                {
                    return NotFound($"Customer with ID {orderCreateDTO.CustomerId} not found.");
                }

                // Validate the products in the order
                // Extract product IDs from the order items
                var productIDs = orderCreateDTO.Items.Select(x => x.ProductId).ToList();

                //Retrieve products from the database
                var products = await _context.Products.Where(p => productIDs.Contains(p.Id)).ToListAsync();

                if(products.Count != productIDs.Count)
                {
                    return BadRequest("One or more products in the order are invalid.");
                }

                //Map OrderCreateDTO to Order entity
                var order = _mapper.Map<Order>(orderCreateDTO);

                // Calculate the total amount of the order based on product prices and quantities
                decimal totalAmount = 0;

                foreach(var item in order.OrderItems)
                {
                    var product = products.First(p => p.Id == item.ProductId);

                    totalAmount += product.Price * item.Quantity;
                }

                order.Amount = totalAmount;

                //Add the new order
                _context.Orders.Add(order);
                await _context.SaveChangesAsync(); //Save changes asynchronously.

                // Fetch the created order along with related data to return in the response
                var createdOrder = await _context.Orders
                    .Include(o => o.Customer)
                    .ThenInclude(c => c.Address)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.Id == order.Id);

                if (createdOrder != null)
                    return StatusCode(500, "An error occurred while creating the order.");

                // Map the created Order entity to OrderDTO
                var orderDTO = _mapper.Map<OrderDTO>(createdOrder);

                // Return the created order data in the response with a status code 201 (Created)
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, orderDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById([FromRoute] int id)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.Customer)
                    .ThenInclude (c => c.Address)
                    .Include (o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (order == null)
                    return NotFound($"Order with ID: {id} not found.");

                var orderDTO = _mapper.Map<OrderDTO>(order);

                // Return the order data in the response
                return Ok(orderDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching data: {ex.Message}");
            }
        }
    }
}
