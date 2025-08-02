using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ChannelTypes", Schema = "communities")]
    public partial class ChannelType
    {
        [Key]
        public int ChannelTypeID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
    }
}
