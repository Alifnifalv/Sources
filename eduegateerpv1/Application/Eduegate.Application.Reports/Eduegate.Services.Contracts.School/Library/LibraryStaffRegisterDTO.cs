using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Library
{
    [DataContract]
    public class LibraryStaffRegisterDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  LibraryStaffResiterIID { get; set; }
        [DataMember]
        public long?  EmployeeID { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public string  LibraryCardNumber { get; set; }
        [DataMember]
        public System.DateTime?  RegistrationDate { get; set; }
        [DataMember]
        public string  Notes { get; set; }
        [DataMember]
        public string StaffName { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
    }
}


