using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PageTypes", Schema = "cms")]
    public partial class PageType
    {
        public PageType()
        {
            Pages = new HashSet<Page>();
        }

        [Key]
        public byte PageTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }

        [InverseProperty("PageType")]
        public virtual ICollection<Page> Pages { get; set; }
    }
}
