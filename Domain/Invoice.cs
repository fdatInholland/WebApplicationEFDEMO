using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }
        public int CustomerID { get; set; }

        public DateTime InvoiceDate { get; set; }
        public virtual Customer customer { get; set; }

        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }

        public Invoice()
        {
            InvoiceLines = new List<InvoiceLine>();
        }
    }
}
