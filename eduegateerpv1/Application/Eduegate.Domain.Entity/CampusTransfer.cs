namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.CampusTransfers")]
    public partial class CampusTransfer
    {
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

        [StringLength(250)]
        public string CancelReason { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual AcademicYear AcademicYear1 { get; set; }

        public virtual Class Class { get; set; }

        public virtual School School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Student Student { get; set; }

        public virtual Class Class1 { get; set; }

        public virtual School School1 { get; set; }

        public virtual Section Section1 { get; set; }
    }
}
