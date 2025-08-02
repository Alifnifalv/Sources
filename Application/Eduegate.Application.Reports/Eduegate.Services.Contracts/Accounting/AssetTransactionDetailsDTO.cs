using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums.Accounting;

namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]
   public  class AssetTransactionDetailsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long DetailIID { get; set; }
        [DataMember]
        public Nullable<long> HeadID { get; set; }
        [DataMember]
        public Nullable<long> AssetID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> StartDate { get; set; }
        [DataMember]
        public Nullable<int> Quantity { get; set; }
        [DataMember]
        public Nullable<decimal> Amount { get; set; }
        [DataMember]
        public Nullable<long> AccountID { get; set; }
        [DataMember]
        public Nullable<int> Createdby { get; set; }
        [DataMember]
        public  AccountDTO Account { get; set; }
        [DataMember]
        public  AssetDTO Asset { get; set; }
        [DataMember]
        public KeyValueDTO AssetCode { get; set; }
        [DataMember]
        public KeyValueDTO AssetGlAccount { get; set; }
        [DataMember]
        public Nullable<long> AssetGlAccID { get; set; }

        //[DataMember]
        //public KeyValueDTO AccumulatedDepGLAccount { get; set; }
        //[DataMember]
        //public Nullable<long> AccumulatedDepGLAccID { get; set; }

        //[DataMember]
        //public KeyValueDTO DepreciationExpGLAccount { get; set; }
        //[DataMember]
        //public Nullable<long> DepreciationExpGLAccId { get; set; }








    }
}
