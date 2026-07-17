using Assignment1.Models;

namespace Assignment1.Persistence
{
    /// <summary>
    /// This class Repository contains the classes for the CRUD operations in the database.
    /// </summary>
    internal class Repository
    {
        private List<ContactInfo> _contactInfos = new List<ContactInfo>();

        /// <summary>
        /// Adds the contact to the repository list.
        /// </summary>
        /// <param name="contactInfo">The object containing the information.</param>
        public void AddContactInfo(ContactInfo contactInfo)
        {
            this._contactInfos.Add(contactInfo);
        }

        /// <summary>
        /// Returns a copy of the contact list.
        /// </summary>
        /// <returns>It returns the copy of the total contact list.</returns>
        public List<ContactInfo> GetAllContacts()
        {
            List<ContactInfo> duplicate = new List<ContactInfo>();
            foreach (ContactInfo contact in this._contactInfos)
            {
                duplicate.Add(new ContactInfo(contact.Id, contact.Name, contact.Email, contact.Phone, contact.Notes));
            }

            return duplicate;
        }

        /// <summary>
        /// Sends whether the contact exists or not.
        /// </summary>
        /// <param name="phone">The phone number to verify.</param>
        /// <returns>It returns either True or False based on whether the contact exists or not.</returns>
        public bool IsContactExists(string? phone)
        {
            return this._contactInfos.Any(contact => contact.Phone == phone);
        }

        /// <summary>
        /// Returns the contact with the given phone number.
        /// </summary>
        /// <param name="phone">The phone number of the contact.</param>
        /// <returns>The contact if found, otherwise null.</returns>
        public ContactInfo? GetContactByPhone(string phone)
        {
            return this._contactInfos.Find(contact => contact.Phone == phone);
        }

        /// <summary>
        /// Sorts the contacts based on their names.
        /// </summary>
        public void SortContacts()
        {
            this._contactInfos.Sort((a, b) => string.Compare(a.Name, b.Name));
        }

        /// <summary>
        /// Deletes the contact with the given phone number.
        /// </summary>
        /// <param name="phone">The phone number of the contact.</param>
        /// <returns>Either true or false depending on the deletion operation.</returns>
        public bool DeleteContact(string phone)
        {
            ContactInfo? contact = this._contactInfos.Find(contact => contact.Phone == phone);

            if (contact == null)
            {
                return false;
            }

            this._contactInfos.Remove(contact);
            return true;
        }
    }
}