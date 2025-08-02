using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ComplainTypes", Schema = "schools")]
    public partial class ComplainType
    {
        [Key]
        public byte ComplainTypeID { get; set; }
        [StringLength(50)]
        public string ComplainDescription { get; set; }
    }
}
