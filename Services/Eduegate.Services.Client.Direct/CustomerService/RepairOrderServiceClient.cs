using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.CustomerService;

namespace Eduegate.Service.Client.Direct.CustomerService
{
    public class RepairOrderServiceClient : BaseClient, IRepairOrderService
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string orderService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.REPAIR_ORDER_SERVICE);

        public RepairOrderServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {
        }

        public Services.Contracts.Search.SearchResultDTO GetRepaidOrders(int currentPage, int pageSize, string orderBy)
        {
            var uri = string.Format("{0}/{1}?currentPage={2}&pageSize={3}&orderBy={4}", orderService, "GetRepaidOrders", currentPage, pageSize, orderBy);
            return ServiceHelper.HttpGetRequest<Services.Contracts.Search.SearchResultDTO>(uri, _callContext, _logger);
        }

        public Services.Contracts.Search.SearchResultDTO GetRepairOrderSummary()
        {
            var uri = string.Format("{0}/{1}", orderService, "GetRepairOrderSummary");
            return ServiceHelper.HttpGetRequest<Services.Contracts.Search.SearchResultDTO>(uri, _callContext, _logger);
        }

        public RepairOrderDTO GetRepaidOrder(int orderID)
        {
            var uri = string.Format("{0}/{1}?orderID={2}", orderService, "GetRepaidOrder", orderID);
            return ServiceHelper.HttpGetRequest<RepairOrderDTO>(uri, _callContext, _logger);
        }

        public RepairOrderDTO SaveRepairOrder(RepairOrderDTO orderDTO)
        {
            var uri = string.Format("{0}/SaveRepairOrder", orderService);
            var result = (ServiceHelper.HttpPostRequest(uri, orderDTO, _callContext));
            return JsonConvert.DeserializeObject<RepairOrderDTO>(result);
        }

        public RepairVehicleDTO GetVehcileDetails(string chasisNo, string regitrationNo)
        {
            var uri = string.Format("{0}/{1}?chasisNo={2}&regitrationNo={3}", orderService, "GetVehcileDetails", chasisNo, regitrationNo);
            return ServiceHelper.HttpGetRequest<RepairVehicleDTO>(uri, _callContext, _logger);
        }


        public Services.Contracts.Search.SearchResultDTO GetCustomers(int currentPage, int pageSize, string orderBy)
        {
            var uri = string.Format("{0}/{1}?currentPage={2}&pageSize={3}&orderBy={4}", orderService, "GetCustomers", currentPage, pageSize, orderBy);
            return ServiceHelper.HttpGetRequest<Services.Contracts.Search.SearchResultDTO>(uri, _callContext, _logger);
        }
    }
}
