namespace taskcurdapi.Dto.Departments
{
    public class GetDepartmentsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Employees { get; set; } 

    }
}
