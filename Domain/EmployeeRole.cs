namespace Domain
{
    //enum
    public class EmployeeRole
    {
        public int EmployeeRoleID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
