using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fines
{
    [DataContract]
    public class FineMasterStudentMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FineMasterStudentMapDTO()
        {
            Student = new KeyValueDTO();
            FineMaster = new KeyValueDTO();
        }

        [DataMember]
        public long FineMasterStudentMapIID { get; set; }

        [DataMember]
        public int? FineMasterID { get; set; }

        [DataMember]
        public long? StudentId { get; set; }

        [DataMember]
        public KeyValueDTO Student { get; set; }

        [DataMember]
        public KeyValueDTO FineMaster { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public DateTime? FineMapDate { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public bool IsCollected { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
    }
}