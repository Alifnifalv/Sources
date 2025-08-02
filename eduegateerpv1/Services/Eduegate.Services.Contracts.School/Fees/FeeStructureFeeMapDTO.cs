using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeStructureFeeMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public FeeStructureFeeMapDTO()
        {
            FeeMaster = new KeyValueDTO();
            FeePeriod = new KeyValueDTO();
            FeeStructureMontlySplitMaps = new List<FeeStructureMontlySplitMapDTO>();
        }

        [DataMember]
        public long FeeStructureFeeMapIID { get; set; }
        [DataMember]
        public long FeeStructureID { get; set; }
        [DataMember]
        public int? FeeMasterID { get; set; }
        [DataMember]
        public int? FeePeriodID { get; set; }
        [DataMember]
        public byte? FeeCycleID { get; set; }
        [DataMember]
        public bool IsFeePeriodDisabled { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public KeyValueDTO FeeMaster { get; set; }
        [DataMember]
        public KeyValueDTO FeePeriod { get; set; }
        [DataMember]
        public  List<FeeStructureMontlySplitMapDTO> FeeStructureMontlySplitMaps { get; set; }
    }
}
