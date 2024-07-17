using Fiap.Ingresso.Evento.API.Configuration;
using Fiap.Ingresso.WebAPI.Core.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddJwtConfiguration();
builder.Services.AddApiConfiguration(builder.Configuration);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseApiConfiguration();

app.Run();
