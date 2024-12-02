﻿using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
}
