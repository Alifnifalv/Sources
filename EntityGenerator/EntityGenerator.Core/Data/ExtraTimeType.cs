using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ExtraTimeTypes", Schema = "saloon")]
    public partial class ExtraTimeType
    {
        public ExtraTimeType()
        {
            Services = new HashSet<Service>();
        }

        [Key]
        public int ExtraTimeTypeID { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

        [InverseProperty("ExtraTimeType")]
        public virtual ICollection<Service> Services { get; set; }
    }
}
