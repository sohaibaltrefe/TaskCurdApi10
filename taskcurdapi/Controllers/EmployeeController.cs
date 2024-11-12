using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using taskcurdapi.Data;
using taskcurdapi.Dto.Departments;
using taskcurdapi.Dto.Employee;
using taskcurdapi.Models;

namespace taskcurdapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbcontext context;

        public EmployeeController(ApplicationDbcontext context)
        {
            this.context = context;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var employees = context.Employees.Include(e => e.Department).ToList();
            var empDto = employees.Adapt<List<GetAllEmployeeDto>>();

            return Ok(empDto);
        }

        [HttpGet("Details")]
        public IActionResult GetById(int id)
        {
            var employee = context.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.Id == id);
            if (employee is null)
            {
                return NotFound("Employee not found");
            }

            var empDto = employee.Adapt<GetIdEmpDto>();
            return Ok(empDto);
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateemployeeDto empDto)
        {
            var department = context.departments.Find(empDto.DepartmentId);
            if (department == null)
            {
                return NotFound("Department not found");
            }

            var employee = empDto.Adapt<Employee>();
            employee.Department = department;

            context.Employees.Add(employee);
            context.SaveChanges();

            var emplDto = employee.Adapt<GetIdEmpDto>();
            return Ok(emplDto);
        }

        [HttpPut("Update")]
        public IActionResult Update(int id, UpdateEmployeeDto empDto)
        {
            var current = context.Employees.Find(id);
            if (current is null)
            {
                return NotFound("Employee not found");
            }

            empDto.Adapt(current);

            context.SaveChanges();


            var emplDto = current.Adapt<UpdateEmployeeDto>();
            return Ok(emplDto);
        }

        [HttpDelete("Remove")]
        public IActionResult Remove(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee is null)
            {
                return NotFound("Employee not found");
            }

            context.Employees.Remove(employee);
            context.SaveChanges();


            return Ok("succes");
        }
    }
}
