using Catering.API.Infrastructure;
using Catering.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .SetBasePath(Directory.GetCurrentDirectory());

// Add services to the container.
builder.Services
    .AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CateringContext>(option =>
{
    option.UseSqlServer(configuration.GetConnectionString("SqlConnection"),
        options => options.EnableRetryOnFailure());
});

builder.Services.AddTransient<IRestaurantRepository, RestaurantRepository>();

// Add Api version
builder.Services.AddApiVersioning(option =>
{
    option.ReportApiVersions = true;
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.DefaultApiVersion = new(1, 0);
    option.ApiVersionSelector = new CurrentImplementationApiVersionSelector(option);
});

var app = builder.Build();

// Database migrations
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<CateringContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<CateringContextSeed>>();

db.Database.Migrate();
new CateringContextSeed().SeedAsync(db, logger).Wait();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
