using Eduegate.Domain;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Services
{
    public class Designer : IDesigner
    {
        DesignerBL designerBL = new DesignerBL();

        //Getting the designer details based on selected from designer menu
        public DesignerOverviewDTO GetDesignerDetail(int designerIID)
        {
            DesignerOverviewDTO designerOverviewDTO = designerBL.GetDesignerDetail(designerIID);
            return designerOverviewDTO;
        }
    }
}
