using ChessServerTCP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ChessServerTCP.functions.SaltValue;
using ChessServerTCP.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Identity.Core;

namespace ChessServerTCP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController: ControllerBase
    {

        private readonly AppDBContext _dbContext;
        private SaltValue _salt;

        public AuthController(AppDBContext context)
        {
            _dbContext = context;
            _salt = new SaltValue();
        }


        [HttpPost(Name = "/create-account")]
        public async Task<IActionResult> createAccount(string email, string password)
        {
            var existingUser = _dbContext.User.Where(user => user.email == email);

            if (existingUser == null) throw new Exception("Email is already in use");
            var hashedPassword = _salt.getSaltedValue(password);

            var user = new User { Id = 12241331, totalWins = 0, totalLosses = 0, email = email, password=hashedPassword };

            try
            {
                _dbContext.User.Add(user);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(createAccount), new { user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        [HttpGet(Name = "/login")]
        public IActionResult login(string email, string password)
        {

            var existingUser = _dbContext.User.Where<User>(_user => _user.email == email).FirstOrDefault();

            if (existingUser == null) throw new Exception("Invalid username or password");

            if (!_salt.parsedSaltedValue(password, existingUser.password)) throw new Exception("Invalid username or password");

            return Ok(User);


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
