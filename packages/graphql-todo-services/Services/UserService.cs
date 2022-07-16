using GraphQL.Todo.Core.Interfaces;
using GraphQL.Todo.Core.Models;

namespace GraphQL.Todo.Services.Services
{
    public class UserService : IUserService
    {
        private readonly TodoContext _context;

        public UserService(TodoContext context)
        {
            this._context = context;
        }

        public User Find(Guid id)
        {
            return _context.Users.Find(id);
        }

        public IQueryable<User> FindAll()
        {
            return _context.Users;
        }
    }
}
