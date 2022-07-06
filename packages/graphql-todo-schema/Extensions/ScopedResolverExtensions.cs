using GraphQL.Builders;
using GraphQL.Extensions;
using GraphQL.MicrosoftDI;
using GraphQL.Todo.Schema.Interfaces;

namespace GraphQL.Todo.Schema.Extensions;

public static class ScopedResolverExtensions
{
    public static void WithScopedResolver<TResolver, TSourceType, TReturnType>(
        this FieldBuilder<TSourceType, object> builder
    ) where TResolver : IScopedResolver<TSourceType, TReturnType>
    {
        builder
            .Resolve()
            .WithScope()
            .WithService<TResolver>()
            .Resolve((ctx, resolver) => resolver.Resolve(ctx));
    }

    public static void WithScopedConnectionResolver<TConnectionResolver, TSourceType, TReturnType>(
        this ConnectionBuilder<TSourceType> builder
    ) where TConnectionResolver : IScopedConnectionResolver<TSourceType, TReturnType>
    {
        builder
            .Resolve()
            .WithScope()
            .WithService<TConnectionResolver>()
            .Resolve((ctx, resolver) => ctx.ToConnection(resolver.Resolve(ctx)));
    }
}
