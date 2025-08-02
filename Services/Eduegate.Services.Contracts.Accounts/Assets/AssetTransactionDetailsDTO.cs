using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Accounts.Assets
{
    [DataContract]
    public class AssetTransactionDetailsDTO : BaseMasterDTO
    {
        public AssetTransactionDetailsDTO()
        {
            AssetTransactionSerialMaps = new List<AssetTransactionSerialMapDTO>();
        }

        [DataMember]
        public long DetailIID { get; set; }

        [DataMember]
        public long? HeadID { get; set; }

        [DataMember]
        public long? AssetID { get; set; }

        [DataMember]
        public string AssetName { get; set; }

        [DataMember]
        public string AssetCode { get; set; }

        [DataMember]
        public long? ProductSKUMapID { get; set; }

        [DataMember]
        public string SKU { get; set; }

        [DataMember]
        public decimal? Quantity { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public long? AccountID { get; set; }

        [DataMember]
        public int? Createdby { get; set; }

        [DataMember]
        public AccountDTO Account { get; set; }

        [DataMember]
        public AssetDTO Asset { get; set; }

        [DataMember]
        public KeyValueDTO AssetCodeKeyValue { get; set; }

        [DataMember]
        public KeyValueDTO AssetGlAccount { get; set; }

        [DataMember]
        public long? AssetGlAccID { get; set; }

        [DataMember]
        public long? AccumulatedDepGLAccID { get; set; }

        [DataMember]
        public long? DepreciationExpGLAccID { get; set; }

        [DataMember]
        public decimal? CostAmount { get; set; }

        [DataMember]
        public int? AccountingPeriodDays { get; set; }

        [DataMember]
        public DateTime? DepAccumulatedTillDate { get; set; }

        [DataMember]
        public DateTime? DepFromDate { get; set; }

        [DataMember]
        public DateTime? DepToDate { get; set; }

        [DataMember]
        public decimal? DepAbovePeriod { get; set; }

        [DataMember]
        public decimal? BookedDepreciation { get; set; }

        [DataMember]
        public decimal? AccumulatedDepreciationAmount { get; set; }

        [DataMember]
        public bool IsError { get; set; }

        [DataMember]
        public List<AssetTransactionSerialMapDTO> AssetTransactionSerialMaps { get; set; }

        [DataMember]
        public bool? IsRequiredSerialNumber { get; set; }

        [DataMember]
        public DateTime? CutOffDate { get; set; }

        [DataMember]
        public DocumentReferenceTypes? DocumentReferenceTypeID { get; set; }

        [DataMember]
        public decimal? NetValue { get; set; }

        [DataMember]
        public string AssetSerialMapSequenceCode { get; set; }

        [DataMember]
        public long? AssetCategoryID { get; set; }

        [DataMember]
        public string AssetCategoryName { get; set; }

        [DataMember]
        public long? AssetCategoryTotalAssetCount { get; set; }

        [DataMember]
        public decimal? PreviousAcculatedDepreciationAmount { get; set; }

        [DataMember]
        public decimal? AvailableAssetQuantity { get; set; }

        [DataMember]
        public long? BatchID { get; set; }
    }
}