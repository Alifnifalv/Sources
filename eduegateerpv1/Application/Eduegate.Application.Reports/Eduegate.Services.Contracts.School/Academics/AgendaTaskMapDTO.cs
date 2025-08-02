using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public  class AgendaTaskMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AgendaTaskMapDTO()
        {
            AgendaTaskAttachmentMaps = new List<AgendaTaskAttachmentMapDTO>();
        }

        [DataMember]
        public long AgendaTaskMapIID { get; set; }
        
        [DataMember]
        public long? AgendaTopicMapID { get; set; }
        
        [DataMember]
        public string Task { get; set; }
        
        [DataMember]
        public byte? TaskTypeID { get; set; }
        
        [DataMember]
        public KeyValueDTO TaskType { get; set; }
        
        [DataMember]
        public DateTime? StartDate { get; set; }
        
        [DataMember]
        public DateTime? EndDate { get; set; }
        
        [DataMember]
        public long? AgendaID { get; set; }
        
        [DataMember]
        public long AgendacTaskAttachmentMapIID { get; set; }
        
        [DataMember]
        public long? AgendaTaskMapID { get; set; }
        
        [DataMember]
        public long? AttachmentReferenceID { get; set; }

        [DataMember]
        public string AttachmentName { get; set; }

        [DataMember]
        public string Date1String { get; set; }

        [DataMember]
        public string Date2String { get; set; }

        [DataMember]
        public virtual List<AgendaTaskAttachmentMapDTO> AgendaTaskAttachmentMaps { get; set; }

    }
}