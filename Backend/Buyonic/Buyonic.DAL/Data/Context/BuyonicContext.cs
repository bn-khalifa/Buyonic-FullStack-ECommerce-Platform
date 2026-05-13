using Buyonic.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Buyonic.DAL
{
    public class BuyonicContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public BuyonicContext(DbContextOptions<BuyonicContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BuyonicContext).Assembly);
            DbInitializer.Seed(modelBuilder);

            // Disable cascade delete for all foreign keys
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w =>
                w.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        // I am using Set<> to prevent assigning incorrect values to the property
        public virtual DbSet<Customer> Customers => Set<Customer>();
        public virtual DbSet<Seller> Sellers => Set<Seller>();
        public virtual DbSet<Product> Products => Set<Product>();
        public virtual DbSet<Category> Categories => Set<Category>();
        public virtual DbSet<Order> Orders => Set<Order>();
        public virtual DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public virtual DbSet<Cart> Carts => Set<Cart>();
        public virtual DbSet<CartItem> CartItems => Set<CartItem>();
        public virtual DbSet<Wishlist> Wishlists => Set<Wishlist>();
        public virtual DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();
        public virtual DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
        public virtual DbSet<CustomerPayment> CustomerPayments => Set<CustomerPayment>();
        public virtual DbSet<Review> Reviews => Set<Review>();
    }
}
