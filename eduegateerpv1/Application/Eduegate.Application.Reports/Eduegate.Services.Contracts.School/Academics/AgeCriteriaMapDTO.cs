using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class AgeCriteriaMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AgeCriteriaMapDTO()
        {
            Class = new KeyValueDTO();
        }

        [DataMember]
        public long AgeCriteriaIID { get; set; }
        [DataMember]
        public int? ClassID { get; set; }
        [DataMember]
        public decimal? MinAge { get; set; }
        [DataMember]
        public decimal? MaxAge { get; set; }

        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public DateTime? BirthFrom { get; set; }
        [DataMember]
        public string BirthFromString { get; set; }
        [DataMember]
        public DateTime? BirthTo { get; set; }
        [DataMember]
        public string BirthToString { get; set; }
        [DataMember]
        public byte? SchoolID { get; set; }
        [DataMember]
        public int? AcademicYearID { get; set; }
        [DataMember]
        public bool? IsActive { get; set; }
        [DataMember]
        public KeyValueDTO Class { get; set; }
    }
}
