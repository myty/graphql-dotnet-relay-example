using GraphQL.Builders;
using GraphQL.Todo.Core.Interfaces;
using GraphQL.Todo.Core.Models;
using GraphQL.Todo.Schema.Interfaces;

namespace GraphQL.Todo.Schema.ScopedResolvers;

public class UserConnectionResolver : IScopedConnectionResolver<User>
{
    private readonly IUserService _userService;

    public UserConnectionResolver(IUserService userService)
    {
        _userService = userService;
    }

    public IQueryable<User> Resolve(IResolveConnectionContext ctx)
    {
        return _userService.FindAll();
    }
}
