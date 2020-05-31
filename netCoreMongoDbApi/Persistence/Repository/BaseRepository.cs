using Microsoft.Extensions.Options;
using netCoreMongoDbApi.Persistence.Contexts;

namespace netCoreMongoDbApi.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;

        protected BaseRepository(IOptions<Settings> settings)
        {
            _context = new AppDbContext(settings);
        }
    }
}