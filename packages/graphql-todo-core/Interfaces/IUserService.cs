using GraphQL.Todo.Core.Models;

namespace GraphQL.Todo.Core.Interfaces
{
    public interface IUserService
    {
        User Find(Guid id);
        IQueryable<User> FindAll();
    }
}
