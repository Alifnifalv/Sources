using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class VisitorDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public VisitorDTO()
        {
            VisitorAttachmentMapDTOs = new List<VisitorAttachmentMapDTO>();
        }

        [DataMember]
        public long VisitorIID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string VisitorNumber { get; set; }

        [DataMember]
        [StringLength(50)]
        public string QID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string PassportNumber { get; set; }

        [DataMember]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DataMember]
        [StringLength(50)]
        public string MiddleName { get; set; }

        [DataMember]
        [StringLength(50)]
        public string LastName { get; set; }

        [DataMember]
        public string VisitorFullName { get; set; }

        [DataMember]
        [StringLength(20)]
        public string MobileNumber1 { get; set; }

        [DataMember]
        [StringLength(20)]
        public string MobileNumber2 { get; set; }

        [DataMember]
        [StringLength(100)]
        public string EmailID { get; set; }

        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public string OtherDetails { get; set; }

        [DataMember]
        public List<VisitorAttachmentMapDTO> VisitorAttachmentMapDTOs { get; set; }

        [DataMember]
        public bool? IsError { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public long? VisitorProfileID { get; set; }

        

    }
}