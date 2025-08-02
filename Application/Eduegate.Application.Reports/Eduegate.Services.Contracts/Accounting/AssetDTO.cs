using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums.Accounting;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]
   public  class AssetDTO: Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long AssetIID { get; set; }
        [DataMember]
        public Nullable<long> AssetCategoryID { get; set; }
        [DataMember]
        public string AssetCode { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Nullable<long> AssetGlAccID { get; set; }
        [DataMember]
        public Nullable<long> AccumulatedDepGLAccID { get; set; }
        [DataMember]
        public Nullable<long> DepreciationExpGLAccId { get; set; }
        [DataMember]
        public Nullable<int> DepreciationYears { get; set; }

        [DataMember]
        public KeyValueDTO AssetCategory { get; set; }
        [DataMember]
        public KeyValueDTO AssetGlAccount { get; set; }
        [DataMember]
        public KeyValueDTO AccumulatedDepGLAccount { get; set; }
        [DataMember]
        public KeyValueDTO DepreciationExpGLAccount { get; set; }

        [DataMember]
        public Nullable<decimal> AccumulatedDepreciation { get; set; }

        [DataMember]
        public Nullable<DateTime> StartDate { get; set; }

        [DataMember]
        public Nullable<Decimal> AssetValue { get; set; }
        [DataMember]
        public Nullable<int> Quantity { get; set; }

    }
}
