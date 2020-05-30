using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using netCoreMongoDbApi.IRepository;
using netCoreMongoDbApi.Models;

namespace netCoreMongoDbApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Student>), 200)]
        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _studentRepository.GetAll();
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<Student>), 200)]
        public async Task<IActionResult> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Invalid id.");
            var result = await _studentRepository.Get(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Student student)
        {
            await _studentRepository.Add(student);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] Student student)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Invalid id.");
            await _studentRepository.Update(id, student);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Invalid id.");
            await _studentRepository.Remove(id);
            return Ok();
        }

    }
}