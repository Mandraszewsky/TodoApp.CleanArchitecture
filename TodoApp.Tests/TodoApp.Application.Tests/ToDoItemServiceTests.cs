using Moq;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Domain.Entities;
using Xunit;

namespace TodoApp.Application.Tests;

public class ToDoItemServiceTests
{
    private readonly Mock<IToDoItemRepository> _mockRepository;
    private readonly ToDoItemService _service;

    public ToDoItemServiceTests()
    {
        _mockRepository = new Mock<IToDoItemRepository>();
        _service = new ToDoItemService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllItemsAsync_ReturnItems()
    {
        var expectedItems = new List<ToDoItem>
        {
            new ToDoItem { ToDoItemId = 1, Title = "Task #1", Description = "Description #1"},
            new ToDoItem { ToDoItemId = 2, Title = "Task #2", Description = "Description #2"},
        };

        _mockRepository.Setup(x => x.GetAllItemsAsync()).ReturnsAsync(expectedItems);

        var result = await _service.GetAllItemsAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(expectedItems, result);
    }

    [Fact]
    public async Task GetItemByIdAsync_ReturnItem()
    {
        var expectedItem = new ToDoItem { ToDoItemId = 1, Title = "Task #1", Description = "Description #1" };

        _mockRepository.Setup(x => x.GetItemByIdAsync(1)).ReturnsAsync(expectedItem);

        var result = await _service.GetItemByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(expectedItem, result);
    }

    [Fact]
    public async Task AddItemAsync_RepositoryMethodCall()
    {
        var item = new ToDoItem { ToDoItemId = 1, Title = "Task #1", Description = "Description #1" };

        await _service.AddItemAsync(item);

        _mockRepository.Verify(x => x.AddItemAsync(item), Times.Once);
    }

    [Fact]
    public async Task UpdateItemAsync_RepositoryMethodCall()
    {
        var item = new ToDoItem { ToDoItemId = 1, Title = "Task #1 updated", Description = "Description #1 updated" };

        await _service.UpdateItemAsync(item);

        _mockRepository.Verify(x => x.UpdateItemAsync(item), Times.Once);
    }

    [Fact]
    public async Task DeleteItemAsync_RepositoryMethodCall()
    {
        int itemId = 1;

        await _service.DeleteItemAsync(itemId);

        _mockRepository.Verify(x => x.DeleteItemAsync(itemId), Times.Once);
    }

}
