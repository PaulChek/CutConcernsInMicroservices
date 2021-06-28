using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Cart.Model {
    public class ShoppingCart : IEntity {
        public ShoppingCart() {
            Items = new List<Items>();
        }
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public List<Items> Items { get; set; }
    }

    public class Items {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
