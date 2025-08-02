using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Eduegate.Hub.Helpers
{
    public class MessageHelper
    {
        //public static async Task<SubscriptionDetail> MessageResolver(SubscriptionDetail detail)
        //{
        //    switch(detail.SubscriptionType)
        //    {
        //        case SubscriptionTypes.UserActive:
        //            var dynamicData = detail.Data as dynamic;
        //            if(dynamicData != null && dynamicData.callContext != null)
        //            {
        //                var callContext = JsonConvert.DeserializeObject<CallContext>(JsonConvert.SerializeObject(dynamicData.callContext));
        //                //get customer geo location
        //                string geoLocation = null;

        //                try
        //                {
        //                    geoLocation = await new UserServiceBL(null, null, null).GetUserGeoLocationByLogin(callContext.LoginID);
        //                }
        //                catch(Exception ex)
        //                {
        //                    Console.WriteLine(ex.Message);
        //                }

        //                return new SubscriptionDetail()
        //                {
        //                    SubscriptionType = SubscriptionTypes.UserActive,
        //                    Data = new
        //                    {
        //                        callContext.LoginID,
        //                        callContext.CustomerID,
        //                        callContext.SupplierID,
        //                        callContext.AppID,
        //                        dynamicData.DeviceDetails,
        //                        dynamicData.OSDetails,
        //                        GeoLocation = geoLocation
        //                    }
        //                };
        //            }

        //            break;
        //    }

        //    return detail;
        //}
    }
}
