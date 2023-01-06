namespace Retail_Banking_API.Models
{
    public interface ICustomerInterface
    {
        public (List<Customer>?,string) GetAllCustomers();
        public (Customer?,string) GetCustomer(int cid);
        public (Customer?, string) GetCustomerBySSN(int ssnid);
        public (Customer?,string) AddCustomer(Customer customer);
        public string DeleteCustomer(int CustomerID);
        public (Customer?, string) UpdateCustomer(Customer customer);
    }
}
