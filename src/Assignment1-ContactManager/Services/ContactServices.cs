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
        /// <param name="name">The name to be added.</param>
        /// <param name="email">The email to be added.</param>
        /// <param name="phone">The phone number to be added.</param>
        /// <param name="notes">The notes to be added.</param>
        /// <returns>Status message.</returns>
        public string AddContact(string? name, string? email, string? phone, string? notes)
        {
            Guid id = Guid.NewGuid();
            ContactInfo contact = new ContactInfo(id, name, email, phone, notes);
            this._repository.AddContactInfo(contact);
            this._repository.SortContacts();
            return "Contact Added Successfully.";
        }

        /// <summary>
        /// Returns all contacts.
        /// </summary>
        /// <returns>All contacts or an error message.</returns>
        public string GetAllContacts()
        {
            List<ContactInfo> contacts = this._repository.GetAllContacts();
            if (this._helper.IsContactListEmpty(contacts))
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
        /// Searches for a contact.
        /// </summary>
        /// <param name="phone">The phone number of the contact.</param>
        /// <returns>Status message.</returns>
        public string SearchContact(string phone)
        {
            List<ContactInfo> contacts = this._repository.GetAllContacts();
            if (this._helper.IsContactListEmpty(contacts))
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
            List<ContactInfo> contacts = this._repository.GetAllContacts();
            if (this._helper.IsContactListEmpty(contacts))
            {
                return "Contact list is empty.";
            }

            ContactInfo? contact = contacts.Find(contact => contact.Phone == phone);
            if (contact == null)
            {
                return "Contact not found.";
            }

            this._repository.DeleteContact(contact.Id);
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
            List<ContactInfo> contacts = this._repository.GetAllContacts();
            ContactInfo? foundContact = contacts.Find(contact => contact.Phone == phone);
            if (foundContact == null)
            {
                return "Contact not found.";
            }

            ContactInfo? contact = this._repository.GetContactById(foundContact.Id);
            if (contact == null)
            {
                return "Contact not found.";
            }

            contact.Name = name;
            this._repository.SortContacts();
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
            List<ContactInfo> contacts = this._repository.GetAllContacts();
            ContactInfo? foundContact = contacts.Find(contact => contact.Phone == phone);
            if (foundContact == null)
            {
                return "Contact not found.";
            }

            ContactInfo? contact = this._repository.GetContactById(foundContact.Id);
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
            List<ContactInfo> contacts = this._repository.GetAllContacts();
            ContactInfo? foundContact = contacts.Find(contact => contact.Phone == phone);
            if (foundContact == null)
            {
                return "Contact not found.";
            }

            ContactInfo? contact = this._repository.GetContactById(foundContact.Id);
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
            return this._helper.IsContactListEmpty(this._repository.GetAllContacts());
        }

        /// <summary>
        /// Checks whether a contact exists.
        /// </summary>
        /// <param name="value">The phone number.</param>
        /// <returns>True if the contact exists, otherwise false.</returns>
        public bool IsContactExists(string value)
        {
            return this._repository.IsContactExists(value);
        }

        /// <summary>
        /// Checks whether the name is empty.
        /// </summary>
        /// <param name="value">The phone number.</param>
        /// <returns>Return True or False based on the name emptiness.</returns>
        public bool IsNullString(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Checks whether the email is correct.
        /// </summary>
        /// <param name="email">The phone number.</param>
        /// <returns>Return True or False based on the email validation.</returns>
        public bool ValidateEmail(string email)
        {
            return this._helper.ValidateEmail(email);
        }

        /// <summary>
        /// Checks whether the phone number is correct or not.
        /// </summary>
        /// <param name="phone">The phone number.</param>
        /// <returns>Return True or False based on the contact list validation.</returns>
        public bool ValidatePhoneLength(string phone)
        {
            return this._helper.ValidatePhoneLength(phone);
        }

        /// <summary>
        /// Checks whether the phone number is correct or not.
        /// </summary>
        /// <param name="phone">The phone number.</param>
        /// <returns>Return True or False based on the contact list character validation even if string is equal to 10 digits.</returns>
        public bool ValidatePhoneNoCharacters(string phone)
        {
            return this._helper.ValidatePhoneNoCharacters(phone);
        }
    }
}