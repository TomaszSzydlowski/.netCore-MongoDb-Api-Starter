using Microsoft.Extensions.Options;
using MongoDB.Driver;
using netCoreMongoDbApi.Domain.Models;

namespace netCoreMongoDbApi.Persistence.Contexts
{
    public class AppDbContext
    {
        private IMongoDatabase _database = null;

        public AppDbContext(IOptions<Settings> settings)
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