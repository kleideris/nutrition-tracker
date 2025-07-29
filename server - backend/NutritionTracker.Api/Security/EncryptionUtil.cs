namespace NutritionTracker.Api.Security
{
    public class EncryptionUtil
    {
        // Encrypts the plain text using BCrypt hashing algorithm
        public static string EncryptPassword(string plainText)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainText, workFactor: 12, enhancedEntropy: true);
        }

        // Verifies that a plain text matches the hashed cipher text
        public static bool VerifyPasswordHash(string plainText, string cypherText)
        {
            return BCrypt.Net.BCrypt.Verify(plainText, cypherText);
        }
    }
}