using Assignment9.Model;

namespace Assignment9.Data
{
    public class SampleDatabaseContext
    {
        public List<Product> Products { get; set; }

        public List<Supplier> Suppliers { get; set; }

        public List<Order> Orders { get; set; }

        public SampleDatabaseContext()
        {
            this.Products = new List<Product>();
            this.Suppliers = new List<Supplier>();
            this.Orders = new List<Order>();
        }
    }
}