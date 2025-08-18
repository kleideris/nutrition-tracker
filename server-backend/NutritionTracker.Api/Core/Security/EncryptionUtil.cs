namespace NutritionTracker.Api.Core.Security
{
    public static class EncryptionUtil
    {
        /// <summary>
        /// Encrypts the plain text using BCrypt hashing algorithm
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string EncryptPassword(string plainText)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainText, workFactor: 12);
        }

        /// <summary>
        /// Verifies that a plain text matches the hashed cipher text
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static bool IsValidPassword(string plainText, string cipherText)
        {
            return BCrypt.Net.BCrypt.Verify(plainText, cipherText);
        }
    }
}
// TODO: Maybe add a check in EncryptPassword to check for null strings or empty spaces and throw exception