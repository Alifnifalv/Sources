using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("LibraryBookCategories_Bkp_2904204", Schema = "schools")]
    public partial class LibraryBookCategories_Bkp_2904204
    {
        public byte LibraryBookCategoryID { get; set; }
        public string BookCategoryName { get; set; }
        public string CreatedDate { get; set; }
        [StringLength(50)]
        public string UpdatedDate { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public short? CreatedBy { get; set; }
        [StringLength(60)]
        public string CategoryCode { get; set; }
    }
}
