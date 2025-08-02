using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class SchoolDateSettingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SchoolDateSettingDTO()
        {
            SchoolDateSettingMaps = new List<SchoolDateSettingMapDTO>();
        }

        [DataMember]
        public long SchoolDateSettingIID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        //[DataMember]
        //public virtual AcademicYear AcademicYear { get; set; }

        //[DataMember]
        //public virtual Schools School { get; set; }

        //[DataMember]
        //public KeyValueDTO AcademicYear { get; set; }

        [DataMember]
        public List<SchoolDateSettingMapDTO> SchoolDateSettingMaps { get; set; }

    }
}