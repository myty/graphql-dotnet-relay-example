using GraphQL.MicrosoftDI;
using GraphQL.Relay.Types;
using GraphQL.Relay.Utilities;
using GraphQL.Todo.Core.Interfaces;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Models = GraphQL.Todo.Core.Models;

namespace GraphQL.Todo.Schema.Types;

public class UserGraphType : NodeGraphType<Models.User>
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
            .Resolve()
            .WithScope()
            .WithService<ITodoService>()
            .Resolve(
                (ctx, todoService) =>
                    ctx.ToConnection(
                        todoService.FindAll(ctx.Source.Id, ctx.GetArgument<string>("status"))
                    )
            );

        Field<IntGraphType>()
            .Name("totalCount")
            .Resolve()
            .WithScope()
            .WithService<ITodoService>()
            .Resolve((ctx, todoService) => todoService.FindAll(ctx.Source.Id).Count());

        Field<IntGraphType>()
            .Name("completedCount")
            .Resolve()
            .WithScope()
            .WithService<ITodoService>()
            .Resolve((ctx, todoService) => todoService.FindAll(ctx.Source.Id, "completed").Count());
    }

    public override Models.User GetById(IResolveFieldContext<object> context, string id)
    {
        using var scope = context.RequestServices.CreateScope();
        var services = scope.ServiceProvider;
        return services.GetRequiredService<IUserService>().Find(id);
    }
}
