using Cart.Model;
using Cart.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Cart.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase {
        private readonly IRepo<ShoppingCart> _cartrepo;
        private readonly ILogger<CartController> _logger;

        public CartController(IRepo<ShoppingCart> cartrepo, ILogger<CartController> logger) {
            _cartrepo = cartrepo;
            _logger = logger;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<ShoppingCart>> Get(string customerId) {
            return Ok(await _cartrepo.Get(customerId));
        }

        [HttpDelete("{customerId}")]
        public async Task<ActionResult<bool>> Delete(string customerId) {
            _logger.LogInformation("Cart of Customer {customer} was deleted", customerId);
            return Ok(await _cartrepo.Delete(customerId));
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Put(ShoppingCart cart) {
            return Ok(await _cartrepo.Update(cart));
        }
    }
}
