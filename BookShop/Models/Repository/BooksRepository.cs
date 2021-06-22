using BookShop.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.Repository
{
    public class BooksRepository
    {
        private readonly BookShopContext _context;
        public BooksRepository(BookShopContext context)
        {
            _context = context;
        }

        public List<TreeViewCategory> GetAllCategories()
        {
            var categores = _context.Categories.Where(p => p.ParentCategoryId == null)
                .Select(p => new TreeViewCategory()
                {
                    Id = p.Id,
                    Title = p.Name
                }).ToList();

            foreach (var item in categores)
            {
                BindSubCategory(item);
            }

            return categores;
        }

        public void BindSubCategory(TreeViewCategory category)
        {
            var subCategory = _context.Categories
                .Where(p => p.ParentCategoryId == category.Id)
                .Select(p => new TreeViewCategory() {
                    Id = p.Id,
                    Title = p.Name
                }).ToList();

            foreach (var item in subCategory)
            {
                BindSubCategory(item);
                category.Subs.Add(item);
            }
        }

        public List<BookIndexViewModel> GetAllBooks(BookAdvancedSearch param)
        {
            List<BookIndexViewModel> result = new List<BookIndexViewModel>();

            var books = _context.Books
               .Where(p => p.IsDelete == false 
               && p.Title.Contains(param.Title.TrimStart().TrimEnd())
               && p.ISBN.Contains(param.ISBN.TrimStart().TrimEnd())             
               );

            if (!string.IsNullOrEmpty(param.LanguageId))
            {
                books = books.Where(p => p.LanguageId == int.Parse(param.LanguageId));
            }

            if (!string.IsNullOrEmpty(param.AuthorId))
            {
                books = (from a in books
                         join b in _context.AuthorBooks on a.Id equals b.BookId
                         where(b.AuthorId == int.Parse(param.AuthorId))
                         select a);
               // books.Where(p => p.AuthorId == int.Parse(param.AuthorId));
            }
            if (!string.IsNullOrEmpty(param.PublisherId))
            {
                books = books.Where(p => p.PublisherId == int.Parse(param.PublisherId));
            }
            if (!string.IsNullOrEmpty(param.TranslatorId))
            {
                books = (from a in books
                         join b in _context.BookTranslator on a.Id equals b.BookId
                         where (b.TranslatorId == int.Parse(param.TranslatorId))
                         select a);
                //books = books.Where(p => p.bookTranlators == int.Parse(param.PublisherId));
            }
            if (!string.IsNullOrEmpty(param.CategoryName))
            {
                books = (from a in books
                         join b in _context.BookCategories on a.Id equals b.BookId
                         where (b.Category.Name.Contains(param.CategoryName.TrimStart().TrimEnd()))
                         select a);
            }
            var tmp = books.ToList();
            result = books.Select(p => new BookIndexViewModel
            {
                Id = p.Id,
                Title = p.Title,
                PublisherName = p.Publisher.Name,
                Price = p.Price,
                ISBN = p.ISBN,
                Stock = p.Stock,
                PublishDate = p.PublishDate,
                IsPublish = p.IsPublish,
                Authors = p.AuthorBooks.Select(p => p.Author.FirstName + " " + p.Author.LastName).ToList(),
                Translators = p.bookTranlators.Select(p => p.Translator.FirstName + " " + p.Translator.LastName).ToList(),
                Language = p.Language.Name,
                Categories = p.BookCategories.Select(p => p.Category.Name).ToList(),
            }).ToList();

            return result;
        }
    }
}
