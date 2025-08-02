using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class StudentFeeConcessionDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentFeeConcessionDTO()
        {
            StudentGroupFeeMaster = new StudentGroupFeeMasterDTO();
            StudentFeeConcessionDetail = new List<StudentFeeConcessionDetailDTO>();
        }
        [DataMember]
        public long StudentFeeConcessionIID { get; set; }

        [DataMember]
        public DateTime? ConcessionDate { get; set; }

        [DataMember]
        public long StudentGroupFeeMasterID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }
       
        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public long? StaffID { get; set; }

        [DataMember]
        public long? CreditNoteID { get; set; }

        [DataMember]
        public string StaffName { get; set; }

        [DataMember]
        public long? ParentID { get; set; }

        [DataMember]
        public string ParentName { get; set; }

        [DataMember]
        public KeyValueDTO Staff { get; set; }

        [DataMember]
        public List<KeyValueDTO> StudentList { get; set; }

        [DataMember]
        public KeyValueDTO FeeConcessionApprovalType { get; set; }

        [DataMember]
        public short? ConcessionApprovalTypeID { get; set; }

        [DataMember]
        public KeyValueDTO Parent { get; set; }

        [DataMember]
        public KeyValueDTO StudentGroup { get; set; }

        [DataMember]
        public StudentGroupFeeMasterDTO StudentGroupFeeMaster { get; set; }

        [DataMember]
        public List<StudentFeeConcessionDetailDTO> StudentFeeConcessionDetail { get; set; }
    }
}
