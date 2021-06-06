using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using BookShop.Mapping;

namespace BookShop.Models
{
    public class BookShopContext:DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=.;Database=BookShopDb;Trusted_Connection=True");
        //}

        public BookShopContext(DbContextOptions<BookShopContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookMap());

            modelBuilder.ApplyConfiguration(new CategoryMap());

            modelBuilder.Entity<Language>().ToTable("Language");
            modelBuilder.Entity<Language>().HasKey(l => l.Id);
            modelBuilder.Entity<Language>().Property(l => l.Name).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Discount>().ToTable("Discount");
            modelBuilder.Entity<Discount>().HasKey(d => d.BookId);
            modelBuilder.Entity<Discount>().Property(d => d.percent).IsRequired();

            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Author>().HasKey(a => a.Id);
            modelBuilder.Entity<Author>().Property(a => a.FirstName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Author>().Property(a => a.LastName).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<AuthorBook>().ToTable("AuthorBook");
            modelBuilder.Entity<AuthorBook>().HasKey(ab => new { ab.BookId, ab.AuthorId });
            modelBuilder.Entity<AuthorBook>().Property(ab => ab.AuthorId).IsRequired();
            modelBuilder.Entity<AuthorBook>().Property(ab => ab.BookId).IsRequired();
            modelBuilder.Entity<AuthorBook>().HasOne(ab => ab.Book).WithMany(ab => ab.AuthorBooks).HasForeignKey(ab => ab.BookId);
            modelBuilder.Entity<AuthorBook>().HasOne(ab => ab.Author).WithMany(ab => ab.AuthorBooks).HasForeignKey(ab => ab.AuthorId);

            modelBuilder.ApplyConfiguration(new CustomerMap());

            modelBuilder.Entity<Provice>().ToTable("Provice");
            modelBuilder.Entity<Provice>().HasKey(p => p.Id);
            modelBuilder.Entity<Provice>().Property(p => p.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Provice>().Property(p => p.Id).ValueGeneratedNever();

            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<City>().HasKey(c => c.Id);
            modelBuilder.Entity<City>().Property(c => c.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<City>().Property(c => c.Id).ValueGeneratedNever();
            modelBuilder.Entity<City>().HasOne(c => c.Provice).WithMany(c => c.Cities).HasForeignKey(c => c.ProviceId);

            modelBuilder.ApplyConfiguration(new OrderMap());

            modelBuilder.Entity<OrderStatus>().ToTable("OrderStatus");
            modelBuilder.Entity<OrderStatus>().HasKey(os => os.Id);
            modelBuilder.Entity<OrderStatus>().Property(os => os.Name).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<OrderBook>().ToTable("OrderBook");
            modelBuilder.Entity<OrderBook>().HasKey(ob => new { ob.BookId, ob.OrderId });
            modelBuilder.Entity<OrderBook>().HasOne(ob => ob.Book).WithMany(ob => ob.OrderBooks).HasForeignKey(ob => ob.BookId);
            modelBuilder.Entity<OrderBook>().HasOne(ob => ob.Order).WithMany(ob => ob.OrderBooks).HasForeignKey(ob => ob.OrderId);

            modelBuilder.Entity<Publisher>().ToTable("Publisher");
            modelBuilder.Entity<Publisher>().HasKey(p => p.Id);
            modelBuilder.Entity<Publisher>().Property(p => p.Name).HasMaxLength(100).IsRequired();

            modelBuilder.Entity<BookTranslator>().ToTable("BookTranslator");
            modelBuilder.Entity<BookTranslator>().HasKey(a => new { a.BookId, a.TranslatorId });
            modelBuilder.Entity<BookTranslator>().HasOne(ab => ab.Book).WithMany(ab => ab.bookTranlators).HasForeignKey(ab => ab.BookId);
            modelBuilder.Entity<BookTranslator>().HasOne(ab => ab.Translator).WithMany(ab => ab.bookTranlators).HasForeignKey(ab => ab.TranslatorId);

            modelBuilder.Entity<Translator>().ToTable("Translator");
            modelBuilder.Entity<Translator>().HasKey(o => o.Id);
            modelBuilder.Entity<Translator>().Property(p => p.FirsName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Translator>().Property(p => p.LastName).HasMaxLength(100).IsRequired();

        }

        DbSet<Book> Books { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Provice> Provices { get; set; }
        DbSet<AuthorBook> Author_Books { get; set; }
        DbSet<OrderBook> Order_Books { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<Discount> Discounts { get; set; }
        DbSet<OrderStatus> OrderStatuses { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Publisher> Publisher { get; set; }
        DbSet<BookTranslator> BookTranslator { get; set; }
        DbSet<Translator> Translator { get; set; }
    }
}
