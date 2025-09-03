using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces;
using ToDo.Infrastructure.Persistence;

namespace ToDo.Infrastructure.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly AppDbContext _context;
        public TodoItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ToDoItem item)
        {
            await _context.TodoItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item =await _context.TodoItems.FindAsync(id);
            if(item is not null)
            {
                 _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public  async Task<List<ToDoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<ToDoItem?> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task UpdateAsync(ToDoItem item)
        {
            var existing = await _context.TodoItems.FindAsync(item.Id);
            if (existing is not null)
            {
                _context.Entry(existing).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
