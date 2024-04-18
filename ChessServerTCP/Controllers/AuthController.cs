using ChessServerTCP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace ChessServerTCP.Controllers
{
    [ApiController]
    [Route("login")]
    public class AuthController
    {
        private readonly UserContext? _context;

        [HttpGet(Name="login")]
        public User createAccount(string userName, string password)
            
        {
            var serializedUsername = JsonSerializer.Deserialize(userName);
            var user = new User { id = "1234121sdasdsddasdasa", totalWins = 0, userName = userName, password = password };

            _context?.Add<User>(user);

            _context?.SaveChanges();

            return user;
        }




    }
}
