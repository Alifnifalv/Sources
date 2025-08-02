using Microsoft.AspNetCore.Mvc.Rendering;
using Eduegate.Web.Library.ViewModels.Common;
using System.Text;
using System.Collections.Generic;

namespace Eduegate.ERP.Admin.Helpers
{
    public static class CRUDHtmlHelper
    {
        public static string AngularPipes(this IHtmlHelper helper, string fieldName, List<ScreenFieldSettingViewModel> fieldSettings)
        {
            if (fieldSettings == null)
            {
                return null;
            }

            var angularPipes = new StringBuilder();

            foreach (var setting in fieldSettings.
                Where(x => (x.FieldName != null && x.FieldName == fieldName) || (x.ModelName != null && x.ModelName == fieldName)))
            {
                if (!string.IsNullOrEmpty(setting.DefaultValue))
                {
                    switch (setting.DefaultValue.ToLower())
                    {
                        case "uppercase":
                            angularPipes.Append("uppercase");
                            break;
                        case "lowercase":
                            angularPipes.Append("lowercase");
                            break;
                        case "capitalize":
                            angularPipes.Append("capitalize");
                            break;
                    }
                }
            }

            return angularPipes.ToString();
        }
    }
}