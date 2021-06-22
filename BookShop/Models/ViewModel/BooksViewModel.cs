using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.ViewModel
{
    public class BooksCreateViewModel
    {
        public BooksCreateViewModel(IEnumerable<TreeViewCategory> viewCategories)
        {
            Categories = viewCategories;
        }

        public BooksCreateViewModel()
        {

        }

        public IEnumerable<TreeViewCategory> Categories { get; set; }

        [Required(ErrorMessage = "Please enter your {0}")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Summary")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Please enter your {0}")]
        [Display(Name = "Price")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Please enter your {0}")]
        [Display(Name = "Stock")]
        public int Stock { get; set; }
        public string File { get; set; }

        [Display(Name = "NumOfPages")]
        public int NumOfPages { get; set; }

        [Display(Name = "ISBN")]
        public string ISBN { get; set; }

        [Display(Name = "This Book Published in Site")]
        public bool IsPublish { get; set; }


        [Display(Name = "PublishYear")]
        public int PublishYear { get; set; }

        [Required(ErrorMessage = "Please enter your {0}")]
        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        [Required(ErrorMessage = "Please enter your {0}")]
        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }

        [Required(ErrorMessage = "Please enter your {0}")]
        [Display(Name = "Authors")]
        public int[] AuthorId { get; set; }

        [Display(Name = "Translators")]
        public int[] TranslatorId { get; set; }
        public int[] CategoryId { get; set; }
    }

    public class AuthorList
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class TranslatorList
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class BookIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string ISBN { get; set; }
        [Display(Name = "Publisher Name")]
        public string PublisherName { get; set; }
        [Display(Name = "Publish Date")]
        public DateTime? PublishDate { get; set; }
        [Display(Name = "Status")]
        public bool? IsPublish { get; set; }
        public List<string> Authors { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public List<string> Translators { get; set; }
        public List<string> Categories { get; set; }
    }

    public class BookAdvancedSearch
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string AuthorId { get; set; }
        public string PublisherId { get; set; }
        public string LanguageId { get; set; }
        public string TranslatorId { get; set; }
        public string CategoryName { get; set; }
    }
}
