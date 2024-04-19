using System.Security.Cryptography;


namespace ChessServerTCP.functions.SaltValue
{
    public class SaltValue
    {

        public byte[] getSaltedValue(string unsaltedString)
        {
            Byte[] data = System.Text.Encoding.UTF8.GetBytes(unsaltedString);

            Byte[] hash = SHA256.Create().ComputeHash(data);

            return hash;
        }

        public string parsedSaltedValue(byte[] data)
        {
            byte[] unHashedSring = SHA256.HashData(data);

            string parsedData = System.Text.Encoding.Default.GetString(unHashedSring);

            return parsedData;
        }
    }
}


