using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.SignUp.SignUps
{
    [DataContract]
    public class SignUpDTO : BaseMasterDTO
    {
        public SignUpDTO()
        {
            SignupSlotMaps = new List<SignupSlotMapDTO>();
            SignupAudienceMaps = new List<SignupAudienceMapDTO>();
        }

        [DataMember]
        public long SignupIID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string SignupName { get; set; }

        [DataMember]
        public int? SignupGroupID { get; set; }

        [DataMember]
        public string SignupGroup { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public string ClassName {  get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public long? OrganizerEmployeeID { get; set; }

        [DataMember]
        public string OrganizerEmployeeName { get; set; }
         
        [DataMember]
        public string LocationInfo { get; set; }

        [DataMember]
        [StringLength(200)]
        public string Message { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public byte? SignupCategoryID { get; set; }

        [DataMember]
        public string SignupCategoryName { get; set; }

        [DataMember]
        public byte? SignupTypeID { get; set; }

        [DataMember]
        public string SignupTypeName { get; set; }

        [DataMember]
        public byte? SignupStatusID { get; set; }

        [DataMember]
        public string SignupStatusName { get; set; }

        [DataMember]
        public DateTime? DateFrom { get; set; }

        [DataMember]
        public DateTime? DateTo { get; set; }

        [DataMember]
        public string FromDateString { get; set; }

        [DataMember]
        public string ToDateString { get; set; }

        [DataMember]
        public bool IsExpand { get; set; }

        [DataMember]
        public List<SignupSlotMapDTO> SignupSlotMaps { get; set; }

        [DataMember]
        public List<SignupAudienceMapDTO> SignupAudienceMaps { get; set; }

        [DataMember]
        public int? SlotAvailableCount { get; set; }

        [DataMember]
        public byte? SignupOldStatusID { get; set; }

        [DataMember]
        public DateTime? GroupDateFrom { get; set; }

        [DataMember]
        public DateTime? GroupDateTo { get; set; }

        [DataMember]
        public bool? IsSendNotification { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public bool? IsSlotShowToUser { get; set; }

        [DataMember]
        public string NotificationStatus { get; set; }
    }
}