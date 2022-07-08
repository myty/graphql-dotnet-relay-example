namespace GraphQL.Todo.Core.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public List<Todo> Todos { get; } = new();
}
