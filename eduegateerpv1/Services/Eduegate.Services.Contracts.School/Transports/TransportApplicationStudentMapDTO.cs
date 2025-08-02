using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class TransportApplicationStudentMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public TransportApplicationStudentMapDTO()
        {
            Student = new KeyValueDTO();
        }

        [DataMember]
        public long TransportApplctnStudentMapIID { get; set; }

        [DataMember]
        public long? TransportApplicationID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string AdmissionNumber { get; set; }

        [DataMember]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DataMember]
        [StringLength(50)]
        public string MiddleName { get; set; }

        [DataMember]
        [StringLength(50)]
        public string LastName { get; set; }

        [DataMember]
        public byte? GenderID { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public bool? IsNewRider { get; set; }

        [DataMember]
        public bool? LocationChange { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? TransportApplcnStatusID { get; set; }

        [DataMember]
        public bool? IsMedicalCondition { get; set; }

        [DataMember]
        [StringLength(500)]
        public string Remarks { get; set; }

        [DataMember]
        public string GenderName { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string SchoolName { get; set; }

        [DataMember]
        public string StartDateString { get; set; }

        [DataMember]
        public KeyValueDTO Student { get; set; }

        [DataMember]
        public bool? CheckBoxStudent { get; set; }

        [DataMember]
        public string ApplicationNumber { get; set; }


        [DataMember]
        public string UpdatedDateString { get; set; }

        [DataMember]
        public string IsActiveOrNot { get; set; }

        [DataMember]
        public string ContactNumber { get; set; }

        [DataMember]
        public string ApplicationStatus { get; set; }

        [DataMember]
        public string Remarks1 { get; set; }
    }
}