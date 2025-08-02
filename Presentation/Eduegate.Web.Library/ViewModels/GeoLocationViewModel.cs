using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Web.Library.ViewModels
{
    public class GeoLocationViewModel
    {
        public string ip { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string region_code { get; set; }
        public string region_name { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string time_zone { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public int metro_code { get; set; }

        public static GeoLocationDTO FromDTOListToViewModelList(GeoLocationViewModel geoLocationViewModel)
        {
            return new GeoLocationDTO()
            {
                ip = geoLocationViewModel.ip,
                country_code = geoLocationViewModel.country_code,
                country_name = geoLocationViewModel.country_name,
                region_code = geoLocationViewModel.region_code,
                region_name = geoLocationViewModel.region_name,
                city = geoLocationViewModel.city,
                zip_code = geoLocationViewModel.zip_code,
                time_zone = geoLocationViewModel.time_zone,
                latitude = geoLocationViewModel.latitude,
                longitude = geoLocationViewModel.longitude,
                metro_code = geoLocationViewModel.metro_code,
            };
        }
    }
}
