using Customers.Models;
using Customers.Queries;
using MassTransit;
using MediatR;
using SharedCode;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Customers.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IMediator mediator, IPublishEndpoint publishEndpoint, ILogger<CustomerController> logger) {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(string id) {
            var res = await _mediator.Send(new GetCustomerById.Query(id));
            return Ok(res.customer);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] Customer customer) {
            var res = await _mediator.Send(new CreateNewCustomer.Command(customer));

            //create cart rabbit mq
            await _publishEndpoint.Publish(new RabbitMqSetup.Message() { CustomerId = res });
            //
            _logger.LogInformation("Customer {customer} was created", res);
            return Ok(res);
        }

    }
}
