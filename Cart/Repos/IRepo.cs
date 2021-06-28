using Cart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.Repos {
    public interface IRepo<T> {
        Task<T> Get(string customerId);
        Task<bool> CreateCart(T cart);
        Task<bool> Delete(string customerId);
        Task<bool> Update(T cart);
    }
}
