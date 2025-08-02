using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.OnlineExam.Exam
{
    [DataContract]
    public class CandidateDTO : BaseMasterDTO
    {
        public CandidateDTO()
        {
            CandidateOnlineExamMaps = new List<CandidateOnlineExamMapDTO>();
        }

        [DataMember]
        public long CandidateIID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string CandidateName { get; set; }

        [DataMember]
        [StringLength(50)]
        public string Email { get; set; }

        [DataMember]
        [StringLength(20)]
        public string Telephone { get; set; }

        [DataMember]
        [StringLength(20)]
        public string MobileNumber { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        [StringLength(30)]
        public string UserName { get; set; }

        [DataMember]
        [StringLength(20)]
        public string Password { get; set; }

        [DataMember]
        public int? ClassID { get; set; }
        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string NationalID { get; set; }

        [DataMember]
        public List<KeyValueDTO> Student { get; set; }

        [DataMember]
        public List<KeyValueDTO> ExceptStudentList { get; set; }

        [DataMember]
        public bool? IsAllStudents { get; set; }

        [DataMember]
        public List<CandidateOnlineExamMapDTO> CandidateOnlineExamMaps { get; set; }

        [DataMember]
        public bool? IsNewCandidate { get; set; }

        [DataMember]
        public byte? CandidateStatusID { get; set; }

        [DataMember]
        public long? StudentApplicationID { get; set; }

    }
}