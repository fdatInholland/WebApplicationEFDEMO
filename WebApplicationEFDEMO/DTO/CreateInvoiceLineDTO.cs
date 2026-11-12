namespace WebApplicationEFDEMO.DTO
{
    public class CreateInvoiceLineDTO
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
