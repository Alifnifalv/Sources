using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.AI
{
    [DataContract]
    public class ChatBotDTO : BaseMasterDTO
    {
        [DataMember]

        public string Message { get; set; }

        [DataMember]

        public string VoiceData { get; set; }
    }
}