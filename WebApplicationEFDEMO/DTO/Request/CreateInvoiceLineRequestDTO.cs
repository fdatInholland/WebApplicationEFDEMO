namespace WebApplicationEFDEMO.DTO.Request
{
    public record CreateInvoiceLineRequestDTO(string Description, int Quantity, decimal UnitPrice);
}
