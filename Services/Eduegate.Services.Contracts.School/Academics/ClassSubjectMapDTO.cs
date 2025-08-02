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
    public class ClassSubjectMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public ClassSubjectMapDTO()
        {
            Class = new KeyValueDTO();
            Subject = new List<KeyValueDTO>();
            Section = new List<KeyValueDTO>();
            ClassSubjectWorkFlow = new List<ClassSubjectWorkFlowMapDTO>();
            ClassSectionSubjectList = new ClassSectionSubjectListMapDTO();
            ListData = new List<ClassSectionSubjectListMapDTO>();
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
        public List<KeyValueDTO> Subject { get; set; }

        [DataMember]
        public List<KeyValueDTO> Section { get; set; }

        [DataMember]
        public List<ClassSubjectWorkFlowMapDTO> ClassSubjectWorkFlow { get; set; }

        [DataMember]
        public ClassSectionSubjectListMapDTO ClassSectionSubjectList { get; set; }

        [DataMember]
        public List<ClassSectionSubjectListMapDTO> ListData { get; set; }
    }
}


