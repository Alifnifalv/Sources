
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class ClassCoordinatorClassMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ClassCoordinatorClassMapDTO()
        {
            Section = new KeyValueDTO();
            Class = new KeyValueDTO();
        }

        [DataMember]
        public long ClassCoordinatorClassMapIID { get; set; }

        [DataMember]
        public long? ClassCoordinatorID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public bool? AllClass { get; set; }

        [DataMember]
        public bool? AllSection { get; set; }

        [DataMember]
        public bool? AllDepartment { get; set; }

        [DataMember]
        public KeyValueDTO Class  { get; set; }

        [DataMember]
        public KeyValueDTO Section { get; set; }
    }
}