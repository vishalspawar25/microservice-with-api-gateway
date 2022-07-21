using Microsoft.AspNetCore.Mvc;

namespace ProductService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {


        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        
        public IEnumerable<Product> Get()
        {
            return new List<Product> {
                new Product { Id = 1, Name = "apple", Price = 34 },
              new Product { Id = 1, Name = "orages", Price = 26 }};
        }
    }
}