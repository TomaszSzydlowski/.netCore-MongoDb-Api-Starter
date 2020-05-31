using System.Threading.Tasks;
using netCoreMongoDbApi.Domain.Models;
using netCoreMongoDbApi.Domain.Services.Communication;

namespace netCoreMongoDbApi.Domain.Services
{
    public interface IStudentService
    {
        Task<StudentResponse> FindAsync(string id);
        Task<StudentsResponse> ListAsync();

        Task<StudentResponse> SaveAsync(Student student);

        Task<StudentResponse> UpdateAsync(string id, Student student);

        Task<StudentResponse> DeleteAsync(string id);
        Task<StudentsResponse> DeleteAllAsync();
    }
}