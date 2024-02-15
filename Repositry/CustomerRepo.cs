using API.Models;
using API.Repositry.Interfaces;
using System.Data.SqlClient;

using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.NetworkInformation;

namespace API.Repositry
{
    public class CustomerRepo : ICustomerRepo
    {
        readonly string connectionString = "";

        public CustomerRepo()
        {
            connectionString = "Data Source = APINP-ELPTBL26K\\SQLEXPRESS; Initial Catalog = nthwind; User ID = tap2023; Password = tap2023; Encrypt = False";
            // connectionString = "Data Source=APINP-ELPTBL26K\\SQLEXPRESS;Initial Catalog=nthwind;User ID=tap2023;Password=tap2023;Encrypt=False;ApplicationIntent=ReadWrite;";
        }
        public Customer GetCustomerById(string id)
        {
            Customer c = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
                string query = $"select c.CustomerID, c.CompanyName, c.Address, c.ContactName, o.OrderID, o.OrderDate, o.ShipAddress, o.ShipVia,s.CompanyName as ShipperAddress ,s.CompanyName from customers c\r\ninner join orders o\r\non o.CustomerID=c.CustomerID\r\ninner join [Shippers] s \r\non s.ShipperID=o.ShipVia where c.CustomerID='{id}'";
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = query;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    c = new Customer();
                    c.CustomerId = dr["CustomerId"].ToString();
                    c.CompanyName = dr["CompanyName"].ToString();
                    c.ContactName = dr["ContactName"].ToString();
                }
            }
            return c;
        }

        public List<string> GetCustomers()
        {
            throw new NotImplementedException();
        }
        public Customer CreateCustomer(Customer customer)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"INSERT INTO customers (CustomerID, CompanyName, Address, ContactName) VALUES ('{customer.CustomerId}', '{customer.CompanyName}', '{customer.Address}', '{customer.ContactName}')";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            return customer;
        }

        public Customer UpdateCustomer(Customer customer, string id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"UPDATE customers SET CompanyName='{customer.CompanyName}', Address='{customer.Address}', ContactName='{customer.ContactName}' WHERE CustomerID='{id}'";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            return customer;
        }

        public void DeleteCustomer(string id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string updateOrdersQuery = $"UPDATE orders SET CustomerID = NULL WHERE CustomerID = '{id}'";
                SqlCommand updateOrdersCmd = new SqlCommand(updateOrdersQuery, con);
                con.Open();
                var rowsAffected = updateOrdersCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    string deleteCustomerQuery = $"DELETE FROM customers WHERE CustomerID = '{id}'";
                    SqlCommand deleteCustomerCmd = new SqlCommand(deleteCustomerQuery, con);
                    deleteCustomerCmd.ExecuteNonQuery();
                }
                else
                {
                    throw new Exception("No orders associated with the customer.");
                    // Or handle the case where no orders are associated with the customer as per your application's requirements
                }
            }
        }
    }
    }


