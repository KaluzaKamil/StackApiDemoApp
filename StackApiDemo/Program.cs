using Microsoft.EntityFrameworkCore;
using StackApiDemo.Contexts;
using StackApiDemo.Extensions;
using StackApiDemo.Handlers;
using StackApiDemo.Repositories;
using StackApiDemo.StackOverflowApiIntegration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StackOverflowTagsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StackOverflowTagsContext")));
builder.Services.AddScoped<IStackOverflowTagsHandler, StackOverflowTagsHandler>();
builder.Services.AddScoped<IStackOverflowTagsDownloader, StackOverflowTagsDownloader>();
builder.Services.AddScoped<IStackOverflowTagsRepository,  StackOverflowTagsRepository>();
builder.Services.AddScoped<IStackOverflowHttpClient, StackOverflowHttpClient>();
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddLogging(options =>
    options
        .AddDebug()
        .AddConsole()
        .AddConfiguration(builder.Configuration.GetSection("Logging"))
        .SetMinimumLevel(LogLevel.Information)
    );

var app = builder.Build();

await app.MigrateDatabase().SeedDataBase();

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

