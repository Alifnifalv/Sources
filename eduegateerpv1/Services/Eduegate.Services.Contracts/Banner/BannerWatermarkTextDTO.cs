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
    public class BannerWatermarkTextDTO : BannerPropertyDTO
    {
        [DataMember]
        public string WatermarkText { get; set; }
        [DataMember]
        public string FontFamily { get; set; }
        [DataMember]
        public string FontStyle { get; set; }
        [DataMember]
        public string FontColor { get; set; }
        [DataMember]
        public int FontSize { get; set; }
        [DataMember]
        public int Optacity { get; set; }
        [DataMember]
        public bool IsVeritical { get; set; }
        [DataMember]
        public bool IsRightToLeft { get; set; }
        [DataMember]
        public bool IsDropShadow { get; set; }

        public static BannerWatermarkTextMap ToEntity(BannerWatermarkTextDTO textDto)
        {
            return new BannerWatermarkTextMap()
            {
                FontColor = textDto.FontColor,
                FontFamily = textDto.FontFamily,
                FontSize = textDto.FontSize,
                FontStyle = textDto.FontStyle,
                DropShadow = textDto.IsDropShadow,
                Opacity = textDto.Optacity,
                RightToLeft = textDto.IsRightToLeft,
                Vertical = textDto.IsVeritical,
                WatermarkText = textDto.WatermarkText,
                PositionX = textDto.PositionX,
                PositionY = textDto.PositionY
            };
        }
    }
}
