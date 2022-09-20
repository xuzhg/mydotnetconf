﻿using OData.WebApi.Models;

namespace OData.WebApi.Extensions;

public static class DbExtensions
{
    public static void MakeSureDbCreated(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
        }
    }
}
