using Catering.API.Infrastructure;
using Catering.API.Infrastructure.Repositories;
using GrpcCatering;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using System.Net;
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
builder.Services.AddGrpc();
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

// Configure the HTTP request pipeline.
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5001, listenOptions =>
    {
        // api listner
        listenOptions.UseHttps();
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });
    options.Listen(IPAddress.Any, 7178, listenOptions =>
    {
        // gRPC listner
        listenOptions.UseHttps();// pas obligatoir si on a le Http2
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

var app = builder.Build();

// Database migrations
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<CateringContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<CateringContextSeed>>();

db.Database.Migrate();
new CateringContextSeed().SeedAsync(db, logger).Wait();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGrpcService<CateringService>();

app.MapControllers();

app.Run();
