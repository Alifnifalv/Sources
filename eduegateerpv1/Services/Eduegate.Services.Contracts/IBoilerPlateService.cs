using System.Collections.Generic;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common.BoilerPlates;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Services.Contract;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBoilerPlateService" in both code and config file together.
    public interface IBoilerPlateService
    {
        Task<BoilerPlateDataSourceDTO> GetBoilerPlates(BoilerPlateInfo boilerPlateInfo);

        BoilerPlateDTO SaveBoilerPlate(BoilerPlateDTO boilerPlateDTO);

        BoilerPlateDTO GetBoilerPlate(string boilerPlateID);

        List<BoilerPlateParameterDTO> GetBoilerPlateParameters(long boilerPlateID);

        List<PageBoilerplateReportDTO> GetBoilerPlateReports(long? boilerPlateID, long? pageBoilerPlateMapIID);


    }
}