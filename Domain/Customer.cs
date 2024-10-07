using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [MaxLength(60)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        //NB
        [NotMapped]
        public int Age { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }


        public Customer()
        {
            Invoices = new List<Invoice>();
        }

    }
}
