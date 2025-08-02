using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentFeeDues", Schema = "schools")]
    [Index("StudentId", "CollectionStatus", Name = "IDX_STUDENTFEEDUES_STUDENTID_COLLECTIONSTATUS")]
    [Index("ClassId", "InvoiceDate", "AcadamicYearID", "SectionID", Name = "IDX_StudentFeeDues_ClassId__InvoiceDate__AcadamicYearID__SectionID_CreatedDate__UpdatedDate__Update")]
    [Index("CollectionStatus", Name = "IDX_StudentFeeDues_CollectionStatus")]
    [Index("CollectionStatus", "InvoiceDate", Name = "IDX_StudentFeeDues_CollectionStatus_InvoiceDate")]
    [Index("InvoiceDate", Name = "IDX_StudentFeeDues_InvoiceDate")]
    [Index("IsCancelled", "CollectionStatus", Name = "IDX_StudentFeeDues_IsCancelledCollectionStatus_AcadamicYearID__SchoolID")]
    [Index("SchoolID", Name = "IDX_StudentFeeDues_SchoolID_ClassId__CreatedDate__UpdatedDate__UpdatedBy__CreatedBy__StudentId__Inv")]
    [Index("SchoolID", Name = "IDX_StudentFeeDues_SchoolID_ClassId__UpdatedBy__CreatedBy__StudentId")]
    [Index("CollectionStatus", Name = "schools_StudentFeeDues_CollectionStatus")]
    public partial class StudentFeeDue
    {
        public StudentFeeDue()
        {
            FeeDueCancellations = new HashSet<FeeDueCancellation>();
            FeeDueFeeTypeMaps = new HashSet<FeeDueFeeTypeMap>();
            FeeDueInventoryMaps = new HashSet<FeeDueInventoryMap>();
            StudentFeeConcessions = new HashSet<StudentFeeConcession>();
            TicketFeeDueMaps = new HashSet<TicketFeeDueMap>();
        }

        [Key]
        public long StudentFeeDueIID { get; set; }
        public int? ClassId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? StudentId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [StringLength(50)]
        public string InvoiceNo { get; set; }
        public bool CollectionStatus { get; set; }
        public bool? IsAccountPost { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AccountPostingDate { get; set; }
        public int? AcadamicYearID { get; set; }
        public int? SectionID { get; set; }
        public byte? SchoolID { get; set; }
        public bool? IsCancelled { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CancelledDate { get; set; }
        [StringLength(250)]
        public string CancelReason { get; set; }
        public string Remarks { get; set; }

        [ForeignKey("AcadamicYearID")]
        [InverseProperty("StudentFeeDues")]
        public virtual AcademicYear AcadamicYear { get; set; }
        [ForeignKey("ClassId")]
        [InverseProperty("StudentFeeDues")]
        public virtual Class Class { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StudentFeeDues")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("StudentFeeDues")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentId")]
        [InverseProperty("StudentFeeDues")]
        public virtual Student Student { get; set; }
        [InverseProperty("StudentFeeDue")]
        public virtual ICollection<FeeDueCancellation> FeeDueCancellations { get; set; }
        [InverseProperty("StudentFeeDue")]
        public virtual ICollection<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }
        [InverseProperty("StudentFeeDue")]
        public virtual ICollection<FeeDueInventoryMap> FeeDueInventoryMaps { get; set; }
        [InverseProperty("StudentFeeDue")]
        public virtual ICollection<StudentFeeConcession> StudentFeeConcessions { get; set; }
        [InverseProperty("StudentFeeDue")]
        public virtual ICollection<TicketFeeDueMap> TicketFeeDueMaps { get; set; }
    }
}
