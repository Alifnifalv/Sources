using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity;

namespace Eduegate.Services.Contracts.Banner
{
    [DataContract]
    public class BannerWatermarkImageDTO : BannerPropertyDTO
    {
        [DataMember]
        public string WatermarkImageFileName { get; set; }

        public static BannerWatermarkImageMap ToEntity(BannerWatermarkImageDTO imageDto)
        {
            return new BannerWatermarkImageMap() { WatermarkImageFile = imageDto.WatermarkImageFileName };
        }
    }
}
