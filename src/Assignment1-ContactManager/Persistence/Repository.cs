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
        /// Adds the contact to the repository list.
        /// </summary>
        /// <param name="contactInfo">The object containing the information.</param>
        /// <returns>It returns the copy of the total contact list.</returns>
        public List<ContactInfo> GetAllContacts()
        {
            List<ContactInfo> duplicate = new List<ContactInfo>();
            foreach (ContactInfo contact in this._contactInfos)
            {
                ContactInfo rc = contact;
                duplicate.Add(rc);
            }

            return duplicate;
        }

        /// <summary>
        /// Sends whether the contact exists or not.
        /// </summary>
        /// <param name="value">The Guid to verify.</param>
        /// <returns>It returns either True or False based on the existing list.</returns>
        public bool IsContactExists(string? value)
        {
            return this._contactInfos.Any(contact => contact.Name == value || contact.Email == value || contact.Phone == value);
        }

        /// <summary>
        /// Returns the contact with the given Guid.
        /// </summary>
        /// <param name="id">The Guid of the contact.</param>
        /// <returns>The contact if found, otherwise null.</returns>
        public ContactInfo? GetContactById(Guid id)
        {
            return this._contactInfos.Find(contact => contact.Id == id);
        }

        /// <summary>
        /// Sorts the contacts based on their names.
        /// </summary>
        public void SortContacts()
        {
            this._contactInfos.Sort((a, b) => string.Compare(a.Name, b.Name));
        }

        /// <summary>
        /// Deletes the contact with the given Guid.
        /// </summary>
        /// <param name="id">The Guid of the contact.</param>
        public void DeleteContact(Guid id)
        {
            ContactInfo? contact = this.GetContactById(id);
            if (contact == null)
            {
                return;
            }

            this._contactInfos.Remove(contact);
        }
    }
}