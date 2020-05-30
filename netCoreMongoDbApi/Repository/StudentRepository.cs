using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using netCoreMongoDbApi.DbModels;
using netCoreMongoDbApi.IRepository;
using netCoreMongoDbApi.Models;

namespace netCoreMongoDbApi.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentContext _context = null;

        public StudentRepository(IOptions<Settings> settings)
        {
            _context = new StudentContext(settings);
        }

        public async Task Add(Student student)
        {
            await _context.Students.InsertOneAsync(student);
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            try
            {
                return await _context.Students.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Student> Get(string id)
        {
            try
            {
                var student = Builders<Student>.Filter.Eq("Id", id);
                return await _context.Students.Find(student).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<DeleteResult> Remove(string id)
        {
            return await _context.Students.DeleteOneAsync(Builders<Student>.Filter.Eq("Id", id));
        }

        public async Task<DeleteResult> RemoveAll()
        {
            return await _context.Students.DeleteManyAsync(new BsonDocument());
        }

        public async Task<string> Update(string id, Student student)
        {
            await _context.Students.ReplaceOneAsync(s => s.Id == id, student);
            return "";
        }
    }
}