using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services;

public class TodoItemService : ITodoItemService
{    
  /*  public Task AddItemAsync(TodoItem item)
{
    // Implementation for adding a TodoItem
}

public Task MarkDoneAsync(Guid itemId)
{
    // Implementation for marking a TodoItem as done
} */
    private readonly ApplicationDbContext _context;

    public TodoItemService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser user)
    {
        var items = await _context.Items
            .Where(x => x.IsDone == false && x.UserId == user.Id)  // Simplified this to use '!' instead of '== false'
            .ToArrayAsync();
        return items;
    }

    public async Task<bool> AddItemAsync(TodoItem newItem, IdentityUser user)
    {
        newItem.Id = Guid.NewGuid();
        newItem.IsDone = false;
        newItem.DueAt = DateTimeOffset.Now.AddDays(3);
         newItem.UserId = user.Id;

        _context.Items.Add(newItem);

        var saveResult = await _context.SaveChangesAsync();
        return saveResult == 1;
    }
    public async Task<bool> MarkDoneAsync(Guid id, IdentityUser user) 
    {
        var item = await _context.Items
            .Where(x => x.Id == id && x.UserId == user.Id)
            .SingleOrDefaultAsync();

        if (item == null) return false;

        item.IsDone = true;

        Task<int> task = _context.SaveChangesAsync();
        var saveResult = await task;
        return saveResult == 1; // One entity should have been updated
    }


// ...

}

