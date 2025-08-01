namespace NutritionTracker.Api.Security
{
    public static class EncryptionUtil
    {
        // Encrypts the plain text using BCrypt hashing algorithm
        public static string EncryptPassword(string plainText)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainText, workFactor: 12);
        }

        // Verifies that a plain text matches the hashed cipher text
        public static bool IsValidPassword(string plainText, string cipherText)
        {
            return BCrypt.Net.BCrypt.Verify(plainText, cipherText);
        }
    }
}
// TODO: Maybe add a check in EncryptPassword to check for null strings or empty spaces and throw exception