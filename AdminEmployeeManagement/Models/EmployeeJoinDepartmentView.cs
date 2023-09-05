namespace AdminEmployeeManagement.Models
{
    public class EmployeeJoinDepartmentView
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Department_Name { get; set; }
        public bool Status { get; set; }
    }
}
