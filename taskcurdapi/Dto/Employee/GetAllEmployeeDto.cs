using taskcurdapi.Dto.Departments;
using taskcurdapi.Models;

namespace taskcurdapi.Dto.Employee
{
    public class GetAllEmployeeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DepartmentDto Department { get; set; }
    }
}
