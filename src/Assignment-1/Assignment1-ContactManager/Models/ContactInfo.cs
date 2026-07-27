namespace Assignment1.Models
{
    /// <summary>
    /// This class ContactInfo contains the base class to store the info of individual contacts.
    /// </summary>
    internal class ContactInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactInfo"/> class.
        /// </summary>
        /// <param name="id">The Guid of the contact.</param>
        /// <param name="name">The name to be added.</param>
        /// <param name="email">The email to be added.</param>
        /// <param name="phone">The phone number to be added.</param>
        /// <param name="notes">The additional notes to be added if needed.</param>
        public ContactInfo(Guid id, string name, string email, string phone, string? notes)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Phone = phone;
            this.Notes = notes;
        }

        /// <summary>
        /// Gets the Guid for the contact.
        /// </summary>
        /// <value>The Guid is the value used.</value>
        public Guid Id { get; }

        /// <summary>
        /// Gets or sets the Name for the contact.
        /// </summary>
        /// <value>The name is the value used.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email for the contact.
        /// </summary>
        /// <value>The email is the value used.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number for the contact.
        /// </summary>
        /// <value>The phone number is the value used.</value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the notes for the contact if needed.
        /// </summary>
        /// <value>The notes is the value used.</value>
        public string? Notes { get; set; }
    }
}