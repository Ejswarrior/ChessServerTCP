using ChessServerTCP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ChessServerTCP.functions.SaltValue;

namespace ChessServerTCP.Controllers
{
    [ApiController]
    [Route("login")]
    public class AuthController: ControllerBase
    {
        private readonly UserContext? _context;
        private SaltValue? _salt;

        [HttpPost(Name="/create-account")]
        public IActionResult createAccount(string userName, string password)
            
        {
            var existingUserName = _context.User.Where(_user => _user.userName == userName);

            if (existingUserName != null) throw new Exception("Username already exists");
            var hashedPassword = _salt.getSaltedValue(password);

            var user = new User { id = "1234121sdasdsddasdasa", totalWins = 0, userName = userName, password = hashedPassword};

            _context?.Add<User>(user);

            _context?.SaveChanges();

            return Ok(user);
        }


        [HttpGet(Name="/login")]
        public IActionResult login(string userName, string password)
        {

            var existingUser = _context.User.Where(_user => _user.userName == userName);

            if (existingUser != null) throw new Exception("Invalid username or password");

            var unHashedPassword = _salt.parsedSaltedValue(existingUser.password);

            if (unHashedPassword != password) throw new Exception("Invalid username or password");
            

            return Ok("Username");

        }



    }
}
