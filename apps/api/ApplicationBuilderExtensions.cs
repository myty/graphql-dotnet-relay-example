using GraphQL.Todo.Core.Models;
using GraphQL.Todo.Services;

namespace Microsoft.AspNetCore.Builder;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseDataSeed(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<TodoContext>();

        _ = context.Database.EnsureCreated();

        var testUser = new User { Name = "Test User", Username = "test-user" };

        var foundUser = context.Users.FirstOrDefault(user => user.Username == testUser.Username);
        if (foundUser == null)
        {
            var addedUser = context.Users.Add(testUser);

            foundUser = addedUser.Entity;
        }

        Console.WriteLine("----  USER ----");
        Console.WriteLine(foundUser.Id);

        var todos = new List<Todo>
        {
            new Todo { UserId = foundUser.Id, Text = "Test Text #1" },
            new Todo { UserId = foundUser.Id, Text = "Test Text #2" },
            new Todo { UserId = foundUser.Id, Text = "Test Text #3" },
            new Todo { UserId = foundUser.Id, Text = "Test Text #4" },
            new Todo
            {
                UserId = foundUser.Id,
                Text = "Test Text #4 (Completeted)",
                Completed = true
            },
        };

        foreach (var testTodo in todos)
        {
            var foundTodo = context.Todos.FirstOrDefault(todo => todo.Text == testTodo.Text);
            if (foundTodo == null)
            {
                _ = context.Todos.Add(testTodo);
            }
        }

        _ = context.SaveChanges();

        return app;
    }
}
