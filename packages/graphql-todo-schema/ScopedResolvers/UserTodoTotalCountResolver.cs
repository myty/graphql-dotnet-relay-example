using GraphQL.Todo.Core.Interfaces;
using GraphQL.Todo.Core.Models;
using GraphQL.Todo.Schema.Interfaces;

namespace GraphQL.Todo.Schema.ScopedResolvers;

public class UserTodoTotalCountResolver : IScopedResolver<User, int>
{
    private readonly ITodoService _todoService;

    public UserTodoTotalCountResolver(ITodoService todoService)
    {
        _todoService = todoService;
    }

    public int Resolve(IResolveFieldContext<User> ctx)
    {
        return _todoService.FindAll(ctx.Source.Id).Count();
    }
}
