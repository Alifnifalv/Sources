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
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Supports;
using Accounting = Eduegate.Services.Contracts.Accounting;

namespace Eduegate.Service.Client
{
    public class CustomerServiceClient : BaseClient, ICustomer
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.CUSTOMER_SERVICE_NAME);

        public CustomerServiceClient(CallContext context = null, Action<string> logger = null)
            :base(context, logger)
        {
        }

        public CustomerMembershipDTO GetCustomerMembership(string customerId)
        {
            throw new NotImplementedException();
        }

        public List<CustomerDTO> GetCustomers(string searchText, int dataSize)
        {
            throw new NotImplementedException();
        }

        public string AddSubscribe(string Email)
        {
            throw new NotImplementedException();
        }

        public string SubscribeConfirmation(string VarificationCode)
        {
            throw new NotImplementedException();
        }

        public CustomerDTO GetCustomer(string customerID)
        {
            var result = ServiceHelper.HttpGetRequest(service + "GetCustomer/" + customerID.ToString(), _callContext);
            return JsonConvert.DeserializeObject<CustomerDTO>(result);
        }

        public CustomerDTO SaveCustomer(CustomerDTO customerDTO)
        {
            return ServiceHelper.HttpPostGetRequest<CustomerDTO>(ServiceHost + Eduegate.Framework.Helper.Constants.CUSTOMER_SERVICE_NAME + "SaveCustomer/", customerDTO, _callContext);
        }

        public CustomerGroupDTO GetCustomerGroup(string customerGroupID)
        {
            return ServiceHelper.HttpGetRequest<CustomerGroupDTO>(ServiceHost + Eduegate.Framework.Helper.Constants.CUSTOMER_SERVICE_NAME + "GetCustomerGroup/" + customerGroupID, _callContext);
        }

        public CustomerGroupDTO SaveCustomerGroup(CustomerGroupDTO customerDTO)
        {
            return ServiceHelper.HttpPostGetRequest<CustomerGroupDTO>(ServiceHost + Eduegate.Framework.Helper.Constants.CUSTOMER_SERVICE_NAME + "SaveCustomerGroup/", customerDTO, _callContext);
        }

        public CustomerDTO IsCustomerExist(string email, string phone)
        {
            throw new NotImplementedException();
        }

        public List<CustomerDTO> GetCustomerByCustomerIdAndCR(string searchText)
        {
            throw new NotImplementedException();
        }

        public CustomerDTO GetCustomerByContactID(long contactID)
        {
            var result = ServiceHelper.HttpGetRequest(service + "GetCustomerByContactID/" + contactID, _callContext);
            return JsonConvert.DeserializeObject<CustomerDTO>(result);
        }

        public NewsletterDTO AddNewsletterSubscription(NewsletterSubscriptionDTO newsletterSubscriptionDTO)
        {
            var result = ServiceHelper.HttpPostRequest(service + "AddNewsletterSubscription", newsletterSubscriptionDTO, _callContext);
            return JsonConvert.DeserializeObject<NewsletterDTO>(result);
        }
        public bool AddCustomerSupportTicket(CustomerSupportTicketDTO ticketDTO)
        {
            var uri = string.Format("{0}/AddCustomerSupportTicket", service);
            var result = ServiceHelper.HttpPostRequest(uri, ticketDTO, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }
        public bool JustAskInsert(JustAskDTO dto)
        {
            var uri = string.Format("{0}/JustAskInsert", service);
            var result = ServiceHelper.HttpPostRequest(uri, dto, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }

        public bool JobInsert(UserJobApplicationDTO dto)
        {
            var uri = string.Format("{0}/JobInsert", service);
            var result = ServiceHelper.HttpPostRequest(uri, dto, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }

        public List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> GetCustomerAccountMaps(string CustomerID)
        {
            string uri = string.Concat(service + "\\GetCustomerAccountMaps?CustomerID=" + CustomerID);
            return ServiceHelper.HttpGetRequest<List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO>>(uri);
        }
        public List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> SaveCustomerAccountMaps(List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> SupplierAccountMapsDTOs)
        {
            string uri = string.Concat(service + "\\SaveCustomerAccountMaps");
            var request = ServiceHelper.HttpPostRequest(uri, SupplierAccountMapsDTOs);
            return JsonConvert.DeserializeObject<List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO>>(request);
        }

        public bool CheckContactMobileAvailability(long contactId, string mobileNumber)
        {
            string uri = string.Concat(service + "\\CheckContactMobileAvailability?contactId=" + contactId + "&mobileNumber=" + mobileNumber);
            return ServiceHelper.HttpGetRequest<bool>(uri);
        }


        public CustomerDTO GetCustomerDetailsLoyaltyPoints(long customerID)
        {
            var result = ServiceHelper.HttpGetRequest(service + "GetCustomerDetailsLoyaltyPoints?customerID=" + customerID, _callContext);
            return JsonConvert.DeserializeObject<CustomerDTO>(result);
        }

        public List<TransactionHeadDTO> GetTransactionHeadLoyaltyPoints(long customerID)
        {
            var result = ServiceHelper.HttpGetRequest(service + "GetTransactionHeadLoyaltyPoints?customerID=" + customerID, _callContext);
            return JsonConvert.DeserializeObject<List<TransactionHeadDTO>>(result);
        }
    }
}
