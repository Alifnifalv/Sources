using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Framework.Contracts.Common.BoilerPlates;
using Eduegate.Services.Contract;

namespace Eduegate.Service.Client
{
    public class BoilerPlateServiceClient : BaseClient, IBoilerPlateService
    {
        private static string _serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string _boilerPlateService = string.Concat(_serviceHost, Eduegate.Framework.Helper.Constants.BOILERPLATE_SERVICE_NAME);

        public BoilerPlateServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {
        }

        public Task<BoilerPlateDataSourceDTO> GetBoilerPlates(BoilerPlateInfo boilerPlateInfo)
        {
            var result = ServiceHelper.HttpPostRequest(_boilerPlateService + "GetBoilerPlates", boilerPlateInfo, _callContext);
            return Task.FromResult(JsonConvert.DeserializeObject<BoilerPlateDataSourceDTO>(result));
        }

        public BoilerPlateDTO SaveBoilerPlate(BoilerPlateDTO boilerPlateDTO)
        {
            var uri = string.Format("{0}/SaveBoilerPlate", _boilerPlateService);
            var result =  ServiceHelper.HttpPostRequest(uri,boilerPlateDTO,_callContext);
            return JsonConvert.DeserializeObject<BoilerPlateDTO>(result);
        }

        public BoilerPlateDTO GetBoilerPlate(string boilerPlateID)
        {
            var bannerUri = string.Format("{0}/{1}?boilerPlateID={2}", _boilerPlateService, "GetBoilerPlate", boilerPlateID);
            return ServiceHelper.HttpGetRequest<BoilerPlateDTO>(bannerUri, _callContext, _logger);
        }

        public List<BoilerPlateParameterDTO> GetBoilerPlateParameters(long boilerPlateID)
        {
            var bannerUri = string.Format("{0}/{1}?boilerPlateID={2}", _boilerPlateService, "GetBoilerPlateParameters", boilerPlateID);
            return ServiceHelper.HttpGetRequest<List<BoilerPlateParameterDTO>>(bannerUri, _callContext, _logger);
        }
        public List<PageBoilerplateReportDTO> GetBoilerPlateReports(long? boilerPlateID, long? pageBoilerPlateMapIID)
        {
            var reportsUri = string.Format("{0}/{1}?boilerPlateID={2}&pageBoilerPlateMapIID={3} ", _boilerPlateService, "GetBoilerPlateReports", boilerPlateID);
            return ServiceHelper.HttpGetRequest<List<PageBoilerplateReportDTO>>(reportsUri, _callContext, _logger);
        }

    }
}
