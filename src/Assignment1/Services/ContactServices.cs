using Assignment1.Helpers;
using Assignment1.Models;
using Assignment1.Persistence;

namespace Assignment1.Services
{
    /// <summary>
    /// Contains all business logic related to contacts.
    /// </summary>
    internal class ContactServices
    {
        private Repository _repository = new Repository();
        private ContactHelper _helper = new ContactHelper();

        /// <summary>
        /// Adds a contact to the repository.
        /// </summary>
        /// <param name="contact">The contact object.</param>
        /// <returns>Status message.</returns>
        public string AddContact(ContactInfo contact)
        {
            if (_repository.IsContactExists(contact.Id))
            {
                return "Phone number already exists. Try again with a different phone number.";
            }

            _repository.AddContactInfo(contact);
            _repository.SortContacts();
            return "Contact Added Successfully.";
        }

        /// <summary>
        /// Returns all contacts.
        /// </summary>
        /// <returns>All contacts or an error message.</returns>
        public string GetAllContacts()
        {
            List<ContactInfo> contacts = _repository.GetAllContacts();

            if (_helper.IsContactListEmpty(contacts))
            {
                return "Contact list is empty";
            }

            string result = string.Empty;
            result += "Contact List:\n\n";
            int i = 1;
            foreach (ContactInfo contact in contacts)
            {
                result += $"Contact {i}\n";
                result += $"Name: {contact.Name}\n";
                result += $"Email: {contact.Email}\n";
                result += $"Phone Number: {contact.Phone}\n";
                result += $"Notes: {contact.Notes}\n\n";
                i++;
            }

            return result;
        }

        /// <summary>
        /// Searches for a contact using the phone number.
        /// </summary>
        /// <param name="phone">The phone number to search.</param>
        /// <returns>The contact details or an error message.</returns>
        public string SearchContact(string phone)
        {
            List<ContactInfo> contacts = _repository.GetAllContacts();
            if (_helper.IsContactListEmpty(contacts))
            {
                return "Contact list is empty.";
            }

            ContactInfo? contact = contacts.Find(contact => contact.Phone == phone);
            if (contact == null)
            {
                return "Contact not found.";
            }

            string result = string.Empty;
            result += "Contact Found!\n\n";
            result += $"Name : {contact.Name}\n";
            result += $"Email : {contact.Email}\n";
            result += $"Phone Number : {contact.Phone}\n";
            result += $"Notes : {contact.Notes}";
            return result;
        }

        /// <summary>
        /// Deletes a contact.
        /// </summary>
        /// <param name="phone">The phone number of the contact.</param>
        /// <returns>Status message.</returns>
        public string DeleteContact(string phone)
        {
            List<ContactInfo> contacts = _repository.GetAllContacts();
            if (_helper.IsContactListEmpty(contacts))
            {
                return "Contact list is empty.";
            }

            ContactInfo? contact = contacts.Find(contact => contact.Phone == phone);
            if (contact == null)
            {
                return "Contact not found.";
            }

            _repository.DeleteContact(contact.Id);
            return "Contact deleted successfully.";
        }

        /// <summary>
        /// Edits the name of a contact.
        /// </summary>
        /// <param name="phone">The phone number of the contact.</param>
        /// <param name="name">The new name.</param>
        /// <returns>Status message.</returns>
        public string EditName(string phone, string name)
        {
            List<ContactInfo> contacts = _repository.GetAllContacts();
            ContactInfo? foundContact = contacts.Find(contact => contact.Phone == phone);
            if (foundContact == null)
            {
                return "Contact not found.";
            }

            ContactInfo? contact = _repository.GetContactById(foundContact.Id);
            if (contact == null)
            {
                return "Contact not found.";
            }

            contact.Name = name;
            _repository.SortContacts();
            return "Name updated successfully.";
        }

        /// <summary>
        /// Edits the email of a contact.
        /// </summary>
        /// <param name="phone">The phone number of the contact.</param>
        /// <param name="email">The new email.</param>
        /// <returns>Status message.</returns>
        public string EditEmail(string phone, string email)
        {
            List<ContactInfo> contacts = _repository.GetAllContacts();
            ContactInfo? foundContact = contacts.Find(contact => contact.Phone == phone);
            if (foundContact == null)
            {
                return "Contact not found.";
            }

            ContactInfo? contact = _repository.GetContactById(foundContact.Id);
            if (contact == null)
            {
                return "Contact not found.";
            }

            contact.Email = email;
            return "Email updated successfully.";
        }

        /// <summary>
        /// Edits the notes of a contact.
        /// </summary>
        /// <param name="phone">The phone number of the contact.</param>
        /// <param name="notes">The new notes.</param>
        /// <returns>Status message.</returns>
        public string EditNotes(string phone, string? notes)
        {
            List<ContactInfo> contacts = _repository.GetAllContacts();
            ContactInfo? foundContact = contacts.Find(contact => contact.Phone == phone);
            if (foundContact == null)
            {
                return "Contact not found.";
            }

            ContactInfo? contact = _repository.GetContactById(foundContact.Id);
            if (contact == null)
            {
                return "Contact not found.";
            }

            contact.Notes = notes;
            return "Notes updated successfully.";
        }

        /// <summary>
        /// Checks whether the contact list is empty.
        /// </summary>
        /// <returns>Return True or False based on the contact list emptiness.</returns>
        public bool IsContactListEmpty()
        {
            return _helper.IsContactListEmpty(_repository.GetAllContacts());
        }

        /// <summary>
        /// Checks whether a contact exists.
        /// </summary>
        /// <param name="phone">The phone number.</param>
        /// <returns>True if the contact exists, otherwise false.</returns>
        public bool IsContactExists(string phone)
        {
            List<ContactInfo> contacts = _repository.GetAllContacts();
            ContactInfo? contact = contacts.Find(contact => contact.Phone == phone);
            return _repository.IsContactExists(contact.Id);
        }
    }
}