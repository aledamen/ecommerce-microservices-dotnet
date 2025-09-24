using ecommerce.frontend.Components;
using ecommerce.frontend.Interfaces;
using ecommerce.frontend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddHttpClient<IProductService, ProductService>(client =>
{
    var apiBase = builder.Configuration["ApiProductsUrl"] 
        ?? throw new InvalidOperationException("ApiProductsUrl is not configured.");

    client.BaseAddress = new Uri(apiBase);
});

builder.Services.AddHttpClient<ICustomerService, CustomerService>(client =>
{
    var apiBase = builder.Configuration["ApiCustomersUrl"] 
        ?? throw new InvalidOperationException("ApiCustomersUrl is not configured.");
    client.BaseAddress = new Uri(apiBase);
});

builder.Services.AddHttpClient<IOrderService, OrderService>(client =>
{
    var apiBase = builder.Configuration["ApiOrdersUrl"] 
        ?? throw new InvalidOperationException("ApiOrdersUrl is not configured.");
    client.BaseAddress = new Uri(apiBase);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
