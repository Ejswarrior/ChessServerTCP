using ChessServerTCP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ChessServerTCP.functions.SaltValue;
using ChessServerTCP.Interfaces;

namespace ChessServerTCP.Controllers
{
    [ApiController]
    [Route("login")]
    public class AuthController: ControllerBase
    {
        private readonly UserContext _context;
        private SaltValue _salt;

        public AuthController(UserContext context)
        {
            _context = context;
            _salt = new SaltValue();
        }

        [HttpPost(Name="/create-account")]
        public async Task<IActionResult> createAccount(string userName, string password)
            
        {
            var existingUserName = _context.User.Where<User>(_user => _user.userName == userName).FirstOrDefault();

            if (existingUserName != null) throw new Exception("Username already exists");
            var hashedPassword = _salt.getSaltedValue(password);

            var user = new User { id = "1234121sdasdsddasdasa", totalWins = 0, totalLosses = 0, userName = userName, password = hashedPassword};

            try
            {
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(createAccount), new { user.id }, user);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpGet(Name="/login")]
        public IActionResult login(string userName, string password)
        {

            var existingUser = _context.User.Where<User>(_user => _user.userName == userName).FirstOrDefault();

            if (existingUser == null) throw new Exception("Invalid username or password");

            var unHashedPassword = _salt.parsedSaltedValue(existingUser.password);

            if (unHashedPassword != password) throw new Exception("Invalid username or password");


            return Ok(User);

        }

        [HttpPatch(Name="update-user/{id}")]
        public async Task<IActionResult> updateUser(string id, IUserCreateDTO userUpdate)
        {
            var existingUser = await _context.User.FindAsync(id);

            if (existingUser == null) throw new Exception("No user found");


            if(existingUser.totalWins != 0) existingUser.totalWins = userUpdate.totalWins;
            if(existingUser.totalLosses != 0) existingUser.totalLosses = userUpdate.totalLosses;

            try
            {
                _context.Update<User>(existingUser);
                await _context.SaveChangesAsync();
                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
