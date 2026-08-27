using Assignment9.Model;

namespace Assignment9.Data
{
    /// <summary>
    /// The SampleDatabaseContext class is used to make new lists for <see cref="Product"/>, <see cref="Supplier"/> and <see cref="Order"/>
    /// </summary>
    public class SampleDatabaseContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleDatabaseContext"/> class.
        /// </summary>
        public SampleDatabaseContext()
        {
            this.Products = new List<Product>();
            this.Suppliers = new List<Supplier>();
            this.Orders = new List<Order>();
        }

        /// <summary>
        /// Gets or sets the list of <see cref="Product"/>
        /// </summary>
        /// <value>The list of <see cref="Product"/></value>
        public List<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="Supplier"/>
        /// </summary>
        /// <value>The list of <see cref="Supplier"/></value>
        public List<Supplier> Suppliers { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="Order"/>
        /// </summary>
        /// <value>The list of <see cref="Order"/></value>
        public List<Order> Orders { get; set; }
    }
}