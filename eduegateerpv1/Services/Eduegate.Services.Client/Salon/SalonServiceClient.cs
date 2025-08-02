using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Service.Client;
using Eduegate.Services.Contracts.Salon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Client.Salon
{
    public class SalonServiceClient : BaseClient, ISalonService
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string accountService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.SALON_SERVICE);

        public SalonServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public ServiceDTO GetService(long serviceID)
        {
            throw new NotImplementedException();
        }

        public ServiceDTO SaveService(ServiceDTO service)
        {
            throw new NotImplementedException();
        }
    }
}
