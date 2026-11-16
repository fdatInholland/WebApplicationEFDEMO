using Domain;
using Shared.Request;
using Shared.Response;

namespace ServiceLayer
{
    public class CustomerService : ICustomerService
    {
        public Task<int> CreateCustomerInvoiceAsync(CreateCustomerInvoiceRequestDTO createCustomerInvoiceRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCustomerAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> FindCustomerByLastNameAsync(string lastName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllCustomersAsync()
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
