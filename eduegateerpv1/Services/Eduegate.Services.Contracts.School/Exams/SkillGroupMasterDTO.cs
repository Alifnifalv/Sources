using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class SkillGroupMasterDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int SkillGroupMasterID { get; set; }

        [DataMember]
        public string SkillGroup { get; set; }

    }
}
