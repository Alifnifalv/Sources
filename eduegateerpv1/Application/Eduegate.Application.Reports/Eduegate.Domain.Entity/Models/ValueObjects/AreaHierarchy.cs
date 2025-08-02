using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    public class AreaHierarchy
    {
        public AreaHierarchy()
        {
            CategoryTree = new List<CategoryTree>();
        }

        public List<CategoryTree> CategoryTree { get; set; }
    }
}
