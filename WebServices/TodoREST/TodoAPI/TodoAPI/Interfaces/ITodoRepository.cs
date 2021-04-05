using System.Collections.Generic;
using System.Threading.Tasks;
using TodoAPI.Domain;

namespace TodoAPI.Interfaces
{
    public interface ITodoRepository
    {
        bool DoesItemExist(string id);
        Task<List<TodoItem>> GetAllAsync();
        Task<TodoItem> FindAsync(string id);
        Task InsertAsync(TodoItem item);
        Task UpdateAsync(TodoItem item);
        Task DeleteAsync(string id);
        Task InitializeDataAsync();
    }
}
