using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class AssignmentDTO :Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AssignmentDTO()
        {
            AssignmentType = new KeyValueDTO();
            Class = new KeyValueDTO();
            Section = new KeyValueDTO();
            Subject = new KeyValueDTO();
            AcademicYear = new KeyValueDTO();
            AssignmentStatus = new KeyValueDTO();
            SectionList = new List<KeyValueDTO>();
        }

        [DataMember]
        public long AssignmentIID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? AssignmentTypeID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public DateTime? DateOfSubmission { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? FreezeDate { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public byte? AssignmentStatusID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public  KeyValueDTO AcademicYear { get; set; }

        [DataMember]
        public virtual List<AssignmentAttachmentMapDTO> AssignmentAttachmentMaps { get; set; }

        [DataMember]
        public  KeyValueDTO AssignmentStatus { get; set; }

        [DataMember]
        public KeyValueDTO AssignmentType { get; set; }

        [DataMember]
        public KeyValueDTO  Class { get; set; }

        [DataMember]
        public KeyValueDTO  Section { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }

        [DataMember]
        public byte? OldAssignmentStatusID { get; set; }

        [DataMember]
        public List<KeyValueDTO> SectionList { get; set; }
    }
}