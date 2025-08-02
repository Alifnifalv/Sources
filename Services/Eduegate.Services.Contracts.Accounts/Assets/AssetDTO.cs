using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Accounts.Assets
{
    [DataContract]
    public class AssetDTO : BaseMasterDTO
    {
        public AssetDTO()
        {
            AssetGlAccount = new KeyValueDTO();
            AccumulatedDepGLAccount = new KeyValueDTO();
            DepreciationExpGLAccount = new KeyValueDTO();
            AssetProductMapDTOs = new List<AssetProductMapDTO>();
            AssetSerialMapDTOs = new List<AssetSerialMapDTO>();
        }

        [DataMember]
        public long AssetIID { get; set; }

        [DataMember]
        public long? AssetCategoryID { get; set; }

        [DataMember]
        public string AssetCategoryName { get; set; }

        [DataMember]
        public string AssetCode { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public long? AssetGlAccID { get; set; }

        [DataMember]
        public long? AccumulatedDepGLAccID { get; set; }

        [DataMember]
        public long? DepreciationExpGLAccID { get; set; }

        [DataMember]
        public int? DepreciationYears { get; set; }

        [DataMember]
        public KeyValueDTO AssetGlAccount { get; set; }

        [DataMember]
        public KeyValueDTO AccumulatedDepGLAccount { get; set; }

        [DataMember]
        public KeyValueDTO DepreciationExpGLAccount { get; set; }

        [DataMember]
        public decimal? AccumulatedDepreciation { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public decimal? AssetValue { get; set; }

        [DataMember]
        public int? AssetGroupID { get; set; }

        [DataMember]
        public long? AssetSubCategoryID { get; set; }

        [DataMember]
        public long? UnitID { get; set; }

        [DataMember]
        public decimal? Quantity { get; set; }

        [DataMember]
        public int? AssetTypeID { get; set; }

        [DataMember]
        [StringLength(100)]
        public string AssetPrefix { get; set; }

        [DataMember]
        public long? LastSequenceNumber { get; set; }

        [DataMember]
        public bool? IsRequiredSerialNumber { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        [StringLength(100)]
        public string Reference { get; set; }

        [DataMember]
        public int? DepreciationTypeID { get; set; }

        [DataMember]
        public List<AssetProductMapDTO> AssetProductMapDTOs { get; set; }

        [DataMember]
        public List<AssetSerialMapDTO> AssetSerialMapDTOs { get; set; }

        [DataMember]
        public string AssetCategoryPrefix { get; set; }

        [DataMember]
        public decimal? AssetCategoryDepreciationRate { get; set; }

    }
}