using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repo
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Customer>> GetAllCustomers();

        Task<bool> DeleteCustomer(int customerId);

        Task<Customer?> UpdateCustomer(int id, Customer updatedCustomer);

        Task<List<Customer>> GetCustomerWithInvoicesAsync(int customerId);

        Task<Customer?> FindCustomerByLastNameAsync(string lastName);

        Task<IEnumerable<Customer>> SearchByLastNameAsync(string lastName);

        Task<Customer?> GetCustomerDtoAsync(int customerId);

        Task<IEnumerable<object>> GetCustomerInvoicesByFirstName(string firstName);

        Task<int> CreateCustomerInvoiceAsync(Customer Customer);
    }
}
