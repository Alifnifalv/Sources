using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Supports;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICustomer" in both code and config file together.
    [ServiceContract]
    public interface ICustomer
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CustomerMembership/{CustomerId}")]
        CustomerMembershipDTO GetCustomerMembership(string customerId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomers?searchText={searchText}&dataSize={dataSize}")]
        List<CustomerDTO> GetCustomers(string searchText, int dataSize);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddSubscribe/{Email}")]
        string AddSubscribe(string Email);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SubscribeConfirmation/{VarificationCode}")]
        string SubscribeConfirmation(string VarificationCode);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomer/{CustomerID}/")]
        CustomerDTO GetCustomer(string customerID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCustomer")]
        CustomerDTO SaveCustomer(CustomerDTO customerDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomerGroup/{CustomerGroupID}/")]
        CustomerGroupDTO GetCustomerGroup(string customerGroupID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCustomerGroup")]
        CustomerGroupDTO SaveCustomerGroup(CustomerGroupDTO customerDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "IsCustomerExist?email={email}&phone={phone}")]
        CustomerDTO IsCustomerExist(string email, string phone);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomerByCustomerIdAndCR?searchText={searchText}")]
        List<CustomerDTO> GetCustomerByCustomerIdAndCR(string searchText);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomerByContactID?contactID={contactID}")]
        CustomerDTO GetCustomerByContactID(long contactID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddNewsletterSubscription")]
        NewsletterDTO AddNewsletterSubscription(NewsletterSubscriptionDTO newsletterSubscriptionDTO);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddCustomerSupportTicket")]
        bool AddCustomerSupportTicket(CustomerSupportTicketDTO ticketDTO);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "JustAskInsert")]
        bool JustAskInsert(JustAskDTO dto);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "JobInsert")]
        bool JobInsert(UserJobApplicationDTO dto);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomerAccountMaps?CustomerID={CustomerID}")]
        List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> GetCustomerAccountMaps(string CustomerID);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCustomerAccountMaps")]
        List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> SaveCustomerAccountMaps(List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> SupplierAccountMapsDTOs);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CheckContactMobileAvailability?contactId={contactId}&mobileNumber={mobileNumber}")]
        bool CheckContactMobileAvailability(long contactId, string mobileNumber);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomerDetailsLoyaltyPoints?customerID={customerID}")]
        CustomerDTO GetCustomerDetailsLoyaltyPoints(long customerID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTransactionHeadLoyaltyPoints?customerID={customerID}")]
        List<TransactionHeadDTO> GetTransactionHeadLoyaltyPoints(long customerID);

    }
}
