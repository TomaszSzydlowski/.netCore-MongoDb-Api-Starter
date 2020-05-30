using Microsoft.Extensions.Options;
using MongoDB.Driver;
using netCoreMongoDbApi.Models;

namespace netCoreMongoDbApi.DbModels
{
    public class StudentContext
    {
        private IMongoDatabase _database = null;

        public StudentContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Student> Students
        {
            get
            {
                return _database.GetCollection<Student>("Student");
            }
        }
    }
}