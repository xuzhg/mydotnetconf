using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using OData.WebApi.Extensions;
using OData.WebApi.Models;

namespace OData.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=app.db"));

        builder.Services.AddControllers()
            .AddOData(opt =>
                opt.EnableQueryFeatures().AddRouteComponents("odata", EdmModelBuilder.GetEdmModel()));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseODataRouteDebug();

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MakeSureDbCreated();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

