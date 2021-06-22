using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
            builder.HasData(
                new Category { Id = 1, Name = "Entertainment" },
                new Category { Id = 2, Name = "Medical" },
                new Category { Id = 3, Name = "Academic" },
                new Category { Id = 4, Name = "Business" },
                new Category { Id = 5, ParentCategoryId = 1, Name = "Hobbies" },
                new Category { Id = 6, ParentCategoryId = 5, Name = "Fashion" },
                new Category { Id = 7, ParentCategoryId = 5, Name = "Jewelry Making" },
                new Category { Id = 8, ParentCategoryId = 3, Name = "Engineering" },
                new Category { Id = 9, ParentCategoryId = 8, Name = "Computer" },
                new Category { Id = 10, ParentCategoryId = 8, Name = "Electronics" },
                new Category { Id = 11, ParentCategoryId = 9, Name = "It" },
                new Category { Id = 12, ParentCategoryId = 9, Name = "Programming" },
                new Category { Id = 13, ParentCategoryId = 4, Name = "Finance" },
                new Category { Id = 14, ParentCategoryId = 4, Name = "Industries" },
                new Category { Id = 15, ParentCategoryId = 2, Name = "Dentistry" }
                );
            builder.HasOne(c => c.category).WithMany(c => c.Categories).HasForeignKey(c => c.ParentCategoryId);
        }
    }
}
