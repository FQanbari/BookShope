using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.ViewModel
{
    public class TreeViewCategory
    {
        public TreeViewCategory()
        {
            Subs = new List<TreeViewCategory>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public List<TreeViewCategory> Subs { get; set; }
    }
}
