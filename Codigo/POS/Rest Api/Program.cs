using Services.Interfaces;
using Services.Models;
using Services;

using Microsoft.Extensions.DependencyInjection;
using Factory;
using DataAccess.Repositories;
using DataAccess;

var builder = WebApplication.CreateBuilder(args);
RepositoriesFactory repositoriesFactory = new RepositoriesFactory();
repositoriesFactory.SetupRepositories();

ServicesFactory servicesFactory = new ServicesFactory(repositoriesFactory);
servicesFactory.SetupServices();

builder.Services.AddScoped<IGetService<Brand>>(sp =>
{
    return servicesFactory.BrandService;
});

builder.Services.AddScoped<IGetService<Colour>>(sp =>
{
    return servicesFactory.ColourService;
});

builder.Services.AddScoped<IGetService<Category>>(sp =>
{
    return servicesFactory.CategoryService;
});

builder.Services.AddScoped<IProductService>(sp =>
{
    return servicesFactory.ProductService;
});

builder.Services.AddScoped<IUserService>(sp =>
{
    return servicesFactory.UserService;
});

builder.Services.AddScoped<IPurchaseService>(sp =>
{ 
    return servicesFactory.PurchaseService; 
});

builder.Services.AddScoped<PromoService>(sp =>
{
    return servicesFactory.PromoService;
});



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
