using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class AcademicSchoolMapWorkingDayDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AcademicSchoolMapWorkingDayDTO()
        {

        }

        [DataMember]
        public int? TotalWorkingDays { get; set; }

        [DataMember]
        public DateTime? PayrollCutOffDate { get; set; }

        [DataMember]
        [StringLength(1000)]
        public string Description { get; set; }

        [DataMember]
        public byte? MonthID { get; set; }

        [DataMember]
        public string MonthName { get; set; }

        [DataMember]
        public int? YearID { get; set; }

    }
}