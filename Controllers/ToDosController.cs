using Microsoft.AspNetCore.Mvc;
using TaskApp.Models;
using TaskApp.Repositories.Database;

namespace TaskApp.Controllers
{
    [Route("api/toDos")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;

        public ToDosController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ToDo>> Get([FromRoute] int id)
        {
            var toDo = await unitOfWork.ToDoRepository.Get(id);
            if (toDo == null)
            {
                return NotFound();
            }
            return toDo;
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDo>>> GetAll()
        {
            var toDos = await unitOfWork.ToDoRepository.GetAll();
            return toDos;
        }

        [HttpPost]
        public async Task<ActionResult<ToDo>> Post([FromBody] ToDo newTodo)
        {
            await unitOfWork.ToDoRepository.Add(newTodo);
            await unitOfWork.SaveAsync();
            return newTodo;
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
    }
}