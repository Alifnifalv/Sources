using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Circulars
{
    [DataContract]
    public class CircularMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public CircularMapDTO()
        {
            Circular = new KeyValueDTO();
            Class = new KeyValueDTO();
            Section = new KeyValueDTO();
            Department = new KeyValueDTO();
        }

        [DataMember]
        public long CircularMapIID { get; set; }

        [DataMember]
        public long? CircularID { get; set; }

        [DataMember]
        public bool? AllClass { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public bool? AllSection { get; set; }

        [DataMember]
        public long? DepartmentID { get; set; }

        [DataMember]
        public bool? AllDepartment { get; set; }

        [DataMember]
        public KeyValueDTO Circular { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public KeyValueDTO Section { get; set; }

        [DataMember]
        public KeyValueDTO Department { get; set; }

    }
}