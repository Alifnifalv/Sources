using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class ClassSectionSubjectListMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public ClassSectionSubjectListMapDTO()
        {
            Class = new KeyValueDTO();
            Subject = new KeyValueDTO();
            Section = new KeyValueDTO();
            Subjects = new List<KeyValueDTO>();
            Sections = new List<KeyValueDTO>();
            OtherTeacher = new KeyValueDTO();
        }

        [DataMember]
        public long ClassSubjectMapIID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        
        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }

        [DataMember]
        public  KeyValueDTO Section { get; set; }

        [DataMember]
        public List<KeyValueDTO> Sections { get; set; }

        [DataMember]
        public List<KeyValueDTO> Subjects { get; set; }

        [DataMember]
        public string SubjectName { get; set; }

        [DataMember]
        public List<ClassSubjectWorkFlowMapDTO> ClassSubjectWorkFlow { get; set; }

        [DataMember]
        public KeyValueDTO OtherTeacher { get; set; }
    }
}


