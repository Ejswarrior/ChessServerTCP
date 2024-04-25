using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace ChessServerTCP.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string email { get; set; }
        [Required]
        [MaxLength(25)]
        public string password { get; set; }

        [AllowNull]
        public int totalWins { get; set; }
        [AllowNull]
        public int totalLosses { get; set; }
    }
}
