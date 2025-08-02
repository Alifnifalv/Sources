using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Eduegate.Framework.Contracts.Common;
namespace Eduegate.Services.Contracts.AI
{
    [DataContract]
    public partial class PatternTypeDTO
    {

        public PatternTypeDTO()
        {
            Rules = new List<KeyValueDTO> ();
        }
        [DataMember]
        public int PatternTypeID { get; set; }
        [DataMember]
        public string PatternTypeName { get; set; }
        [DataMember]
        public List<KeyValueDTO> Rules { get; set; }
    }
}
