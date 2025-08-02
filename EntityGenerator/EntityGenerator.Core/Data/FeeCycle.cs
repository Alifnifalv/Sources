using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeCycles", Schema = "schools")]
    public partial class FeeCycle
    {
        public FeeCycle()
        {
            FeeMasters = new HashSet<FeeMaster>();
            FeeTypes = new HashSet<FeeType>();
        }

        [Key]
        public byte FeeCycleID { get; set; }
        [StringLength(50)]
        public string Cycle { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("FeeCycle")]
        public virtual ICollection<FeeMaster> FeeMasters { get; set; }
        [InverseProperty("FeeCycle")]
        public virtual ICollection<FeeType> FeeTypes { get; set; }
    }
}
