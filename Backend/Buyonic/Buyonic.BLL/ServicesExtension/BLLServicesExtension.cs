using Buyonic.BLL.Managers.Payment;
using Buyonic.BLL.Managers.ProductMng;
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
            services.AddScoped<ICartManager, CartManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IPaymentManager, PaymentManager>();
            services.AddOptions<EmailSettings>().Bind(configuration.GetSection("EmailSettings"));
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<ISellerManager, SellerManager>();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IReviewManager, ReviewManager>();
            services.AddScoped<IWishlistManager, WishlistManager>();
        }
    }
}
