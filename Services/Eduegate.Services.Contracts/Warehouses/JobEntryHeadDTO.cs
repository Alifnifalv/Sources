using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Warehouses
{
    [DataContract]
    public class JobEntryHeadDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long JobEntryHeadIID { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public Nullable<long> BranchID { get; set; }

        [DataMember]
        public Nullable<int> DocumentTypeID { get; set; }

        [DataMember]
        public string DocumentTypeName { get; set; }

        [DataMember]
        public string JobNumber { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Nullable<System.DateTime> JobStartDate { get; set; }

        [DataMember]
        public Nullable<System.DateTime> JobEndDate { get; set; }

        [DataMember]
        public Nullable<int> ReferenceDocumentTypeID { get; set; }

        [DataMember]
        public Nullable<long> TransactionHeadID { get; set; }

        [DataMember]
        public string TransactionNumber { get; set; }

        [DataMember]
        public Nullable<byte> PriorityID { get; set; }

        [DataMember]
        public string Priority { get; set; }

        [DataMember]
        public Nullable<int> BasketID { get; set; }

        [DataMember]
        public string BasketName { get; set; }

        [DataMember]
        public Nullable<int> JobStatusID { get; set; }

        [DataMember]
        public string JobStatus { get; set; }

        [DataMember]
        public Nullable<byte> JobOperationStatusID { get; set; }

        [DataMember]
        public string JobOperationStatus { get; set; }

        [DataMember]
        public Nullable<Int16> JobSizeID { get; set; }

        [DataMember]
        public Nullable<long> EmployeeID { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ProcessStartDate { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ProcessEndDate { get; set; }

        [DataMember]
        public List<JobEntryDetailDTO> JobEntryDetails { get; set; }

        [DataMember]
        public Nullable<long> VehicleID { get; set; }

        [DataMember]
        public string VehicleCode { get; set; }

        [DataMember]
        public bool IsCashCollected { get; set; }

        [DataMember]
        public long LoginID { get; set; }

        [DataMember]
        public Byte PacketNo { get; set; }

        [DataMember]
        public Nullable<int> ServiceProviderId { get; set; }

        [DataMember]
        public string ServiceProviderName { get; set; }
        [DataMember]
        public Nullable<long> ParentJobEntryHeadId { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }

        [DataMember]
        public Nullable<int> JobActivityID { get; set; }

        [DataMember]
        public string JobActivity { get; set; }

        [DataMember]
        public string Reason { get; set; }

        [DataMember]
        public OrderContactMapDTO OrderContactMap { get; set; }
    } 
}
