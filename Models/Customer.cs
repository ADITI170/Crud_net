namespace API.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipAddress { get; set; }
        public int ShipVia { get; set; }
        public string ShipperAddress { get; set; }
        public string ShipperCompanyName { get; set; }
    }
}