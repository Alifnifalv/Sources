using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("JobEntryHeads", Schema = "jobs")]
    public partial class JobEntryHead
    {
        public JobEntryHead()
        {
            InverseParentJobEntryHead = new HashSet<JobEntryHead>();
            JobEntryDetailJobEntryHeads = new HashSet<JobEntryDetail>();
            JobEntryDetailParentJobEntryHeads = new HashSet<JobEntryDetail>();
            JobsEntryHeadPayableMaps = new HashSet<JobsEntryHeadPayableMap>();
            JobsEntryHeadReceivableMaps = new HashSet<JobsEntryHeadReceivableMap>();
        }

        [Key]
        public long JobEntryHeadIID { get; set; }
        public long? BranchID { get; set; }
        public int? DocumentTypeID { get; set; }
        [StringLength(50)]
        public string JobNumber { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JobStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JobEndDate { get; set; }
        public int? ReferenceDocumentTypeID { get; set; }
        public long? TransactionHeadID { get; set; }
        public byte? PriorityID { get; set; }
        public int? BasketID { get; set; }
        public int? JobStatusID { get; set; }
        public byte? JobOperationStatusID { get; set; }
        public long? EmployeeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ProcessStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ProcessEndDate { get; set; }
        public long? VehicleID { get; set; }
        public bool? IsCashCollected { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public short? JobSizeID { get; set; }
        public byte? NoOfPacket { get; set; }
        public long? ParentJobEntryHeadId { get; set; }
        public int? ServiceProviderId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string AirWaybillNo { get; set; }
        public int? CompanyID { get; set; }
        public int? JobActivityID { get; set; }
        [StringLength(100)]
        public string Reason { get; set; }
        public long? OrderContactMapID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeliveredDate { get; set; }

        [ForeignKey("BasketID")]
        [InverseProperty("JobEntryHeads")]
        public virtual Basket Basket { get; set; }
        [ForeignKey("BranchID")]
        [InverseProperty("JobEntryHeads")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("DocumentTypeID")]
        [InverseProperty("JobEntryHeadDocumentTypes")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("JobEntryHeads")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("JobActivityID")]
        [InverseProperty("JobEntryHeads")]
        public virtual JobActivity JobActivity { get; set; }
        [ForeignKey("JobOperationStatusID")]
        [InverseProperty("JobEntryHeads")]
        public virtual JobOperationStatus JobOperationStatus { get; set; }
        [ForeignKey("JobSizeID")]
        [InverseProperty("JobEntryHeads")]
        public virtual JobSize JobSize { get; set; }
        [ForeignKey("JobStatusID")]
        [InverseProperty("JobEntryHeads")]
        public virtual JobStatus JobStatus { get; set; }
        [ForeignKey("OrderContactMapID")]
        [InverseProperty("JobEntryHeads")]
        public virtual OrderContactMap OrderContactMap { get; set; }
        [ForeignKey("ParentJobEntryHeadId")]
        [InverseProperty("InverseParentJobEntryHead")]
        public virtual JobEntryHead ParentJobEntryHead { get; set; }
        [ForeignKey("PriorityID")]
        [InverseProperty("JobEntryHeads")]
        public virtual Priority Priority { get; set; }
        [ForeignKey("ReferenceDocumentTypeID")]
        [InverseProperty("JobEntryHeadReferenceDocumentTypes")]
        public virtual DocumentType ReferenceDocumentType { get; set; }
        [ForeignKey("TransactionHeadID")]
        [InverseProperty("JobEntryHeads")]
        public virtual TransactionHead TransactionHead { get; set; }
        [ForeignKey("VehicleID")]
        [InverseProperty("JobEntryHeads")]
        public virtual Vehicle Vehicle { get; set; }
        [InverseProperty("ParentJobEntryHead")]
        public virtual ICollection<JobEntryHead> InverseParentJobEntryHead { get; set; }
        [InverseProperty("JobEntryHead")]
        public virtual ICollection<JobEntryDetail> JobEntryDetailJobEntryHeads { get; set; }
        [InverseProperty("ParentJobEntryHead")]
        public virtual ICollection<JobEntryDetail> JobEntryDetailParentJobEntryHeads { get; set; }
        [InverseProperty("JobEntryHead")]
        public virtual ICollection<JobsEntryHeadPayableMap> JobsEntryHeadPayableMaps { get; set; }
        [InverseProperty("JobEntryHead")]
        public virtual ICollection<JobsEntryHeadReceivableMap> JobsEntryHeadReceivableMaps { get; set; }
    }
}
