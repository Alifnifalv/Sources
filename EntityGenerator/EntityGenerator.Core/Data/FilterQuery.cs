using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FilterQueries", Schema = "setting")]
    public partial class FilterQuery
    {
        public FilterQuery()
        {
            Views = new HashSet<View>();
        }

        [Key]
        public int FilterQueriesID { get; set; }
        [Column("FilterQuery")]
        public string FilterQuery1 { get; set; }

        [InverseProperty("FilterQueries")]
        public virtual ICollection<View> Views { get; set; }
    }
}
