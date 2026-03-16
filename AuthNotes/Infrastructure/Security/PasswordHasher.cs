using System.Security.Cryptography;
using System.Text;

namespace AuthNotes.Infrastructure.Security
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            ArgumentNullException.ThrowIfNull(password);

            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public static bool Verify(string password, string hash)
        {
            ArgumentNullException.ThrowIfNull(hash);

            if (password == null)
                return false;

            return Hash(password) == hash;
        }
    }
}
