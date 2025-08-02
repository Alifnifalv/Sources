using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDesigner" in both code and config file together.
    public interface IDesigner
    {
        DesignerOverviewDTO GetDesignerDetail(int designerIID);
    }
}
