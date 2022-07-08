using GraphQL.Todo.Core.Interfaces;
using GraphQL.Todo.Core.Models;

namespace GraphQL.Todo.Services.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            this._context = context;
        }

        public Core.Models.Todo Find(Guid id)
        {
            return _context.Todos.Find(id);
        }

        public IQueryable<Core.Models.Todo> FindAll(Guid userId, string status = null)
        {
            var queryable = _context.Todos.Where(todo => todo.UserId == userId);

            return status != "completed" ? queryable : queryable.Where(todo => todo.Completed);
        }
    }
}
