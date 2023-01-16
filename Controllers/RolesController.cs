using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskApp.DTOs;
using TaskApp.Models;
using TaskApp.Repositories.Database;

namespace TaskApp.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;

        public RoleController(UnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateRoleDTO createRoleDTO)
        {
            Role role = mapper.Map<Role>(createRoleDTO);
            var result = await unitOfWork.UserRepository.CreateRole(role);
            if (result.Succeeded)
                return Ok();
            else
                return BadRequest();
        }
    }
}