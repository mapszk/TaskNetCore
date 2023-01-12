using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskApp.DTOs;
using TaskApp.Models;
using TaskApp.Repositories.Database;

namespace Name.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<AuthController> logger;

        public AuthController(
            IConfiguration configuration,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            UnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<AuthController> logger
        )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
        }

        [HttpPost("signUp")]
        public async Task<ActionResult> SignUp([FromBody] UserRegistrationDTO userRegistrationDTO)
        {
            var emailExists = unitOfWork.UserRepository.ExistsByEmailOrUsername(userRegistrationDTO.Email);
            if (emailExists)
            {
                return BadRequest($"User with email {userRegistrationDTO.Email} already exists");
            }
            var userNameExists = unitOfWork.UserRepository.ExistsByEmailOrUsername(userRegistrationDTO.UserName);
            if (userNameExists)
            {
                return BadRequest($"User with username {userRegistrationDTO.UserName} already exists");
            }
            var user = mapper.Map<User>(userRegistrationDTO);
            await userManager.CreateAsync(user, userRegistrationDTO.Password);
            return Ok();
        }

        [HttpPost("signIn")]
        public async Task<ActionResult<string>> SignInAsync([FromBody] UserSignInDTO userSignInDTO)
        {
            var user = unitOfWork.UserRepository.FindByEmailOrUsername(userSignInDTO.EmailOrUsername);
            if (user == null)
            {
                return BadRequest("User doesn't exists");
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, userSignInDTO.Password, false);
            logger.LogInformation(result.ToString());
            if (result.Succeeded)
            {
                string token = CreateToken(user);
                return Ok(token);
            }
            else
            {
                return BadRequest("Incorrect password");
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}