using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using prof_edna_teles_shop_api.Data;
using prof_edna_teles_shop_api.Data.Repositories;
using prof_edna_teles_shop_api.Data.Repositories.Interfaces;
using prof_edna_teles_shop_api.Services;
using prof_edna_teles_shop_api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Variáveis de Ambiente
builder.Configuration.AddEnvironmentVariables();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Development", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
    options.AddPolicy("Production", builder =>
    {
        builder.WithOrigins("https://localhost")
            .AllowAnyHeader()
            .WithMethods("GET", "POST");
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Documentation - Prof Edna Teles", Version = "v1" });
});

//Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultDatabaseMigrations")));

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPageRepository, PageRepository>();

//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPageService, PageService>();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
}

app.UseHttpsRedirection();

app.UseCors("Development");

app.UseAuthorization();

app.MapControllers();

app.Run();
