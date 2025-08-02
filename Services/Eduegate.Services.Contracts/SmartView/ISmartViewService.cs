using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.SmartView
{
    public interface ISmartViewService
    {
        SmartTreeViewDTO GetProductTree(string categoryID);

        SmartTreeViewDTO GetSmartTreeView(SmartViewType type, long? parentID, string searchText);
    }
}