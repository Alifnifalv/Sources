using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class ClassCoordinatorsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ClassCoordinatorsDTO()
        {
            ClassCoordinatorClassMaps = new List<ClassCoordinatorClassMapDTO>();
            Coordinator = new KeyValueDTO();
        }

        [DataMember]
        public long ClassCoordinatorIID { get; set; }

        [DataMember]
        public long? CoordinatorID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public bool? ISACTIVE { get; set; }

        [DataMember]
        public List<ClassCoordinatorClassMapDTO> ClassCoordinatorClassMaps { get; set; }


        [DataMember]
        public KeyValueDTO Coordinator { get; set; }
    }
}