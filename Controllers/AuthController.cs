using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskApp.DTOs;
using TaskApp.Models;
using TaskApp.Repositories.Database;
using TaskApp.Services.Interfaces;

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
        private readonly IMailer mailer;

        public AuthController(
            IConfiguration configuration,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            UnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<AuthController> logger,
            IMailer mailer
        )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
            this.mailer = mailer;
        }

        [HttpPost("signUp")]
        public async Task<ActionResult> SignUp([FromBody] UserRegistrationDTO userRegistrationDTO)
        {
            var emailExists = await unitOfWork.UserRepository.ExistsByEmailOrUsername(userRegistrationDTO.Email);
            if (emailExists)
            {
                return BadRequest($"User with email {userRegistrationDTO.Email} already exists");
            }
            var userNameExists = await unitOfWork.UserRepository.ExistsByEmailOrUsername(userRegistrationDTO.UserName);
            if (userNameExists)
            {
                return BadRequest($"User with username {userRegistrationDTO.UserName} already exists");
            }
            var user = mapper.Map<User>(userRegistrationDTO);
            try
            {
                await userManager.CreateAsync(user, userRegistrationDTO.Password);
                string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                await mailer.Send(user.Email, "Confirma el mail pa", CreateConfirmEmailBody(token, user.Email), "from@api.com");
                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost("confirmEmail")]
        public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmailDTO confirmEmailDTO)
        {
            var user = await unitOfWork.UserRepository.FindByEmailOrUsername(confirmEmailDTO.Email);
            if (user == null)
            {
                return BadRequest("User doesn't exists");
            }
            try
            {
                await userManager.ConfirmEmailAsync(user, confirmEmailDTO.Token);
                return Ok("Account confirmed");
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost("signIn")]
        public async Task<ActionResult<string>> SignInAsync([FromBody] UserSignInDTO userSignInDTO)
        {
            var user = await unitOfWork.UserRepository.FindByEmailOrUsername(userSignInDTO.EmailOrUsername);
            if (user == null)
            {
                return BadRequest("User doesn't exists");
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, userSignInDTO.Password, false);
            if (result.Succeeded)
            {
                string token = CreateToken(user);
                return Ok(token);
            }
            else
            {
                bool isConfirmed = await userManager.IsEmailConfirmedAsync(user);
                if (!isConfirmed)
                {
                    return BadRequest("Email is not confirmed");
                }
                return BadRequest("Incorrect password");
            }
        }

        // [HttpPost("assignRole")]
        // public async Task<ActionResult> AssignRole([FromBody] AssignRoleDTO assignRoleDTO)
        // {
        //     try
        //     {
        //         var user = await unitOfWork.UserRepository.FindByEmailOrUsername(assignRoleDTO.UserName);
        //         if (user == null) 
        //             return BadRequest("User doesn't exists");

        //         var result = await unitOfWork.UserRepository.AddRoleToUser(user, assignRoleDTO.Role).ToString());

        //     }
        //     return Ok();
        // }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                // claves para poder buscar luego el usuario
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
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

        private string CreateConfirmEmailBody(string token, string email)
        {
            return $"<h1>Confirma el email:</h1><br><h2>Token: {token}</h2><br><h2>Email: {email}</h2>";
        }
    }
}