using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Framework
{
    [DataContract]
    public class ScreenLookupDTO : BaseMasterDTO
    {
        public ScreenLookupDTO()
        {
            Lookups = new List<KeyValueDTO>();
            IsLazyLoad = false;
            LookUpQueryParameters = new List<string>();
            InitLookups = new List<KeyValueDTO>();
            IsReLoadedByParameters = false;
        }

        [DataMember]
        public bool? IsOnInit { get; set; }

        [DataMember]
        public string LookUpName { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string CallBack { get; set; }

        [DataMember]
        public List<KeyValueDTO> Lookups { get; set; }

        [DataMember]
        public bool IsLazyLoad { get; set; }

        [DataMember]
        public string LookUpQuery { get; set; }

        [DataMember]
        public List<string> LookUpQueryParameters { get; set; }

        [DataMember]
        public List<KeyValueDTO> InitLookups { get; set; }

        [DataMember]
        public string ParameterKey { get; set; }

        [DataMember]
        public string ParameterValue { get; set; }

        [DataMember]
        public bool IsReLoadedByParameters { get; set; }
    }
}