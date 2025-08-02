using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Forms
{
    [DataContract]
    public class FormValueDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FormValueDTO()
        {
            FormValues = new List<FormValueDTO>();
        }

        [DataMember]
        public long FormValueIID { get; set; }

        [DataMember]
        public long? FormFieldID { get; set; }

        [DataMember]
        public string FormFieldName { get; set; }

        [DataMember]
        public int? FormID { get; set; }

        [DataMember]
        public long? ReferenceID { get; set; }

        [DataMember]
        public string FormFieldValue { get; set; }

        [DataMember]
        public List<FormValueDTO> FormValues { get; set; }
    }
}