using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class MarkGradeMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long MarksGradeMapIID { get; set; }

        [DataMember]
        public int? MarksGradeID { get; set; }

        [DataMember]
        public string GradeName { get; set; }

        [DataMember]
        public decimal? GradeFrom { get; set; }

        [DataMember]
        public decimal? GradeTo { get; set; }

        [DataMember]
        public bool? IsPercentage { get; set; }

        [DataMember]
        public string Description { get; set; }

       
    }
}
