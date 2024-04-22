namespace ChessServerTCP.Interfaces
{
    public partial interface IUserCreateDTO
    {
        int totalWins { get; }

        int totalLosses { get; }
    }

    public interface IUserDTO 
    {
        public int totalWins { get; set; }

        public int totalLosses { get; set; }

        public string userName { get; set; }
    }

}
