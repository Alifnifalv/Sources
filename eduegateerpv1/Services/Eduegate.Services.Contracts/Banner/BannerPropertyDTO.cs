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
    public class BannerPropertyDTO
    {
        [DataMember]
        public int PositionX { get; set; }
        [DataMember]
        public int PositionY { get; set; }

        public static List<BannerWatermarkImageMap> ToEntity(List<BannerWatermarkImageDTO> waterMarkImageDtos)
        {
            var imageMap = new List<BannerWatermarkImageMap>();

            foreach (var dto in waterMarkImageDtos)
            {
                var entity = BannerWatermarkImageDTO.ToEntity(dto);
                entity.PositionX = dto.PositionX;
                entity.PositionY = dto.PositionY;
                imageMap.Add(entity);
            }

            return imageMap;
        }
        public static List<BannerWatermarkTextMap> ToEntity(List<BannerWatermarkTextDTO> wanterMarkTextDtos)
        {
            var textMap = new List<BannerWatermarkTextMap>();

            foreach (var dto in wanterMarkTextDtos)
            {
                textMap.Add(BannerWatermarkTextDTO.ToEntity(dto));
            }

            return textMap;
        }       
    }
}
