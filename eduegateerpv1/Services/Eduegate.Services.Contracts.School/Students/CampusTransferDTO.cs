using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class CampusTransferDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public CampusTransferDTO()
        {
            CampusTransferMap = new List<CampusTransferMapDTO>();
            FromClass = new KeyValueDTO();
            Student = new KeyValueDTO();
            FromSection = new KeyValueDTO();
            ToClass = new KeyValueDTO();
            ToSection = new KeyValueDTO();
            ToAcademicYear = new KeyValueDTO();
            FeeTypeMap = new List<FeeCollectionFeeTypeDTO>();
        }

        [DataMember]
        public long CampusTransferIID { get; set; }

        [DataMember]
        public DateTime? TransferDate { get; set; }
        
        [DataMember]
        public int? FromClassID { get; set; }

        [DataMember]
        public int? FromSectionID { get; set; }

        [DataMember]
        public int? FromAcademicYearID { get; set; }

        [DataMember]
        public int? ToAcademicYearID { get; set; }

        [DataMember]
        public int? ToClassID { get; set; }

        [DataMember]
        public int? ToSectionID { get; set; }

        [DataMember]
        public byte? FromSchoolID { get; set; }

        [DataMember]
        public byte? ToSchoolID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public KeyValueDTO Student { get; set; }

        [DataMember]
        public KeyValueDTO FromClass { get; set; }

        [DataMember]
        public KeyValueDTO FromSection { get; set; }

        [DataMember]
        public KeyValueDTO ToClass { get; set; }

        [DataMember]
        public KeyValueDTO ToAcademicYear { get; set; }

        [DataMember]
        public KeyValueDTO ToSection { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string AdmissionNumber { get; set; }

        [DataMember]
        public string FromAcademicyear { get; set; }

        [DataMember]
        public List<FeeCollectionFeeTypeDTO> FeeTypeMap { get; set; }

        [DataMember]
        public List<CampusTransferMapDTO> CampusTransferMap { get; set; }
    }
}