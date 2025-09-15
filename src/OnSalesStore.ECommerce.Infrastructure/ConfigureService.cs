using OnSalesStore.ECommerce.Application.Interfaces.Infrastructure;
using OnSalesStore.ECommerce.Infrastructure.EventBus;
using OnSalesStore.ECommerce.Infrastructure.EventBus.Options;
using OnSalesStore.ECommerce.Infrastructure.Notification;
using OnSalesStore.ECommerce.Infrastructure.Notification.Options;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SendGrid.Extensions.DependencyInjection;

namespace OnSalesStore.ECommerce.Infrastructure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.ConfigureOptions<RabbitMqOptionsSetup>();
            services.AddScoped<IEventBus, EventBusRabbitMQ>();
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((context, cfg) =>
                {
                    RabbitMqOptions? opt = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<RabbitMqOptions>>()
                        .Value;
                    cfg.Host(opt.HostName, opt.VirtualHost, h =>
                    {
                        h.Username(opt.UserName);
                        h.Password(opt.Password);
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<INotification, NotificationSendGrid>();
            services.ConfigureOptions<SendgridOptionsSetup>();
            SendgridOptions? sendgridOptions = services.BuildServiceProvider()
                .GetRequiredService<IOptions<SendgridOptions>>()
                .Value;

            services.AddSendGrid(options =>
            {
                options.ApiKey = sendgridOptions.ApiKey;
            });

            return services;
        }
    }
}
