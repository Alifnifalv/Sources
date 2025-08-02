using Eduegate.Framework;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.Setting.Settings;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Eduegate.ERP.Admin.Helpers
{
    public class MetadataHelper
    {
        public static List<ScreenFieldViewModel> GetColumnByScreenID(CallContext callContext, int screenID)
        {
            var fields = new List<ScreenFieldViewModel>();
            var metaData = ClientFactory
                 .FrameworkServiceClient(callContext)
                 .GetScreenMetadata(screenID);

            if (metaData.ScreenTypeID == 1)
            {
                var viewModel = CRUDViewModel.FromDTO(metaData);
                fields.AddRange(MetadataHelper.GetFieldsFromModel(viewModel.ViewModel, "header"));
            }
            else
            {
                var viewModel = CRUDMasterDetailViewModel.FromDTO(metaData);
                fields.AddRange(MetadataHelper.GetFieldsFromModel(viewModel.MasterViewModel, "header"));
                fields.AddRange(MetadataHelper.GetFieldsFromModel(viewModel.DetailViewModel, "grid"));
            }

            return fields;
        }

        public static List<ScreenFieldViewModel> GetFieldsFromModel(object model, string type)
        {
            var fields = new List<ScreenFieldViewModel>();
            var orderBy = (OrderAttribute)model.GetType().GetCustomAttributes(typeof(OrderAttribute)).FirstOrDefault();
            IEnumerable<PropertyInfo> properties = null;

            if (orderBy == null)
            {
                properties = model.GetType().GetProperties();
            }
            else
            {
                properties = model.GetType().GetProperties().Where(a => a.GetCustomAttribute(typeof(OrderAttribute), false) != null).OrderBy(a => (a.GetCustomAttribute(typeof(OrderAttribute), false) as OrderAttribute).Order);
            }

            foreach (var property in properties)
            {
                var title = (System.ComponentModel.DisplayNameAttribute)property.GetCustomAttributes(typeof(System.ComponentModel.DisplayNameAttribute)).FirstOrDefault();
                var containerType = (ContainerTypeAttribute)property.GetCustomAttributes(typeof(ContainerTypeAttribute)).FirstOrDefault(); // make sure this is a field

                if (title != null && containerType == null &&
                    !string.IsNullOrEmpty(title.DisplayName))
                {
                    fields.Add(new ScreenFieldViewModel()
                    {
                        ModelName = property.Name,
                        FieldName = title.DisplayName,
                        FieldType = type,
                    });
                }
            }

            return fields;
        }
    }
}
