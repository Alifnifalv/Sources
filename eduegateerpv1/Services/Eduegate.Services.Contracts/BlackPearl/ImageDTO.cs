using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class ImageDTO
    {
        [DataMember]
        public long ImageId { get; set; }
        [DataMember]
        public string ImageSrc { get; set; }

        [DataMember]
        public string ImageAlt { get; set; }

        [DataMember]
        public Nullable<long> ImageTypeId { get; set; }

        [DataMember]
        public string ImageTypeName { get; set; }

        [DataMember]
        public Nullable<byte> ImageSequence { get; set; }
    }
}
