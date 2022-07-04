using GraphQL.Todo.Core.Interfaces;
using GraphQL.Todo.Core.Models;

namespace GraphQL.Todo.Services.Services
{
    public class UserService : IUserService
    {
        public User Find(string id)
        {
            return new User { Id = id };
        }
    }
}
