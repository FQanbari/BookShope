using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Mapping
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Moblie).HasMaxLength(11).IsRequired();
            builder.HasOne(c => c.City1).WithMany(c => c.Customers1).HasForeignKey(c => c.CityId1);
            builder.HasOne(c => c.City2).WithMany(c => c.Customers2).HasForeignKey(c => c.CityId2).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
