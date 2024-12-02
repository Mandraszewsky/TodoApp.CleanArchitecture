namespace TodoApp.Domain.Entities;

public class ToDoList
{
    public int ToDoListId { get; set; }
    public int UserId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<ToDoItem> Items { get; set; } = new List<ToDoItem>();

}
