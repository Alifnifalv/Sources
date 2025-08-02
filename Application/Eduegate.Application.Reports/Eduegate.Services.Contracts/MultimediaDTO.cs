using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public partial class MultimediaDTO
    {
        // Image
        [DataMember]
        public string ImageSourcePath { get; set; }
        [DataMember]
        public string ImageDestinationPath { get; set; }

        // Video
        [DataMember]
        public string VideoSourcePath { get; set; }
        [DataMember]
        public string VideoDestinationPath { get; set; }

        // Document
        [DataMember]
        public string DocumentSourcePath { get; set; }
        [DataMember]
        public string DocumentDestinationPath { get; set; }
    }
}
