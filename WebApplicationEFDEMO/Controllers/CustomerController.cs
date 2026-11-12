using Azure.Core;
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

        //        {
        //  "customerID": 5,
        //  "firstName": "Jane",
        //  "lastName": "Doe",
        //  "age": 30
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyCustomer(int id, [FromBody] Customer updatedCustomer)
        {
            if (id != updatedCustomer.CustomerID)
            {
                return BadRequest("Customer ID mismatch.");
            }

            var existingCustomer = await _customersContext.Customer.FindAsync(id);

            if (existingCustomer is null)
            {
                return NotFound("Customer not found.");
            }

            existingCustomer.FirstName = updatedCustomer.FirstName;
            existingCustomer.LastName = updatedCustomer.LastName;

            _customersContext.Entry(existingCustomer).State = EntityState.Modified;
            await _customersContext.SaveChangesAsync();

            return Ok(existingCustomer);
        }

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

        //Postman: http://localhost:5241/api/customer/FindCustomerByLN?lastName=Om
        [HttpPost("FindCustomerByLN")]
        public async Task<IActionResult> FindCustomerByLN(string lastName)
        {
            Customer foundCustomer = null;

            if (string.IsNullOrWhiteSpace(lastName))
            {
                return BadRequest("The 'lastName' query parameter is required.");
            }
            lastName = lastName.Trim();

            //method syntax
            foundCustomer = _customersContext.Customer
                            .Where(c => c.LastName == lastName)
                            .FirstOrDefault<Customer>();

            ////query syntax
            //var query = from st in _customersContext.Customer
            //            where st.LastName == LastName
            //            select st;

            if (foundCustomer == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(foundCustomer);
        }

        //Postman: http://localhost:5241/api/customer/SearchByLastName/Yost
        [HttpGet("SearchByLastName/{lastName}")]
        public async Task<IActionResult> SearchByLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return BadRequest("The 'lastName' query parameter is required.");
            }
            lastName = lastName.Trim();

            var Customer = await _customersContext.Customer.
                                Where(l => l.LastName == lastName).
                                OrderBy(c => c.LastName).ToListAsync();

            return Ok(Customer);
        }

        //Postman:http://localhost:5241/api/customer/GetCustomerDTO/31
        [HttpGet("[action]/{customerId}")]
        public async Task<IActionResult> GetCustomerDTO(int customerId)
        {
            CustomerDTO? customerDTO = await _customersContext.Customer.Where(c => c.CustomerID == customerId).
                            Select(p => new CustomerDTO(p.LastName)).
                            FirstOrDefaultAsync();

            return Ok(customerDTO);
        }

        //Postman:http://localhost:5241/api/customer/GetCustomerInvoices/Lon
        [HttpGet("[action]/{firstName}")]
        public async Task<IActionResult> GetCustomerInvoices(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return BadRequest("The 'firstName' query parameter is required.");
            }

            var CustInvoice = await _customersContext.Customer
                            .Where(c => c.FirstName.StartsWith(firstName))
                            .SelectMany(c => c.Invoices, (c, i) =>
                            new
                            {
                                c.LastName,
                                i.InvoiceDate
                            })
                            .ToListAsync();

            return Ok(CustInvoice);
        }

        //Postman: http://localhost:5241/api/customer/CreateCustomerInvoices
        //        {
        //  "firstName": "Jane",
        //  "lastName": "Doe",
        //  "invoices": [
        //    {
        //      "description": "Consulting Services",
        //      "quantity": 2,
        //      "unitPrice": 150.00
        //    },
        //    {
        //      "description": "Software License",
        //      "quantity": 1,
        //      "unitPrice": 49.99
        //    }
        //  ]
        //}
        [HttpPost]
        public async Task<IActionResult> CreateCustomerInvoices([FromBody] CreateCustomerInvoiceRequest createCustomerInvoiceRequest)
        {
            var customer = new Customer
            {
                FirstName = createCustomerInvoiceRequest.FirstName,
                LastName = createCustomerInvoiceRequest.LastName,
                Invoices = new List<Invoice>
                {
                new Invoice
                    {
                        InvoiceDate = DateTime.UtcNow,
                        InvoiceLines = createCustomerInvoiceRequest.Invoices.Select(l => new InvoiceLine
                                {
                                    Description = l.Description,
                                    Quantity = l.Quantity,
                                    UnitPrice = l.UnitPrice
                                }).ToList()
                            }
                        }
            };

            _customersContext.Customer.Add(customer);
            await _customersContext.SaveChangesAsync();

            return Ok(new
            {
                message = "Customer, invoice, and invoice lines created successfully!",
                customerId = customer.CustomerID
            });
        }
    }
}
