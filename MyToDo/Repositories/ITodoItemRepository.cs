using MyToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Repositories
{
    public interface ITodoItemRepository
    {
        event EventHandler<TodoItem> OnItemAdded;
        event EventHandler<TodoItem> OnItemUpdated;

        Task<List<TodoItem>> GetItemsAsync();
        Task AddItemAsync(TodoItem item);
        Task UpdateItemAsync(TodoItem item);
        Task AddOrUpdateAsync(TodoItem item);
    }
}
