using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using netCoreMongoDbApi.Domain.Models;

namespace netCoreMongoDbApi.Domain.IRepository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> ListAsync();
        Task<Student> FindByIdAsync(string id);
        Task AddAsync(Student student);
        Task UpdateAsync(string id, Student student);
        Task RemoveAsync(string id);
        Task RemoveAllAsnyc();
    }
}