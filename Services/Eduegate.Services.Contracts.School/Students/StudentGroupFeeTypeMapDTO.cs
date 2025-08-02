using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students 
{
    [DataContract]
    public class StudentGroupFeeTypeMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentGroupFeeTypeMapDTO()
        {
            FeeMaster = new KeyValueDTO();
            FeePeriod = new KeyValueDTO();
            FeeMasterClassMontlySplitMaps = new List<FeeMasterClassMonthlySplitDTO>();
        }

        [DataMember]
        public long StudentGroupFeeTypeMapIID { get; set; }
        
        [DataMember]
        public long StudentGroupFeeMasterID { get; set; }
       
        [DataMember]
        public int? FeeMasterID { get; set; }
        
        [DataMember]
        public int? FeePeriodID { get; set; }

        //[DataMember]
        //public decimal? Amount { get; set; }

        //[DataMember]
        //public bool? IsDiscount { get; set; }

        //[DataMember]
        //public decimal? Percentage { get; set; }
       
        [DataMember]
        public bool? IsPercentage { get; set; }
       
        [DataMember]
        public decimal? PercentageAmount { get; set; }

        [DataMember]
        public string Formula { get; set; }

        [DataMember]
        public decimal? TaxPercentage { get; set; }
       
        [DataMember]
        public decimal? TaxAmount { get; set; }

        [DataMember]
        public bool IsFeePeriodDisabled { get; set; }

        [DataMember]
        public KeyValueDTO FeeMaster { get; set; }

        [DataMember]
        public KeyValueDTO FeePeriod { get; set; }
        [DataMember]
        public virtual List<FeeMasterClassMonthlySplitDTO> FeeMasterClassMontlySplitMaps { get; set; }
    }
}
