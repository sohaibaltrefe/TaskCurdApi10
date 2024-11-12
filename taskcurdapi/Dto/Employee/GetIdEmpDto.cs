using taskcurdapi.Dto.Departments;
using taskcurdapi.Models;

namespace taskcurdapi.Dto.Employee
{
    public class GetIdEmpDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DepartmentDto Department { get; set; } // Use DepartmentDto
    }
}
