using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeTypes", Schema = "schools")]
    public partial class FeeType
    {
        public FeeType()
        {
            FeeFineTypes = new HashSet<FeeFineType>();
            FeeMasters = new HashSet<FeeMaster>();
        }

        [Key]
        public int FeeTypeID { get; set; }
        [Required]
        [StringLength(20)]
        public string FeeCode { get; set; }
        [Required]
        [StringLength(100)]
        public string TypeName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? FeeGroupId { get; set; }
        public byte? FeeCycleId { get; set; }
        public bool? IsRefundable { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("FeeTypes")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("FeeCycleId")]
        [InverseProperty("FeeTypes")]
        public virtual FeeCycle FeeCycle { get; set; }
        [ForeignKey("FeeGroupId")]
        [InverseProperty("FeeTypes")]
        public virtual FeeGroup FeeGroup { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("FeeTypes")]
        public virtual School School { get; set; }
        [InverseProperty("FeeType")]
        public virtual ICollection<FeeFineType> FeeFineTypes { get; set; }
        [InverseProperty("FeeType")]
        public virtual ICollection<FeeMaster> FeeMasters { get; set; }
    }
}
