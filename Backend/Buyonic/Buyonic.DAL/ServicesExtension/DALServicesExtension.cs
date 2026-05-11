using Buyonic.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Buyonic.DAL
{
    public static class DALServicesExtension
    {
        public static void AddDALServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
