using GraphQL.Builders;
using GraphQL.Todo.Core.Interfaces;
using GraphQL.Todo.Core.Models;
using GraphQL.Todo.Schema.Interfaces;

namespace GraphQL.Todo.Schema.ScopedResolvers;

public class UserTodoConnectionResolver : IScopedConnectionResolver<User, Core.Models.Todo>
{
    private readonly ITodoService _todoService;

    public UserTodoConnectionResolver(ITodoService todoService)
    {
        _todoService = todoService;
    }

    public IQueryable<Core.Models.Todo> Resolve(IResolveConnectionContext<User> ctx)
    {
        return _todoService.FindAll(ctx.Source.Id, ctx.GetArgument<string>("status"));
    }
}
