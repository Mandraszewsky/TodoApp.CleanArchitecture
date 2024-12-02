namespace TodoApp.Domain.Entities;
public class User
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<ToDoList> ToDoLists { get; set; } = new List<ToDoList>();
}
