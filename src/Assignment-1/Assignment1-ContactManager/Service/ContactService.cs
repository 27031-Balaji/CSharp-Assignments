using ContactManager.Model;
using ContactManager.Repository;

namespace ContactManager.Service
{
    /// <summary>
    /// Contains all business logic related to contacts.
    /// </summary>
    internal class ContactService
    {
        private ContactRepository _repository = new ContactRepository();

        /// <summary>
        /// Adds a contact to the repository.
        /// </summary>
        /// <param name="name">The name to be added.</param>
        /// <param name="email">The email to be added.</param>
        /// <param name="phone">The phone number to be added.</param>
        /// <param name="notes">The notes to be added.</param>
        public void AddContact(string name, string email, string phone, string? notes)
        {
            Guid id = Guid.NewGuid();
            ContactInfo contact = new ContactInfo(id, name, email, phone, notes);
            this._repository.AddContact(contact);
        }

        /// <summary>
        /// Returns all contacts.
        /// </summary>
        /// <returns>All contacts or an error message.</returns>
        public List<ContactInfo> GetAllContacts()
        {
            List<ContactInfo> contacts = this._repository.GetAllContacts();
            contacts.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));

            return contacts;
        }

        /// <summary>
        /// Searches for a contact by phone number.
        /// </summary>
        /// <param name="phone">Phone number.</param>
        /// <returns>The contact if found; otherwise null.</returns>
        public ContactInfo? SearchContactByPhoneNumber(string phone)
        {
            return this._repository.GetContactByPhone(phone);
        }

        /// <summary>
        /// Deletes a contact.
        /// </summary>
        /// <param name="phone">The phone number of the contact.</param>
        /// <returns>Status message.</returns>
        public bool DeleteContactByPhoneNumber(string phone)
        {
            ContactInfo? contact = this._repository.GetContactByPhone(phone);
            if (contact == null)
            {
                return false;
            }

            this._repository.DeleteContact(contact);

            return true;
        }

        /// <summary>
        /// Edits the name of a contact.
        /// </summary>
        /// <param name="phone">The phone number of the contact.</param>
        /// <param name="name">The new name.</param>
        /// <returns>Status message.</returns>
        public bool EditContactNameByPhoneNumber(string phone, string name)
        {
            ContactInfo? contact = this._repository.GetContactByPhone(phone);
            if (contact == null)
            {
                return false;
            }

            this._repository.UpdateContactName(contact, name.Trim());

            return true;
        }

        /// <summary>
        /// Edits the email of a contact.
        /// </summary>
        /// <param name="phone">The phone number of the contact.</param>
        /// <param name="email">The new email.</param>
        /// <returns>Status message.</returns>
        public bool EditContactEmailByPhoneNumber(string phone, string email)
        {
            ContactInfo? contact = this._repository.GetContactByPhone(phone);
            if (contact == null)
            {
                return false;
            }

            this._repository.UpdateContactEmail(contact, email);

            return true;
        }

        /// <summary>
        /// Edits the phone number of a contact.
        /// </summary>
        /// <param name="phone">The phone number of the contact.</param>
        /// <param name="phoneNumberToChange">The new phone number.</param>
        /// <returns>Status message.</returns>
        public bool EditContactPhoneByPhoneNumber(string phone, string phoneNumberToChange)
        {
            ContactInfo? contact = this._repository.GetContactByPhone(phone);
            if (contact == null)
            {
                return false;
            }

            this._repository.UpdateContactPhone(contact, phoneNumberToChange);

            return true;
        }

        /// <summary>
        /// Edits the notes of a contact.
        /// </summary>
        /// <param name="phone">The phone number of the contact.</param>
        /// <param name="notes">The new notes.</param>
        /// <returns>Status message.</returns>
        public bool EditContactNotesByPhoneNumber(string phone, string? notes)
        {
            ContactInfo? contact = this._repository.GetContactByPhone(phone);
            if (contact == null)
            {
                return false;
            }

            this._repository.UpdateContactNotes(contact, notes);

            return true;
        }

        /// <summary>
        /// Checks whether the contact list is empty.
        /// </summary>
        /// <returns>Return True or False based on the contact list emptiness.</returns>
        public bool IsContactEmpty()
        {
            return this._repository.ContactCount == 0;
        }

        /// <summary>
        /// Checks whether a contact exists.
        /// </summary>
        /// <param name="phone">The phone number.</param>
        /// <returns>True if the contact exists, otherwise false.</returns>
        public bool IsPhoneRegistered(string phone)
        {
            return this._repository.IsContactExistsByPhoneNumber(phone);
        }
    }
}