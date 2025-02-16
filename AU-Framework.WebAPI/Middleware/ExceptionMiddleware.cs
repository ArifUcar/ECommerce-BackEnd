using AU_Framework.Domain.Entities;
using AU_Framework.Persistance.Context;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AU_Framework.WebAPI.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly AppDbContext _appDbContext;

        public ExceptionMiddleware(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await LogExceptionToDatabaseAsync(ex, context.Request);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500; // Varsayılan hata kodu

            if (ex is ValidationException validationException)
            {
                context.Response.StatusCode = 400; // Bad Request

                var validationErrors = validationException.Errors
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}") // PropertyName + Message
                    .ToList();

                var errorResponse = new ValidationErrorDetails
                {
                    StatusCode = 403,
                    Errors = validationErrors
                };

                await context.Response.WriteAsync(errorResponse.ToString());
                return;
            }

            var errorResult = new ErrorResult
            {
                Message = ex.Message,
                StatusCode = context.Response.StatusCode
            };

            await context.Response.WriteAsync(errorResult.ToString());
        }

        private async Task LogExceptionToDatabaseAsync(Exception ex, HttpRequest request)
        {
            try
            {
                var errorLog = new ErrorLog
                {
                    ErrorMessage = ex.Message,
                    StackTrace = ex.StackTrace,
                    RequestPath = request.Path,
                    RequestMethod = request.Method,
                    Timestamp = DateTime.Now
                };

                await _appDbContext.Set<ErrorLog>().AddAsync(errorLog);
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception logEx)
            {
                Console.WriteLine($"[HATA] Log kaydedilirken hata oluştu: {logEx.Message}");
            }
        }
    }
}
