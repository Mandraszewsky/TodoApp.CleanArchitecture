using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services;

public class ToDoItemService : IToDoItemService
{
    private readonly IToDoItemRepository _toDoItemRepository;

    public ToDoItemService(IToDoItemRepository toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
    }

    public async Task<IEnumerable<ToDoItem>> GetAllItemsAsync()
    {
        return await _toDoItemRepository.GetAllItemsAsync();
    }

    public async Task<ToDoItem> GetItemByIdAsync(int id)
    {
        return await _toDoItemRepository.GetItemByIdAsync(id);
    }

    public Task<IEnumerable<ToDoItem>> GetItemsByListIdAsync(int toDoListId)
    {
        throw new NotImplementedException();
    }

    public async Task AddItemAsync(ToDoItem item)
    {
        await _toDoItemRepository.AddItemAsync(item);
    }

    public async Task UpdateItemAsync(ToDoItem item)
    {
        await _toDoItemRepository.UpdateItemAsync(item);
    }

    public async Task DeleteItemAsync(int id)
    {
        await _toDoItemRepository.DeleteItemAsync(id);
    }
}
