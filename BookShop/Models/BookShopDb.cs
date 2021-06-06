using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string File { get; set; }
        public byte[] Image { get; set; }
        public string ISBN { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
        public int PublisherId { get; set; }

        public Category Category { get; set; }
        public Language Language { get; set; }
        public Discount Discount { get; set; }
        public Publisher Publisher { get; set; }
        public List<AuthorBook> AuthorBooks { get; set; }
        public List<OrderBook> OrderBooks { get; set; }
        public List<BookTranslator> bookTranlators { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Category> Categories { get; set; }
        public List<Book> Books { get; set; }
        public Category category { get; set; }
        public Publisher Publisher { get; set; }
    }

    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }

    public class Discount
    {
        public int BookId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte percent { get; set; }

        public Book Book { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<AuthorBook> AuthorBooks { get; set; }
    }

    public class AuthorBook
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        public Book Book { get; set; }
        public Author Author { get; set; }
    }

    public class Customer
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Moblie { get; set; }
        public string Image { get; set; }
        public int CityId1 { get; set; }
        public int CityId2 { get; set; }

        public City City1 { get; set; }
        public City City2 { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class Provice
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<City> Cities { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProviceId { get; set; }

        public Provice Provice { get; set; }
        public List<Customer> Customers1 { get; set; }
        public List<Customer> Customers2 { get; set; }
    }

    public class Order
    {
        public string Id { get; set; }
        public long AmountPaid { get; set; }
        public string DispatchNumber { get; set; }
        public DateTime BuyDate { get; set; }
        public int OrderStatusId { get; set; }
        public string CustomerId { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public Customer Customer { get; set; }
        public List<OrderBook> OrderBooks { get; set; }
    }

    public class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Order> Orders { get; set; }
    }

    public class OrderBook
    {
        public string OrderId { get; set; }
        public int BookId { get; set; }

        public Order Order { get; set; }
        public Book Book { get; set; }
    }

    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }

    public class BookTranslator
    {
        public int TranslatorId { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
        public Translator Translator { get; set; }
    }

    public class Translator
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<BookTranslator> bookTranlators { get; set; }
    }
}
