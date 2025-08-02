using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class StreamCompulsorySubjectMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StreamCompulsorySubjectMapDTO()
        {
            Subject = new KeyValueDTO();
        }

        [DataMember]
        public long StreamSubjectMapIID { get; set; }

        [DataMember]
        public byte? StreamID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public string SubjectName { get; set; }

        [DataMember]
        public int OrderBy { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }

    }
}