using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace task_1135.Domain.Models
{
    public class ProductReview
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string ProductId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
