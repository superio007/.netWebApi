using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentDbContext dbContext;
        public StudentsController(StudentDbContext dbContext)
        {
            this.dbContext= dbContext;
        }

        [HttpGet]
        public IActionResult Read()
        {
            var read = dbContext.student.ToList();
            return Ok(read);
        }
        [HttpPost]
        public IActionResult Create(AddStudent addStudent)
        {
            var Student = new Student()
            {
                Id = new Guid(),
                Name = addStudent.Name,
                Age = addStudent.Age,
            };
            dbContext.student.Add(Student);
            dbContext.SaveChanges();
            return Ok(Student);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id,UpdateStudent updateStudent)
        {
            var update = dbContext.student.Find(id);
            if(update != null)
            {
                update.Name = updateStudent.Name;
                update.Age = updateStudent.Age;
            }
            dbContext.student.Update(update);
            dbContext.SaveChanges();
            return Ok(update);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var delete = dbContext.student.Find(id);
            if(delete != null)
            {
                dbContext.Remove(delete);
            }
            dbContext.SaveChanges();
            return Ok(delete);
        }
    }
}
