﻿using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Enums;
using TodoApp.Infrastructure.Data;
using TodoApp.Infrastructure.Repositories;
using Xunit;

namespace TodoApp.Infrastructure.Tests;

public class ToDoItemRepositoryTests
{
    private readonly AppDbContext _context;
    private readonly ToDoItemRepository _repository;

    public ToDoItemRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase").Options;

        _context = new AppDbContext(options);
        _repository = new ToDoItemRepository(_context);
    }

    [Fact]
    public async Task GetAllItemsAsync_ShouldReturnAllItems()
    {
        _context.ToDoItems.AddRange(
            new ToDoItem
            {
                ToDoItemId = 1,
                ToDoListId = 1,
                Title = "Item #1",
                Description = "Description #1",
                DueDate = DateTime.Now,
                IsCompleted = false,
                Priority = PriorityLevel.Medium,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            },
            new ToDoItem
            {
                ToDoItemId = 2,
                ToDoListId = 1,
                Title = "Item #2",
                Description = "Description #2",
                DueDate = DateTime.Now,
                IsCompleted = false,
                Priority = PriorityLevel.High,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            }
        );

        await _context.SaveChangesAsync();

        var items = await _repository.GetAllItemsAsync();

        Assert.True(items.Count() >= 2);
    }

    [Fact]
    public async Task GetItemById_ShouldReturnItem()
    {
        var item = new ToDoItem
        {
            ToDoItemId = 1,
            ToDoListId = 1,
            Title = "Item #1",
            Description = "Description #1",
            DueDate = DateTime.Now,
            IsCompleted = false,
            Priority = PriorityLevel.Medium,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        await _context.ToDoItems.AddAsync(item);
        await _context.SaveChangesAsync();

        var result = await _repository.GetItemByIdAsync(item.ToDoItemId);

        Assert.NotNull(result);
        Assert.Equal("Item #1", result.Title);
    }


    [Fact]
    public async Task AddItemAsync_ShouldAddItem()
    {
        var item = new ToDoItem
        {
            ToDoItemId = 1,
            ToDoListId = 1,
            Title = "Item #1 added",
            Description = "Description #1",
            DueDate = DateTime.Now,
            IsCompleted = false,
            Priority = PriorityLevel.Medium,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        await _repository.AddItemAsync(item);
        var items = await _repository.GetItemByIdAsync(1);

        Assert.Equal("Item #1 added", items.Title);
    }

    [Fact]
    public async Task DeleteItemAsync_ShouldRemoveItem()
    {
        var item = new ToDoItem
        {
            ToDoItemId = 1,
            ToDoListId = 1,
            Title = "Item #1",
            Description = "Description #1",
            DueDate = DateTime.Now,
            IsCompleted = false,
            Priority = PriorityLevel.Medium,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        await _context.ToDoItems.AddAsync(item);
        await _context.SaveChangesAsync();

        await _repository.DeleteItemAsync(1);
        var result = await _repository.GetItemByIdAsync(1);

        Assert.Null(result);
    }
}
