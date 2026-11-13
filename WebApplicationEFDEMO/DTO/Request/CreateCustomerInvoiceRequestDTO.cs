namespace WebApplicationEFDEMO.DTO.Request
{
    public record CreateCustomerInvoiceRequestDTO(string FirstName, string LastName, string Email, List<CreateInvoiceLineRequestDTO> Invoices);
}
