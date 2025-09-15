using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OnSalesStore.ECommerce.Services.WebAPI.Modules.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = "OnSalesStore API Market",
                Description = "OnSalesStore is a Web API that enable the management of an e-commerce business",
                License = new OpenApiLicense
                {
                    Name = "General Public License v3",
                    Url = new Uri("https://www.gnulicense.com"),
                }
            };

            if (description.IsDeprecated)
            {
                info.Description = $"{info.Description} This api version is deprecated.";
            }

            return info;
        }
    }
}
