using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class MarkGradeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
       
        [DataMember]
        public int MarkGradeIID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public List<MarkGradeMapDTO> GradeTypes { get; set; }

    }
}



