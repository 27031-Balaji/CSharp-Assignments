using Assignment1.Models;

namespace Assignment1.Persistence
{
    /// <summary>
    /// Provides an in-memory repository for contact CRUD operations.
    /// </summary>
    internal class Repository
    {
        private List<ContactInfo> _contacts;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// This is done for future initialization changes if needed. Currently, it initializes an empty list of contacts.
        /// </summary>
        public Repository()
        {
            this._contacts = new List<ContactInfo>();
        }

        /// <summary>
        /// Gets the count of contacts in the repository.
        /// </summary>
        /// <value>The count of the contact list.</value>
        public int ContactCount { get => this._contacts.Count; }

        /// <summary>
        /// Adds the contact to the repository list.
        /// </summary>
        /// <param name="contactInfo">The object containing the information.</param>
        public void AddContactInfo(ContactInfo contactInfo)
        {
            this._contacts.Add(contactInfo);
        }

        /// <summary>
        /// Returns a copy of the contact list.
        /// </summary>
        /// <returns>A new list containing copies of stored contacts.</returns>
        public List<ContactInfo> GetAllContacts()
        {
            List<ContactInfo> duplicate = new List<ContactInfo>();
            foreach (ContactInfo contact in this._contacts)
            {
                duplicate.Add(new ContactInfo(contact.Id, contact.Name, contact.Email, contact.Phone, contact.Notes));
            }

            return duplicate;
        }

        /// <summary>
        /// Determines whether a contact exists with the specified phone number.
        /// </summary>
        /// <param name="phone">The phone number to verify.</param>
        /// <returns>True if a contact with the phone exists, otherwise false.</returns>
        public bool IsContactExistsByPhoneNumber(string? phone)
        {
            return this._contacts.Any(contact => contact.Phone == phone);
        }

        /// <summary>
        /// Returns the contact with the given phone number.
        /// </summary>
        /// <param name="phone">The phone number of the contact.</param>
        /// <returns>The contact if found, otherwise null.</returns>
        public ContactInfo? GetContactByPhone(string phone)
        {
            return this._contacts.Find(contact => contact.Phone == phone);
        }

        /// <summary>
        /// Deletes the contact from the contact list.
        /// </summary>
        /// <param name="contact">The contact entry to be deleted.</param>
        public void DeleteContact(ContactInfo contact)
        {
            this._contacts.Remove(contact);
        }

        /// <summary>
        /// Updates the contact's name in the contact list.
        /// </summary>
        /// <param name="contact">The contact entry to be updated.</param>
        /// <param name="name">The name used for updation.</param>
        public void UpdateName(ContactInfo contact, string name)
        {
            contact.Name = name;
        }

        /// <summary>
        /// Updates the contact's email in the contact list.
        /// </summary>
        /// <param name="contact">The contact entry to be updated.</param>
        /// <param name="email">The email used for updation.</param>
        public void UpdateEmail(ContactInfo contact, string email)
        {
            contact.Email = email;
        }

        /// <summary>
        /// Updates the contact's notes in the contact list.
        /// </summary>
        /// <param name="contact">The contact entry to be updated.</param>
        /// <param name="notes">The new notes value (may be null).</param>
        public void UpdateNotes(ContactInfo contact, string? notes)
        {
            contact.Notes = notes;
        }

        // This is for future implementations if needed.

        /// <summary>
        /// Returns the contact with the given identifier.
        /// </summary>
        /// <param name="id">The Guid identifier of the contact.</param>
        /// <returns>The contact if found, otherwise null.</returns>
        public ContactInfo? GetContactById(Guid id)
        {
            return this._contacts.Find(contact => contact.Id == id);
        }

        /// <summary>
        /// Returns the contact with the given email.
        /// </summary>
        /// <param name="email">The email of the contact.</param>
        /// <returns>The contact if found, otherwise null.</returns>
        public ContactInfo? GetContactByEmail(string email)
        {
            return this._contacts.Find(contact => contact.Email == email);
        }

        /// <summary>
        /// Returns the contact with the given name (case-insensitive).
        /// </summary>
        /// <param name="name">The name of the contact.</param>
        /// <returns>The contact if found, otherwise null.</returns>
        public ContactInfo? GetContactByName(string name)
        {
            return this._contacts.Find(contact => contact.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Determines whether a contact exists with the specified email.
        /// </summary>
        /// <param name="email">The email to verify.</param>
        /// <returns>True if a contact with the email exists, otherwise false.</returns>
        public bool IsContactExistsByEmail(string email)
        {
            return this._contacts.Any(contact => contact.Email == email);
        }
    }
}