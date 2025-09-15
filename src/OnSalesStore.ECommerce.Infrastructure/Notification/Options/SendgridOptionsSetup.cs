using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace OnSalesStore.ECommerce.Infrastructure.Notification.Options
{
    public class SendgridOptionsSetup : IConfigureOptions<SendgridOptions>
    {
        private const string ConfigurationSectionName = "Sendgrid";
        private readonly IConfiguration configuration;

        public SendgridOptionsSetup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(SendgridOptions options)
        {
            configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}
