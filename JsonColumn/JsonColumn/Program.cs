using JsonColumn.Extensions;
using JsonColumn.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query.Expressions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<AuthorContext>(options => options.UseSqlite("Data Source=author.db")); // JSON Column not supported for SQLlite
builder.Services.AddDbContext<AuthorContext>(options => options.UseSqlServer(@$"Server=(localdb)\mssqllocaldb;Database=AuthorJsonColumn"));

builder.Services.AddControllers()
    .AddOData(opt =>
                opt.EnableQueryFeatures()
                .AddRouteComponents(
                    "odata",
                    EdmModelBuilder.GetEdmModel()
                    ,
                    services => services.
                    // services.AddSingleton<ISearchBinder, StudentSearchBinder>().
                    //    AddSingleton<ISelectExpandBinder, SchoolStudentSelectExpandBinder>().
                    //    AddSingleton<ODataResourceSerializer, MyResourceSerializer>().
                        AddSingleton<IFilterBinder, AuthorFilterBinder>()
                ) // End of AddRouteComponents
            ); // End of AddOData

var app = builder.Build();

// Configure the HTTP request pipeline.

// await app.MakeSureDbCreated();

app.UseAuthorization();

app.MapControllers();

app.Run();
