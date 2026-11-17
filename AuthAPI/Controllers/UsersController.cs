using AuthAPI.Models;
using AuthAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AuthuserContext _context;

        public UsersController(AuthuserContext context)
        {
            _context = context;
        }

        [HttpPost("newUser")]
        public async Task<IActionResult> CreateUser([FromBody] NewUserDTO newUserDTO)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = newUserDTO.Name,
                Email = newUserDTO.Email,
                Password = newUserDTO.Password,
                RegTime = DateTime.UtcNow
            };

            if (user is null)
            {
                return NotFound("Nincs user.");
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(new { message = $"{user.Name} felvéve." });
        }
    }
}
