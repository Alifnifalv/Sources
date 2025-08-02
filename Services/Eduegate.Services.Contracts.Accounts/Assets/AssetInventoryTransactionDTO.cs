using Eduegate.Framework.Contracts.Common;
using System.ComponentModel.DataAnnotations;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Assets
{
    [DataContract]
    public class AssetInventoryTransactionDTO : BaseMasterDTO
    {
        [DataMember]
        public long AssetInvetoryTransactionIID { get; set; }

        [DataMember]
        public int? SerialNo { get; set; }

        [DataMember]
        public int? DocumentTypeID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string TransactionNo { get; set; }

        [DataMember]
        public DateTime? TransactionDate { get; set; }

        [DataMember]
        public long? AssetID { get; set; }

        [DataMember]
        public long? AssetSerialMapID { get; set; }

        [DataMember]
        public long? BatchID { get; set; }

        [DataMember]
        public long? AccountID { get; set; }

        [DataMember]
        public decimal? Quantity { get; set; }

        [DataMember]
        public decimal? OriginalQty { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public long? BranchID { get; set; }

        [DataMember]
        public int? CompanyID { get; set; }

        [DataMember]
        public long? AssetTransactionHeadID { get; set; }

        [DataMember]
        public string TransactionDateString { get; set; }
    }
}