using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class StreamSubjectMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StreamSubjectMapDTO()
        {
            Subject = new List<KeyValueDTO>();
            OptionalSubject = new List<KeyValueDTO>();
        }

        [DataMember]
        public long StreamSubjectMapIID { get; set; }

        [DataMember]
        public byte? StreamID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public bool? IsOptionalSubject { get; set; }

        //[DataMember]
        //public string AcademicYearName { get; set; }

        [DataMember]
        public string SubjectName { get; set; }

        [DataMember]
        public int OrderBy { get; set; }

        [DataMember]
        public List<KeyValueDTO> Subject { get; set; }

        [DataMember]
        public List<KeyValueDTO> OptionalSubject { get; set; }

        [DataMember]
        public List<StreamCompulsorySubjectMapDTO> StreamCompulsorySubjectMap { get; set; }

        [DataMember]
        public List<StreamOptionalSubjectMapDTO> StreamOptionalSubjectMap { get; set; }

    }
}