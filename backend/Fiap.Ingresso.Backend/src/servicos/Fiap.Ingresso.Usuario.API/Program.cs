using Fiap.Ingresso.Usuario.API.Configuration;
using Fiap.Ingresso.WebAPI.Core.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.AddJwtConfiguration();
builder.Services.AddApiConfiguration(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseApiConfiguration();

app.Run();
