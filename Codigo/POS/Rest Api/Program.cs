using Factory;
using Rest_Api.Filters;
using Services;
using Services.Interfaces;
using Services.Models;

var builder = WebApplication.CreateBuilder(args);
RepositoriesFactory repositoriesFactory = new RepositoriesFactory();
repositoriesFactory.SetupRepositories();

ServicesFactory servicesFactory = new ServicesFactory(repositoriesFactory);
servicesFactory.SetupServices();

builder.Services.AddScoped(sp =>
{
    return servicesFactory.BrandService;
});

builder.Services.AddScoped(sp =>
{
    return servicesFactory.ColourService;
});

builder.Services.AddScoped(sp =>
{
    return servicesFactory.CategoryService;
});

builder.Services.AddScoped(sp =>
{
    return servicesFactory.ProductService;
});

builder.Services.AddScoped(sp =>
{
    return servicesFactory.UserService;
});

builder.Services.AddScoped(sp =>
{ 
    return servicesFactory.PurchaseService; 
});

builder.Services.AddScoped(sp =>
{
    return servicesFactory.PromoService;
});

builder.Services.AddScoped(sp =>
{
    return new AuthenticationFilter(repositoriesFactory.UserRepository);
});


builder.Services.AddScoped(sp =>
{
    return new AuthorizationFilter(repositoriesFactory.UserRepository);
});
builder.Services.AddScoped(sp =>
{
    return new ExceptionFilter();
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Add the CORS middleware
app.UseCors();

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
