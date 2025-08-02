using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.SignUp.SignUps
{
    [DataContract]
    public class SignupAudienceMapDTO : BaseMasterDTO
    {
        public SignupAudienceMapDTO()
        {
        }

        [DataMember]
        public long SignupAudienceMapIID { get; set; }

        [DataMember]
        public long? SignupID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public long? ParentID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
    }
}