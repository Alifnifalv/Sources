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
    public class FeeCategoryMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FeeCategoryMapDTO()
        {
            //FeeMaster = new KeyValueDTO();
            //Categories = new KeyValueDTO();
        }

        [DataMember]
        public long CategoryFeeMapIID { get; set; }
        [DataMember]
        public long? FeeMasterID { get; set; }
        [DataMember]
        public long? CategoryID { get; set; }
        [DataMember]
        public bool? IsPrimary { get; set; }

        [DataMember]
        public string FeeMaster { get; set; }

        [DataMember]
        public string Categories { get; set; }
    }
}
