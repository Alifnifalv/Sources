using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Domain
{
    public class DesignerBL
    {
        private DesginerRepository designerRepository = new DesginerRepository();

        public DesignerOverviewDTO GetDesignerDetail(int designerIID)
        {
            DesignerOverviewDTO _DesignerOverviewDTO = designerRepository.GetDesignerDetail(designerIID);
            return _DesignerOverviewDTO;
        }
    }
}
