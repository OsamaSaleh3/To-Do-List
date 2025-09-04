using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.DTOs;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Interfaces
{
    public interface ITodoItemService
    {
        Task<List<TodoItemDto>> GetAllAsync();
        Task<TodoItemDto?> GetByIdAsync(int id);
        Task<ToDoItem> AddAsync(TodoItemCreateDto dto);
        Task UpdateAsync(TodoItemUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
