using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeFineTypes", Schema = "schools")]
    public partial class FeeFineType
    {
        public FeeFineType()
        {
            FineMasters = new HashSet<FineMaster>();
        }

        [Key]
        public short FeeFineTypeID { get; set; }
        public int FeeTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string FineType { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("FeeTypeID")]
        [InverseProperty("FeeFineTypes")]
        public virtual FeeType FeeType { get; set; }
        [InverseProperty("FeeFineType")]
        public virtual ICollection<FineMaster> FineMasters { get; set; }
    }
}
