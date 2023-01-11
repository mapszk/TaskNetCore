using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, UnitOfWork unitOfWork, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost("signUp")]
        public async Task<ActionResult> SignUp([FromBody] UserRegistrationDTO userRegistrationDTO)
        {
            var alreadyExists = unitOfWork.UserRepository.ExistsByEmailOrUsername(userRegistrationDTO.Email);
            if(alreadyExists)
            {
                return BadRequest($"User with email {userRegistrationDTO.Email} already exists");
            }
            var user = mapper.Map<User>(userRegistrationDTO);
            await userManager.CreateAsync(user, userRegistrationDTO.Password);
            return Ok();
        }

        // [HttpPost("signIn")]
        // public async Task<ActionResult<object>> SignIn([FromBody] UserSignInDTO userSignInDTO)
        // {
        //     var user = await unitOfWork.UserRepository.FindByEmail(userSignInDTO.Email);
        //     if (user == null)
        //     {
        //         return BadRequest("User doesn't exists");
        //     }
        //     var result = await signInManager.PasswordSignInAsync(user, userSignInDTO.Password);
        //     return { }
        // }
    }
}