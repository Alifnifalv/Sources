using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;

namespace Eduegate.Web.Library.ViewModels
{
    public class DataFeedTypeViewModel
    {
        private static string rootURL { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("RootUrl"); } }
        private static string documentVirtualPath { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsVirtualPath"); } }

        public int DataFeedTypeID { get; set; }
        public string Name { get; set; }
        public string TemplatePath { get; set; }

        public static DataFeedTypeViewModel FromDTO(DataFeedTypeDTO dataFeedTypeDTO)
        {
            DataFeedTypeViewModel dataFeedTypeViewModel = new DataFeedTypeViewModel();
            dataFeedTypeViewModel.DataFeedTypeID = dataFeedTypeDTO.DataFeedTypeID;
            dataFeedTypeViewModel.Name = dataFeedTypeDTO.Name;
            dataFeedTypeViewModel.TemplatePath = string.Concat(rootURL, documentVirtualPath, "DataFeed/", "Templates/", dataFeedTypeDTO.TemplateName);
            return dataFeedTypeViewModel;
        }
    }
}