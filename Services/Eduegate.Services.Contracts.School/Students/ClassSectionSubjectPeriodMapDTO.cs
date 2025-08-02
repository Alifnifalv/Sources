using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class ClassSectionSubjectPeriodMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ClassSectionSubjectPeriodMapDTO()
        {
            //ClassTeacherMaps = new List<ClassTeacherMapDTO>();

            Subject = new List<KeyValueDTO>();
            SubjectMapDetails = new List<ClassSectionSubjectPeriodMapMapDTO>();
        }
        [DataMember]
        public long PeriodMapIID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public int? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public string AcademicYearName { get; set; }

        //[DataMember]
        //public List<ClassTeacherMapDTO> ClassTeacherMaps { get; set; }


        //[DataMember]
        //public List<KeyValueDTO> ClassAssociateTeacherMaps { get; set; }

        [DataMember]
        public List<KeyValueDTO> Subject { get; set; }

        [DataMember]
        public List<ClassSectionSubjectPeriodMapMapDTO> SubjectMapDetails { get; set; }
    }
}