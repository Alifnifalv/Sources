using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Mutual;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class DirectorsDashBoardDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public DirectorsDashBoardDTO()
        {
            Schools = new List<SchoolsDTO>();
        }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string MainHeader { get; set; }

        [DataMember]
        public string Academic { get; set; }

        [DataMember]
        public List<SchoolsDTO> Schools { get; set; }

        [DataMember]
        public int TotalStudents { get; set; }

        [DataMember]
        public int TotalTeachers { get; set; }

        [DataMember]
        public int TotalSections { get; set; }

        [DataMember]
        public int TotalEmployees { get; set; }

        [DataMember]
        public int TotalAdmins { get; set; }

    }
}
