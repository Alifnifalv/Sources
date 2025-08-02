using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Helpers
{
    public class GeoLocationHelper
    {
        public static double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            // haversine great circle distance approximation, returns meters
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2))
                    + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2))
                    * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60; // 60 nautical miles per degree of seperation
            dist = dist * 1852; // 1852 meters per nautical mile
            return (dist);
        }

        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double rad2deg(double rad)
        {
            return (rad * 180.0 / Math.PI);
        }
    }
}
