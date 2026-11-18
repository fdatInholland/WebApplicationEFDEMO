using Domain;
using Shared.Request;

namespace ServiceLayer
{
    public interface ICustomerService
    {
        /// <summary>
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        Task<bool> DeleteCustomerAsync(int customerId);

        Task<Customer?> UpdateCustomerAsync(int id, Customer updatedCustomer);

        Task<List<Customer>> GetCustomerWithInvoicesAsync(int customerId);

        Task<Customer?> FindCustomerByLastNameAsync(string lastName);

        Task<IEnumerable<Customer>> SearchByLastNameAsync(string lastName);

        Task<Customer?> GetCustomerAsync(int customerId);

        Task<IEnumerable<object>> GetCustomerInvoicesByFirstNameAsync(string firstName);

        Task<int> CreateCustomerInvoiceAsync(CreateCustomerInvoiceRequestDTO createCustomerInvoiceRequest);
    }
}
