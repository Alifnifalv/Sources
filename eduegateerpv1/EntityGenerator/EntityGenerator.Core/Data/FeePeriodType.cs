using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeePeriodTypes", Schema = "schools")]
    public partial class FeePeriodType
    {
        public FeePeriodType()
        {
            FeePeriods = new HashSet<FeePeriod>();
        }

        [Key]
        public byte FeePeriodTypeID { get; set; }
        [Required]
        [StringLength(20)]
        public string TypeCode { get; set; }
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
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [InverseProperty("FeePeriodType")]
        public virtual ICollection<FeePeriod> FeePeriods { get; set; }
    }
}
