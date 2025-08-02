using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("CampusTransfers", Schema = "schools")]
    public partial class CampusTransfers
    {
        public CampusTransfers()
        {
            CampusTransferFeeTypeMaps = new HashSet<CampusTransferFeeTypeMap>();
        }

        [Key]
        public long CampusTransferIID { get; set; }
        public string Remarks { get; set; }
        public int ToAcademicYearID { get; set; }
        public int FromAcademicYearID { get; set; }
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
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsCancelled { get; set; }
        public DateTime? CancelledDate { get; set; }
        public string CancelReason { get; set; }
        public virtual AcademicYear FromAcademicYear { get; set; }
        public virtual Class FromClass { get; set; }
        public virtual Schools FromSchool { get; set; }
        public virtual Schools ToSchool { get; set; }
        public virtual Section FromSection { get; set; }
        public virtual Student Student { get; set; }
        public virtual AcademicYear ToAcademicYear { get; set; }
        public virtual Class ToClass { get; set; }
        public virtual Section ToSection { get; set; }
        public virtual ICollection<CampusTransferFeeTypeMap> CampusTransferFeeTypeMaps { get; set; }

    }
}