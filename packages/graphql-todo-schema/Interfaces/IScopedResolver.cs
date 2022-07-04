namespace GraphQL.Todo.Schema.Interfaces;

public interface IScopedResolver<TSourceType, TReturnType>
{
    TReturnType Resolve(IResolveFieldContext<TSourceType> ctx);
}
