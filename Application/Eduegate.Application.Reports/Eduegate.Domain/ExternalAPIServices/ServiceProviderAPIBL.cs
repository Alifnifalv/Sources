using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.ServiceProviders.Core;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.ExternalServices;

namespace Eduegate.Domain.ExternalAPIServices
{
    public class ServiceProviderAPIBL
    {
        private CallContext _callContext;
        public ServiceProviderAPIBL(CallContext context)
        {
            _callContext = context;
        }
        public string GetTracking(string referenceID)
        {
            var passKey = new SettingRepository().GetSettingDetail("SMSAPASSKEY").SettingValue;
            return new SMSAProvider(null, passKey).GetTracking(referenceID);
            //return null;
        }

        public string AddShipment(ServiceProviderShipmentDetailDTO dto)
        {
            var parameters = new KeyValueParameterDTOs();
            parameters.AddParameter("REFERENCENO", dto.ReferenceNo);
            parameters.AddParameter("SENTDATE", String.Format(DateTime.Now.ToString(), "yyyy-MM-dd"));
            parameters.AddParameter("IDNUMBER", "");
            parameters.AddParameter("CUSTOMERNAME", dto.CustomerName);
            parameters.AddParameter("COUNTRY", "KSA");
            parameters.AddParameter("CUSTOMERCITY", dto.City);
            parameters.AddParameter("CUSTOMERZIP", "");
            parameters.AddParameter("CUSTOMERPOBOX", "");
            parameters.AddParameter("CUSTOMERMOBILE", dto.Mobile);
            parameters.AddParameter("CUSTOMERTELE1", dto.Telephone);
            parameters.AddParameter("CUSTOMERTELE2", "");
            parameters.AddParameter("CUSTOMERADDRESS1", dto.Address);
            parameters.AddParameter("CUSTOMERADDRESS2", "");
            parameters.AddParameter("SHIPMENTTYPE", "DLV");
            parameters.AddParameter("NOOFPEICES", dto.NoOfPcs);
            parameters.AddParameter("CUSTOMEREMAIL", "");
            parameters.AddParameter("CARRYINGVALUE", "");
            parameters.AddParameter("CARRYINGCURRENCY", "");
            parameters.AddParameter("CODAMOUNT", dto.CODAmount);
            parameters.AddParameter("WEIGHT", dto.Weight);
            parameters.AddParameter("CUSTOMERVALUE", "");
            parameters.AddParameter("CUSTOMERCURRENCY", "");
            parameters.AddParameter("INSUREDAMOUNT", "");
            parameters.AddParameter("INSUREDCURRENCY", "");
            parameters.AddParameter("ITEMDESCRIPTION", dto.ItemDescription);

            var result = new ExternalAPIServices.ServiceProviderAPIBL(_callContext).AddShipment(parameters);

            // Save PDF here

            return parameters.GetParameterValue<string>("ReferenceNo");
        }

        private KeyValueParameterDTOs AddShipment(KeyValueParameterDTOs parameter)
        {
            var passKey = new SettingRepository().GetSettingDetail("SMSAPASSKEY").SettingValue;
            var provider = ServiceProviders.ServiceProvider.Provider(parameter, passKey);
            var result = provider.AddShipment();
            parameter.AddParameter("ReferenceNo", result);
            return parameter;
        }

        public byte[] GenerateAWBPDF(string referenceID)
        {
            var passKey = new SettingRepository().GetSettingDetail("SMSAPASSKEY").SettingValue;
            var pdfBytes = new SMSAProvider(null, passKey).GenerateAWBPDF(referenceID);
            return pdfBytes;
        }

        public List<KeyValueDTO> GetSMSACities()
        {
            var passKey = new SettingRepository().GetSettingDetail("SMSAPASSKEY").SettingValue;
            return new SMSAProvider(null, passKey).GetCities();
        }
    }
}
