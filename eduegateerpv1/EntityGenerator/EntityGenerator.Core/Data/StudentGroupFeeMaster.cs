using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentGroupFeeMasters", Schema = "schools")]
    public partial class StudentGroupFeeMaster
    {
        public StudentGroupFeeMaster()
        {
            StudentGroupFeeTypeMaps = new HashSet<StudentGroupFeeTypeMap>();
        }

        [Key]
        public long StudentGroupFeeMasterIID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public int? StudentGroupID { get; set; }
        public int? AcadamicYearID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AcadamicYearID")]
        [InverseProperty("StudentGroupFeeMasters")]
        public virtual AcademicYear AcadamicYear { get; set; }
        [InverseProperty("StudentGroupFeeMaster")]
        public virtual ICollection<StudentGroupFeeTypeMap> StudentGroupFeeTypeMaps { get; set; }
    }
}
