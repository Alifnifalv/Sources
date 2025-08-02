using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class OrderDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    { 
        public OrderDetailDTO() 
        {
            orderDetails = new List<OrderContactMapDTO>();
        }

        [DataMember]
        public long HeadIID { get; set; } 
        [DataMember]
        public Nullable<int> DeliveryTypeID { get; set; } 
        [DataMember]
        public Nullable<DateTime> DeliveryDate { get; set; }
        [DataMember]
        public string DeliveryType { get; set; }
        [DataMember]
        public Nullable<long> BranchID { get; set; }

        [DataMember]
        public Nullable<long> ToBranchID { get; set; }

        [DataMember]
        public Nullable<int> DocumentTypeID { get; set; }

        [DataMember]
        public string TransactionNo { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Nullable<long> CustomerID { get; set; }

        [DataMember]
        public Nullable<long> SupplierID { get; set; }

        [DataMember]
        public Nullable<long> StudentID { get; set; }

        [DataMember]
        public Nullable<DateTime> TransactionDate { get; set; }

        [DataMember]
        public Nullable<byte> TransactionStatusID { get; set; }

        [DataMember]
        public Nullable<int> JobStatusID { get; set; }

        [DataMember]
        public Nullable<decimal> DiscountAmount { get; set; }

        [DataMember]
        public Nullable<decimal> DiscountPercentage { get; set; }

        [DataMember]
        public Nullable<decimal> ExchangeRate { get; set; }

        [DataMember]
        public Nullable<DateTime> DueDate { get; set; }

        [DataMember]
        public Nullable<int> CurrencyID { get; set; }

        [DataMember]
        public bool IsShipment { get; set; }

        [DataMember]
        public long EmployeeID { get; set; }

        [DataMember]
        public Nullable<byte> EntitlementID { get; set; }

        [DataMember]
        public Nullable<long> ReferenceHeadID { get; set; }

        [DataMember]
        public Nullable<int> CompanyID { get; set; }

        [DataMember]
        public Nullable<long> JobEntryHeadID { get; set; }

        [DataMember]
        public Nullable<decimal> DeliveryCharge { get; set; }

        [DataMember]
        public Nullable<short> DeliveryMethodID { get; set; }

        [DataMember]
        public Nullable<int> DeliveryDays { get; set; }

        [DataMember]
        public Nullable<long> DocumentStatusID { get; set; }

        [DataMember]
        public Nullable<int> TransactionRole { get; set; } 

        [DataMember]
        public List<OrderContactMapDTO> orderDetails { get; set; }

    }
}
