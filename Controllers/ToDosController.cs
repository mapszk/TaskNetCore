using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskApp.DTOs;
using TaskApp.Models;
using TaskApp.Repositories.Database;

namespace TaskApp.Controllers
{
    [Route("api/toDos")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ToDosController(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public ActionResult<ToDoDTO> Get([FromRoute] int id)
        {
            var toDo = unitOfWork.ToDoRepository.GetToDoDetails(id);
            if (toDo == null)
            {
                return NotFound();
            }
            return mapper.Map<ToDoDTO>(toDo);
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDoShortDTO>>> GetAll()
        {
            var toDos = await unitOfWork.ToDoRepository.GetShortToDos();
            return mapper.Map<List<ToDoShortDTO>>(toDos);
        }

        [HttpPost]
        public async Task<ActionResult<ToDoDTO>> Post([FromBody] CreateToDoDTO todoDTO)
        {
            var toDo = mapper.Map<ToDo>(todoDTO);
            unitOfWork.ToDoRepository.Add(toDo);
            await unitOfWork.SaveAsync();
            return Created(nameof(Get), mapper.Map<ToDoDTO>(toDo));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ToDoDTO>> Put([FromRoute] int id, [FromBody] UpdateToDoDTO updateToDoDTO)
        {
            var exists = await unitOfWork.ToDoRepository.Exists(id);
            if (!exists)
            {
                return NotFound();
            }
            var toDo = mapper.Map<ToDo>(updateToDoDTO);
            toDo.Id = id;
            unitOfWork.ToDoRepository.Update(toDo);
            await unitOfWork.SaveAsync();
            return Ok(mapper.Map<ToDoDTO>(toDo));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var toDo = await unitOfWork.ToDoRepository.Get(id);
            if (toDo == null)
            {
                return NotFound();
            }
            unitOfWork.ToDoRepository.Delete(toDo);
            await unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpPost("{toDoId:int}/comment")]
        public async Task<ActionResult<ToDoDTO>> Comment([FromRoute] int toDoId, [FromBody] CreateCommentDTO createCommentDTO)
        {
            var toDo = await unitOfWork.ToDoRepository.Get(toDoId);
            if (toDo == null)
            {
                return BadRequest($"To do with ID {toDoId} doesn't exists");
            }
            var comment = mapper.Map<Comment>(createCommentDTO);
            comment.ToDoId = toDoId;
            unitOfWork.CommentRepository.Add(comment);
            await unitOfWork.SaveAsync();
            return Ok(mapper.Map<ToDoDTO>(toDo));
        }
    }
}