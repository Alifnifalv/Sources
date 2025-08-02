using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Circulars;
using Eduegate.Services.Contracts.School.CounselorHub;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.CounselorHub
{
    [DataContract]
    public class CounselorHubDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public CounselorHubDTO()
        {
            AcademicYear = new KeyValueDTO();
            CounselorHubMaps = new List<CounselorHubMapDTO>();
            CounselorHubAttachments = new List<CounselorHubAttachmentMapDTO>();

        }

        [DataMember]
        public long CounselorHubIID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
        [StringLength(500)]

        [DataMember]
        public string Title { get; set; }
        [StringLength(500)]

        [DataMember]
        public string ShortTitle { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public bool? IsSendNotification { get; set; }

        [DataMember]
        public bool? IsSelected { get; set; }


        [DataMember]
        public bool? IsFill { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public DateTime? CounselorHubEntryDate { get; set; }

        [DataMember]
        public string CounselorHubExpiryDateString { get; set; }

        [DataMember]
        public DateTime? CounselorHubExpiryDate { get; set; }


        [DataMember]
        public byte? CounselorHubStatusID { get; set; }


        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }

        [DataMember]
        public KeyValueDTO School { get; set; }

        [DataMember]
        public List<CounselorHubMapDTO> CounselorHubMaps { get; set; }

        [DataMember]
        public List<CounselorHubAttachmentMapDTO> CounselorHubAttachments { get; set; }


        [DataMember]
        public long? AttachmentReferenceID { get; set; }
    }
}
