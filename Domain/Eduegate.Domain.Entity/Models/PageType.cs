using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("PageTypes", Schema = "cms")]
    public partial class PageType
    {
        public PageType()
        {
            this.Pages = new List<Page>();
        }

        [Key]
        public byte PageTypeID { get; set; }
        public string TypeName { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
    }
}
