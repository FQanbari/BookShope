using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Mapping
{
    public class OrderMap: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.AmountPaid).IsRequired();
            builder.Property(o => o.BuyDate).IsRequired();
            builder.Property(o => o.OrderStatusId).IsRequired();
            builder.HasOne(o => o.OrderStatus).WithMany(o => o.Orders).HasForeignKey(o => o.OrderStatusId);
            builder.HasOne(o => o.Customer).WithMany(o => o.Orders).HasForeignKey(o => o.CustomerId);
        }
    }
}
