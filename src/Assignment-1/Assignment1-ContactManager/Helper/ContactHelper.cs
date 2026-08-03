using System.Text.RegularExpressions;

namespace ContactManager.Helper
{
    /// <summary>
    /// Provides common validation and utility methods used throughout the application.
    /// </summary>
    internal class ContactHelper
    {
        /// <summary>
        /// Determines whether the provided name is valid (non-empty and not whitespace).
        /// </summary>
        /// <param name="nameToValidate">The name to validate.</param>
        /// <returns>True if the name is valid, otherwise false.</returns>
        public bool IsValidName(string? nameToValidate)
        {
            return !string.IsNullOrWhiteSpace(nameToValidate);
        }

        /// <summary>
        /// Determines whether the provided email is in a valid format.
        /// </summary>
        /// <param name="email">The email to validate.</param>
        /// <returns>True if the email has a valid format, otherwise false.</returns>
        public bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// Determines whether the provided phone number is valid (10 digits and numeric).
        /// </summary>
        /// <param name="phone">The phone number to validate.</param>
        /// <returns>True if the phone number is valid, otherwise false.</returns>
        public bool IsValidPhone(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return false;
            }

            return phone.Length == 10 && long.TryParse(phone, out _);
        }
    }
}