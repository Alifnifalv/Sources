using Eduegate.Framework;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.SmartView;
using System;

namespace Eduegate.Services.Client.Direct.Frameworks
{
    public class SmartViewServiceClient : ISmartViewService
    {
        SmartViewService service = new SmartViewService();

        public SmartViewServiceClient(CallContext callContext = null, Action<string> logger = null)
        {
            service.CallContext = callContext;
        }

        public SmartTreeViewDTO GetProductTree(string categoryID)
        {
            return service.GetProductTree(categoryID);
        }

        public SmartTreeViewDTO GetSmartTreeView(SmartViewType type, long? parentID, string searchText)
        {
            return service.GetSmartTreeView(type, parentID, searchText);
        }
    }
}
