using API.Models;

using System.Collections.Generic;

namespace API.Repositry.Interfaces
{
    public interface ICustomerRepo
    {
        List<string> GetCustomers();
        Customer GetCustomerById(string id);
        Customer CreateCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer, string id);
        void DeleteCustomer(string id);
    }
}