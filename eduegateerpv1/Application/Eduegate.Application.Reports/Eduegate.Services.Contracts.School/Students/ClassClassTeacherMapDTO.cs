using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class ClassClassTeacherMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ClassClassTeacherMapDTO()
        {
            ClassTeacherMaps = new List<ClassTeacherMapDTO>();

            Subject = new KeyValueDTO();
            OtherTeacher = new KeyValueDTO();
        }
        [DataMember]
        public long ClassClassTeacherMapIID { get; set; }

        [DataMember]
        public long? ClassTeacherID { get; set; }


        [DataMember]
        public string HeadTeacherName { get; set; }

        [DataMember]
        public string CoordinatorName { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        //for edit time refernce to prevent section change in edit 
        [DataMember]
        public int? OldSectionID { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public long? CoordinatorID { get; set; }

        [DataMember]
        public string EmployeePhoto { get; set; }

        [DataMember]
        public string SubjectName { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public string WorkEmail { get; set; }

        [DataMember]
        public int? GenderID { get; set; }

        [DataMember]
        public string GenderDescription { get; set; }

        [DataMember]
        public List<ClassTeacherMapDTO> ClassTeacherMaps { get; set; }


        [DataMember]
        public List<KeyValueDTO> ClassAssociateTeacherMaps { get; set; }


        //For RefreshButton
        [DataMember]
        public KeyValueDTO Subject { get; set; }
        [DataMember] 
        public KeyValueDTO OtherTeacher { get; set; }
    }
}