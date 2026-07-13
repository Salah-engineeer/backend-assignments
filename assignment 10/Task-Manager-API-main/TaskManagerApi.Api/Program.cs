using TaskManagerApi.Api.Repositories;
using TaskManagerApi.Api.Services;
using TaskManagerApi.Api.Services.Interfaces; 
using TaskManagerApi.Api.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddOpenApi();
builder.Services.AddSingleton<TaskService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
