using Buyonic.DAL.Repositories.CartRepository;
using Buyonic.DAL.Repositories.CustomerPaymentRepository;
using Buyonic.DAL.Repositories.OrderRepository;
using Buyonic.DAL.Repositories.PaymentMethodRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Buyonic.DAL
{
    public static class DALServicesExtension
    {
        public static void AddDALServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();                         
            services.AddScoped<IOrderRepository, OrderRepository>();                       
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();       
            services.AddScoped<ICustomerPaymentRepository, CustomerPaymentRepository>();   
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
