using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Employee
    {

        [Key]
        public int EmployeeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EmployeeRole> employeeRoles { get; set; }
    }
}
