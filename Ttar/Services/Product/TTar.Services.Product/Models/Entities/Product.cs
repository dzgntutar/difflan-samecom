using MongoDB.Bson.Serialization.Attributes;

namespace TTar.Services.Product.Models.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Price { get; set; }

        public int Stock { get; set; }  

        public string Description { get; set; }
        
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime CreateDate { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CategoryId { get; set; }

        [BsonIgnore]
        public Category Category { get; set; }
    }
}
