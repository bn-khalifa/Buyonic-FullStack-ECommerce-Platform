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
            builder.Property(p => p.Rating).HasColumnType("decimal(3,2)");
        }
    }
}
