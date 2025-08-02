using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Lms
{
    [DataContract]
    public class LmsGroupDTO : BaseMasterDTO
    {
        public LmsGroupDTO()
        {
            SignUpDTOs = new List<LmsDTO>();
        }

        [DataMember]
        public int SignupGroupID { get; set; }

        [DataMember]
        [StringLength(100)]
        public string GroupTitle { get; set; }

        [DataMember]
        public string GroupDescription { get; set; }

        [DataMember]
        public DateTime? FromDate { get; set; }

        [DataMember]
        public DateTime? ToDate { get; set; }

        [DataMember]
        public DateTime? DueDate { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public string FromDateString { get; set; }

        [DataMember]
        public string ToDateString { get; set; }

        [DataMember]
        public string DueDateString { get; set; }

        [DataMember]
        public bool? IsSelected { get; set; }

        [DataMember]
        public List<LmsDTO> SignUpDTOs { get; set; }

        [DataMember]
        public DateTime? CurrentDate { get; set; }

        [DataMember]
        public string CurrentDateString { get; set; }

        [DataMember]
        public bool? IsExpired { get; set; }

        [DataMember]
        public List<KeyValueDTO> Students { get; set; }
    }
}