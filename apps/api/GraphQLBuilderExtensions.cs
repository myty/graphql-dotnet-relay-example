using GraphQL.DI;

namespace GraphQL.Examples.Todo;

public static class GraphQLBuilderExtensions
{
    public static IGraphQLBuilder AddUnhandledExceptionHandler(this IGraphQLBuilder builder)
    {
        return builder.ConfigureExecutionOptions(options =>
        {
            var logger = options.RequestServices.GetRequiredService<ILogger<Program>>();
            options.UnhandledExceptionDelegate = ctx =>
            {
                logger.UnhandledException(ctx.OriginalException.Message);
                return Task.CompletedTask;
            };
        });
    }
}

internal static class LoggerExtensions
{
    private static readonly Action<ILogger, string, Exception> _unhandledException =
        LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(1, nameof(UnhandledException)),
            "Error occurred: {Error}"
        );

    public static void UnhandledException(this ILogger logger, string message)
    {
        _unhandledException(logger, message, null);
    }
}
