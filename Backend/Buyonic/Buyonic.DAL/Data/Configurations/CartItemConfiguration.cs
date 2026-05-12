using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyonic.DAL.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            //builder.HasKey(ci => new
            //{
            //    ci.Id,
            //    ci.productId
            //});

            //builder.HasOne(ci => ci.Cart)
            //       .WithMany(c => c.CartItems)
            //       .HasForeignKey(ci => ci.Id);

            //builder.HasOne(ci => ci.Product)
            //       .WithMany(p => p.CartItems)
            //       .HasForeignKey(ci => ci.productId);
        }
    }
}
