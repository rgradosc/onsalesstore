using Microsoft.AspNetCore.Http.Timeouts;
using System.Text.Json.Serialization;

namespace OnSalesStore.ECommerce.Services.WebAPI.Modules.Features
{
    public static class FeaturesExtensions
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services, IConfiguration configuration)
        {
            string policyEcommerce = "PolyceEcommerce";

            // Config Cors
            services.AddCors(options =>
            {
                options.AddPolicy(policyEcommerce, builder =>
                {
                    builder.WithOrigins(configuration["Config:OriginCors"])
                        .AllowAnyOrigin()
                        .AllowAnyMethod();
                });
            });

            services.AddMvc();

            // Config Json Options
            services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    var enumConverter = new JsonStringEnumConverter();
                    opts.JsonSerializerOptions.Converters.Add(enumConverter);
                });
            services.AddRequestTimeouts(options =>
            {
                options.DefaultPolicy =
                    new RequestTimeoutPolicy { Timeout = TimeSpan.FromMilliseconds(1500) };
                options.AddPolicy("CustomPolicy", TimeSpan.FromMilliseconds(2000));
            });

            return services;
        }
    }
}
