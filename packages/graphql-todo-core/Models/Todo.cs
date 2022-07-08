namespace GraphQL.Todo.Core.Models;

public class Todo
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public bool Completed { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}
