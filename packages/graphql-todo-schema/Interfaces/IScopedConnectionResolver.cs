using GraphQL.Builders;

namespace GraphQL.Todo.Schema.Interfaces;

public interface IScopedConnectionResolver<TSourceType, TReturnType>
{
    IQueryable<TReturnType> Resolve(IResolveConnectionContext<TSourceType> ctx);
}
