using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class ClassSectionSubjectPeriodMapMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ClassSectionSubjectPeriodMapMapDTO()
        {
            //ClassTeacherMaps = new List<ClassTeacherMapDTO>();

            Subject = new KeyValueDTO();
        }
        [DataMember]
        public long PeriodMapIID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public int? SubjectTypeID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public int? TotalPeriods { get; set; }

        [DataMember]
        public int? WeekPeriods { get; set; }

        [DataMember]
        public int? MinimumPeriods { get; set; }

        [DataMember]
        public int? MaximumPeriods { get; set; }

        [DataMember]
        public int? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        //[DataMember]
        //public List<ClassTeacherMapDTO> ClassTeacherMaps { get; set; }


        //[DataMember]
        //public List<KeyValueDTO> ClassAssociateTeacherMaps { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }
    }
}