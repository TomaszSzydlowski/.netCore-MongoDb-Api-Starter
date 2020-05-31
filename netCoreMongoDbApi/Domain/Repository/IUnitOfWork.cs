using System;
using System.Threading.Tasks;

namespace netCoreMongoDbApi.Domain.Repository{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
