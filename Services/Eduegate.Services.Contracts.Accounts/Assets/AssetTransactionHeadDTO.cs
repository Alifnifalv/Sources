using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Accounts.Assets
{
    [DataContract]
    public class AssetTransactionHeadDTO : BaseMasterDTO
    {
        public AssetTransactionHeadDTO()
        {
        }

        [DataMember]
        public long HeadIID { get; set; }

        [DataMember]
        public int? DocumentTypeID { get; set; }

        [DataMember]
        public string DocumentTypeName { get; set; }

        [DataMember]
        public DateTime? EntryDate { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public long? DocumentStatusID { get; set; }

        [DataMember]
        public string DocumentStatusName { get; set; }

        [DataMember]
        public byte? ProcessingStatusID { get; set; }

        [DataMember]
        public string ProcessingStatusName { get; set; }

        //[DataMember]
        //public KeyValueDTO TransactionStatus { get; set; }

        //[DataMember]
        //public KeyValueDTO DocumentStatus { get; set; }

        //[DataMember]
        //public KeyValueDTO DocumentType { get; set; }

        [DataMember]
        public int? CompanyID { get; set; }

        [DataMember]
        public long? BranchID { get; set; }

        [DataMember]
        public string BranchName { get; set; }

        [DataMember]
        public long? ToBranchID { get; set; }

        [DataMember]
        public string ToBranchName { get; set; }

        [DataMember]
        [StringLength(50)]
        public string TransactionNo { get; set; }

        [DataMember]
        public long? ReferenceHeadID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public long? SupplierID { get; set; }

        [DataMember]
        public string SupplierName { get; set; }

        [DataMember]
        [StringLength(50)]
        public string Reference { get; set; }

        [DataMember]
        public long? DepartmentID { get; set; }

        [DataMember]
        [StringLength(100)]
        public string AssetLocation { get; set; }

        [DataMember]
        [StringLength(100)]
        public string SubLocation { get; set; }

        [DataMember]
        [StringLength(10)]
        public string AssetFloor { get; set; }

        [DataMember]
        [StringLength(10)]
        public string RoomNumber { get; set; }

        [DataMember]
        [StringLength(100)]
        public string UserName { get; set; }

        [DataMember]
        public long? AssetID { get; set; }

        [DataMember]
        public string AssetName { get; set; }

        [DataMember]
        public string AssetCode { get; set; }

        [DataMember]
        public DocumentReferenceTypes? DocumentReferenceTypeID { get; set; }

        [DataMember]
        public string DocumentReferenceType { get; set; }

        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public bool IsError { get; set; }

        [DataMember]
        public bool IsTransactionCompleted { get; set; }

        [DataMember]
        public string ReferenceTransactionNo { get; set; }

        [DataMember]
        public long? AgainstReferenceHeadID { get; set; }

        [DataMember]
        public List<AssetTransactionDetailsDTO> AssetTransactionDetails { get; set; }

        [DataMember]
        public bool? IsRequiredSerialNumber { get; set; }

        [DataMember]
        public long? OldTransactionHeadID { get; set; }

        [DataMember]
        public decimal? TotalNetAmount { get; set; }

        [DataMember]
        public decimal? TotalAssetQuantity { get; set; }

        [DataMember]
        public long? AssetInventoryID { get; set; }

        [DataMember]
        public long? AssetCategoryID { get; set; }

        [DataMember]
        public string AssetCategoryName { get; set; }

        [DataMember]
        public DateTime? DepreciationStartDate { get; set; }

        [DataMember]
        public DateTime? DepreciationEndDate { get; set; }

        [DataMember]
        public string AssetPrefix { get; set; }

        [DataMember]
        public long? AssetLastSequenceNumber { get; set; }

        [DataMember]
        public string AssetCategoryPrefix { get; set; }

        [DataMember]
        public decimal? AssetCategoryDepreciationRate { get; set; }
    }
}