using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Library
{
    [DataContract]
    public class LibraryStudentRegisterDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  LibraryStudentRegisterIID { get; set; }
        [DataMember]
        public string  LibraryCardNumber { get; set; }
        [DataMember]
        public System.DateTime?  RegistrationDate { get; set; }
        [DataMember]
        public string  Notes { get; set; }
        [DataMember]
        public long?  StudentID { get; set; }
        [DataMember]
        public string StudentName { get; set; }
        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
    }
}


