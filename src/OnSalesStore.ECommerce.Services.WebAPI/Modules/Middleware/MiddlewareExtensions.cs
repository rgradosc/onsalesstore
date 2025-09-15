using OnSalesStore.ECommerce.Services.WebAPI.Modules.GlobalException;

namespace OnSalesStore.ECommerce.Services.WebAPI.Modules.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder AddMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionHandler>();
        }
    }
}
