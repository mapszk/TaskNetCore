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
            var user = mapper.Map<User>(userRegistrationDTO);
            await userManager.CreateAsync(user, userRegistrationDTO.Password);
            return Ok();
        }

        // [HttpPost("signIn")]
        // public Task<ActionResult<object>> SignIn([FromBody] UserSignInDTO userSignInDTO)
        // {
        //     var exists = unitOfWork.UserRepository.ExistsByEmailOrUsername(userSignInDTO.Email);
        //     if (!exists)
        //     {
        //         return BadRequest("User doesn't exists");
        //     }
        //     var user = 
        //     signInManager.SignInAsync()
        // }
    }
}