using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CampusTransfers", Schema = "schools")]
    public partial class CampusTransfer
    {
        public CampusTransfer()
        {
            CampusTransferFeeTypeMaps = new HashSet<CampusTransferFeeTypeMap>();
        }

        [Key]
        public long CampusTransferIID { get; set; }
        public string Remarks { get; set; }
        public int ToAcademicYearID { get; set; }
        public int FromAcademicYearID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransferDate { get; set; }
        public long StudentID { get; set; }
        public int ToClassID { get; set; }
        public int ToSectionID { get; set; }
        public int FromClassID { get; set; }
        public int FromSectionID { get; set; }
        public byte? FromSchoolID { get; set; }
        public byte? ToSchoolID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? IsCancelled { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CancelledDate { get; set; }
        [StringLength(250)]
        public string CancelReason { get; set; }

        [ForeignKey("FromAcademicYearID")]
        [InverseProperty("CampusTransferFromAcademicYears")]
        public virtual AcademicYear FromAcademicYear { get; set; }
        [ForeignKey("FromClassID")]
        [InverseProperty("CampusTransferFromClasses")]
        public virtual Class FromClass { get; set; }
        [ForeignKey("FromSchoolID")]
        [InverseProperty("CampusTransferFromSchools")]
        public virtual School FromSchool { get; set; }
        [ForeignKey("FromSectionID")]
        [InverseProperty("CampusTransferFromSections")]
        public virtual Section FromSection { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("CampusTransfers")]
        public virtual Student Student { get; set; }
        [ForeignKey("ToAcademicYearID")]
        [InverseProperty("CampusTransferToAcademicYears")]
        public virtual AcademicYear ToAcademicYear { get; set; }
        [ForeignKey("ToClassID")]
        [InverseProperty("CampusTransferToClasses")]
        public virtual Class ToClass { get; set; }
        [ForeignKey("ToSchoolID")]
        [InverseProperty("CampusTransferToSchools")]
        public virtual School ToSchool { get; set; }
        [ForeignKey("ToSectionID")]
        [InverseProperty("CampusTransferToSections")]
        public virtual Section ToSection { get; set; }
        [InverseProperty("CampusTransfer")]
        public virtual ICollection<CampusTransferFeeTypeMap> CampusTransferFeeTypeMaps { get; set; }
    }
}
