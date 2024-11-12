using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using taskcurdapi.Data;
using taskcurdapi.Dto.Departments;
using taskcurdapi.Models;

namespace taskcurdapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbcontext context;

        public DepartmentController(ApplicationDbcontext context)
        {
            this.context = context;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var departments = context.departments
                .Select(x => new DepartmentDto()
                {
                    Name = x.Name,
                }).ToList();

            return Ok(departments);
        }

        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {
            var department = context.departments
                .Where(d => d.Id == id)
                .Select(d => new GetDepartmentsDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Employees = d.employees.Select(e => e.Name).ToList()
                })
                .FirstOrDefault();

            if (department == null)
            {
                return NotFound("Department not found");
            }

            return Ok(department);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateDepartmentDto depdto)
        {
            if (depdto is null)
            {
                return BadRequest("Department name is required");
            }

            Department dep = new Department()
            {
                Name = depdto.Name,
            };

            context.departments.Add(dep);
            context.SaveChanges();

            return Ok(depdto);
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, UpdateDepartDto depdto)
        {
            var current = context.departments.Find(id);
            if (current == null)
            {
                return NotFound("Department not found");
            }

            current.Name = depdto.Name;
            context.SaveChanges();

            var updatedDepartment = new DepartmentDto()
            {
                Name = current.Name
            };

            return Ok(updatedDepartment);
        }

        [HttpDelete("Remove")]
        public IActionResult Remove(removeDepatDto removeDto)
        {
            var department = context.departments.Find(removeDto.Id);
            if (department == null)
            {
                return NotFound("Department not found");
            }

            context.departments.Remove(department);
            context.SaveChanges();

            return Ok("sucsses");
        }
    }
}
