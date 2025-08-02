using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class StudentAssignmentDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentAssignmentDTO()
        {
            StudentAssignmentMaps = new List<StudentAssignmentMapDTO>();
        }

        [DataMember]
        public long StudentAssignmentMapIID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public string SubjectName { get; set; }

        [DataMember]
        public long? AssignmentID { get; set; }

        [DataMember]
        public string AssignmentName { get; set; }

        [DataMember]
        public List<StudentAssignmentMapDTO> StudentAssignmentMaps { get; set; }

        [DataMember]

        public DateTime? SubmissionDate { get; set; }

        [DataMember]
        public object SubmittedFilePath { get; set; }

        [DataMember]
        public object SubmittedFileName { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public long? StudentId { get; set; }
    }
}