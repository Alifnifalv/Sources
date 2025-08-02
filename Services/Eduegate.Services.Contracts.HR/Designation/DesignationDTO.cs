using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Designation
{
   public class DesignationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int DesignationID { get; set; }
        [DataMember]
        public string DesignationName { get; set; }
        [DataMember]
        public bool? IsTransportNotification { get; set; }

    }
}
