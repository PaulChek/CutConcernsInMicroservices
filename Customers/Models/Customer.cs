using MongoDB.Bson.Serialization.Attributes;

namespace Customers.Models {
    public class Customer : IEntity {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
