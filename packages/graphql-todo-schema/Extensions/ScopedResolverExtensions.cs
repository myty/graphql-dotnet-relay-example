using GraphQL.Builders;
using GraphQL.MicrosoftDI;
using GraphQL.Relay.Types;
using GraphQL.Todo.Schema.Interfaces;
using GraphQL.Types.Relay.DataObjects;

namespace GraphQL.Todo.Schema.Extensions;

public static class ScopedResolverExtensions
{
    public static void WithScopedResolver<TResolver, TSourceType, TReturnType>(
        this FieldBuilder<TSourceType, TReturnType> builder
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

    /// <summary>
    /// Creates a <see cref="Connection{TSource}"/> based on the given <see cref="SliceMetrics{TSource}"/>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="metrics"></param>
    /// <returns></returns>
    public static Connection<TSource> ToConnection<TSource>(this SliceMetrics<TSource> metrics)
    {
        var edges = metrics.Slice
            .Select(
                (item, i) =>
                    new Edge<TSource>
                    {
                        Node = item,
                        Cursor = ConnectionUtils.OffsetToCursor(metrics.StartIndex + i)
                    }
            )
            .ToList();

        var firstEdge = edges.FirstOrDefault();
        var lastEdge = edges.LastOrDefault();

        return new Connection<TSource>
        {
            Edges = edges,
            TotalCount = metrics.TotalCount,
            PageInfo = new PageInfo
            {
                StartCursor = firstEdge?.Cursor,
                EndCursor = lastEdge?.Cursor,
                HasPreviousPage = metrics.HasPrevious,
                HasNextPage = metrics.HasNext,
            }
        };
    }

    /// <summary>
    /// From the connection context, <see cref="IResolveConnectionContext"/>,
    /// it creates a <see cref="Connection{TSource}"/> based on the given <see cref="IQueryable{TSource}"/>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="context"></param>
    /// <param name="items"></param>
    /// <param name="totalCount"></param>
    /// <returns></returns>
    public static Connection<TSource> ToConnection<TSource>(
        this IResolveConnectionContext context,
        IQueryable<TSource> items,
        int? totalCount = null
    )
    {
        return SliceMetrics.Create(items, context, totalCount).ToConnection();
    }
}
