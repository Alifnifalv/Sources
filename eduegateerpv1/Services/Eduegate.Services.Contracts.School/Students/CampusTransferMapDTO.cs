using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class CampusTransferMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public CampusTransferMapDTO()
        {
            ToClass = new KeyValueDTO();
            ToSection = new KeyValueDTO();
            Student = new KeyValueDTO();
            Student = new KeyValueDTO();
        }

        [DataMember]
        public long CampusTransferIID { get; set; }

        [DataMember]
        public long StudentID { get; set; }

        [DataMember]
        public int ToAcademicYearID { get; set; }

        [DataMember]
        public int FromAcademicYearID { get; set; }

        [DataMember]
        public int ToClassID { get; set; }

        [DataMember]
        public int ToSectionID { get; set; }

        [DataMember]
        public byte? FromSchoolID { get; set; }

        [DataMember]
        public byte? ToSchoolID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public KeyValueDTO ToClass { get; set; }

        [DataMember]
        public KeyValueDTO ToSection { get; set; }

        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }


        [DataMember]
        public KeyValueDTO Student { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public string ClassName { get; set; }
        
        [DataMember]
        public string FirstName { get; set; }
        
        [DataMember]
        public string MiddleName { get; set; }
       
        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public byte? StudentSchoolID { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }
    }
}