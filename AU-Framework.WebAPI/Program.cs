using AU_Framework.Application.Behaviors;
using AU_Framework.Application.Services;
using AU_Framework.Domain.Entities;
using AU_Framework.Persistance.Context;
using AU_Framework.Persistance.Services;
using AU_Framework.Persistance.Repository;
using AU_Framework.Application.Repository;
using AU_Framework.WebAPI.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// HTTP Context ve Log servisi ekle
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILogService, LogService>();

// Diğer servisler
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

// Repository servisini kaydediyoruz
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<AppDbContext>();

builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddAutoMapper(typeof(AU_Framework.Persistance.AssemblyReferance).Assembly);

string connectingString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectingString));
builder.Services.AddControllers().AddApplicationPart(typeof(AU_Framework.Presentation.AssemblyReferance).Assembly );
builder.Services.AddMediatR(cfr => cfr.RegisterServicesFromAssembly(typeof(AU_Framework.Application.AsssemblyReferance).Assembly)); 
builder.Services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(AU_Framework.Application.AsssemblyReferance).Assembly);
builder.Services.AddEndpointsApiExplorer();

// Swagger yapılandırması
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "AU Framework API", 
        Version = "v1",
        Description = "AU Framework API Documentation"
    });

    // JWT authentication için security tanımı
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            securityScheme,
            new[] { "Bearer" }
        }
    };

    c.AddSecurityRequirement(securityRequirement);

    // [Authorize] attribute'u olan endpoint'leri işaretle
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

// JWT Authentication yapılandırması
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var claims = context.Principal?.Claims;
            var roles = claims?.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            // Loglama ekleyelim
            Console.WriteLine($"Token validated. Roles: {string.Join(", ", roles ?? new List<string>())}");
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
});

// Authorization'ı ekleyelim
builder.Services.AddAuthorization();

// CORS politikasını ekleyelim
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy =>
        policy.RequireRole("Admin"));
        
    options.AddPolicy("RequireManagerRole", policy =>
        policy.RequireRole("Manager"));
        
    options.AddPolicy("RequireUserRole", policy =>
        policy.RequireRole("User"));
});

// Middleware'leri kaydet
builder.Services.AddTransient<RequestLoggingMiddleware>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AU Framework API V1");
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        c.DefaultModelsExpandDepth(-1);
        
        // JWT token için authorize butonu ayarları
        c.OAuthUsePkce();
    });
}
app.UseMiddlewareExtensions();
app.UseHttpsRedirection();
app.UseRouting();

// CORS'u Authentication'dan önce ekleyin
app.UseCors("AllowAll");

// Authentication ve Authorization sırası önemli
app.UseAuthentication();

// AUAuthorize middleware'ini Authorization'dan önce ekleyin
app.UseAUAuthorize();

app.UseAuthorization();

// Request loglama middleware'ini ekle
app.UseMiddleware<RequestLoggingMiddleware>();

// Global exception handling
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
