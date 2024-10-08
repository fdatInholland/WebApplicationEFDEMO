﻿using DAL;
using Domain;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("GetAll")]
        public IActionResult GetAllCustomers()
        {
            return Ok(_customersContext.Customer);
        }

        [HttpPost("DeleteCustomer")]
        public IActionResult DeleteCustomer(int ID)
        {
            Customer foundCustomer = _customersContext.Customer.Find(ID);
            _customersContext.Customer.Remove(foundCustomer);

            _customersContext.SaveChanges();

            return Ok(_customersContext.Customer);
        }

        [HttpPost("EditCustomer")]
        public IActionResult EditCustomer(int ID, string FN)
        {
            Customer foundCustomer = _customersContext.Customer.Find(ID);
            foundCustomer.FirstName = FN;

            _customersContext.SaveChanges();

            return Ok(_customersContext.Customer);
        }


        [HttpPost("ModifyCustomer")]
        public IActionResult ModifyCustomer(Customer customer)
        {
            _customersContext.Entry(customer).State = EntityState.Modified;
            _customersContext.SaveChanges();

            return Ok(_customersContext.Customer);
        }


        [HttpGet("Include")]
        public IActionResult Include(int ID)
        {
            var Customer = _customersContext.Customer
                            .Include(c => c.Invoices)
                            .Where(n => n.CustomerID == ID)
                            .ToList();

            return Ok(Customer);
        }

        [HttpGet("CustomerInvoices")]
        public IActionResult CustomerInvoices()
        {
            var Customers = _customersContext.Customer
                            .Include(c => c.Invoices)
                            .ToList();

            return Ok(Customers);
        }

        [HttpGet("EasyLinq")]
        public IActionResult EasyLinq()
        {
            //method syntax
            var Customer = _customersContext.Customer
                            .Where(c => c.LastName == "Jacobs")
                            .FirstOrDefault<Customer>();

            //query syntax
            var query = from st in _customersContext.Customer
                        where st.LastName == "Billy"
                        select st;

            return Ok(Customer);
        }

        [HttpGet("LN")]
        public IActionResult LN()
        {
            var Customer = _customersContext.Customer.OrderBy(c => c.LastName).ToList();

            return Ok(Customer);
        }

        [HttpGet("Projection")]
        public IActionResult Projection()
        {
            var CustDTO = _customersContext.Customer.
                            Select(p => new CustomerDTO { LastName = p.LastName })
                            .ToList();

            return Ok(CustDTO);
        }

        [HttpGet("SelectNotEasy")]
        public IActionResult SelectNotEasy()
        {
            var CustInvoice = _customersContext.Customer
                            .Where(c => c.FirstName.StartsWith("G"))
                            .SelectMany(c => c.Invoices, (c, i) =>
                            new
                            {
                                c.LastName,
                                i.InvoiceDate
                            })
                            .ToList();

            return Ok(CustInvoice);
        }

        [HttpGet("ThenInclude")]
        public IActionResult ThenInclude()
        {
            var CustInvoice = _customersContext.Customer
                            .Where(c => c.FirstName.StartsWith("S"))
                            .Include(i => i.Invoices)
                            .ThenInclude(j => j.InvoiceLines)
                            .ToList();

            return Ok(CustInvoice);
        }
    }
}
