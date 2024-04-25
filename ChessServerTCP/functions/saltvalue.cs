using BCrypt.Net;
using System.Security.Cryptography;
using System.Text;


namespace ChessServerTCP.functions.SaltValue
{
    public class SaltValue
    {
        private const int keySize = 64;
        private const int iterations = 350000;
        private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        public string getSaltedValue(string unsaltedString)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);

            return BCrypt.Net.BCrypt.HashPassword(unsaltedString, salt);
        }

        public bool parsedSaltedValue(string unsaltedPassword, string saltedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(unsaltedPassword, saltedPassword);
        }
    }
}


