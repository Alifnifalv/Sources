using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.ExternalServices;

namespace Eduegate.Service.Client.ExternalServices
{
    public class ServiceProviderAPIServiceClient : BaseClient, IServiceProviderAPIServices
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string service = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.SERVICE_PROVIDER_API_SERVICE);

        public ServiceProviderAPIServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public string GetTracking(string referenceID)
        {
            var uri = string.Format("{0}GetTracking?referenceID={1}", service, referenceID);
            return ServiceHelper.HttpGetRequest<string>(uri, _callContext, _logger);
        }

        public string AddShipment(ServiceProviderShipmentDetailDTO dto)
        {
            var uri = string.Format("{0}AddShipment", service);
            return ServiceHelper.HttpPostRequest<ServiceProviderShipmentDetailDTO>(uri, dto, _callContext);
        }

        public byte[] GenerateAWBPDF(string referenceID)
        {
            var uri = string.Format("{0}GenerateAWBPDF?referenceID={1}", service, referenceID);
            return ServiceHelper.HttpGetRequest<byte[]>(uri, _callContext, _logger);
        }

        public List<KeyValueDTO> GetSMSACities()
        {
            var uri = string.Format("{0}/{1}", service, "GetSMSACities");
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext, _logger);
        }
    }
}
