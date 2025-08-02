using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public  class SkillMasterDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SkillMasterDTO()
        {
            SkillGroup = new KeyValueDTO();
        }

        [DataMember]
        public int SkillMasterID { get; set; }

        [DataMember]
        public string SkillName { get; set; }

        [DataMember]
        public int? SkillGroupMasterID { get; set; }

        [DataMember]
        public KeyValueDTO SkillGroup { get; set; }
    }
}
