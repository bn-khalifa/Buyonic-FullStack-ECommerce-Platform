using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.DAL.Data.Configurations
{
    internal class SellerConfigration : IEntityTypeConfiguration<Seller>
    {

        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            /*
            // Table Name (اختياري)
            builder.ToTable("Sellers");

            // Primary Key
            builder.HasKey(s => s.Id);

            // Properties
            builder.Property(s => s.storeName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.rating)
                   .HasColumnType("real"); // لأن float في SQL Server = real

            builder.Property(s => s.userId)
                   .IsRequired();

            // العلاقة مع User (ApplicationUser)
            builder.HasOne(s => s.User)
                   .WithMany()
                   .HasForeignKey(s => s.userId)
                   .OnDelete(DeleteBehavior.Cascade);

            // العلاقة مع Products (One-to-Many)
            builder.HasMany(s => s.Products)
                   .WithOne(p => p.Seller)
                   .HasForeignKey(p => p.sellerId)
                   .OnDelete(DeleteBehavior.Cascade);
            */
        }
    }
}
