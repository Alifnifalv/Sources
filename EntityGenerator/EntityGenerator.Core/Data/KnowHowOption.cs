using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("KnowHowOption", Schema = "cms")]
    public partial class KnowHowOption
    {
        [Key]
        public long KnowHowOptionIID { get; set; }
        [Required]
        [StringLength(100)]
        public string KnowHowOptionText { get; set; }
        public bool IsEditable { get; set; }
    }
}
