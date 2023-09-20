using Services.Interfaces;
using Services.Models;
using Services;

using Microsoft.Extensions.DependencyInjection;
using Factory;

var builder = WebApplication.CreateBuilder(args);
RepositoriesFactory repositoriesFactory = new RepositoriesFactory();
repositoriesFactory.SetupRepositories();

ServicesFactory servicesFactory = new ServicesFactory(repositoriesFactory);
servicesFactory.SetupServices();


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
