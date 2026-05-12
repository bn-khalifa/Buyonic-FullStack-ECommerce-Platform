using Buyonic.BLL.Managers.Cart;
using Buyonic.BLL.Managers.Order;
using Buyonic.BLL.Managers.Payment;
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

            services.AddScoped<ICartManager, CartManager>();        
            services.AddScoped<IOrderManager, OrderManager>();      
            services.AddScoped<IPaymentManager, PaymentManager>();
        }
    }
}
