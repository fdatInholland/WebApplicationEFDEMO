using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Request;
using Shared.Response;

namespace ServiceLayer
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerDbContext _customerContext;

        public Task<int> CreateCustomerInvoiceAsync(CreateCustomerInvoiceRequestDTO createCustomerInvoiceRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            Customer foundCustomer = await _customerContext.Customers.FindAsync(customerId);

            if (foundCustomer is null)
            {
                throw new KeyNotFoundException("Customer not Found");
            }

             _customerContext.Customers.Remove(foundCustomer);

            await _customerContext.SaveChangesAsync();
            
            return true;
        }

        public Task<Customer?> FindCustomerByLastNameAsync(string lastName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerContext.Customers
                             .AsQueryable()
                             .ToListAsync();
        }

        public Task<Customer?> GetCustomerAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerResponseDTO?> GetCustomerDtoAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<object>> GetCustomerInvoicesByFirstNameAsync(string firstName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> GetCustomerWithInvoicesAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> SearchByLastNameAsync(string lastName)
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> UpdateCustomerAsync(int id, Customer updatedCustomer)
        {
            throw new NotImplementedException();
        }
    }
}
