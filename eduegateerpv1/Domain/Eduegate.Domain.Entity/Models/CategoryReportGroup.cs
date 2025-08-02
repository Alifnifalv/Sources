using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryReportGroup
    {
        [Key]
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
