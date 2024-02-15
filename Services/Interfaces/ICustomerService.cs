using API.Models;

namespace API.Services.Interfaces
{
    public interface ICustomerService
    {
        List<string> GetCustomers();
        Customer GetCustomerById(string id);
        Customer CreateCustomer(Customer customer); // Uncomment this line
        Customer UpdateCustomer(Customer customer, string id); // Uncomment this line
        void DeleteCustomer(string id); // Uncomment this line
    }
}