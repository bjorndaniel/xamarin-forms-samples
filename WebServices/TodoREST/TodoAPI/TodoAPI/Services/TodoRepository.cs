using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data;
using TodoAPI.Domain;
using TodoAPI.Interfaces;

namespace TodoAPI.Services
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoAPIContext _context;

        public TodoRepository(TodoAPIContext context)
        {
            _context = context;
        }

        public Task<List<TodoItem>> GetAllAsync() => _context.TodoItems.ToListAsync();

        public bool DoesItemExist(string id) => _context.TodoItems.Any(item => item.ID == id);

        public Task<TodoItem> FindAsync(string id) => _context.TodoItems.FirstOrDefaultAsync(item => item.ID == id);

        public async Task InsertAsync(TodoItem item)
        {
            await _context.TodoItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TodoItem item)
        {
            var todoItem = await FindAsync(item.ID);
            if(todoItem != null)
            {
                todoItem.Done = item.Done;
                todoItem.Name = item.Name;
                todoItem.Notes = item.Notes;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(string id)
        {
            var todoItem = await FindAsync(id);
            if(todoItem != null)
            {
                _context.TodoItems.Remove(todoItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task InitializeDataAsync()
        {
            var todoItem1 = new TodoItem
            {
                ID = "6bb8a868-dba1-4f1a-93b7-24ebce87e243",
                Name = "Learn app development",
                Notes = "Take Microsoft Learn Courses",
                Done = true
            };

            var todoItem2 = new TodoItem
            {
                ID = "b94afb54-a1cb-4313-8af3-b7511551b33b",
                Name = "Develop apps",
                Notes = "Use Visual Studio and Visual Studio for Mac",
                Done = false
            };

            var todoItem3 = new TodoItem
            {
                ID = "ecfa6f80-3671-4911-aabe-63cc442c1ecf",
                Name = "Publish apps",
                Notes = "All app stores",
                Done = false,
            };
            if (!_context.TodoItems.Any(_ => _.ID == todoItem1.ID))
            {
                await _context.AddAsync(todoItem1);
            }
            if (!_context.TodoItems.Any(_ => _.ID == todoItem2.ID))
            {
                await _context.AddAsync(todoItem2);
            }
            if (!_context.TodoItems.Any(_ => _.ID == todoItem3.ID))
            {
                await _context.AddAsync(todoItem3);
            }
            await _context.SaveChangesAsync();
        }
    }
}
