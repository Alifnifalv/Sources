using Eduegate.Framework.Helpers;
using System;
using System.Globalization;
using System.Resources;

namespace Eduegate.ERP.Admin.Globalization
{
    public class ResourceHelper
    {
        private static ResourceManager resourceManager = new ResourceManager("Eduegate.ERP.Admin.Globalization.Resources", typeof(ResourceHelper).Assembly);
        public static string GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            string langugeCode = CultureHelper.GetCurrentCulture(); //"en";
            if (string.IsNullOrEmpty(langugeCode))
                langugeCode = CultureHelper.GetDefaultCulture();

            return GetValue(key, langugeCode);
            //return resourceManager.GetString(key, CultureInfo.CreateSpecificCulture(langugeCode));
        }

        public static string GetValue(string key, string langugeCode)
        {
            if (string.IsNullOrEmpty(langugeCode))
                langugeCode = "en";

            if (string.IsNullOrEmpty(key)) return null;

            try
            {
                return resourceManager.GetString(key, CultureInfo.CreateSpecificCulture(langugeCode));
            }
            catch (Exception ex)
            {
                Logger.LogHelper<ResourceHelper>.Fatal(ex.Message + "," + key + " not exists.", ex);
                return key;
            }
        }

        //public static string GetValue(string key, string langugeCode)
        //{
        //    if (langugeCode == null)
        //        langugeCode = "en";

        //    langugeCode = langugeCode == "ar" ? "ar" : "en";

        //    if (string.IsNullOrEmpty(key)) return null;

        //    try
        //    {
        //        return resourceManager.GetString(key, CultureInfo.CreateSpecificCulture(langugeCode));
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<ResourceHelper>.Fatal(ex.Message + "," + key + " not exists.", ex);
        //        return key;
        //    }
        //}
    }
}
