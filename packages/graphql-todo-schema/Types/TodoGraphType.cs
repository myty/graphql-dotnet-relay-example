using GraphQL.Relay.Types;
using GraphQL.Todo.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models = GraphQL.Todo.Core.Models;

namespace GraphQL.Todo.Schema.Types;

public class TodoGraphType : NodeGraphType<Models.Todo>
{
    public TodoGraphType()
    {
        Name = "Todo";

        Id(t => t.Id);
        Field(t => t.Text);
        Field("complete", t => t.Completed);
    }

    public override Models.Todo GetById(IResolveFieldContext<object> context, string id)
    {
        using var scope = context.RequestServices.CreateScope();
        var services = scope.ServiceProvider;
        return services.GetRequiredService<ITodoService>().Find(id);
    }
}
