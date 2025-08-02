using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Services;
using Eduegate.Framework.Contracts.Common.BoilerPlates;
using System.Threading.Tasks;
using Eduegate.Services.Contract;

namespace Eduegate.Service.Client.Direct
{
    public class BoilerPlateServiceClient :  IBoilerPlateService
    {
        BoilerPlateService service = new BoilerPlateService();

        public BoilerPlateServiceClient(CallContext callContext = null, Action<string> logger = null)
        {
            service.CallContext = callContext;
        }

        public Task<BoilerPlateDataSourceDTO> GetBoilerPlates(BoilerPlateInfo boilerPlateInfo)
        {
            return service.GetBoilerPlates(boilerPlateInfo);
        }

        public BoilerPlateDTO SaveBoilerPlate(BoilerPlateDTO boilerPlateDTO)
        {
            return service.SaveBoilerPlate(boilerPlateDTO);
        }

        public BoilerPlateDTO GetBoilerPlate(string boilerPlateID)
        {
            return service.GetBoilerPlate(boilerPlateID);
        }

        public List<BoilerPlateParameterDTO> GetBoilerPlateParameters(long boilerPlateID)
        {
            return service.GetBoilerPlateParameters(boilerPlateID);
        }

        public List<PageBoilerplateReportDTO> GetBoilerPlateReports(long? boilerPlateID, long? pageBoilerPlateMapIID)
        {
            return service.GetBoilerPlateReports(boilerPlateID, pageBoilerPlateMapIID);
        }
    }
}
