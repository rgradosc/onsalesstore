using OnSalesStore.ECommerce.Application.Interfaces.Presentation;
using OnSalesStore.ECommerce.Services.WebAPI.Modules.GlobalException;
using OnSalesStore.ECommerce.Services.WebAPI.Services;

namespace OnSalesStore.ECommerce.Services.WebAPI.Modules.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddTransient<GlobalExceptionHandler>();
            services.AddScoped<ICurrentUser, CurrentUser>();

            return services;
        }
    }
}
