using System.Security.Cryptography;

namespace Koggo.Application.Helpers;

public class HashHelper
{
    public static string GetSalt()
    {
        var saltBytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }

        return Convert.ToBase64String(saltBytes);
    }

    public static string HashPassword(string password, string salt)
    {
        // Convert the salt string to a byte array
        var saltBytes = Convert.FromBase64String(salt);

        // Create an instance of Rfc2898DeriveBytes with the given password and salt
        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000);
        // Get the hash value (this will be a 20-byte value in the case of SHA-1)
        var hashBytes = pbkdf2.GetBytes(20);

        // Return the hash value as a base64-encoded string
        return Convert.ToBase64String(hashBytes);
    }
}