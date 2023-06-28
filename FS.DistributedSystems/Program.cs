using FK.Services;
using FK.Services.Contracts;
using FK.Services.Implementations;
using FS.Domain.Entities.Contracts;
using FS.Infrastructure.DataAccess;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Logging.ClearProviders(); // me cargo los loggers que habia antes
// Defino el nuevo
var logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration) // esto lee del appSettings.json la informacion del log
    .CreateLogger();
builder.Logging.AddSerilog(logger);

builder.Services.AddScoped<IRepositoryCarts, RepositoryCartPersistent>();
builder.Services.AddScoped<IRepositoryProductsExternalService, RepositoryProductExternalApi>();
builder.Services.AddScoped<IRepositoryProducts, RepositoryProductPersistent>();

builder.Services.AddScoped<IServicesProduct, ServicesProduct>();
builder.Services.AddScoped<IServicesCart, ServicesCart>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
