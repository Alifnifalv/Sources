using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Services.Contracts.AI
{
    [DataContract]
    public partial class RuleSetDTO
    {
        public RuleSetDTO()
        {
            Rules = new List<KeyValueDTO>();
            BankAccounts=new List<KeyValueDTO>();
        }
        [DataMember]
        public string RuleSetName { get; set; }
        [DataMember]
        public List<KeyValueDTO> Rules { get; set; }
        [DataMember]
        public List<KeyValueDTO> BankAccounts { get; set; }
    }
}
