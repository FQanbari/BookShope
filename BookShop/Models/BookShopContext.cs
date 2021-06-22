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

            modelBuilder.Entity<BookCategory>().ToTable("BookCategory");
            modelBuilder.Entity<BookCategory>().HasKey(bc => new { bc.BookId, bc.CategoryId });
            modelBuilder.Entity<BookCategory>().Property(bc => bc.CategoryId).IsRequired();
            modelBuilder.Entity<BookCategory>().Property(bc => bc.BookId).IsRequired();
            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Book).WithMany(bc => bc.BookCategories).HasForeignKey(bc => bc.BookId);
            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Category).WithMany(bc => bc.BookCategories).HasForeignKey(bc => bc.CategoryId);

            modelBuilder.Entity<Language>().ToTable("Language");
            modelBuilder.Entity<Language>().HasKey(l => l.Id);
            modelBuilder.Entity<Language>().Property(l => l.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Name = "Persion"},
                new Language { Id = 2, Name = "Arabic"},
                new Language { Id = 3, Name = "Spanich"},
                new Language { Id = 4, Name = "English"}
                );

            modelBuilder.Entity<Discount>().ToTable("Discount");
            modelBuilder.Entity<Discount>().HasKey(d => d.BookId);
            modelBuilder.Entity<Discount>().Property(d => d.percent).IsRequired();

            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Author>().HasKey(a => a.Id);
            modelBuilder.Entity<Author>().Property(a => a.FirstName).HasMaxLength(100);
            modelBuilder.Entity<Author>().Property(a => a.LastName).HasMaxLength(100);
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FirstName = "A.J" , LastName = "Hock" },
                new Author { Id = 2, FirstName = "Jack" , LastName = "Simon" },
                new Author { Id = 3, FirstName = "Sara" , LastName = "Rice" },
                new Author { Id = 4, FirstName = "Barbara" , LastName = "Okley" }
                );

            modelBuilder.Entity<AuthorBook>().ToTable("AuthorBook");
            modelBuilder.Entity<AuthorBook>().HasKey(ab => new { ab.BookId, ab.AuthorId });
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
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "Pskr"},
                new Publisher { Id = 2, Name = "Amazon"},
                new Publisher { Id = 3, Name = "Rayan"}
                );

            modelBuilder.Entity<BookTranslator>().ToTable("BookTranslator");
            modelBuilder.Entity<BookTranslator>().HasKey(a => new { a.BookId, a.TranslatorId });
            modelBuilder.Entity<BookTranslator>().HasOne(ab => ab.Book).WithMany(ab => ab.bookTranlators).HasForeignKey(ab => ab.BookId);
            modelBuilder.Entity<BookTranslator>().HasOne(ab => ab.Translator).WithMany(ab => ab.bookTranlators).HasForeignKey(ab => ab.TranslatorId);

            modelBuilder.Entity<Translator>().ToTable("Translator");
            modelBuilder.Entity<Translator>().HasKey(o => o.Id);
            modelBuilder.Entity<Translator>().Property(p => p.FirstName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Translator>().Property(p => p.LastName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Translator>().HasData(
                new Translator { Id = 1, FirstName = "Adam", LastName = "Hock" },
                new Translator { Id = 2, FirstName = "Jack" , LastName = "kidman" },
                new Translator { Id = 3, FirstName = "Sara" , LastName = "Delbargo" },
                new Translator { Id = 4, FirstName = "Jim" , LastName = "Okley" }
                );

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Provice> Provices { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<OrderBook> OrderBooks { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<BookTranslator> BookTranslator { get; set; }
        public DbSet<Translator> Translator { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
    }
}
