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
    public class StudentAssignmentMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentAssignmentMapDTO()
        {  
            Assignment = new KeyValueDTO();
        }
        [DataMember]
        public long StudentAssignmentMapIID { get; set; }

        [DataMember]
        public long? AssignmentID { get; set; }

        [DataMember]
        public DateTime? DateOfSubmission { get; set; }

        [DataMember]
        public byte? AssignmentStatusID { get; set; }

        [DataMember]
        public long? AttachmentReferenceId { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string AttachmentName { get; set; }

        [DataMember]
        public long? StudentId { get; set; }

        [DataMember]
        public KeyValueDTO Assignment { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public string Remarks { get; set; }

    }
}