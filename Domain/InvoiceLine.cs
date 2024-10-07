using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class InvoiceLine
    {
        [Key]
        public int InvoiceLineID { get; set; }

        public int InvoiceID { get; set; }

        public string Productname { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
