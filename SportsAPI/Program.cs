using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using SportsAPI.Context;
using SportsAPI.Models;

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Team>("Teams");
    builder.EntitySet<Player>("Players");
    return builder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SportsContext>(
        options => options.UseInMemoryDatabase(databaseName: "Sports"));

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    )
    .AddOData(opt => opt.AddRouteComponents("v1", GetEdmModel()).EnableQueryFeatures());

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
