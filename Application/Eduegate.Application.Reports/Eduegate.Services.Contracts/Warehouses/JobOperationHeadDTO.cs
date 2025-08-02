using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums.Warehouses;

namespace Eduegate.Services.Contracts.Warehouses
{
    [DataContract]
    public class JobOperationHeadDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long JobEntryHeadIID { get; set; }
        [DataMember]
        public string BranchName { get; set; }
        [DataMember]
        public long BranchID { get; set; }
        [DataMember]
        public string DocumentTypeName { get; set; }
        [DataMember]
        public long DocumentTypeID { get; set; }
        [DataMember]
        public long LoginID { get; set; }
        [DataMember]
        public Nullable<long> EmployeeID { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public string JobNumber { get; set; }
        [DataMember]
        public JobOperationTypes OperationTypes { get; set; }
        [DataMember]
        public DateTime? JobDate { get; set; }
        [DataMember]
        public DateTime? DeliveryDate { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Nullable<long> ReferenceTransaction { get; set; }
        [DataMember]
        public int? JobPriorityID { get; set; }
        [DataMember]
        public string JobPriority { get; set; }
        [DataMember]
        public Nullable<int> JobStatusID { get; set; }
        [DataMember]
        public Nullable<byte> JobOperationStatusID { get; set; }
        [DataMember]
        public Nullable<Int16> JobSizeID { get; set; }
        [DataMember]
        public string JobOperationStatus { get; set; }
        [DataMember]
        public Nullable<int> BasketID { get; set; }
        [DataMember]
        public string BasketName { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ProcessStartDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ProcessEndDate { get; set; }
        [DataMember]
        public bool IsPicked { get; set; }
        [DataMember]
        public List<JobOperationDetailDTO> Detail { get; set; }
        [DataMember]
        public bool IsCashCollected { get; set; }
        [DataMember]
        public Nullable<decimal> LocationLatitude { get; set; }
        [DataMember]
        public Nullable<decimal> LocationLongitude { get; set; }
        [DataMember]
        public byte PacketNo { get; set; }
        [DataMember]
        public KeyValueDTO Customer { get; set; }
        [DataMember]
        public Nullable<long> ParentJobEntryHeadId { get; set; }
        [DataMember]
        public Nullable <long> TransactionHeadId { get; set; }
        [DataMember]
        public String TransactionNo { get; set; }
        [DataMember]
        public string ShoppingCartID { get; set; }
        [DataMember]
        public string CityName { get; set; }
        [DataMember]
        public string Amount { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
        [DataMember]
        public string Reason { get; set; }
    }
}
