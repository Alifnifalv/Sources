using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums.Synchronizer
{
    [DataContract(Name = "OperationTypes")]
    public enum OperationTypes
    {
        [Description("Add")]
        [EnumMember]
        Add = 1,
        [Description("Update")]
        [EnumMember]
        Update = 2,
        [Description("Delete")]
        [EnumMember]
        Delete = 3
    }
}
