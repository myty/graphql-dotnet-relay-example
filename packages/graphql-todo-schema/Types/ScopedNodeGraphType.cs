using GraphQL.Relay.Types;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Todo.Schema.Types;

public abstract class ScopedNodeGraphType<TSource> : NodeGraphType<TSource>
{
    public abstract TSource GetByIdScoped(
        IServiceProvider services,
        IResolveFieldContext<object> context,
        string id
    );

    public override TSource GetById(IResolveFieldContext<object> context, string id)
    {
        using var scope = context.RequestServices.CreateScope();
        var services = scope.ServiceProvider;
        return GetByIdScoped(services, context, id);
    }
}
