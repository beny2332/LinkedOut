using LinkedOut.DAL;
using Microsoft.EntityFrameworkCore;
using LinkedOut.Controllers;
using LinkedOut.Services;

var builder = WebApplication.CreateBuilder(args);
string cs = "server = LAPTOP-ARI; initial catalog = linked_out; user id = sa; password = 211488770; TrustServerCertificate=Yes";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataLayer>(options => options.UseSqlServer(cs));
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PostService>();

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
