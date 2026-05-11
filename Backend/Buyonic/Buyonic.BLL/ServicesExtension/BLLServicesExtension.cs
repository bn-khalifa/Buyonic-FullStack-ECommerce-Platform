using Buyonic.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace Buyonic.BLL
{
    public static class BLLServicesExtension
    {
        public static void AddBLLServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<ISellerManager, SellerManager>();
            services.AddScoped<IAuthManager, AuthManager>();
        }
    }
}
