using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using netCoreMongoDbApi.Domain.Models;

namespace netCoreMongoDbApi.Domain.IRepository
{
    public interface IStudentRepository{
        Task<IEnumerable<Student>> GetAll();
        Task<Student> Get(string id);
        Task Add(Student student);
        Task<string> Update(string id, Student student);
        Task<DeleteResult> Remove(string id);
        Task<DeleteResult> RemoveAll();
    }
}