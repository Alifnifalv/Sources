using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Assets
{
    [DataContract]
    public class AssetInventoryDTO : BaseMasterDTO
    {
        public AssetInventoryDTO()
        {

        }

        [DataMember]
        public long AssetInventoryIID { get; set; }

        [DataMember]
        public long? AssetID { get; set; }

        [DataMember]
        public long? BranchID { get; set; }

        [DataMember]
        public long? ToBranchID { get; set; }

        [DataMember]
        public long? Batch { get; set; }

        [DataMember]
        public long? HeadID { get; set; }

        [DataMember]
        public int? CompanyID { get; set; }

        [DataMember]
        public decimal? CostAmount { get; set; }

        [DataMember]
        public decimal? Quantity { get; set; }

        [DataMember]
        public decimal? OriginalQty { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public DocumentReferenceTypes DocumentReferenceType { get; set; }

        [DataMember]
        public long TransactioHeadID { get; set; }

        [DataMember]
        public long TransactioDetailID { get; set; }

        [DataMember]
        public decimal? AvailableQuantity { get; set; }

        [DataMember]
        public List<long> AssetSerialMapIDs { get; set; }

        [DataMember]
        public DateTime? CutOffDate { get; set; }

        [DataMember]
        public bool? IsSerialMapDisposed { get; set; }

        [DataMember]
        public List<AssetSerialMapDTO> AssetSerialMapDTOs { get; set; }
    }
}