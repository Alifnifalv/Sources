using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Accounts.Assets
{
    [DataContract]
    public class AssetSerialMapDTO : BaseMasterDTO
    {
        public AssetSerialMapDTO()
        {
        }

        [DataMember]
        public long AssetSerialMapIID { get; set; }

        [DataMember]
        public long? AssetID { get; set; }

        [DataMember]
        public long? AssetInventoryID { get; set; }

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
        public bool? IsActive { get; set; }

        [DataMember]
        public DateTime? DateOfFirstUse { get; set; }

        [DataMember]
        public long? SupplierID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string BillNumber { get; set; }

        [DataMember]
        public DateTime? BillDate { get; set; }

        [DataMember]
        public decimal? LastDepreciationValue { get; set; }

        [DataMember]
        public decimal? LastDepreciationBooked { get; set; }

        [DataMember]
        public decimal? LastNetValue { get; set; }

        [DataMember]
        public DateTime? LastFromDate { get; set; }

        [DataMember]
        public DateTime? LastToDate { get; set; }

        [DataMember]
        public decimal? PreviousEntriesDepAbovePeriodTotal { get; set; }

        [DataMember]
        public string BillDateString { get; set; }

        [DataMember]
        public string FirstUseDateString { get; set; }

        [DataMember]
        public string LastFromDateString { get; set; }

        [DataMember]
        public string LastToDateString { get; set; }

        [DataMember]
        public long? InventoryBranchID { get; set; }

        [DataMember]
        public string InventoryBranchName { get; set; }

        [DataMember]
        public bool? IsDisposed { get; set; }
    }
}