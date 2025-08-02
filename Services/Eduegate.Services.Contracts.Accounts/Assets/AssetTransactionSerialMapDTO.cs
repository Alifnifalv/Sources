using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Accounts.Assets
{
    [DataContract]
    public class AssetTransactionSerialMapDTO : BaseMasterDTO
    {
        public AssetTransactionSerialMapDTO()
        {
        }

        [DataMember]
        public long AssetTransactionSerialMapIID { get; set; }

        [DataMember]
        public long? TransactionDetailID { get; set; }

        [DataMember]
        public long? AssetID { get; set; }

        [DataMember]
        [StringLength(200)]
        public string AssetSequenceCode { get; set; }

        [DataMember]
        [StringLength(200)]
        public string SerialNumber { get; set; }

        [DataMember]
        [StringLength(200)]
        public string AssetTag { get; set; }

        [DataMember]
        public decimal? CostPrice { get; set; }

        [DataMember]
        public int? ExpectedLife { get; set; }

        [DataMember]
        public decimal? DepreciationRate { get; set; }

        [DataMember]
        public decimal? ExpectedScrapValue { get; set; }

        [DataMember]
        public decimal? AccumulatedDepreciationAmount { get; set; }

        [DataMember]
        public bool? IsRequiredSerialNumber { get; set; }

        [DataMember]
        public DateTime? DateOfFirstUse { get; set; }

        [DataMember]
        public long? SupplierID { get; set; }

        [DataMember]
        public string SupplierName { get; set; }

        [DataMember]
        [StringLength(50)]
        public string BillNumber { get; set; }

        [DataMember]
        public DateTime? BillDate { get; set; }

        [DataMember]
        public long? AssetSerialMapID { get; set; }

        [DataMember]
        public string AssetSerialMapSequenceCode { get; set; }

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
        public decimal? PreviousAcculatedDepreciationAmount { get; set; }

        [DataMember]
        public decimal? NetValue { get; set; }

        [DataMember]
        public decimal? DisposibleAmount { get; set; }

        [DataMember]
        public decimal? DifferenceAmount { get; set; }

        [DataMember]
        public decimal? Quantity { get; set; }

        [DataMember]
        public DateTime? LastDepreciationDate { get; set; }
    }
}