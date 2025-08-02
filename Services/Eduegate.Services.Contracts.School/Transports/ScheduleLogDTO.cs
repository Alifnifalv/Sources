using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class ScheduleLogDTO : BaseMasterDTO
    {
        public ScheduleLogDTO()
        {
            IIDs = new List<KeyValueDTO>();
        }

        [DataMember]
        public List<KeyValueDTO> IIDs { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public bool IsRowUpdation { get; set; }
    }
}