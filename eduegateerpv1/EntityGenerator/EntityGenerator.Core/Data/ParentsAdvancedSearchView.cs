using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ParentsAdvancedSearchView
    {
        public long ParentIID { get; set; }
        [StringLength(10)]
        public string ParentCode { get; set; }
        [Required]
        [StringLength(302)]
        public string FatherName { get; set; }
        [Required]
        [StringLength(302)]
        public string MotherName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FatherPhone { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MotherPhone { get; set; }
    }
}
