using System.Security.Cryptography;
using System.Text;

namespace Cnpm.Models;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Convert the input string to a byte array and compute the hash
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert the byte array to a hexadecimal string
            StringBuilder builder = new();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public static bool VerifyPassword(string inputPassword, string storedHash)
    {
        // Hash the input password
        string inputHash = HashPassword(inputPassword);
        
        // Compare the hashes
        return string.Equals(inputHash, storedHash, StringComparison.OrdinalIgnoreCase);
    }
}