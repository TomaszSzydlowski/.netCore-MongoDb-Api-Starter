using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace netCoreMongoDbApi.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id{get;set;}
        public string Name { get; set; }
        public string Roll { get; set; }
    }
}