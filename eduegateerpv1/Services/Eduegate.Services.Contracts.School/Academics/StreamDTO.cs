using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class StreamDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StreamDTO()
        {
            CompulsorySubjects = new List<KeyValueDTO>();
            OptionalSubjects = new List<KeyValueDTO>();
        }

        [DataMember]
        public byte StreamID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string Code { get; set; }

        [DataMember]
        [StringLength(50)]
        public string Description { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? StreamGroupID { get; set; }


        [DataMember]
        public bool? IsActive { get; set; }


        //[DataMember]
        //public string AcademicYearName { get; set; }

        [DataMember]
        public List<KeyValueDTO> CompulsorySubjects { get; set; }

        [DataMember]
        public List<KeyValueDTO> OptionalSubjects { get; set; }
    }
}