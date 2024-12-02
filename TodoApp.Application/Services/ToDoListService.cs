using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services;

public class ToDoListService : IToDoListService
{
    private readonly IToDoListRepository _repository;

    public ToDoListService(IToDoListRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ToDoList>> GetAllListsAsync()
    {
        return await _repository.GetAllListsAsync();
    }

    public async Task<IEnumerable<ToDoList>> GetListsByUserIdAsync(int userId)
    {
        return await _repository.GetListsByUserIdAsync(userId);
    }

    public async Task<ToDoList> GetListByIdAsync(int id)
    {
        return await _repository.GetListByIdAsync(id);
    }

    public async Task AddListAsync(ToDoList list)
    {
        await _repository.AddListAsync(list);
    }

    public async Task UpdateListAsync(ToDoList list)
    {
        await _repository.UpdateListAsync(list);
    }

    public async Task DeleteListAsync(int id)
    {
        await _repository.DeleteListAsync(id);
    }
}
