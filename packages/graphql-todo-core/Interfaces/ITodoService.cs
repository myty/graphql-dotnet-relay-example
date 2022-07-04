namespace GraphQL.Todo.Core.Interfaces
{
    public interface ITodoService
    {
        Models.Todo Find(string id);
        IQueryable<Models.Todo> FindAll(string userId, string status = null);
    }
}
