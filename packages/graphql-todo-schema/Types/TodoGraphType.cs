using GraphQL.Todo.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models = GraphQL.Todo.Core.Models;

namespace GraphQL.Todo.Schema.Types;

public class TodoGraphType : ScopedNodeGraphType<Models.Todo>
{
    public TodoGraphType()
    {
        Name = "Todo";

        Id(t => t.Id);
        Field(t => t.Text);
        Field("complete", t => t.Completed);
    }

    public override Models.Todo GetByIdScoped(
        IServiceProvider services,
        IResolveFieldContext<object> context,
        string id
    )
    {
        return services.GetRequiredService<ITodoService>().Find(id);
    }
}
