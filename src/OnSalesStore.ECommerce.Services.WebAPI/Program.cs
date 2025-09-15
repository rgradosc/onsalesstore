using Asp.Versioning.ApiExplorer;
using OnSalesStore.ECommerce.Application.UseCases;
using OnSalesStore.ECommerce.Infrastructure;
using OnSalesStore.ECommerce.Persistence;
using OnSalesStore.ECommerce.Services.WebAPI.Modules.Authentication;
using OnSalesStore.ECommerce.Services.WebAPI.Modules.DependencyInjection;
using OnSalesStore.ECommerce.Services.WebAPI.Modules.Features;
using OnSalesStore.ECommerce.Services.WebAPI.Modules.HealthChecks;
using OnSalesStore.ECommerce.Services.WebAPI.Modules.Middleware;
using OnSalesStore.ECommerce.Services.WebAPI.Modules.RateLimiter;
using OnSalesStore.ECommerce.Services.WebAPI.Modules.Redis;
using OnSalesStore.ECommerce.Services.WebAPI.Modules.Swagger;
using OnSalesStore.ECommerce.Services.WebAPI.Modules.Versioning;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFeatures(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddVersioning();
builder.Services.AddSwagger();
builder.Services.AddHealthCheck(builder.Configuration);
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddRateLimiting(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            config.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"OnSalesStore API {description.GroupName.ToUpperInvariant()}");
        }
    });
    app.UseReDoc(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.DocumentTitle = "OnSalesStore API Market";
            options.SpecUrl = $"/swagger/{description.GroupName}/swagger.json";
        }
    });
}

app.UseHttpsRedirection();
app.UseCors("PolyceEcommerce");
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.UseRequestTimeouts();
app.MapControllers();
app.MapHealthChecksUI();
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.AddMiddleware();

app.Run();

public partial class Program { }