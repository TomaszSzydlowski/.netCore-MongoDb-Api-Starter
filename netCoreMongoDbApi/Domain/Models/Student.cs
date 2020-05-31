using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace netCoreMongoDbApi.Domain.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int IndexNumber { get; set; }
        public ESemester Semester { get; set; }

    }
}