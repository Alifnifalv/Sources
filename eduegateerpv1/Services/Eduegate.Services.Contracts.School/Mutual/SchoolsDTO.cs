using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;
using Eduegate.Services.Contracts.School.Inventory;
using System.Collections.Generic;

namespace Eduegate.Services.Contracts.School.Mutual
{
    [DataContract]
    public class SchoolsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SchoolsDTO()
        {
            PayerBankDTO = new List<SchoolPayerBankDTO>();
        }

        [DataMember]
        public byte  SchoolID { get; set; }
        [DataMember]
        public string  SchoolName { get; set; }
        [DataMember]
        public string  Description { get; set; }
        [DataMember]
        public string  Address1 { get; set; }
        [DataMember]
        public string  Address2 { get; set; }
        [DataMember]
        public string  RegistrationID { get; set; }
        [DataMember]
        public int?  CompanyID { get; set; }


        [DataMember]
        public long? SponsorID { get; set; }

        [DataMember]
        public string Place { get; set; }

        [DataMember]
        public string SchoolCode { get; set; }


        [DataMember]
        public string SchoolShortName { get; set; }

        [DataMember]
        public string EmployerEID { get; set; }
        [DataMember]
        public string PayerEID { get; set; }
        [DataMember]
        public string PayerQID { get; set; }

        [DataMember]
        public List<SchoolPayerBankDTO> PayerBankDTO { get; set; }


  

        //For Directors DashBoard
        [DataMember]
        public int StudentsCount { get; set; }

        [DataMember]
        public int TeachersCount { get; set; }

        [DataMember]
        public int TeachersCountPermanant { get; set; }

        [DataMember]
        public int TeachersCountTemporary { get; set; }

        [DataMember]
        public string TeacherRatio { get; set; }

        [DataMember]
        public int SectionCount { get; set; }

        [DataMember]
        public int AdminsCount { get; set; }

        [DataMember]
        public long? ProfileContentID { get; set; }

        [DataMember]
        public long? SchoolSealContentID { get; set; }

        [DataMember]
        public List<KeyValueDTO> AcademicYears { get; set; }

    }
}


