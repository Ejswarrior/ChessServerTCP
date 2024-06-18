using Microsoft.EntityFrameworkCore;
using ChessServerTCP.Models;
using Microsoft.AspNetCore.Identity;
using ChessServerTCP.Sockets;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<AppDBContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
*/builder.Services.AddSwaggerGen();

var app = builder.Build();
/*var socket = new SocketSetup();
*/Console.WriteLine("Hello");
/*socket.intializeSocket();
*/
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
/*app.UseAuthentication();
app.UseAuthorization();*/

app.MapControllers();

app.Run();

