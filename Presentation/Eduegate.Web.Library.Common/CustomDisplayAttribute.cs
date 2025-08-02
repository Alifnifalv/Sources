using System.ComponentModel;

namespace Eduegate.Web.Library.Common
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class CustomDisplayAttribute : DisplayNameAttribute
    {
        public string DisplayText { get; set; }

        public CustomDisplayAttribute(string displayText)
        {
            var resourceValue = Eduegate.ERP.Admin.Globalization.ResourceHelper.GetValue(displayText);
            if (string.IsNullOrEmpty(resourceValue))
            {
                resourceValue = displayText;
            }

            this.DisplayText = resourceValue;
        }

        public override string DisplayName => this.DisplayText;
    }
}
