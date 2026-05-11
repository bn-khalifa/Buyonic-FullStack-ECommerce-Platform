using Buyonic.BLL;
using Buyonic.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Buyonic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthManager _authManager;
        public AuthController(IUnitOfWork unitOfWork,
                              UserManager<ApplicationUser> userManager,
                              IAuthManager authManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _authManager = authManager;
        }
        /*
        [HttpPost("seed-admin")]
        public async Task<IActionResult> SeedAdmin()
        {
            if (await _userManager.FindByEmailAsync("admin@buyonic.com") != null)
                return BadRequest("Admin already exists.");

            var admin = new ApplicationUser
            {
                firstName = "Admin",
                lastName = "Buyonic",
                Email = "admin@buyonic.com",
                UserName = "admin@buyonic.com",
                isActive = true,
                createdAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(admin, "Admin@123");
            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(admin, "Admin");
            return Ok("Admin created.");
        }
        */

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var errors = await _authManager.RegisterAsync(dto);
            if (errors != null)
                return BadRequest(errors);

            return Ok("Registration successful.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _authManager.LoginAsync(dto);
            if (token == null)
                return Unauthorized("Invalid email or password.");

            return Ok(new { token });
        }

    }
}
