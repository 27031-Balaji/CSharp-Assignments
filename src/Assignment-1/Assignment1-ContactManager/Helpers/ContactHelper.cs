using System.Text.RegularExpressions;
using Assignment1.Models;

namespace Assignment1.Helpers
{
    /// <summary>
    /// Provides common validation and utility methods used throughout the application.
    /// </summary>
    internal class ContactHelper
    {
        /// <summary>
        /// Validates whether the email is in a valid format.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns>True if the email format is valid; otherwise, false.</returns>
        public bool ValidateEmail(string email)
        {
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// Validates whether the phone number contains exactly 10 digits.
        /// </summary>
        /// <param name="phone">The phone number to validate.</param>
        /// <returns>True if the phone number is valid; otherwise, false.</returns>
        public bool ValidatePhone(string phone)
        {
            return phone.Length == 10 && long.TryParse(phone, out _);
        }

        /// <summary>
        /// Checks whether the contact list is empty.
        /// </summary>
        /// <param name="contacts">The list of contacts.</param>
        /// <returns>True if the contact list is empty; otherwise, false.</returns>
        public bool IsContactListEmpty(List<ContactInfo> contacts)
        {
            return contacts.Count == 0;
        }

        /// <summary>
        /// Checks whether the given string is null, empty, or contains only whitespace.
        /// </summary>
        /// <param name="value">The string to validate.</param>
        /// <returns>True if the string is null or whitespace; otherwise, false.</returns>
        public bool IsNullString(string? value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}