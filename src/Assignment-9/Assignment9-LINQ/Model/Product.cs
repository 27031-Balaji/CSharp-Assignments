namespace Assignment9.Model
{
    /// <summary>
    /// The product class is used to store the product details.
    /// </summary>
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }
    }
}