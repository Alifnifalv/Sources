using Eduegate.Domain.Entity.Accounts.Models.Mutual;
using Eduegate.Domain.Entity.Accounts.Models.Payrolls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Jobs
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

        public DateTime? JobStartDate { get; set; }

        public DateTime? JobEndDate { get; set; }

        public int? ReferenceDocumentTypeID { get; set; }

        public long? TransactionHeadID { get; set; }

        public byte? PriorityID { get; set; }

        public int? BasketID { get; set; }

        public int? JobStatusID { get; set; }

        public byte? JobOperationStatusID { get; set; }

        public long? EmployeeID { get; set; }

        public DateTime? ProcessStartDate { get; set; }

        public DateTime? ProcessEndDate { get; set; }

        public long? VehicleID { get; set; }

        public bool? IsCashCollected { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        //public byte[] TimeStamps { get; set; }

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

        public DateTime? DeliveredDate { get; set; }

        public virtual Basket Basket { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual JobActivity JobActivity { get; set; }

        public virtual JobOperationStatus JobOperationStatus { get; set; }

        public virtual JobSize JobSize { get; set; }

        public virtual JobStatus JobStatus { get; set; }

        public virtual JobEntryHead ParentJobEntryHead { get; set; }

        public virtual Priority Priority { get; set; }

        public virtual DocumentType ReferenceDocumentType { get; set; }

        public virtual ICollection<JobEntryHead> InverseParentJobEntryHead { get; set; }

        public virtual ICollection<JobEntryDetail> JobEntryDetailJobEntryHeads { get; set; }

        public virtual ICollection<JobEntryDetail> JobEntryDetailParentJobEntryHeads { get; set; }

        public virtual ICollection<JobsEntryHeadPayableMap> JobsEntryHeadPayableMaps { get; set; }

        public virtual ICollection<JobsEntryHeadReceivableMap> JobsEntryHeadReceivableMaps { get; set; }
    }
}
