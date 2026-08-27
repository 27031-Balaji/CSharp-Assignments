namespace Assignment9.Model
{
    /// <summary>
    /// Represents an order with the ID, date and status.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets the ID of the order.
        /// </summary>
        /// <value>The ID of the order.</value>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the date in which the order happened.
        /// </summary>
        /// <value>The date of the order.</value>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        /// <value>The status of the order.</value>
        public string OrderStatus { get; set; } = string.Empty;
    }
}