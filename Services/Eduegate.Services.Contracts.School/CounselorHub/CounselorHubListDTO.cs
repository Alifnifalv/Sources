using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.CounselorHub
{
    [DataContract]
    public class CounselorHubListDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public CounselorHubListDTO()
        {
            CounselorHubAttachments = new List<CounselorHubAttachmentMapDTO>();
        }

        [DataMember]
        public long CounselorHubIID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcadamicYearID { get; set; }

        [DataMember]
        public string CounselorHubEntryDate { get; set; }

        [DataMember]
        public string CounselorHubExpiryDate { get; set; }

        [DataMember]
        [StringLength(500)]
        public string ShortTitle { get; set; }


        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string School { get; set; }

        [DataMember]
        public byte? CounselorHubStatusID { get; set; }

        [DataMember]
        public string AcademicYear { get; set; }

        [DataMember]
        public string Classes { get; set; }

        [DataMember]
        public string Sections { get; set; }

        [DataMember]
        public string Student { get; set; }

        [DataMember]
        public List<CounselorHubAttachmentMapDTO> CounselorHubAttachments { get; set; }
    }
}
