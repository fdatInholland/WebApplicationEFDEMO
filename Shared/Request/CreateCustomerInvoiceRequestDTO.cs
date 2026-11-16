using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Request
{
    public record CreateCustomerInvoiceRequestDTO(string FirstName, string LastName, string Email, List<CreateInvoiceLineRequestDTO> Invoices);
}
