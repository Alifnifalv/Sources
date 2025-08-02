using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class SelectDays : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        [DataMember]

        public byte DayID { get; set; }

        [DataMember]
        public bool Value { get; set; }
    }
}
