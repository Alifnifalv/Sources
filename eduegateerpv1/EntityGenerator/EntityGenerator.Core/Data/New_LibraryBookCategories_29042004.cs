using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("New_LibraryBookCategories_29042004", Schema = "schools")]
    public partial class New_LibraryBookCategories_29042004
    {
        public string BookCategoryName { get; set; }
        public int? CreatedDate { get; set; }
        public int? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(4000)]
        public string CategoryCode { get; set; }
    }
}
