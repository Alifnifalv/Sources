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
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class CustomerServiceClient : ICustomer
    {
        Customer service = new Customer();

        public CustomerServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public CustomerMembershipDTO GetCustomerMembership(string customerId)
        {
            return service.GetCustomerMembership(customerId);
        }

        public List<CustomerDTO> GetCustomers(string searchText, int dataSize)
        {
            return service.GetCustomers(searchText, dataSize);
        }

        public string AddSubscribe(string Email)
        {
            return service.AddSubscribe(Email);
        }

        public string SubscribeConfirmation(string VarificationCode)
        {
            return service.SubscribeConfirmation(VarificationCode);
        }

        public CustomerDTO GetCustomer(string customerID)
        {
            return service.GetCustomer(customerID);
        }

        public CustomerDTO SaveCustomer(CustomerDTO customerDTO)
        {
            return service.SaveCustomer(customerDTO);
        }

        public CustomerGroupDTO GetCustomerGroup(string customerGroupID)
        {
            return service.GetCustomerGroup(customerGroupID);
        }

        public CustomerGroupDTO SaveCustomerGroup(CustomerGroupDTO customerDTO)
        {
            return service.SaveCustomerGroup(customerDTO);
        }

        public CustomerDTO IsCustomerExist(string email, string phone)
        {
            return service.IsCustomerExist(email, phone);
        }

        public List<CustomerDTO> GetCustomerByCustomerIdAndCR(string searchText)
        {
            return service.GetCustomerByCustomerIdAndCR(searchText);
        }

        public CustomerDTO GetCustomerByContactID(long contactID)
        {
            return service.GetCustomerByContactID(contactID);
        }

        public NewsletterDTO AddNewsletterSubscription(NewsletterSubscriptionDTO newsletterSubscriptionDTO)
        {
            return service.AddNewsletterSubscription(newsletterSubscriptionDTO);
        }
        public bool AddCustomerSupportTicket(CustomerSupportTicketDTO ticketDTO)
        {
            return service.AddCustomerSupportTicket(ticketDTO);
        }
        public bool JustAskInsert(JustAskDTO dto)
        {
            return service.JustAskInsert(dto);
        }

        public bool JobInsert(UserJobApplicationDTO dto)
        {
            return service.JobInsert(dto);
        }

        public List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> GetCustomerAccountMaps(string CustomerID)
        {
            return service.GetCustomerAccountMaps(CustomerID);
        }
        public List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> SaveCustomerAccountMaps(List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> SupplierAccountMapsDTOs)
        {
            return service.SaveCustomerAccountMaps(SupplierAccountMapsDTOs);
        }

        public bool CheckContactMobileAvailability(long contactId, string mobileNumber)
        {
            return service.CheckContactMobileAvailability(contactId, mobileNumber);
        }


        public CustomerDTO GetCustomerDetailsLoyaltyPoints(long customerID)
        {
            return service.GetCustomerDetailsLoyaltyPoints(customerID);
        }

        public List<TransactionHeadDTO> GetTransactionHeadLoyaltyPoints(long customerID)
        {
            return service.GetTransactionHeadLoyaltyPoints(customerID);
        }
    }
}
