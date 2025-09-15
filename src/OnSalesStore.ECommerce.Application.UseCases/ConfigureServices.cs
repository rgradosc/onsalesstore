using OnSalesStore.ECommerce.Application.Interfaces.UseCases;
using OnSalesStore.ECommerce.Application.UseCases.Categories;
using OnSalesStore.ECommerce.Application.UseCases.Common.Behaviours;
using OnSalesStore.ECommerce.Application.UseCases.Customers;
using OnSalesStore.ECommerce.Application.UseCases.Discounts;
using OnSalesStore.ECommerce.Application.UseCases.Users;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OnSalesStore.ECommerce.Application.UseCases
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ICustomerApplication, CustomerApplication>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<ICategoryApplication, CategoryApplication>();
            services.AddScoped<IDiscountApplication, DiscountApplication>();

            return services;
        }
    }
}
