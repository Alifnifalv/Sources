using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums.Synchronizer
{
    [DataContract(Name = "FieldMapTypes")]
    public enum FieldMapTypes
    {
        [Description("Mapping from DTO to Solr")]
        [EnumMember]
        DTOToSolr = 1,
        [Description("Mapping from DTO to db entity")]
        [EnumMember]
        DTOToDB = 2
    }
}
