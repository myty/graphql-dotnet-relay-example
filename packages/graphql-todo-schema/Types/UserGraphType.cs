using GraphQL.MicrosoftDI;
using GraphQL.Todo.Core.Interfaces;
using GraphQL.Todo.Core.Models;
using GraphQL.Todo.Schema.Extensions;
using GraphQL.Todo.Schema.ScopedResolvers;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Models = GraphQL.Todo.Core.Models;

namespace GraphQL.Todo.Schema.Types;

public class UserGraphType : ScopedNodeGraphType<User>
{
    public UserGraphType()
    {
        Name = "User";

        Id(t => t.Id);

        Connection<TodoGraphType>()
            .Name("todos")
            .Argument<StringGraphType, string>(
                name: "status",
                description: "Filter todos by their status",
                defaultValue: "any"
            )
            .WithScopedConnectionResolver<UserTodoConnectionResolver, User, Models.Todo>();

        Field<IntGraphType>()
            .Name("totalCount")
            .WithScopedResolver<UserTodoTotalCountResolver, User, int>();

        Field<IntGraphType>()
            .Name("completedCount")
            .Resolve()
            .WithScope()
            .WithService<ITodoService>()
            .Resolve((ctx, todoService) => todoService.FindAll(ctx.Source.Id, "completed").Count());
    }

    public override User GetByIdScoped(
        IServiceProvider services,
        IResolveFieldContext<object> context,
        string id
    )
    {
        if (!Guid.TryParse(id, out Guid userId))
        {
            throw new Exception("Not a valid user id type");
        }

        return services.GetRequiredService<IUserService>().Find(userId);
    }
}
