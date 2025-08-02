using System.Collections.Generic;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Supports;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICustomer" in both code and config file together.
    public interface ICustomer
    {
        CustomerMembershipDTO GetCustomerMembership(string customerId);

        List<CustomerDTO> GetCustomers(string searchText, int dataSize);

        string AddSubscribe(string Email);

        string SubscribeConfirmation(string VarificationCode);

        CustomerDTO GetCustomer(string customerID);

        CustomerDTO SaveCustomer(CustomerDTO customerDTO);

        CustomerGroupDTO GetCustomerGroup(string customerGroupID);

        CustomerGroupDTO SaveCustomerGroup(CustomerGroupDTO customerDTO);

        CustomerDTO IsCustomerExist(string email, string phone);

        List<CustomerDTO> GetCustomerByCustomerIdAndCR(string searchText);

        CustomerDTO GetCustomerByContactID(long contactID);

        NewsletterDTO AddNewsletterSubscription(NewsletterSubscriptionDTO newsletterSubscriptionDTO);

        bool AddCustomerSupportTicket(CustomerSupportTicketDTO ticketDTO);

        bool JustAskInsert(JustAskDTO dto);
        
        bool JobInsert(UserJobApplicationDTO dto);

        List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> GetCustomerAccountMaps(string CustomerID);

        List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> SaveCustomerAccountMaps(List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> SupplierAccountMapsDTOs);

        bool CheckContactMobileAvailability(long contactId, string mobileNumber);

        CustomerDTO GetCustomerDetailsLoyaltyPoints(long customerID);

        List<TransactionHeadDTO> GetTransactionHeadLoyaltyPoints(long customerID);
    }
}