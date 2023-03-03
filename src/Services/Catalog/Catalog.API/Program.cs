using Catalog.API.Grpc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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
    options.Listen(IPAddress.Any, 5003, listenOptions =>
    {
        // api listner
        listenOptions.UseHttps();
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });
    options.Listen(IPAddress.Any, 7179, listenOptions =>
    {
        // gRPC listner
        listenOptions.UseHttps();// pas obligatoir si on a le Http2
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGrpcService<CatalogService>();

app.MapControllers();

app.Run();
