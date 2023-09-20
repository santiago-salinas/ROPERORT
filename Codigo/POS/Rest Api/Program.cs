using Services.Interfaces;
using Services.Models;
using Services;
using DataAccess.Repositories;
using DataAccess;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<EFContext, EFContext>();
builder.Services.AddScoped<ICRUDRepository<Product>>(sp =>
{
    EFContext context = sp.GetRequiredService<EFContext>();
    return new EFProductRepository(context);
});
builder.Services.AddScoped<ICRUDRepository<User>>(sp =>
{
    EFContext context = sp.GetRequiredService<EFContext>();
    return new EFUserRepository(context);
});
builder.Services.AddScoped<IGetRepository<Brand>>(sp =>
{
    EFContext context = sp.GetRequiredService<EFContext>();
    return new EFBrandRepository(context);
});
builder.Services.AddScoped<IGetRepository<Colour>>(sp =>
{
    EFContext context = sp.GetRequiredService<EFContext>();
    return new EFColourRepository(context);
});
builder.Services.AddScoped<IGetRepository<Category>>(sp =>
{
    EFContext context = sp.GetRequiredService<EFContext>();
    return new EFCategoryRepository(context);
});


builder.Services.AddScoped<IProductService>(sp =>
{
    ProductService service = new ProductService(sp.GetRequiredService<ICRUDRepository<Product>>());
    service.BrandRepository = sp.GetRequiredService<IGetRepository<Brand>>();
    service.ColourRepository= sp.GetRequiredService<IGetRepository<Colour>>();
    service.CategoryRepository = sp.GetRequiredService<IGetRepository<Category>>();

    return service;
});
builder.Services.AddScoped<IUserService>(sp =>
{
    UserService service = new UserService(sp.GetRequiredService<ICRUDRepository<User>>());
    return service;
});
builder.Services.AddScoped<IGetService<Brand>, BrandService>();
builder.Services.AddScoped<IGetService<Category>, CategoryService>();
builder.Services.AddScoped<IGetService<Colour>, ColourService>();
builder.Services.AddScoped<PromoService, PromoService>();



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
