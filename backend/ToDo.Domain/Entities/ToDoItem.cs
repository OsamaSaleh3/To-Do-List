using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Entities
{
    public class ToDoItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public bool IsCompleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

    }
}
