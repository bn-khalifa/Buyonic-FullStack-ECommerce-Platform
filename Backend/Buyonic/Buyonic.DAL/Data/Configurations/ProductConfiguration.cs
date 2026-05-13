using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.DAL.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            /*
            // Table name (optional)
            builder.ToTable("Products");

            // Primary Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.description)
                   .HasMaxLength(500);

            builder.Property(p => p.price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(p => p.stockQuantity)
                   .IsRequired();

            // Relationships

            // Product → Category
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.products)
                   .HasForeignKey(p => p.categoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Product → Seller
            builder.HasOne(p => p.Seller)
                   .WithMany(s => s.Products)
                   .HasForeignKey(p => p.sellerId)
                   .OnDelete(DeleteBehavior.Restrict);
            */
        }
    }
}
