using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Customers.Models {
    public class MongoContext<T> {
        public MongoContext(IConfiguration configuration) {
            var client = new MongoClient(configuration["MongoSettings:Host"]);
            Collection = client.GetDatabase(configuration["MongoSettings:DbName"])
                .GetCollection<T>(configuration["MongoSettings:CollectionName"]);
        }
        public IMongoCollection<T> Collection { get; }
    }
}
