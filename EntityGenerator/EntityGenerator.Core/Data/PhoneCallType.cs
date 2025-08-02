using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PhoneCallTypes", Schema = "schools")]
    public partial class PhoneCallType
    {
        [Key]
        public byte PhoneCallTypeID { get; set; }
        [StringLength(50)]
        public string TypeDescription { get; set; }
    }
}
