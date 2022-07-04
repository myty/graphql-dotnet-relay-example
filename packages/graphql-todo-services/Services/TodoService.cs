using GraphQL.Todo.Core.Interfaces;

namespace GraphQL.Todo.Services.Services
{
    public class TodoService : ITodoService
    {
        private static Core.Models.Todo TodoFactory(bool completed = false)
        {
            var id = Guid.NewGuid().ToString("N");

            return new Core.Models.Todo
            {
                Id = id,
                Text = $"This is a test, Id={id}",
                Completed = completed
            };
        }

        public Core.Models.Todo Find(string id)
        {
            return TodoFactory();
        }

        public IQueryable<Core.Models.Todo> FindAll(string userId, string status = null)
        {
            var queryable = new Core.Models.Todo[]
            {
                TodoFactory(),
                TodoFactory(),
                TodoFactory(true)
            }.AsQueryable();

            if (status != "completed")
            {
                return queryable;
            }

            return queryable.Where(todo => todo.Completed);
        }
    }
}
