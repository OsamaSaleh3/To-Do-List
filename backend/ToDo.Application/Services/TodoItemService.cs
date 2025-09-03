using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.DTOs;
using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces;

namespace ToDo.Application.Services
{
    public class TodoItemService:ITodoItemService
    {
        private readonly ITodoItemRepository _repository;
        private readonly IMapper _mapper;

        public TodoItemService(ITodoItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(TodoItemCreateDto dto)
        {
            var item =  _mapper.Map<ToDoItem>(dto);
            await _repository.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            var exist = await _repository.GetByIdAsync(id);
            if (exist is null)
            {
                throw new Exception($"the item with ID {id} not found.");
            }
            await _repository.DeleteAsync(id);
        }

        public async Task<List<TodoItemDto>> GetAllAsync()
        {
            var items=await _repository.GetAllAsync();
            if(items is null)
            {
                return null;
            }
            return _mapper.Map<List<TodoItemDto>>(items);
        }

        public async Task<TodoItemDto?> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item is null)
                return null;

            return _mapper.Map<TodoItemDto>(item);
        }

        public async Task UpdateAsync(TodoItemUpdateDto dto)
        {
            var exist=await _repository.GetByIdAsync(dto.Id);
            if(exist is null)
            {
                throw new Exception($"the item with ID {dto.Id} not found.");
            }
            var item = _mapper.Map<ToDoItem>(dto);
            await _repository.UpdateAsync(item);
        }
    }
}
