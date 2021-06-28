using Customers.Models;
using System.Threading.Tasks;

namespace Customers.Repos {
    public interface IRepository<T> where T : IEntity {
        Task<T> GetAsync(string id);
        Task<string> Create(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(string id);
    }
}
