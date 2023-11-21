using JWTToken_API.Enums;
using JWTToken_API.Interfaces;
using JWTToken_API.Interfaces.ServicesInterfaces;
using JWTToken_API.Model;
using JWTToken_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTToken_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(Roles = UserRoles.Admin)]
    public class AuthanticationController : ControllerBase
    {
        

        private readonly ILogger<AuthanticationController> _logger;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthanticationController(ILogger<AuthanticationController> logger, IUserService userService, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
        }
        /// <summary>
        /// Get list of all users, 
        /// but its access will be limited to Admin users.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsers")]        
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAllUsers());
        }

        /// <summary>
        /// This end point will be used to register new user. 
        /// There is no authorization needed to register any new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("RegisterUser")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(UserModel user)
        {
            ServiceResponse response = await _userService.Register(user);
            if (!response.Status)
                return BadRequest(response);
            else
                return Ok(response);
        }

        /// <summary>
        /// This end point will be used to login authorized user. 
        /// In response a JWT token will be passed.
        /// There is no authorization needed to register any new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login objLogin)
        {
            UserModel user = await _userService.Authenticate(objLogin.Email, objLogin.Password);
            if (user == null)
                return Unauthorized(new ServiceResponse { Status = false, Message = "Invalid Credentials" });
            else
            {
                var configKey = _configuration["JWT:Secret"];
                if (configKey == null) return BadRequest();
                //This JWT:Secret would come from KeyVault.
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configKey));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                List<Claim> claimsSet = new List<Claim>{ new Claim(ClaimTypes.Email, user.Email),
                                        new Claim(ClaimTypes.NameIdentifier, user.Email),
                                        new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}"),
                                        new Claim(ClaimTypes.Role, user.Role)};

                var tokeOptions = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claimsSet,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWT:expireMins"])),
                signingCredentials: signinCredentials
            );
                // Genrate token
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new ServiceResponse { Status = true, Message = tokenString });                
            }
               
        }

    }
}
