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

        if (context.Users.FirstOrDefault(user => user.Username == testUser.Username) == null)
        {
            _ = context.Users.Add(testUser);
        }

        _ = context.SaveChanges();

        return app;
    }
}
