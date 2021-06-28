using Customers.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Customers.Repos {
    public class Repository<T> : IRepository<T> where T : IEntity {
        private readonly IMongoCollection<T> _collection;

        private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

        public Repository(MongoContext<T> mongoContext) {
            _collection = mongoContext.Collection;
        }

        public async Task<string> Create(T model) {
            model.Id = ObjectId.GenerateNewId() + "";
            await _collection.InsertOneAsync(model);
            return model.Id;
        }

        public async Task<bool> Delete(string id) {
            var res = await _collection.DeleteOneAsync(id);
            return res.DeletedCount > 0;
        }

        public async Task<T> GetAsync(string id) {
            return await _collection.Find(v => v.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(T model) {
            var res = await _collection.ReplaceOneAsync(_filterBuilder.Eq(v => v.Id, model.Id), model);
            return res.ModifiedCount > 0;
        }
    }
}
