using Domain;

namespace DAL.Repo
{
    public interface ICustomerRepo
    {
        Task<IEnumerable<Customer>> GetAllCustomers();

        Task<bool> DeleteCustomer(int customerId);

        Task<Customer?> UpdateCustomer(int id, Customer updatedCustomer);

        Task<IEnumerable<Customer>> GetCustomerWithInvoices(int customerId);

        Task<Customer?> FindCustomerByLastName(string lastName);

        Task<IEnumerable<Customer>> SearchByLastName(string lastName);

        Task<Customer?> GetCustomer(int customerId);

        Task<IEnumerable<object>> GetCustomerInvoicesByFirstName(string firstName);

        Task<int> CreateCustomerInvoice(Customer Customer);
    }
}
