using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.SmartView;

namespace Eduegate.Services
{
    public class SmartViewService : BaseService, ISmartViewService
    {
        public SmartTreeViewDTO GetProductTree(string categoryID)
        {
            try
            {
                return new SmartTreeViewBL(CallContext).GetProductTree(string.IsNullOrEmpty(categoryID)
                    ? (long?)null : long.Parse(categoryID));
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SmartTreeViewDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public SmartTreeViewDTO GetSmartTreeView(SmartViewType type, long? parentID, string searchText)
        {
            try
            {
                return new SmartTreeViewBL(CallContext).GetSmartTreeView(type, parentID, searchText);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SmartTreeViewDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
