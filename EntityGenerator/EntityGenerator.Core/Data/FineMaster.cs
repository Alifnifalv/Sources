using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FineMasters", Schema = "schools")]
    public partial class FineMaster
    {
        public FineMaster()
        {
            FeeCollectionFeeTypeMaps = new HashSet<FeeCollectionFeeTypeMap>();
            FeeDueFeeTypeMaps = new HashSet<FeeDueFeeTypeMap>();
            FineMasterStudentMaps = new HashSet<FineMasterStudentMap>();
        }

        [Key]
        public int FineMasterID { get; set; }
        [Required]
        [StringLength(20)]
        public string FineCode { get; set; }
        [Required]
        [StringLength(100)]
        public string FineName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public short? FeeFineTypeID { get; set; }
        public long? LedgerAccountID { get; set; }
        [Column(TypeName = "numeric(18, 4)")]
        public decimal? Amount { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("FineMasters")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("FeeFineTypeID")]
        [InverseProperty("FineMasters")]
        public virtual FeeFineType FeeFineType { get; set; }
        [ForeignKey("LedgerAccountID")]
        [InverseProperty("FineMasters")]
        public virtual Account LedgerAccount { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("FineMasters")]
        public virtual School School { get; set; }
        [InverseProperty("FineMaster")]
        public virtual ICollection<FeeCollectionFeeTypeMap> FeeCollectionFeeTypeMaps { get; set; }
        [InverseProperty("FineMaster")]
        public virtual ICollection<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }
        [InverseProperty("FineMaster")]
        public virtual ICollection<FineMasterStudentMap> FineMasterStudentMaps { get; set; }
    }
}
