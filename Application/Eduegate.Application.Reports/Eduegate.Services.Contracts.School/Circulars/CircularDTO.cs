using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Circulars
{
    [DataContract]
    public class CircularDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public CircularDTO()
        {
            AcademicYear = new KeyValueDTO();
            CircularMaps = new List<CircularMapDTO>();
            CircularUserTypeMaps = new List<CircularUserTypeMapDTO>();
            CircularAttachmentMaps = new List<CircularAttachmentMapDTO>();
        }

        [DataMember]
        public long CircularIID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcadamicYearID { get; set; }

        [DataMember]
        public byte? CircularTypeID { get; set; }

        [DataMember]
        public byte? CircularPriorityID { get; set; }

        [DataMember]
        public DateTime? CircularDate { get; set; }

        [DataMember]
        public DateTime? ExpiryDate { get; set; }

        [DataMember]
        [StringLength(50)]
        public string CircularCode { get; set; }

        [DataMember]
        [StringLength(500)]
        public string Title { get; set; }

        [DataMember]
        [StringLength(500)]
        public string ShortTitle { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public byte? CircularStatusID { get; set; }

        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }

        [DataMember]
        public List<CircularMapDTO> CircularMaps { get; set; }

        [DataMember]
        public List<CircularUserTypeMapDTO> CircularUserTypeMaps { get; set; }

        [DataMember]
        public long? AttachmentReferenceID { get; set; }

        [DataMember]
        public List<CircularAttachmentMapDTO> CircularAttachmentMaps { get; set; }

        [DataMember]
        public bool? IsSendPushNotification { get; set; }
    }
}