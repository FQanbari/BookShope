using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Mapping
{
    public class BookMap: IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Title).IsRequired().HasMaxLength(100);
            builder.Property(b => b.Price).IsRequired();
            builder.Property(b => b.Stock).IsRequired();
            builder.Property(b => b.Image).HasColumnType("Image");
            builder.HasOne(b => b.Category).WithMany(b => b.Books).HasForeignKey(b => b.CategoryId);
            builder.HasOne(b => b.Language).WithMany(b => b.Books).HasForeignKey(b => b.LanguageId);
            builder.HasOne(b => b.Publisher).WithMany(b => b.Books).HasForeignKey(b => b.PublisherId);
            builder.HasOne(b => b.Discount).WithOne(b => b.Book).HasForeignKey<Discount>(b => b.BookId);

        }
    }
}
