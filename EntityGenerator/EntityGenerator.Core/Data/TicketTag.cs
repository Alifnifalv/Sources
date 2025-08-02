using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TicketTags", Schema = "cs")]
    public partial class TicketTag
    {
        [Key]
        public int TicketTagsID { get; set; }
        [StringLength(50)]
        public string TagName { get; set; }
    }
}
