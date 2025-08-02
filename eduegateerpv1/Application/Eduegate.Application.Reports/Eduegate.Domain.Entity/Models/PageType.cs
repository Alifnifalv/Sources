using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PageType
    {
        public PageType()
        {
            this.Pages = new List<Page>();
        }

        public byte PageTypeID { get; set; }
        public string TypeName { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
    }
}
