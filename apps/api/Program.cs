using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Examples.Todo;
using GraphQL.MicrosoftDI;
using GraphQL.Relay.Types;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using GraphQL.Todo.Core.Interfaces;
using GraphQL.Todo.Schema;
using GraphQL.Todo.Schema.ScopedResolvers;
using GraphQL.Todo.Services;
using GraphQL.Todo.Services.Services;
using GraphQL.Types.Relay;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TodoContext>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserTodoConnectionResolver>();
builder.Services.AddScoped<UserTodoTotalCountResolver>();

builder.Services.AddTransient(typeof(ConnectionType<>));
builder.Services.AddTransient(typeof(EdgeType<>));
builder.Services.AddTransient<NodeInterface>();
builder.Services.AddTransient<PageInfoType>();

builder.Services.AddGraphQL(
    graphQLBuilder =>
        graphQLBuilder
            .AddHttpMiddleware<TodoSchema>()
            .AddWebSocketsHttpMiddleware<TodoSchema>()
            .AddSchema<TodoSchema>()
            .AddUnhandledExceptionHandler()
            .AddSystemTextJson()
            .AddWebSockets()
            .AddDataLoader()
            .AddGraphTypes(typeof(TodoSchema).Assembly)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseWebSockets();
app.UseGraphQLWebSockets<TodoSchema>();
app.UseGraphQL<TodoSchema>();
app.UseGraphQLAltair();
app.Run();
