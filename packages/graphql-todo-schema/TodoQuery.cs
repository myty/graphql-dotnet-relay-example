using GraphQL.MicrosoftDI;
using GraphQL.Relay.Types;
using GraphQL.Todo.Core.Interfaces;
using GraphQL.Todo.Core.Models;
using GraphQL.Todo.Schema.Extensions;
using GraphQL.Todo.Schema.ScopedResolvers;
using GraphQL.Todo.Schema.Types;
using GraphQL.Types;

namespace GraphQL.Todo.Schema;

public class TodoQuery : QueryGraphType
{
    public TodoQuery()
    {
        Name = "Query";

        Connection<UserGraphType>()
            .Name("users")
            .WithScopedConnectionResolver<UserConnectionResolver, User>();

        Field<UserGraphType>()
            .Name("user")
            .Argument<StringGraphType>("id")
            .Resolve()
            .WithScope()
            .WithService<IUserService>()
            .Resolve((ctx, userService) => userService.Find(ctx.GetArgument<Guid>("id")));
    }
}
