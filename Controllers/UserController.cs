using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskApp.DTOs;
using TaskApp.Models;
using TaskApp.Repositories.Database;

namespace TaskApp.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly ILogger<UserController> logger;

        public UserController(UnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, ILogger<UserController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<UserInfoDTO>> GetUserInfo()
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            User user = await unitOfWork.UserRepository.FindByEmailOrUsername(userEmail);

            var mappedUser = mapper.Map<User, UserInfoDTO>(user);
            return Ok(mappedUser);
        }

        [HttpPost("assignRole")]
        public async Task<ActionResult<UserInfoDTO>> AssignRole([FromBody] AssignRoleDTO assignRoleDTO)
        {
            var user = await unitOfWork.UserRepository.FindByEmailOrUsername(assignRoleDTO.UserName);
            if (user == null)
                return BadRequest("User doesn't exists");
            var roleExists = await unitOfWork.UserRepository.RoleExists(assignRoleDTO.Role);
            if (!roleExists)
                return BadRequest("Role doesn't exists");
            var result = await unitOfWork.UserRepository.AssignRoleToUser(user, assignRoleDTO.Role);
            if (result.Succeeded)
                return Ok();
            else
                return BadRequest();
        }

    }
}