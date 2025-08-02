using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Domain.Support;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Supports;

namespace Eduegate.Services
{
    public class Customer : BaseService, ICustomer
    {
        public CustomerMembershipDTO GetCustomerMembership(string customerId)
        {
            return new CustomerBL(this.CallContext).GetCustomerMembership(customerId);
        }

        public List<CustomerDTO> GetCustomers(string searchText, int dataSize)
        {
            return new CustomerBL(this.CallContext).GetCustomers(searchText, dataSize);
        }

        public string AddSubscribe(string Email)
        {
            return new CustomerBL(this.CallContext).AddSubscription(Email);
        }

        public string SubscribeConfirmation(string VarificationCode)
        {return new CustomerBL(this.CallContext).SubscribeConfirmation(VarificationCode);
        }

        public CustomerDTO GetCustomer(string customerID)
        {
            return new CustomerBL(this.CallContext).GetCustomerV2(long.Parse(customerID));
        }

        public CustomerDTO SaveCustomer(CustomerDTO customer)
        {
            return new CustomerBL(this.CallContext).SaveCustomer(customer);
        }

        public CustomerGroupDTO GetCustomerGroup(string customerGroupID)
        {
            return new CustomerBL(this.CallContext).GetCustomerGroup(long.Parse(customerGroupID));
        }

        public CustomerGroupDTO SaveCustomerGroup(CustomerGroupDTO customerGroup)
        {
            return new CustomerBL(this.CallContext).SaveCustomerGroup(customerGroup);
        }

        public CustomerDTO IsCustomerExist(string email, string phone)
        {
            return new CustomerBL(this.CallContext).IsCustomerExist(email,phone);
        }

        public List<CustomerDTO> GetCustomerByCustomerIdAndCR(string searchText)
        {
            return new CustomerBL(this.CallContext).GetCustomerByCustomerIdAndCR(searchText);
        }

        public CustomerDTO GetCustomerByContactID(long contactID)
        {
            return new CustomerBL(this.CallContext).GetCustomerByContactID(contactID);
        }

        public NewsletterDTO AddNewsletterSubscription(NewsletterSubscriptionDTO newsletterSubscriptionDTO)
        {
            return new CustomerBL(this.CallContext).AddNewsletterSubsciption(newsletterSubscriptionDTO.emailID, newsletterSubscriptionDTO.cultureID, this.CallContext.IPAddress);
        }

        public bool AddCustomerSupportTicket(CustomerSupportTicketDTO ticketDTO)
        {
            try
            {
                Eduegate.Logger.LogHelper<SupportService>.Info("Service Result : " + ticketDTO);
                return new SupportBL(CallContext).AddCustomerSupportTicket(ticketDTO);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SupportService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
        public bool JustAskInsert(JustAskDTO dto)
        {
            try
            {
                Eduegate.Logger.LogHelper<SupportService>.Info("Service Result : " + dto);
                return new SupportBL(CallContext).JustAskInsert(dto);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SupportService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool JobInsert(UserJobApplicationDTO dto)
        {
            try
            {
                Eduegate.Logger.LogHelper<SupportService>.Info("Service Result : " + dto);
                return new SupportBL(CallContext).JobInsert(dto);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SupportService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> GetCustomerAccountMaps(string CustomerID)
        {
            return new CustomerBL(this.CallContext).GetCustomerAccountMaps(long.Parse(CustomerID));
        }
        public List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> SaveCustomerAccountMaps(List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> SupplierAccountMapsDTOs)
        {
            return new CustomerBL(CallContext).SaveCustomerAccountMaps(SupplierAccountMapsDTOs);
        }

        public bool CheckContactMobileAvailability(long contactId, string mobileNumber)
        {
            return new CustomerBL(CallContext).CheckCustomerEmailIDAvailability(contactId, mobileNumber);
        }

        public CustomerDTO GetCustomerDetailsLoyaltyPoints(long customerID)
        {
            return new CustomerBL(this.CallContext).GetCustomerDetailsLoyaltyPoints(customerID);
        }

        public List<TransactionHeadDTO> GetTransactionHeadLoyaltyPoints(long customerID)
        {
            return new CustomerBL(this.CallContext).GetTransactionHeadLoyaltyPoints(customerID);
        }
    }
}
