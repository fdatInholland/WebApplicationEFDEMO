using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationEFDEMO.DTO;

namespace WebApplicationEFDEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerContext _customersContext;

        public CustomerController(CustomerContext customersContext)
        {
            _customersContext = customersContext;
        }

        //Postman: http://localhost:5241/api/customer/getall
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var Customers = await _customersContext.Customer.ToListAsync();

            return Ok(Customers);
        }

        //Postman: http://localhost:5241/api/customer/DeleteCustomer/5
        [HttpPost("DeleteCustomer/{CustomerId}")]
        public async Task<IActionResult> DeleteCustomer(int CustomerId)
        {
            Customer foundCustomer = await _customersContext.Customer.FindAsync(CustomerId);

            if (foundCustomer is null)
            {
                return NotFound("Customer not Found");
            }
            _customersContext.Customer.Remove(foundCustomer);

            _customersContext.SaveChangesAsync();

            return NoContent();
        }

        //[HttpPut("{CustomerId}")]
        //public async Task<IActionResult> EditCustomer(int CustomerId, [FromBody] string FN)
        //{
        //    Customer foundCustomer = await _customersContext.Customer.FindAsync(CustomerId);
        //    if (foundCustomer is null)
        //    {
        //        return NotFound("Customer not Found");
        //    }

        //    foundCustomer.FirstName = FN;

        //    _customersContext.SaveChangesAsync();

        //    return Ok(foundCustomer);
        //}


        //[HttpPut("{id}")]
        //public async Task<IActionResult> ModifyCustomer(int id, [FromBody]Customer updatedCustomer)
        //{
        //    if (id != updatedCustomer.CustomerID)
        //    {
        //        return BadRequest("Customer ID mismatch.");
        //    }

        //    var existingCustomer = await _customersContext.Customer.FindAsync(id);

        //    if (existingCustomer is null)
        //    {
        //        return NotFound("Customer not found.");
        //    }

        //    existingCustomer.FirstName = updatedCustomer.FirstName;
        //    existingCustomer.LastName = updatedCustomer.LastName;

        //    _customersContext.Entry(existingCustomer).State = EntityState.Modified;
        //    await _customersContext.SaveChangesAsync();

        //    return Ok(existingCustomer);
        //}

        //Postman: http://localhost:5241/api/customer/GetCustomerWithInvoices/31
        [HttpPost("GetCustomerWithInvoices/{CustomerId}")]
        public async Task<IActionResult> GetCustomerWithInvoices(int CustomerId)
        {
            var customer = await _customersContext.Customer
                            .Include(c => c.Invoices)
                            .Where(n => n.CustomerID == CustomerId)
                            .ToListAsync();

            if (customer is null)
            {
                return NotFound("Customer not found.");
            }

            return Ok(customer);
        }

        //[HttpGet("bylastname")]
        //public async Task<IActionResult> FindCustomerByLN(string lastName)
        //{
        //    Customer foundCustomer = null;

        //    if (string.IsNullOrWhiteSpace(lastName))
        //    {
        //        return BadRequest("The 'lastName' query parameter is required.");
        //    }
        //        lastName = lastName.Trim();

        //        //method syntax
        //        foundCustomer = _customersContext.Customer
        //                        .Where(c => c.LastName == lastName)
        //                        .FirstOrDefault<Customer>();

        //    ////query syntax
        //    //var query = from st in _customersContext.Customer
        //    //            where st.LastName == LastName
        //    //            select st;

        //    if (foundCustomer == null)
        //    {
        //        return NotFound("Customer not found");
        //    }

        //    return Ok(foundCustomer);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetCustomersOrderedByLastName()
        //{
        //    var Customer = await _customersContext.Customer.OrderBy(c => c.LastName).ToListAsync();

        //    return Ok(Customer);
        //}

        //[HttpGet("{customerId}")]
        //public async Task<IActionResult> GetCustomerDto(int customerId)
        //{
        //    CustomerDTO? customerDTO = await _customersContext.Customer.Where(c => c.CustomerID == customerId).
        //                    Select(p => new CustomerDTO(p.LastName))
        //                    .FirstOrDefaultAsync();

        //    return Ok(customerDTO);
        //}

        //[HttpGet("invoices/search")]
        //public async Task<IActionResult> GetCustomerInvoices(string firstName)
        //{
        //    if (string.IsNullOrWhiteSpace(firstName))
        //    {
        //        return BadRequest("The 'firstName' query parameter is required.");
        //    }

        //    var CustInvoice = await _customersContext.Customer
        //                    .Where(c => c.FirstName.StartsWith(firstName))
        //                    .SelectMany(c => c.Invoices, (c, i) =>
        //                    new
        //                    {
        //                        c.LastName,
        //                        i.InvoiceDate
        //                    })
        //                    .ToListAsync();

        //    return Ok(CustInvoice);
        //}

        //[HttpGet("Search")]
        //public async Task<IActionResult> GetCustomersWithInvoices([FromQuery] string firstName)
        //{
        //    if (string.IsNullOrWhiteSpace(firstName))
        //    {
        //        return BadRequest("A search term for the first name is required.");
        //    }

        //    var CustInvoice = await _customersContext.Customer
        //                .Where(c => c.FirstName.StartsWith(firstName))
        //                .Include(i => i.Invoices)
        //                .ThenInclude(j => j.InvoiceLines)
        //                .ToListAsync();

        //    return Ok(CustInvoice);
        //}
    }
}
