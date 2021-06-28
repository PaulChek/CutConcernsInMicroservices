using Cart.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.Repos {
    public class CartRepository<T> : IRepo<T> where T : IEntity {
        private readonly IMongoCollection<T> collection;

        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public CartRepository(MongoDbContext<T> contextDb) {
            collection = contextDb.collection;
        }
        public async Task<T> Get(string customerId) {
            return await collection.Find(v => v.CustomerId == customerId).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateCart(T cart) {
            await collection.InsertOneAsync(cart);
            return true;
        }

        public async Task<bool> Delete(string customerId) {
            var res = await collection.DeleteOneAsync(v => v.CustomerId == customerId);
            return res.DeletedCount > 0;
        }


        public async Task<bool> Update(T cart) {
            var f = filterBuilder.Eq(v => v.CustomerId, cart.CustomerId);
            var carrtId = await collection.Find(v => v.CustomerId == cart.CustomerId).FirstOrDefaultAsync();
            cart.Id = carrtId.Id;
            var res = await collection.ReplaceOneAsync(f, cart);
            return res.ModifiedCount > 0;
        }
    }
}
