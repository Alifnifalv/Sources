using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Eduegate.Services.Contracts.School.Inventory
{
    [DataContract]
    public class AllergyStudentDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AllergyStudentDTO()
        {
            Allergies = new List<AllergyDTO>();
        }

        [DataMember]
        public long AllergyStudentMapIID { get; set; }

        [DataMember]
        public int? AllergyID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string AllergyName { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public long? ProductID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string SeverityName { get; set; }

        [DataMember]
        public List<AllergyDTO> Allergies { get; set; }

        [DataMember]
        public long? ProductSKUMapIID { get; set; }
    }
}
