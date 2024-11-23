using MaterialDetails.Interfaces;
using MaterialDetails.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MaterialDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup([FromBody] UserSignupModel userSignupModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _authService.RegisterAsync(userSignupModel);

                if (!result)
                {

                    return BadRequest(new { message = "An unexpected error." });
                }

                return StatusCode(StatusCodes.Status201Created, "User created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userLoginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var token = await _authService.LoginAsync(userLoginModel.Email, userLoginModel.Password);

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { message = "Invalid email or password." });
                }
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
