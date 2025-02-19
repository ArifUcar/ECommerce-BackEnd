using E_Commerce.Components;
using E_Commerce.Environments;
using E_Commerce.Services;
using E_Commerce.Services.Implementations;
using E_Commerce.Services.Interfaces;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// HttpClient'ı ekleyelim
builder.Services.AddHttpClient();

// Servisleri ekleyelim
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddApplicationServices();
builder.Services.AddScoped<IAuthService, AuthService>();

// CORS politikasını ekleyelim
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Antiforgery token'ı devre dışı bırakalım
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
    options.Cookie.Name = "CSRF-TOKEN";
});

var app = builder.Build();

// Initialize environment helper
EnvironmentHelper.Initialize(app.Configuration, app.Environment);

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

// Antiforgery middleware'i ekleyelim
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
    await next();
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
