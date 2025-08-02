using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Forms
{
    [DataContract]
    public class FormFieldDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FormFieldDTO()
        {

        }

        [DataMember]
        public long FormFieldID { get; set; }

        [DataMember]
        public int? FormID { get; set; }

        [DataMember]
        public string FieldName { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public bool? IsTitle { get; set; }

        [DataMember]
        public bool? IsSubTitle { get; set; }
    }
}