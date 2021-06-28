using Customers.Models;
using Customers.Repos;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Queries {
    public static class CreateNewCustomer {

        public record Command(Customer customer) : IRequest<string>;

        public class Handler : IRequestHandler<Command, string> {

            private readonly IRepository<Customer> _repo;
            public Handler(IRepository<Customer> repo) {
                _repo = repo;

            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken) {
                return await _repo.Create(request.customer);
            }
        }
    }
}
