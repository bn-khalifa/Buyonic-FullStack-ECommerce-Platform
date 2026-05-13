using Buyonic.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Buyonic.BLL
{
    public static class BLLServicesExtension
    {
        public static void AddBLLServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<ISellerManager, SellerManager>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddOptions<EmailSettings>().Bind(configuration.GetSection("EmailSettings"));
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
