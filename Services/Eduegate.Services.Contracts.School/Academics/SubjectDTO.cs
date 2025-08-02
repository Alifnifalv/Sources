using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class SubjectDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public int  SubjectID { get; set; }
        [DataMember]
        public byte?  SubjectTypeID { get; set; }
        [DataMember]
        public string SubjectTypeName { get; set; }
        [DataMember]
        public string  SubjectName { get; set; }
        [DataMember]
        public string HexCodeUpper { get; set; }
        [DataMember]
        public string HexCodeLower { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string SubjectText { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public string ProgressReportHeader { get; set; }

        [DataMember]
        public string HexColorCode { get; set; }

        [DataMember]
        public string IconFileName { get; set; }
        

    }
}



