using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity;

namespace Eduegate.Services.Contracts.Banner
{
    public class BannerParameterDTO
    {
        public long ParameterIID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public static BannerParameter ToEntity(BannerParameterDTO dto, long configurationID)
        {
            return new BannerParameter()
            {
                ParameterName = dto.Name,
                ParameterValue = dto.Value,
                ParameterIID = dto.ParameterIID,
                BannerConfigurationID = configurationID
            };
        }
    }
}
