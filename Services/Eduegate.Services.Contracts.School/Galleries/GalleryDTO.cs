using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Galleries
{
    [DataContract]
    public class GalleryDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public GalleryDTO()
        {
            GalleryAttachmentMaps = new List<GalleryAttachmentMapDTO>();
            AcademicYear = new KeyValueDTO();
        }

        [DataMember]
        public long GalleryIID { get; set; }

        [DataMember]
        public DateTime? GalleryDate { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        [StringLength(250)]
        public string Description { get; set; }

        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }

        [DataMember]
        public bool? ISActive { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? ExpiryDate { get; set; }

        [DataMember]
        public  List<GalleryAttachmentMapDTO> GalleryAttachmentMaps { get; set; }
    }
}
