using Autofac.Extensions.DependencyInjection;
using framework.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Framework.Api;

public static class Framework
{
    public static WebApplicationBuilder CreateDefaultWebApplicationBuilder(string[] args, bool useAutofac = true)
    {
        var builder = WebApplication.CreateBuilder(args);
        if (useAutofac) builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
        {
            builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        }));
        builder.Services.AddSwaggerGen();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
       
        builder.Logging.ClearProviders();

        return builder;
    }

    public static WebApplication CreateDefaultWebApplication(this WebApplicationBuilder builder)
    {
        var app = builder.Build();

     
        app.UseCors("corsapp");
        app.UseSwagger();
        app.UseSwaggerUI();
     
        app.UseAuthorization();
        app.UseExceptionHandler();
     
        app.MapControllers();

        return app;
        
    }
}