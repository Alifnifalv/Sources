using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ServiceGroups", Schema = "saloon")]
    public partial class ServiceGroup
    {
        public ServiceGroup()
        {
            Services = new HashSet<Service>();
        }

        [Key]
        public long ServiceGroupIID { get; set; }
        [StringLength(200)]
        public string GroupName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("ServiceGroup")]
        public virtual ICollection<Service> Services { get; set; }
    }
}
