using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class SchoolEventsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public SchoolEventsDTO()
        {

        }

        [DataMember]
        public long SchoolEventIID { get; set; }

        [DataMember]
        public string EventName { get; set; }

        [DataMember]
        public DateTime? EventDate { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public TimeSpan? StartTime { get; set; }

        [DataMember]
        public TimeSpan? EndTime { get; set; }

        [DataMember]
        public string Destination { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

    }
}



