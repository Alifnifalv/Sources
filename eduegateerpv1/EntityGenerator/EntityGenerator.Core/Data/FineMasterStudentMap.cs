using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FineMasterStudentMaps", Schema = "schools")]
    public partial class FineMasterStudentMap
    {
        public FineMasterStudentMap()
        {
            FeeCollectionFeeTypeMaps = new HashSet<FeeCollectionFeeTypeMap>();
            FeeDueFeeTypeMaps = new HashSet<FeeDueFeeTypeMap>();
        }

        [Key]
        public long FineMasterStudentMapIID { get; set; }
        public int? FineMasterID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool IsCollected { get; set; }
        public long? StudentId { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FineMapDate { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("FineMasterStudentMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("FineMasterID")]
        [InverseProperty("FineMasterStudentMaps")]
        public virtual FineMaster FineMaster { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("FineMasterStudentMaps")]
        public virtual School School { get; set; }
        [ForeignKey("StudentId")]
        [InverseProperty("FineMasterStudentMaps")]
        public virtual Student Student { get; set; }
        [InverseProperty("FineMasterStudentMap")]
        public virtual ICollection<FeeCollectionFeeTypeMap> FeeCollectionFeeTypeMaps { get; set; }
        [InverseProperty("FineMasterStudentMap")]
        public virtual ICollection<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }
    }
}
