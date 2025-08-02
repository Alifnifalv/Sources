using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Forms
{
    [DataContract]
    public class FormDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FormDTO()
        {

        }

        [DataMember]
        public int FormID { get; set; }

        [DataMember]
        public string FormName { get; set; }
    }
}