using Assignment1.Models;

namespace Assignment1.Helpers
{
    /// <summary>
    /// This class ContactHelper contains the classes for the services of the contact manager.
    /// </summary>
    internal class ContactHelper
    {
        /// <summary>
        /// The method ValidateEmail is used to validate the length of the phone number.
        /// </summary>
        /// <param name="email">The email to be validated.</param>
        /// <returns>Either True or False based on the validation of phone number.</returns>
        public bool ValidateEmail(string email)
        {
            if (!email.Contains('@'))
            {
                return false;
            }

            if (!email.Contains('.'))
            {
                return false;
            }

            if (email.StartsWith('@') || email.EndsWith('@'))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The method ValidatePhone is used to validate the length of the phone number.
        /// </summary>
        /// <param name="phone">The phone no to be validated.</param>
        /// <returns>Either True or False based on the validation of phone number.</returns>
        public bool ValidatePhoneLength(string phone)
        {
            return phone.Length == 10;
        }

        /// <summary>
        /// The method ValidatePhoneNoCharacters is used to validate whether there are characters are not.
        /// </summary>
        /// <param name="phone">The phone no to be validated.</param>
        /// <returns>Either True or False based on the validation of phone number.</returns>
        public bool ValidatePhoneNoCharacters(string phone)
        {
            return long.TryParse(phone, out long _);
        }

        /// <summary>
        /// The method IsNumeric is used to find whether the phone number is full numbers or contains any characters.
        /// </summary>
        /// <param name="contacts">The contact list to be checked.</param>
        /// <returns>Either True or False based on the validation of phone number.</returns>
        public bool IsContactListEmpty(List<ContactInfo> contacts)
        {
            return contacts.Count == 0;
        }

        /// <summary>
        /// Checks whether the name is empty.
        /// </summary>
        /// <param name="value">The phone number.</param>
        /// <returns>Return True or False based on the name emptiness.</returns>
        public bool IsNullString(string? value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}