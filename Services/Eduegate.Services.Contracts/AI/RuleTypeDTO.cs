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
    public partial class RuleTypeDTO
    {
        public RuleTypeDTO()
        {
            Rules = new List<KeyValueDTO>();
        }

        [DataMember]
        public int RuleTypeID { get; set; }
        [DataMember]
        public string RuleTypeName { get; set; }
        [DataMember]
        public List<KeyValueDTO> Rules { get; set; }
    }
}
