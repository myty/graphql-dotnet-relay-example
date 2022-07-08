namespace GraphQL.Todo.Core.Interfaces
{
    public interface ITodoService
    {
        Models.Todo Find(Guid id);
        IQueryable<Models.Todo> FindAll(Guid userId, string status = null);
    }
}
