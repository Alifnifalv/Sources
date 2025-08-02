using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategorySortMaster
    {
        [Key]
        public byte CategorySortID { get; set; }
        public string CategoryText { get; set; }
        public string CategoryValue { get; set; }
        public string CategoryTextCode { get; set; }
    }
}
