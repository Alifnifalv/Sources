using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class AcademicSchoolMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AcademicSchoolMapDTO()
        {
            AcademicSchoolMapWorkingDayDTO = new List<AcademicSchoolMapWorkingDayDTO>();
        }

        [DataMember]
        public long AcademicSchoolMapIID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public List<AcademicSchoolMapWorkingDayDTO> AcademicSchoolMapWorkingDayDTO { get; set; }
    }
}