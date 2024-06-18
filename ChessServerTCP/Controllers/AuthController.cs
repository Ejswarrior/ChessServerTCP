using ChessServerTCP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ChessServerTCP.functions.SaltValue;
using ChessServerTCP.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Identity.Core;
using System.Text;

public interface CreateAcountDto {
    public string email { get; set; }
    public string password { get; set; }
    public string name { get; set; }
}


namespace ChessServerTCP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly AppDBContext _dbContext;
        private SaltValue _salt;

        public AuthController(AppDBContext context, ILogger<AuthController> logger)
        {
            _dbContext = context;
            _salt = new SaltValue();
            _logger = logger;
        }


        [HttpPost]
        public async Task<ActionResult<User>> CreateAccount([FromBody] string content)
        {
            Console.WriteLine(content);
            string rawContent = string.Empty;
            using (var reader = new StreamReader(content,
                          encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false))
            {
                rawContent = await reader.ReadToEndAsync();
            }
            Console.WriteLine(rawContent);
            /*  var existingUser = _dbContext.User.Where(user => user.email == email);

              if (existingUser == null) throw new Exception("Email is already in use");
              var hashedPassword = _salt.getSaltedValue(email);

              var user = new User { totalWins = 0, totalLosses = 0, email = email, password=hashedPassword, firstName= email };

              try
              {
                  _dbContext.User.Add(user);
                  await _dbContext.SaveChangesAsync();
                  Console.WriteLine("User added");
                  return Ok(user);
              }
              catch (Exception ex)
              {
                  Console.WriteLine(ex.ToString());
                  return BadRequest();
              }*/

            return Ok(rawContent);

        }


        [HttpGet(Name = "/login")]
        public IActionResult login(string email, string password)
        {

            Console.WriteLine("Hit login route");
            var existingUser = _dbContext.User.Where<User>(_user => _user.email == email).FirstOrDefault();

            if (existingUser == null)
            {
                Console.WriteLine("No user");
                return Unauthorized();
            }


                if (!_salt.parsedSaltedValue(password, existingUser.password)) Console.WriteLine("invalid password");

            return Ok(existingUser);


        }

        [HttpPatch(Name = "update-user/{id}")]
        public async Task<IActionResult> updateUser(string id, IUserCreateDTO userUpdate)
        {
            var existingUser = await _dbContext.User.FindAsync(id);

            if (existingUser == null) throw new Exception("No user found");


            if (existingUser.totalWins != 0) existingUser.totalWins = userUpdate.totalWins;
            if (existingUser.totalLosses != 0) existingUser.totalLosses = userUpdate.totalLosses;

            try
            {
                _dbContext.Update<User>(existingUser);
                await _dbContext.SaveChangesAsync();
                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



    }
}
