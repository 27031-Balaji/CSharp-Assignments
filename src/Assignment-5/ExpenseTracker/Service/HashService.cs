using System.Security.Cryptography;
using ExpenseTracker.ConstantLiteral;

namespace ExpenseTracker.Service
{
    /// <summary>
    /// Provides methods for generating cryptographic salts, hashing passwords, and verifying password hashes.
    /// </summary>
    internal class HashService
    {
        /// <summary>
        /// Generates a cryptographically secure random salt.
        /// </summary>
        /// <returns>A base64-encoded string representing the generated salt.</returns>
        public string GenerateSalt()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(Constant.SaltSize));
        }

        /// <summary>
        /// Hashes the password given by the user with the secure generated salt.
        /// </summary>
        /// <param name="password">The password entered by the user.</param>
        /// <param name="salt">The salt generated.</param>
        /// <returns>The password hash generated with the salt.</returns>
        public string HashPassword(string password, string salt)
        {
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                Constant.Iterations,
                HashAlgorithmName.SHA256,
                Constant.HashSize);

            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Verifies the password by equating it with the repository-stored hash.
        /// </summary>
        /// <param name="password">The password entered by the user.</param>
        /// <param name="salt">The salt generated when the account is created.</param>
        /// <param name="storedHash">The stored hash for the specific user.</param>
        /// <returns>True if the password hashes match, else false.</returns>
        public bool VerifyPassword(string password, string salt, string storedHash)
        {
            byte[] computedHashInBytes = Convert.FromBase64String(this.HashPassword(password, salt));
            byte[] storedHashInBytes = Convert.FromBase64String(storedHash);

            return CryptographicOperations.FixedTimeEquals(computedHashInBytes, storedHashInBytes);
        }
    }
}