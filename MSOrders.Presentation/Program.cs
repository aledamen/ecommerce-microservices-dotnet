using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MSOrders.Application.Clients;
using MSOrders.Application.Repositories;
using MSOrders.Application.Services;
using MSOrders.Application.Validators;
using MSOrders.Domain.Entities;
using MSOrders.Infraestructure.Clients;
using MSOrders.Infraestructure.Data;
using MSOrders.Infraestructure.Repositories;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add db context configuration

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrdersRepository>();
builder.Services.AddScoped<ICustomerServiceClient, CustomerServiceClient>();
builder.Services.AddScoped<IProductServiceClient, ProductServiceClient>();

builder.Services.AddScoped<IValidator<Order>, OrderValidator>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly);
});

builder.Services.AddHttpClient<IProductServiceClient, ProductServiceClient>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Services:ProductService"] ?? string.Empty);
});

builder.Services.AddHttpClient<ICustomerServiceClient, CustomerServiceClient>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["Services:CustomerService"] ?? string.Empty);
});

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
