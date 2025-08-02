
using Eduegate.Framework.Contracts.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.LedgerAccount
{
    [DataContract]
    public class SubLedgerAccountRelationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        
        public long SL_Rln_ID { get; set; }

        [DataMember]
        public long AccountID { get; set; }

        [DataMember]
        public long SL_AccountID { get; set; }

        
    }
}
