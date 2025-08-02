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
   public  class AssetTransactionHeadDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long HeadIID { get; set; }
        [DataMember]
        public Nullable<int> DocumentTypeID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> EntryDate { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public Nullable<long> DocumentStatusID { get; set; }
        [DataMember]
        public Nullable<byte> ProcessingStatusID { get; set; }
        [DataMember]
        public List<AssetTransactionDetailsDTO> AssetTransactionDetails { get; set; }

        [DataMember]
        public KeyValueDTO TransactionStatus { get; set; }
        [DataMember]
        public KeyValueDTO DocumentStatus { get; set; }
        [DataMember]
        public KeyValueDTO DocumentType { get; set; }
    }
}
