using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoApiDb.Data;
using TodoApiDb.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

const string connectionLifeDb = "Data Source=.;Initial Catalog=LifelongLearning;Integrated Security=True;TrustServerCertificate=Yes";

builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseSqlServer(connectionLifeDb));
//opt.UseSqlServer(Configuration.GetConnectionString("LifeDb"));
//opt.UseInMemoryDatabase("TodoList"));

//builder.Services.AddScoped<ITodoItemsService, TodoItemsService>();

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
