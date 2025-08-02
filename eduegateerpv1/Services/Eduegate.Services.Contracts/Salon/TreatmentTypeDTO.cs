using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Salon
{
    [DataContract]
    public class TreatmentTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int TreatmentTypeID { get; set; }
        [DataMember]
        public string TreatmentName { get; set; }
        [DataMember]
        public int TreatmentGroupID { get; set; }
    }
}
