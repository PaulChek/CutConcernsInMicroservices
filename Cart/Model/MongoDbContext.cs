using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.Model {
    public class MongoDbContext<T> {
        public MongoDbContext(IConfiguration config) {
            var client = new MongoClient(config["MongoSettings:Host"]);
            collection = client.GetDatabase(config["MongoSettings:DbName"]).GetCollection<T>(config["MongoSettings:CollectionName"]);
        }
        public IMongoCollection<T> collection { get; set; }
    }
}
