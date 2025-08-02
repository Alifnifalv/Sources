using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts
{

    [DataContract]
    public class VoucherMasterDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long VoucherID { get; set; }
        [DataMember]
        public string VoucherNo { get; set; }
        [DataMember]
        public string VoucherPin { get; set; }
        [DataMember]
        public string VoucherType { get; set; }
        [DataMember]
        public bool IsSharable { get; set; }

        [DataMember]
        public KeyValueDTO Customer { get; set; }
        [DataMember]
        public Nullable<long> CustomerID { get; set; }
        [DataMember]
        public int VoucherPoint { get; set; }
        [DataMember]
        public decimal VoucherAmount { get; set; }
        [DataMember]
        public decimal CurrentBalance { get; set; }
        [DataMember]
        public int Validity { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ValidTillDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> GenerateDate { get; set; }
        [DataMember]
        public long RefOrderID { get; set; }
        [DataMember]
        public bool IsRedeemed { get; set; }
        [DataMember]
        public Nullable<long> RefOrderItemID { get; set; }
        [DataMember]
        public Nullable<byte> VoucherDiscount { get; set; }
        [DataMember]
        public Nullable<bool> isRefund { get; set; }
        [DataMember]
        public Nullable<int> isRefundForOrder { get; set; }
        [DataMember]
        public Nullable<decimal> MinAmount { get; set; }
        [DataMember]
        public Nullable<int> StatusID { get; set; }
         [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Nullable<int> CompanyID {get;set;}
        [DataMember]
        public VoucherTypes VoucherTypeID { get; set; }
        [DataMember]
        public string ErrorCode { get; set; }
        [DataMember]
        public bool IsError { get; set; }
    }
}