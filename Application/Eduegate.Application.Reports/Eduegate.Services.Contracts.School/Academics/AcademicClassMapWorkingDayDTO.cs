using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class AcademicClassMapWorkingDayDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AcademicClassMapWorkingDayDTO()
        {

        }

        [DataMember]
        public long AcademicClassMapIID { get; set; }


        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? TotalWorkingDays { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

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