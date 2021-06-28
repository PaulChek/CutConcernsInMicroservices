using Cart.Model;
using Cart.Repos;
using MassTransit;
using Microsoft.Extensions.Logging;
using SharedCode;
using System.Threading.Tasks;

namespace Cart {
    internal class MessageConsumer : IConsumer<RabbitMqSetup.Message> {
        private readonly IRepo<ShoppingCart> _cartRepo;
        private readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(IRepo<ShoppingCart> cartRepo, ILogger<MessageConsumer> logger) {
            _cartRepo = cartRepo;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RabbitMqSetup.Message> context) {
            _logger.LogWarning("Create cart for CustomerId: {cart}", context.Message.CustomerId);

            var cart = new ShoppingCart { CustomerId = context.Message.CustomerId };

            await _cartRepo.CreateCart(cart);
        }
    }
}