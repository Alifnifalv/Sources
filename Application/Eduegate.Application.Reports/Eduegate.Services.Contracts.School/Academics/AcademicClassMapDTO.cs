using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class AcademicClassMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public object AcademicYearIID;

        public AcademicClassMapDTO()
        {
            AcademicYear = new KeyValueDTO();
            Class = new List<KeyValueDTO>();
            AcademicClassMapWorkingDayDTO = new List<AcademicClassMapWorkingDayDTO>();
        }

        [DataMember]
        public long AcademicClassMapIID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public List<KeyValueDTO> Class { get; set; }

        [DataMember]
        public int? TotalWorkingDays { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        [StringLength(500)]
        public string Description { get; set; }

        [DataMember]
        public byte? MonthID { get; set; }

        [DataMember]
        public int? YearID { get; set; }

        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }

        [DataMember]
        public List<AcademicClassMapWorkingDayDTO> AcademicClassMapWorkingDayDTO { get; set; }
    }
}