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
    public class BannerConfigurationDTO
    {
        [DataMember]
        public long BannerConfigurationIID { get; set; }
        [DataMember]
        public long BannerID { get; set; }
        [DataMember]
        public string BannerFileName { get; set; }
        [DataMember]
        public List<BannerPropertyDTO> Properties { get; set; }
        public List<BannerParameterDTO> ContextParameters { get; set; }

        public static BannerConfigurationDTO ToDTO(BannerConfiguration entity)
        {
            if (entity == null)
                return null;

            var bannerConfiguration = new BannerConfigurationDTO()
            {
                BannerConfigurationIID = entity.BannerConfigurationIID,
                BannerFileName = entity.BannerFileName,
                BannerID = entity.BannerID.Value,
                Properties = new List<BannerPropertyDTO>(),
                ContextParameters = new List<BannerParameterDTO>()
            };

            foreach (var map in entity.BannerWatermarkImageMaps)
            {
                bannerConfiguration.Properties.Add(new BannerWatermarkImageDTO() { PositionX = map.PositionX.Value, PositionY = map.PositionY.Value, WatermarkImageFileName = map.WatermarkImageFile });
            }

            foreach (var map in entity.BannerWatermarkTextMaps)
            {
                bannerConfiguration.Properties.Add(new BannerWatermarkTextDTO() { PositionX = map.PositionX.Value, PositionY = map.PositionY.Value, FontColor = map.FontColor,
                 FontFamily = map.FontFamily, FontSize = map.FontSize.Value, FontStyle = map.FontStyle, IsDropShadow = map.DropShadow.Value, IsRightToLeft = map.RightToLeft.Value,
                 IsVeritical = map.Vertical.Value,  Optacity = map.Opacity.Value, WatermarkText = map.WatermarkText});
            }

            foreach (var parameter in entity.BannerParameters)
            {
                bannerConfiguration.ContextParameters.Add(new BannerParameterDTO() { Name = parameter.ParameterName, Value = parameter.ParameterValue });
            }

            return bannerConfiguration;
        }

        public static List<BannerConfigurationDTO> ToDTO(List<BannerConfiguration> entities)
        {
            var dtos = new List<BannerConfigurationDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(BannerConfigurationDTO.ToDTO(entity));
            }

            return dtos;
        }

        public static BannerConfiguration ToEntity(BannerConfigurationDTO dto)
        {
            var entity = new BannerConfiguration()
            {
                BannerConfigurationIID = dto.BannerConfigurationIID,
                BannerID = dto.BannerID,
                BannerFileName = dto.BannerFileName
            };
            return entity;
        }

        public static List<BannerParameter> ToEntity(List<BannerParameterDTO> dtos, long configurationID)
        {
            var param = new List<BannerParameter>();

            if (dtos != null)
            {
                foreach (var dto in dtos)
                {
                    param.Add(BannerParameterDTO.ToEntity(dto, configurationID));
                }
            }

            return param;
        }
    }
}
