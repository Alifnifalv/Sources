using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    public class CategoryTree
    {
        public CategoryTree()
        {
            Products = new List<ProductTree>();
            Categories = new List<CategoryTree>();
        }
        public long CategoryID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }

        public List<ProductTree> Products { get; set; }
        public List<CategoryTree> Categories { get; set; }
    }
}
