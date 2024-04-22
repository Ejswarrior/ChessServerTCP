namespace ChessServerTCP.Models
{
    public class User
    {

        public string id { get; set; }

        public int totalWins { get; set; }

        public int totalLosses { get; set; }

        public string userName { get; set; }

        public byte[] password { get; set; }


    }
}
