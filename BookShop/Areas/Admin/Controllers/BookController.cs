using BookShop.Models;
using BookShop.Models.Repository;
using BookShop.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly BookShopContext _context;
        private readonly BooksRepository _repository;
        public BookController(BookShopContext context, BooksRepository repository)
        {
            _context = context;
            _repository = repository;
        }
        public IActionResult Index(int page = 1, int row = 5, string sortExpression = "Title",string title="")
        {
            title = string.IsNullOrEmpty(title) ? "" : title;
            List<int> Rows = new List<int>
            {
                5,10,15,20,50,100
            };

            ViewBag.RowID = new SelectList(Rows, row);
            ViewBag.NumOfRow = (page - 1) * row + 1;
            ViewBag.Search = title;

            var param = new BookAdvancedSearch()
            {
                Title = title,
                ISBN = "",
                CategoryName = ""
            };

            var PagingModel = PagingList.Create(_repository.GetAllBooks(param), row, page, sortExpression,"Title");
            PagingModel.RouteValue = new RouteValueDictionary
            {
                {"row",row},
                {"title", title }
            };

            ViewBag.Categories = _repository.GetAllCategories();
            ViewBag.LanguageId = new SelectList(_context.Languages, "Id", "Name");
            ViewBag.PublisherId = new SelectList(_context.Publisher, "Id", "Name");
            ViewBag.AuthorId = new SelectList(_context.Authors.Select(p => new AuthorList() { Id = p.Id, FullName = p.FirstName + " " + p.LastName }), "Id", "FullName");
            ViewBag.TranslatorId = new SelectList(_context.Translator.Select(p => new TranslatorList() { Id = p.Id, FullName = p.FirstName + " " + p.LastName }), "Id", "FullName");

            return View(PagingModel);
        }

        public IActionResult Create()
        {          
            ViewBag.LanguageId = new SelectList(_context.Languages, "Id", "Name");
            ViewBag.PublisherId = new SelectList(_context.Publisher, "Id", "Name");
            ViewBag.AuthorId = new SelectList(_context.Authors.Select(p => new AuthorList() { Id = p.Id, FullName = p.FirstName + " " + p.LastName }), "Id", "FullName");
            ViewBag.TranslatorId = new SelectList(_context.Translator.Select(p => new TranslatorList() { Id = p.Id, FullName = p.FirstName + " " + p.LastName }), "Id", "FullName");

            BooksCreateViewModel viewModel = new BooksCreateViewModel(_repository.GetAllCategories());
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BooksCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                List<AuthorBook> authors = new List<AuthorBook>();
                List<BookTranslator> translators = new List<BookTranslator>();
                List<BookCategory> categories = new List<BookCategory>();
                DateTime? publishDate = null;
                if (viewModel.IsPublish)
                {
                    publishDate = DateTime.Now;
                }

                Book book = new Book()
                {
                    IsDelete = false,
                    ISBN = viewModel.ISBN,
                    IsPublish = viewModel.IsPublish,
                    NumOfPages = viewModel.NumOfPages,
                    Stock = viewModel.Stock,
                    Price = viewModel.Price,
                    LanguageId = viewModel.LanguageId,
                    Summary = viewModel.Summary,
                    Title = viewModel.Title,
                    PublishYear = viewModel.PublishYear,
                    PublishDate = publishDate,
                    PublisherId = viewModel.PublisherId
                };
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

                //var bookId = _context.Books.Where(p => p.ISBN == book.ISBN).Select(p => p.Id).FirstOrDefault();

                if (viewModel.AuthorId != null)
                {
                    for (int i = 0; i < viewModel.AuthorId.Length ; i++)
                    {
                        AuthorBook author = new AuthorBook()
                        {
                            BookId = book.Id,
                            AuthorId = viewModel.AuthorId[i]
                        };

                        authors.Add(author);                        
                    }

                    await _context.AuthorBooks.AddRangeAsync(authors);
                }

                if (viewModel.TranslatorId != null)
                {
                    for (int i = 0; i < viewModel.TranslatorId.Length; i++)
                    {
                        BookTranslator translator = new BookTranslator()
                        {
                            BookId = book.Id,
                            TranslatorId = viewModel.TranslatorId[i]
                        };

                        translators.Add(translator);
                    }

                    await _context.BookTranslator.AddRangeAsync(translators);
                }

                if (viewModel.CategoryId != null)
                {
                    for (int i = 0; i < viewModel.CategoryId.Length; i++)
                    {
                        BookCategory category = new BookCategory()
                        {
                            BookId = book.Id,
                            CategoryId = viewModel.CategoryId[i]
                        };

                        categories.Add(category);
                    }

                    await _context.BookCategories.AddRangeAsync(categories);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.LanguageId = new SelectList(_context.Languages, "Id", "Name");
                ViewBag.PublisherId = new SelectList(_context.Publisher, "Id", "Name");
                ViewBag.AuthorId = new SelectList(_context.Authors.Select(p => new AuthorList() { Id = p.Id, FullName = p.FirstName + " " + p.LastName }), "Id", "FullName");
                ViewBag.TranslatorId = new SelectList(_context.Translator.Select(p => new TranslatorList() { Id = p.Id, FullName = p.FirstName + " " + p.LastName }), "Id", "FullName");
                viewModel.Categories = _repository.GetAllCategories();

                return View(viewModel);
            }
        }

        public IActionResult AdvancedSearch(BookAdvancedSearch param)
        {
            param.Title = string.IsNullOrEmpty(param.Title) ? "" : param.Title;
            param.ISBN = string.IsNullOrEmpty(param.ISBN) ? "" : param.ISBN;
            param.CategoryName = string.IsNullOrEmpty(param.CategoryName) ? "" : param.CategoryName;

            var books = _repository.GetAllBooks(param);
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var bookInfo = _context.Books.Include(p => p.Language).Include(p => p.Publisher).Where(p => p.Id == id).First();

            ViewBag.Authors = _context.Authors.FromSqlRaw("EXEC dbo.GetAuthorsByBookId {0}", id).ToList();
            ViewBag.Translators = _context.BookTranslator
                .Include(p => p.Translator)
                .Where(p => p.BookId == id)
                .Select(p => new Translator { LastName = p.Translator.LastName, FirstName = p.Translator.FirstName }).ToList();
            ViewBag.Categories = _context.BookCategories.Include(p => p.Category).Where(p => p.BookId == id).Select(p => new Category { Name = p.Category.Name }).ToList();

            return View(bookInfo);
        }
    }
}
