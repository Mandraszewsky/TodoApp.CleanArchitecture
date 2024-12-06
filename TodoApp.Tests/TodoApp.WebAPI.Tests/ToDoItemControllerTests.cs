using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.WebAPI.Controllers;
using Xunit;

namespace TodoApp.WebAPI.Tests;

public class ToDoItemControllerTests
{
    private readonly Mock<IToDoItemService> _mockService;
    private readonly ToDoItemsController _controller;

    public ToDoItemControllerTests()
    {
        _mockService = new Mock<IToDoItemService>();
        _controller = new ToDoItemsController(_mockService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithItems()
    {
        var items = new List<ToDoItem>
            {
                new ToDoItem { ToDoItemId = 1, Title = "Task 1" },
                new ToDoItem { ToDoItemId = 2, Title = "Task 2" }
            };
        _mockService.Setup(service => service.GetAllItemsAsync()).ReturnsAsync(items);

        var result = await _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedItems = Assert.IsType<List<ToDoItem>>(okResult.Value);
        Assert.Equal(2, returnedItems.Count);
    }
    [Fact]
    public async Task GetById_ReturnsNotFound_WhenItemDoesNotExist()
    {
        _mockService.Setup(service => service.GetItemByIdAsync(1)).ReturnsAsync((ToDoItem)null);

        var result = await _controller.GetById(1);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithItem()
    {
        var item = new ToDoItem { ToDoItemId = 1, Title = "Task 1" };
        _mockService.Setup(service => service.GetItemByIdAsync(1)).ReturnsAsync(item);

        var result = await _controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedItem = Assert.IsType<ToDoItem>(okResult.Value);
        Assert.Equal(item.ToDoItemId, returnedItem.ToDoItemId);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtActionResult()
    {
        var newItem = new ToDoItem { ToDoItemId = 3, Title = "New Task" };

        var result = await _controller.Create(newItem);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);

        Assert.Equal("GetById", createdResult.ActionName);
        Assert.Equal(newItem.ToDoItemId, createdResult.RouteValues["id"]);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenIdsDoNotMatch()
    {
        var item = new ToDoItem { ToDoItemId = 1, Title = "Updated Task" };

        var result = await _controller.Update(2, item);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsNoContent()
    {
        var item = new ToDoItem { ToDoItemId = 1, Title = "Updated Task" };

        var result = await _controller.Update(1, item);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent()
    {
        int itemId = 1;

        var result = await _controller.Delete(itemId);

        Assert.IsType<NoContentResult>(result);
    }
}
