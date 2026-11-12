namespace WebApplicationEFDEMO.DTO
{
    public class CreateCustomerInvoiceRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<CreateInvoiceLineDTO> Invoices { get; set; }
    }
}
