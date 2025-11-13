using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KafkaConsumerTest.Models
{
    public class RealThing
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string SomeString { get; set; } = null!;
        public bool IsThisReal { get; set; }
    }
}
