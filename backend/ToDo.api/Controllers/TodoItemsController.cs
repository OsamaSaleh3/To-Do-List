using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using ToDo.Application.DTOs;
using ToDo.Domain.Interfaces;

namespace ToDo.api.Controllers
{
    [Route("api/TodoItems")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _service;

        public TodoItemsController(ITodoItemService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType( StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll()
        {
            var items=await _service.GetAllAsync();
            if (items == null || !items.Any())
                return NotFound("there is no items.");
            return Ok(items);
        }

        [HttpGet("{id}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("invalid Id"); 
            }
            
            var item=await _service.GetByIdAsync(id);
            if(item is null)
            {
                return NotFound($"the item with id : {id} not found.");
            }
            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
      
        public async Task<ActionResult> Add([FromBody] TodoItemCreateDto dto)
        {
           await _service.AddAsync(dto);
            return Ok(dto);
        }

        [HttpPut]
        [ProducesResponseType( StatusCodes.Status200OK)]
      
        public async Task<ActionResult> Update([FromBody] TodoItemUpdateDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok(dto);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult>Delete(int id)
        {
            if(id<= 0)
            {
                return BadRequest("invalid Id");
            }
            await _service.DeleteAsync(id);
            return Ok();

        }

       

    }
}
