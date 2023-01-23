using JsonColumn.Models;

namespace JsonColumn.Extensions;

public static class DbExtensions
{
    public static async Task MakeSureDbCreated(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<AuthorContext>();

            // uncomment the following to delete it
            await context.Database.EnsureDeletedAsync();

            await context.Database.EnsureCreatedAsync();

            await context.Seed();
        }
    }
}
