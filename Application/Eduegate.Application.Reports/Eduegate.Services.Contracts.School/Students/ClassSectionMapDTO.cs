using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class ClassSectionMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ClassSectionMapDTO()
        {
            Class = new KeyValueDTO();
            Sections = new KeyValueDTO();
            Section = new List<KeyValueDTO>();
        }
        [DataMember]
        public long ClassSectionMapIID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public List<KeyValueDTO> Section { get; set; }

        [DataMember]
        public KeyValueDTO Sections { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public int? MinimumCapacity { get; set; }

        [DataMember]
        public int? MaximumCapacity { get; set; }
    }
}
