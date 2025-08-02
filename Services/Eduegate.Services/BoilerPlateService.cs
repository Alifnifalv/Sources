using System.ServiceModel;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Services;
using Eduegate.Domain;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Framework.Contracts.Common.BoilerPlates;
using Eduegate.Services.Contract;

namespace Eduegate.Services
{
    public class BoilerPlateService : BaseService , IBoilerPlateService
    {
        public async Task<BoilerPlateDataSourceDTO> GetBoilerPlates(BoilerPlateInfo boilerPlateInfo)
        {
            return await new BoilerPlateBL(CallContext).GetBoilerPlates(boilerPlateInfo);
        }

        public BoilerPlateDTO SaveBoilerPlate(BoilerPlateDTO boilerPlateDTO)
        {
            return new BoilerPlateBL(CallContext).SaveBoilerPlate(boilerPlateDTO);
        }

        public List<PageBoilerplateReportDTO> GetBoilerPlateReports(long? boilerPlateID, long? pageBoilerPlateMapIID)
        {
            return new BoilerPlateBL(CallContext).GetBoilerPlateReports(boilerPlateID, pageBoilerPlateMapIID);
        }

        public BoilerPlateDTO GetBoilerPlate(string boilerPlateID)
        {
            try
            {
                return new BoilerPlateBL(this.CallContext).GetBoilerPlate(long.Parse(boilerPlateID));
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<BoilerPlateService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<BoilerPlateParameterDTO> GetBoilerPlateParameters(long boilerPlateID)
        {
            try
            {
                return new BoilerPlateBL(this.CallContext).GetBoilerPlateParameters(boilerPlateID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<List<BoilerPlateParameterDTO>>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
