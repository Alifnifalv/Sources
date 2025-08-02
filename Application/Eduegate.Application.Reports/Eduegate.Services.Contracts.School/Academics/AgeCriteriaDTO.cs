using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class AgeCriteriaDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        [DataMember]
        public long AgeCriteriaIID { get; set; }

        [DataMember]
        public byte? CurriculumID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }
        [DataMember]
        public int? AcademicYearID { get; set; }
        [DataMember]
        public string CurriculamString { get; set; }
        [DataMember]
        public List<AgeCriteriaMapDTO> AgeCriteriaMap { get; set; }
    }
}
