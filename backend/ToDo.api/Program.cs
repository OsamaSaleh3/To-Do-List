using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Application.Mapping;
using ToDo.Application.Services;
using ToDo.Domain.Interfaces;
using ToDo.Infrastructure.Persistence;
using ToDo.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

string[] allowedOrigins = new[]
{
    "http://127.0.0.1:5500",
    "http://localhost:5500"
};

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(option => 
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.MigrationsAssembly("ToDo.Infrastructure")
));


builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();

builder.Services.AddAutoMapper(typeof(TodoItemProfile).Assembly);


builder.Services.AddScoped<ITodoItemService, TodoItemService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("JSPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins) 
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
    
var app = builder.Build();
app.UseHttpsRedirection();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseCors("JSPolicy");


app.UseAuthorization();

app.MapControllers();


app.Run();
