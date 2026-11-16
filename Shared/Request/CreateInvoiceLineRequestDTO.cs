using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Request
{
    public record CreateInvoiceLineRequestDTO(string Description, int Quantity, decimal UnitPrice);
}
