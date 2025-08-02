using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MessageSendTypes", Schema = "notification")]
    public partial class MessageSendType
    {
        [Key]
        public int TypeID { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
    }
}
