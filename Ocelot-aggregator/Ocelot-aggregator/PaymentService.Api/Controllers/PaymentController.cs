using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {


        private readonly ILogger<PaymentController> _logger;

        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<Payment> Get()
        {
            return new List<Payment> {
                new Payment { Id = 1, Amount = 34, TransactionType ="Online" },
               new Payment { Id = 2, Amount = 45, TransactionType ="Upi" }};
        }
    }
}