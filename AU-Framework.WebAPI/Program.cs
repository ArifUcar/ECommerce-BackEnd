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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

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
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddlewareExtensions();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
