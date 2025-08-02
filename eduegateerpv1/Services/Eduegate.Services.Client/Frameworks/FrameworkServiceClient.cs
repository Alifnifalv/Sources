using Newtonsoft.Json;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Service.Client;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Client.Frameworks
{
    public class FrameworkServiceClient : BaseClient, IFrameworkService
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, "Frameworks/FrameworkService.svc/");

        public FrameworkServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {

        }

        public ScreenMetadataDTO GetScreenMetadata(long screenID)
        {
            string uri = string.Concat(Service + "\\GetScreenMetadata?screenID=" + screenID);
            return ServiceHelper.HttpGetRequest<ScreenMetadataDTO>(uri, _callContext, _logger);
        }

        public string GetScreenData(long screenID, long IID)
        {
            string uri = string.Concat(Service + "\\GetScreenData?screenID=" + screenID + "&IID=" + IID.ToString());
            return ServiceHelper.HttpGetRequest<string>(uri, _callContext, _logger);
        }

        public ScreenDataDTO SaveScreenData(ScreenDataDTO data)
        {
            string uri = string.Concat(Service + "\\SaveScreenData");
            return ServiceHelper.HttpPostGetRequest<ScreenDataDTO>(uri, data, _callContext, _logger);
        }

        public CRUDDataDTO SaveCRUDData(CRUDDataDTO data)
        {
            string uri = string.Concat(Service + "\\SaveCRUDData");
            return ServiceHelper.HttpPostGetRequest<CRUDDataDTO>(uri, data, _callContext, _logger);
        }

        public KeyValueDTO ValidateField(CRUDDataDTO data, string field)
        {
            string uri = string.Concat(Service + "\\ValidateField?field=" + field);
            string result = ServiceHelper.HttpPostSerializedObject(uri, JsonConvert.SerializeObject(data), _callContext);
            return JsonConvert.DeserializeObject<KeyValueDTO>(result);
        }

        public bool DeleteCRUDData(long screenID, long IID)
        {
            string uri = string.Concat(Service + "\\DeleteCRUDData?screenID=" + screenID + "&IID=" + IID);
            return ServiceHelper.HttpGetRequest<bool>(uri, _callContext, _logger);
        }

        public long CloneCRUDData(long screenID, long IID)
        {
            string uri = string.Concat(Service + "\\DeleteCRUDData?screenID=" + screenID + "&IID=" + IID);
            return ServiceHelper.HttpGetRequest<long>(uri, _callContext, _logger);
        }
    }
}
