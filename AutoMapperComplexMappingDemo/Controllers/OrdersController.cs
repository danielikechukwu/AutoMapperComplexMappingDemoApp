using AutoMapper;
using AutoMapperComplexMappingDemo.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    }
}
