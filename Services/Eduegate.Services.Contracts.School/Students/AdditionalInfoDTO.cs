using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class AdditionalInfoDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AdditionalInfoDTO()
        {
            StudentGroups = new List<KeyValueDTO>();
        }

        [DataMember]
        public long StudentMiscDetailsIID { get; set; }

        [DataMember]
        public string BankAccountNumber { get; set; }

        [DataMember]
        public string BankName { get; set; }

        [DataMember]
        public string IFCCode { get; set; }

        [DataMember]
        public string NationalID { get; set; }

        [DataMember]
        public string LocalID { get; set; }

        [DataMember]
        public string PreviousSchoolDetail { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public bool? IsCurrentAddresIsGuardian { get; set; }

        [DataMember]
        public string CurrentAddress { get; set; }

        [DataMember]
        public bool? IsPermenentAddresIsCurrent { get; set; }

        [DataMember]
        public string PermenentAddress { get; set; }

        [DataMember]
        public byte? GuardianTypeID { get; set; }

        [DataMember]
        public int? StudentGroupID { get; set; }

        [DataMember]
        public long? StaffID { get; set; }

        [DataMember]
        public List<KeyValueDTO> StudentGroups { get; set; }

        [DataMember]
        public string PermenentBuildingNo { get; set; }

        [DataMember]
        public string PermenentFlatNo { get; set; }

        [DataMember]
        public string PermenentStreetNo { get; set; }

        [DataMember]
        public string PermenentStreetName { get; set; }

        [DataMember]
        public string PermenentLocationNo { get; set; }

        [DataMember]
        public string PermenentLocationName { get; set; }

        [DataMember]
        public string PermenentZipNo { get; set; }

        [DataMember]
        public string PermenentCity { get; set; }

        [DataMember]
        public string PermenentPostBoxNo { get; set; }

        [DataMember]
        public int? PermenentCountryID { get; set; }

        [DataMember]
        public string PermenentCountry { get; set; }
    }
}