using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class ContextDTO
    {
        /// <summary>
        /// Logged in User id who triggered the request
        /// </summary>
        [DataMember]
        public string UserID { get; set; }

        /// <summary>
        ///  Guest User id who triggered the request
        /// </summary>
        [DataMember]        
        public string GuestUserID { get; set; }

        /// <summary>
        /// Region from where the request is triggered
        /// </summary>        
        [DataMember]
        public string Region { get; set; }
        /// <summary>
        /// Language id
        /// </summary>        
        [DataMember]
        public string LanguageID { get; set; }
        /// <summary>
        /// Language code
        /// </summary>        
        [DataMember]
        public string LanguageCode { get; set; }

        /// <summary>
        /// IP address of the requester
        /// </summary>        
        [DataMember]
        public string IPAddress { get; set; }

        /// <summary>
        /// Which channel is the request coming from
        /// </summary>
        [DataMember]
        public string Channel { get; set; }

        [DataMember]
        public string SessionID { get; set; }
    }
}
