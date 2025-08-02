using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.LedgerAccount
{
    [DataContract]
    public class SubLedgerAccountDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SubLedgerAccountDTO()
        {
            Accounts = new List<KeyValueDTO>();
            
        }
        [DataMember]
        public long SL_AccountID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string SL_AccountCode { get; set; }

        [DataMember]
        [StringLength(100)]
        public string SL_AccountName { get; set; }

        [DataMember]
        [StringLength(100)]
        public string SL_Alias { get; set; }

        [DataMember]
        public int? CreatedBy { get; set; }

        [DataMember]
        public DateTime? CreatedDate { get; set; }

        [DataMember]
        public int? UpdatedBy { get; set; }

        [DataMember]
        public DateTime? UpdatedDate { get; set; }

        [DataMember]
        public bool? IsHidden { get; set; }

        [DataMember]
        public bool? AllowUserDelete { get; set; }

        [DataMember]
        public bool? AllowUserEdit { get; set; }

        [DataMember]
        public bool? AllowUserRename { get; set; }

        [DataMember]
        public List<KeyValueDTO> Accounts { get; set; }
    }
}
