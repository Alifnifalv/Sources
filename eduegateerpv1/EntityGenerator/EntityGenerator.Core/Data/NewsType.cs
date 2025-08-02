using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("NewsTypes", Schema = "cms")]
    public partial class NewsType
    {
        public NewsType()
        {
            News = new HashSet<News>();
        }

        [Key]
        public int NewsTypeID { get; set; }
        [StringLength(50)]
        public string NewsTypeName { get; set; }

        [InverseProperty("NewsType")]
        public virtual ICollection<News> News { get; set; }
    }
}
