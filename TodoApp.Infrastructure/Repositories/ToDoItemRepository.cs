using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure.Repositories;

public class ToDoItemRepository : IToDoItemRepository
{
    private readonly AppDbContext _context;

    public ToDoItemRepository(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task AddItemAsync(ToDoItem item)
    {
        _context.ToDoItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteItemAsync(int id)
    {
        var item = await _context.ToDoItems.FindAsync(id);

        if (item != null)
        {
            _context.ToDoItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ToDoItem>> GetAllItemsAsync()
    {
        return await _context.ToDoItems.ToListAsync();
    }

    public async Task<ToDoItem> GetItemByIdAsync(int id)
    {
        return await _context.ToDoItems.FirstOrDefaultAsync(x => x.ToDoItemId == id);
    }

    public async Task<IEnumerable<ToDoItem>> GetItemsByListIdAsync(int toDoListId)
    {
        return await _context.ToDoItems.Where(x => x.ToDoItemId == toDoListId).ToListAsync();
    }

    public async Task UpdateItemAsync(ToDoItem item)
    {
        _context.ToDoItems.Update(item);
        await _context.SaveChangesAsync();
    }
}
