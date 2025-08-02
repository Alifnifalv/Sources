using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "UIControlTypes")]
    public enum UIControlTypes
    {
        [EnumMember]
        TextBox = 1,
        [EnumMember]
        DropDown = 2,
        [EnumMember]
        DatePicker = 3,
        [EnumMember]
        CheckBox = 4,
        [EnumMember]
        RadioButton = 5,
    }
}
