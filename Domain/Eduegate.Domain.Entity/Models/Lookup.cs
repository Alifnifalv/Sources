using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Lookups", Schema = "setting")]
    public partial class Lookup
    {
        public Lookup()
        {
            this.ViewFilters = new List<ViewFilter>();
        }

        [Key]
        public int LookupID { get; set; }
        public string LookupType { get; set; }
        public string LookupName { get; set; }
        public string Query { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public virtual ICollection<ViewFilter> ViewFilters { get; set; }
    }
}
