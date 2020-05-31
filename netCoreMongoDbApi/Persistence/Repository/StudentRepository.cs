using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using netCoreMongoDbApi.Domain.IRepository;
using netCoreMongoDbApi.Domain.Models;
using netCoreMongoDbApi.Persistence.Contexts;
using netCoreMongoDbApi.Persistence.Repositories;

namespace netCoreMongoDbApi.Persistence.Repository
{
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(IOptions<Settings> settings) : base(settings)
        {

        }

        public async Task AddAsync(Student student)
        {
            await _context.Students.InsertOneAsync(student);
        }

        public async Task<IEnumerable<Student>> ListAsync()
        {
            return await _context.Students.Find(_ => true).ToListAsync();
        }

        public async Task<Student> FindByIdAsync(string id)
        {
            var student = Builders<Student>.Filter.Eq("Id", id);
            return await _context.Students.Find(student).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(string id)
        {
            await _context.Students.DeleteOneAsync(Builders<Student>.Filter.Eq("Id", id));
        }

        public async Task RemoveAllAsnyc()
        {
            await _context.Students.DeleteManyAsync(new BsonDocument());
        }

        public async Task UpdateAsync(string id, Student student)
        {
            student.Id=id;
            await _context.Students.ReplaceOneAsync(s => s.Id == id, student);
        }
    }
}