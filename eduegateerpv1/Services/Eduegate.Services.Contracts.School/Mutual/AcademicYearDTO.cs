using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Mutual
{
    [DataContract]
    public class AcademicYearDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public int  AcademicYearID { get; set; }

        [DataMember]
        public string  AcademicYearCode { get; set; }

        [DataMember]
        public string  Description { get; set; }

        [DataMember]
        public System.DateTime?  StartDate { get; set; }

        [DataMember]
        public System.DateTime?  EndDate { get; set; }

        [DataMember]
        public byte?  SchoolID { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public byte? AcademicYearStatusID { get; set; }

        [DataMember]
        public int? ORDERNO { get; set; }

        [DataMember]
        public string EndDateString { get; set; }
    }
}