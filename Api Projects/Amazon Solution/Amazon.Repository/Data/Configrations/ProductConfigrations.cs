using Amazon.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Repository.Data.Configrations
{
    internal class ProductConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).HasMaxLength(100);
            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");

            //Configure The Relation Between Product class and ProductBrand class 1 => M
            builder.HasOne(P => P.ProductBrand).WithMany()
                .HasForeignKey(P=>P.ProductBrandId);

            //Configure The Relation Between Product class and ProductType class 1 => M
            builder.HasOne(P=>P.ProductType).WithMany()
                .HasForeignKey(P=>P.ProductTypeId);
        }
    }
}
