using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class JobEntryHead
    {
        public JobEntryHead()
        {
            this.JobEntryDetails = new List<JobEntryDetail>();
            this.JobEntryDetails1 = new List<JobEntryDetail>();
            this.JobEntryHeads1 = new List<JobEntryHead>();
            this.JobsEntryHeadPayableMaps = new List<JobsEntryHeadPayableMap>();
            this.JobsEntryHeadReceivableMaps = new List<JobsEntryHeadReceivableMap>();
        }

        public long JobEntryHeadIID { get; set; }
        public Nullable<long> BranchID { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public string JobNumber { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> JobStartDate { get; set; }
        public Nullable<System.DateTime> JobEndDate { get; set; }
        public Nullable<int> ReferenceDocumentTypeID { get; set; }
        public Nullable<long> TransactionHeadID { get; set; }
        public Nullable<byte> PriorityID { get; set; }
        public Nullable<int> BasketID { get; set; }
        public Nullable<int> JobStatusID { get; set; }
        public Nullable<byte> JobOperationStatusID { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public Nullable<System.DateTime> ProcessStartDate { get; set; }
        public Nullable<System.DateTime> ProcessEndDate { get; set; }
        public Nullable<long> VehicleID { get; set; }
        public Nullable<bool> IsCashCollected { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<short> JobSizeID { get; set; }
        public Nullable<byte> NoOfPacket { get; set; }
        public Nullable<long> ParentJobEntryHeadId { get; set; }
        public Nullable<int> ServiceProviderId { get; set; }
        public string AirWaybillNo { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> JobActivityID { get; set; }
        public string Reason { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
        public virtual Basket Basket { get; set; }
        public virtual JobActivity JobActivity { get; set; }
        public virtual ICollection<JobEntryDetail> JobEntryDetails { get; set; }
        public virtual ICollection<JobEntryDetail> JobEntryDetails1 { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual DocumentType DocumentType1 { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads1 { get; set; }
        public virtual JobEntryHead JobEntryHead1 { get; set; }
        public virtual JobOperationStatus JobOperationStatus { get; set; }
        public virtual JobSize JobSize { get; set; }
        public virtual JobStatus JobStatus { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<JobsEntryHeadPayableMap> JobsEntryHeadPayableMaps { get; set; }
        public virtual ICollection<JobsEntryHeadReceivableMap> JobsEntryHeadReceivableMaps { get; set; }
    }
}
