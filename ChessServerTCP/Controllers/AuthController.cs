using ChessServerTCP.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChessServerTCP.Controllers
{
    [ApiController]
    [Route("login")]
    public class AuthController
    {
        private _context = 
        [HttpGet(Name="login")]
        public Task<ActionResult<User>> createAccount(string userName, string password)
        {

            _context
            return userName;
        }
    }
}
