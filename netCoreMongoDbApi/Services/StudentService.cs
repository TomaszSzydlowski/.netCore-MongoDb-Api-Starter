using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using netCoreMongoDbApi.Domain.IRepository;
using netCoreMongoDbApi.Domain.Models;
using netCoreMongoDbApi.Domain.Services;
using netCoreMongoDbApi.Domain.Services.Communication;

namespace netCoreMongoDbApi.Services
{
    public class StudentService : IStudentService
    {
        private IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentResponse> FindAsync(string id)
        {
            try
            {
                var result = await _studentRepository.FindByIdAsync(id);
                return new StudentResponse(result);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new StudentResponse($"An error occurred when getting the student: {ex.Message}");
            }
        }

        public async Task<StudentsResponse> ListAsync()
        {
            try
            {
                var result = await _studentRepository.ListAsync();
                return new StudentsResponse(result);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new StudentsResponse($"An error occurred when getting list of students: {ex.Message}");
            }
        }

        public async Task<StudentResponse> SaveAsync(Student student)
        {
            try
            {
                await _studentRepository.AddAsync(student);
                return new StudentResponse(student);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new StudentResponse($"An error occurred when saving the student: {ex.Message}");
            }
        }

        public async Task<StudentResponse> UpdateAsync(string id, Student student)
        {
            var exisitngStudent = await _studentRepository.FindByIdAsync(id);

            if (exisitngStudent == null)
            {
                return new StudentResponse("Student not found.");
            }

            try
            {
                await _studentRepository.UpdateAsync(id, student);
                return new StudentResponse(student);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new StudentResponse($"An error occurred when updating the student: {ex.Message}");
            }
        }

        public async Task<StudentResponse> DeleteAsync(string id)
        {
            var exisitngStudent = await _studentRepository.FindByIdAsync(id);

            if (exisitngStudent == null)
            {
                return new StudentResponse("Student not found.");
            }

            try
            {
                await _studentRepository.RemoveAsync(id);
                return new StudentResponse(exisitngStudent);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new StudentResponse($"An error occurred when removing the student: {ex.Message}");
            }
        }



        public async Task<StudentsResponse> DeleteAllAsync()
        {
            var exisitngStudents = await _studentRepository.ListAsync();

            if (exisitngStudents == null)
            {
                return new StudentsResponse("Students not found.");
            }

            try
            {
                await _studentRepository.RemoveAllAsnyc();
                return new StudentsResponse(exisitngStudents);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new StudentsResponse($"An error occurred when removing the student: {ex.Message}");
            }
        }
    }
}