using netCoreMongoDbApi.Domain.Repository;
using netCoreMongoDbApi.Domain.Models;
using netCoreMongoDbApi.Persistence.Repositories;

namespace netCoreMongoDbApi.Persistence.Repository
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository( IAppDbContext context ) : base(context)
        {

        }
    }
}