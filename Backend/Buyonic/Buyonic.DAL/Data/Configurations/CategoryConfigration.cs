using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.DAL.Data.Configurations
{
    internal class CategoryConfigration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.name)
                   .IsRequired()
                   .HasMaxLength(100);

            // Relationship

            builder.HasMany(c => c.products)
                   .WithOne(p => p.Category)
                   .HasForeignKey(p => p.categoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
