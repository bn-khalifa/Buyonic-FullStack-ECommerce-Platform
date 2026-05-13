using Buyonic.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Buyonic.DAL
{
    public static class DbInitializer
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            //var seedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var seedDate = new DateTime(2026, 6, 6, 10, 30, 0, DateTimeKind.Utc);
            // 1. Roles
            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int> { Id = 2, Name = "Customer", NormalizedName = "CUSTOMER" },
                new IdentityRole<int> { Id = 3, Name = "Seller", NormalizedName = "SELLER" }
            );

            // 2. ApplicationUsers
            //var hasher = new PasswordHasher<ApplicationUser>();
            var passwordHash = "AQAAAAIAAYagAAAAEEKyGwLDN+tZyAFPQ74x6xZpmStNgyRKcAgi3Z6z5wmWv4MiPX3+Z0GFmd4Tj638Mw==";
            var user1 = new ApplicationUser { Id = 1, UserName = "ahmed@mail.com", PasswordHash =passwordHash, NormalizedUserName = "AHMED@MAIL.COM", Email = "ahmed@mail.com", NormalizedEmail = "AHMED@MAIL.COM", firstName = "Ahmed", lastName = "Ali", isActive = true, createdAt = seedDate, SecurityStamp = "a1b2c3d4-0001-0000-0000-000000000000" };
            var user2 = new ApplicationUser { Id = 2, UserName = "sara@mail.com", PasswordHash = passwordHash, NormalizedUserName = "SARA@MAIL.COM", Email = "sara@mail.com", NormalizedEmail = "SARA@MAIL.COM", firstName = "Sara", lastName = "Omar", isActive = true, createdAt = seedDate, SecurityStamp = "a1b2c3d4-0002-0000-0000-000000000000" };
            var user3 = new ApplicationUser { Id = 3, UserName = "store@mail.com", PasswordHash = passwordHash, NormalizedUserName = "STORE@MAIL.COM", Email = "store@mail.com", NormalizedEmail = "STORE@MAIL.COM", firstName = "Store", lastName = "Owner", isActive = true, createdAt = seedDate, SecurityStamp = "a1b2c3d4-0003-0000-0000-000000000000" };
            
            modelBuilder.Entity<ApplicationUser>().HasData(user1, user2, user3);

            // 3. UserRoles
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { UserId = 1, RoleId = 2 },
                new IdentityUserRole<int> { UserId = 2, RoleId = 2 },
                new IdentityUserRole<int> { UserId = 3, RoleId = 3 }
            );

            // 4. Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic devices and accessories" },
                new Category { Id = 2, Name = "Clothing", Description = "Men and women clothing" },
                new Category { Id = 3, Name = "Home & Garden", Description = "Home and garden supplies" }
            );

            // 5. Sellers
            modelBuilder.Entity<Seller>().HasData(
                new Seller { Id = 1, StoreName = "TechZone", Rating = 4.5m, UserId = 3 }
            );

            // 6. Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, address = "123 Nile St, Cairo", userId = 1 },
                new Customer { Id = 2, address = "456 Delta Rd, Tanta", userId = 2 }
            );

            // 7. Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop Pro", Description = "15-inch laptop", Price = 1200m, StockQuantity = 50, Rating = 4.7m, Discount = 0, SellerId = 1, CategoryId = 1, createdAt = seedDate },
                new Product { Id = 2, Name = "Wireless Mouse", Description = "Ergonomic mouse", Price = 25m, StockQuantity = 200, Rating = 4.3m, Discount = 0, SellerId = 1, CategoryId = 1, createdAt = seedDate },
                new Product { Id = 3, Name = "Cotton T-Shirt", Description = "Comfortable fit", Price = 15m, StockQuantity = 300, Rating = 4.0m, Discount = 0, SellerId = 1, CategoryId = 2, createdAt = seedDate }
            );

            // 8. PaymentMethods
            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod { Id = 1, methodName = "Credit Card" },
                new PaymentMethod { Id = 2, methodName = "Cash on Delivery" },
                new PaymentMethod { Id = 3, methodName = "PayPal" }
            );

            // 9. Orders
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, customerId = 1, paymentMethodId = 1,ShippingAddress="Cairo" ,status = "Pending", totalAmount = 1225m, createdAt = seedDate }
            );

            // 10. Carts
            modelBuilder.Entity<Cart>().HasData(
                new Cart { Id = 1, customerId = 1 },
                new Cart { Id = 2, customerId = 2 }
            );

            // 11. Wishlists
            modelBuilder.Entity<Wishlist>().HasData(
                new Wishlist { Id = 1, customerId = 1 },
                new Wishlist { Id = 2, customerId = 2 }
            );

            // 12. CustomerPayments
            modelBuilder.Entity<CustomerPayment>().HasData(
                new CustomerPayment { Id = 1, customerId = 1, paymentMethodId = 1 }
            );

            // 13. OrderItems
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, orderId = 1, productId = 1, quantity = 1, price = 1200m },
                new OrderItem { Id = 2, orderId = 1, productId = 2, quantity = 1, price = 25m }
            );

            // 14. CartItems
            modelBuilder.Entity<CartItem>().HasData(
                new CartItem { Id = 1, cartId = 1, productId = 3, quantity = 2 }
            );

            // 15. WishlistItems
            modelBuilder.Entity<WishlistItem>().HasData(
                new WishlistItem { Id = 1, wishlistId = 1, productId = 1 }
            );
        }
    }
}