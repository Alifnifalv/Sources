using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class ClassTeacherMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ClassTeacherMapDTO()
        {

        }

        [DataMember]
        public long ClassTeacherMapIID { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public long? OtherTeacherID { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public long? ClassClassTeacherMapID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public string SubjectName { get; set; }

        [DataMember]
        public string HighestAcademicQualitication { get; set; }

        [DataMember]
        public int? GenderID { get; set; }

        [DataMember]
        public string GenderDescription { get; set; }

        [DataMember]
        public string EmployeePhoto { get; set; }

        [DataMember]
        public string WorkMobileNo { get; set; }

        [DataMember]
        public string WorkEmail { get; set; }

        [DataMember]
        public string OtherTeacherName { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public List<KeyValueDTO> AssociateTeacher { get; set; }

        #region Mobile app use
        [DataMember]
        public int? ClassOrderNumber { get; set; }
        #endregion
    }
}