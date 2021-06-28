using Customers.Models;
using Customers.Repos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Queries {
    public static class GetCustomerById {
        public record Query(string Id) : IRequest<Response>;
        public record Response(Customer customer);

        public class Handler : IRequestHandler<Query, Response> {
            private readonly IRepository<Customer> _repo;

            public Handler(IRepository<Customer> repo) {
                _repo = repo;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken) {
                return new Response(await _repo.GetAsync(request.Id));
            }
        }

    }
}
