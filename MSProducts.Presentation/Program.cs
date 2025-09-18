using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MSProducts.Application.Repositories;
using MSProducts.Application.Services;
using MSProducts.Application.Validators;
using MSProducts.Domain.Entities;
using MSProducts.Infraestructure.Data;
using MSProducts.Infraestructure.Repositories;
using MSProducts.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext configuration

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IValidator<Product>, ProductValidator>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
