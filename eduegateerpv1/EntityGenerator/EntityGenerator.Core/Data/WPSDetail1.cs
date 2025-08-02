using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WPSDetail", Schema = "schools")]
    public partial class WPSDetail1
    {
        [Key]
        public long DetailIID { get; set; }
        public long? HeadIID { get; set; }
        public long? EmployeeID { get; set; }
        public string Entitlement { get; set; }
        public string Comments { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("WPSDetail1")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("HeadIID")]
        [InverseProperty("WPSDetail1")]
        public virtual WPSHead HeadI { get; set; }
    }
}
