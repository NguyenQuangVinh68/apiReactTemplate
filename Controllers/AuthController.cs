using apiReact.Models;
using apiReact.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace apiReact.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Check validate khi dùng attribute
            ValidationContext context = new ValidationContext(request);

            var error = new List<ValidationResult>();

            bool isValidateError = Validator.TryValidateObject(request, context, error, true);

            if (!isValidateError)
                return Ok(error);

            var result = await _authService.Login(request);
            if (result == null)
                return BadRequest(new { message = "user name or password invalid" });
            return Ok( result.ToString());  

        }
    }
}
