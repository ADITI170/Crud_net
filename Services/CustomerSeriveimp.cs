using API.Models;
using API.Repositry.Interfaces;
using API.Services.Interfaces;

namespace API1.Services
{
    public class CustomerSeriveImp : ICustomerService
    {
        ICustomerRepo _customerRepo;
        public CustomerSeriveImp(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public Customer GetCustomerById(string id)
        {
            return _customerRepo.GetCustomerById(id);
        }
        public List<string> GetCustomers()
        {
            throw new NotImplementedException();
        }
        public Customer UpdateCustomer(Customer customer, string id)
        {
            // Check if the customer exists
            var existingCustomer = _customerRepo.GetCustomerById(id);
            if (existingCustomer == null)
            {
                return null; // or throw an exception, depending on your requirements
            }

            // Update the customer properties
            existingCustomer.CustomerId = customer.CustomerId;
            existingCustomer.CompanyName = customer.CompanyName;
            existingCustomer.ContactName = customer.ContactName;
            // Update other properties as needed

            // Call the repository to perform the update
            return _customerRepo.UpdateCustomer(existingCustomer, id);
        }

        public void DeleteCustomer(string id)
        {
            _customerRepo.DeleteCustomer(id);
        }

        public Customer CreateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
