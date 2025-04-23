using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Entities;

namespace ToDoAppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListService _toDoListService;

        public ToDoListController(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDoList(int id)
        {
            var toDoList = await _toDoListService.GetListByIdAsync(id);
            return toDoList == null ? NotFound() : Ok(toDoList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToDoList([FromBody] ToDoList toDoList)
        {
            await _toDoListService.AddListAsync(toDoList);
            return CreatedAtAction(nameof(GetToDoList), new { id = toDoList.ToDoListId }, toDoList);
        }

      
    }

}
