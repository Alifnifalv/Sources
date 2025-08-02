using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Service.Client;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.SmartView;
using System;

namespace Eduegate.Services.Client.Frameworks
{
    public class SmartViewServiceClient : BaseClient, ISmartViewService
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, "Frameworks/SmartViewService.svc/");

        public SmartViewServiceClient(CallContext callContext = null, Action<string> logger = null)
           : base(callContext, logger)
        {

        }

        public SmartTreeViewDTO GetProductTree(string categoryID)
        {
            string uri = string.Concat(Service + "\\GetProductTree?categoryID=" + categoryID);
            return ServiceHelper.HttpGetRequest<SmartTreeViewDTO>(uri, _callContext, _logger);
        }

        public SmartTreeViewDTO GetSmartTreeView(SmartViewType type, long? parentID, string searchText )
        {
            string uri = string.Concat(Service + "\\GetSmartTreeView?type=" 
                + type + "&parentID=" + parentID + "&searchText=" + searchText);
            return ServiceHelper.HttpGetRequest<SmartTreeViewDTO>(uri, _callContext, _logger);
        }
    }
}
