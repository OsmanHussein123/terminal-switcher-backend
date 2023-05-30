using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ContainerService.models
{
    public class Command
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("action")]
        public string Action { get; set; }
    }
}
