using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ActionLinkTypes", Schema = "mutual")]
    public partial class ActionLinkType
    {
        [Key]
        public int ActionLinkTypeID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string ActionLinkTypeName { get; set; }
    }
}
