using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
   public class StudentGroupTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentGroupTypeDTO()
        {
        }

        [DataMember]
        public int GroupTypeIID { get; set; }

        [DataMember]
        public string GroupTypeName { get; set; }
    }
}
