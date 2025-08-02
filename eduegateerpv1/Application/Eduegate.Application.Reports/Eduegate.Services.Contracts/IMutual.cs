using System.Collections.Generic;
using System.ServiceModel;
using Eduegate.Services.Contracts.Mutual;
using System.ServiceModel.Web;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Search;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Metadata;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMutual" in both code and config file together.
    [ServiceContract]
    public interface IMutualService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveEntityTypeRelationMaps")]
        EntityTypeRelationMapDTO SaveEntityTypeRelationMaps(EntityTypeRelationDTO dto);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetEmployeeIdNameEntityTypeRelation")]
        List<KeyValueDTO> GetEmployeeIdNameEntityTypeRelation(EntityTypeRelationDTO dto);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEntityPropertiesByType?entityType={entityType}")]
        List<KeyValueDTO> GetEntityPropertiesByType(int entityType);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CreateEntityProperties")]
        List<ContactDTO> CreateEntityProperties(List<ContactDTO> contactDTOList);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEntityTypeEntitlementByEntityType?entityType={entityType}")]
        List<KeyValueDTO> GetEntityTypeEntitlementByEntityType(EntityTypes entityType);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ExecuteView?view={view}&IID1={IID1}&IID2={IID2}")]
        SearchResultDTO ExecuteView(DynamicViews view, string IID1 = "", string IID2 = "");

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetNextTransactionNumber?documentTypeID={documentTypeID}")]
        string GetNextTransactionNumber(long documentTypeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCustomerEntitlements?customerID={customerID}")]
        List<KeyValueDTO> GetCustomerEntitlements(long customerID);  // GetCustomer Entitlements

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetGeoLocation")]
        GeoLocationDTO GetGeoLocation();


        #region City

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCity")]
        CityDTO SaveCity(CityDTO dtoCity);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCity?cityID={cityID}")]
        CityDTO GetCity(long cityID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCityByCountryID?countryID={countryID}")]
        List<CityDTO> GetCityByCountryID(int countryID);

        #endregion


        #region Zone

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveZone")]
        ZoneDTO SaveZone(ZoneDTO dtoZone);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetZone?zoneID={zoneID}")]
        ZoneDTO GetZone(short zoneID);

        #endregion


        #region Area

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveArea")]
        AreaDTO SaveArea(AreaDTO dtoArea);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetArea?areaID={areaID}")]
        AreaDTO GetArea(int areaID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAreaByCountryID?countryID={countryID}")]
        List<AreaDTO> GetAreaByCountryID(int countryID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAreaByCityID?cityID={cityID}&siteID={siteID}&countryID={countryID}")]
        List<AreaDTO> GetAreaByCityID(int cityID, int siteID, int countryID);



        #endregion

        #region Vehicle


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveVehicle")]
        VehicleDTO SaveVehicle(VehicleDTO vehicleDetail);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetVehicle?vehicleID={vehicleID}")]
        VehicleDTO GetVehicle(long vehicleID);

        #endregion



        #region Comment

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveComment")]
        CommentDTO SaveComment(CommentDTO comment);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetComment?commentID={commentID}")]
        CommentDTO GetComment(int commentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetComments?entityTypeID={entityTypeID}&referenceID={referenceID}&departmentID={departmentID}")]
        List<CommentDTO> GetComments(Eduegate.Infrastructure.Enums.EntityTypes entityTypeID, long referenceID, long departmentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteComment?commentID={commentID}")]
        bool DeleteComment(int commentID);
        #endregion

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAccountBySupplierID?supplierID={supplierID}")]
        SupplierAccountMapDTO GetAccountBySupplierID(long? supplierID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProvisionalAccountByAdditionalExpense?additionalExpenseID={additionalExpenseID}")]
        List<AdditionalExpenseProvisionalAccountMapDTO> GetProvisionalAccountByAdditionalExpense(int? additionalExpenseID);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
          RequestFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveAttachment")]
        AttachmentDTO SaveAttachment(AttachmentDTO comment);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAttachment?attachmentID={attachmentID}")]
        AttachmentDTO GetAttachment(int attachmentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAttachments?entityTypeID={entityTypeID}&referenceID={referenceID}&departmentID={departmentID}")]
        List<AttachmentDTO> GetAttachments(EntityTypes entityTypeID, long referenceID, long departmentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteAttachment?attachmentID={attachmentID}")]
        bool DeleteAttachment(int attachmentID);

        #region Product Delivery Type

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
                                BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveDeliveryCharges?IID={IID}&isProduct={isProduct}")]
        bool SaveDeliveryCharges(List<ProductDeliveryTypeDTO> dtos, long IID, bool isProduct);

        #endregion
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
                              BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCustomerDeliveryCharges?customerGroupID={customerGroupID}")]
        bool SaveCustomerDeliveryCharges(List<CustomerGroupDeliveryChargeDTO> dtos, int customerGroupID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
                             BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveZoneDeliveryCharges?zoneID={zoneID}")]
        bool SaveZoneDeliveryCharges(List<ZoneDeliveryChargeDTO> dtos, short zoneID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
                           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveAreaDeliveryCharges?areaID={areaID}")]
        bool SaveAreaDeliveryCharges(List<AreaDeliveryChargeDTO> dtos, int areaID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDocumentStatus?statusID={statusID}")]
        KeyValueDTO GetDocumentStatus(short statusID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMainDesignation?gdesig_code={gdesig_code}")]
        List<KeyValueDTO> GetMainDesignation(int gdesig_code);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetHRDesignation?mdesig_code={mdesig_code}")]
        List<KeyValueDTO> GetHRDesignation(int mdesig_code);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllowancebyPayComp?paycomp={paycomp}")]
        List<KeyValueDTO> GetAllowancebyPayComp(int paycomp);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetLocationByDept?deptCode={deptCode}")]
        List<KeyValueDTO> GetLocationByDept(int deptCode);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDocType?fileUploadType={fileUploadType}")]
        List<KeyValueDTO> GetDocType(string fileUploadType);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAgent")]
        List<KeyValueDTO> GetAgent();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEntitlementMaps?supplierID={supplierID}&supplier={supplier}")]
        EntitlementMapDTO GetEntitlementMaps(long supplierID, short supplier);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetNextTransactionNumberByMonthYear")]
        string GetNextTransactionNumberByMonthYear(TransactionNumberDTO dto);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetShortCuts?screenID={screenID}")]
        List<ScreenShortCutDTO> GetShortCuts(long screenID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetNextSequence?sequenceType={sequenceType}")]
        string GetNextSequence(string sequenceType);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetMailIDDetailsFromLead?leadID={leadID})")]
        CommunicationDTO GetMailIDDetailsFromLead(long? leadID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEmailTemplateByID?templateID={templateID})")]
        CommunicationDTO GetEmailTemplateByID(int? templateID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAcademicYearListData")]
        List<AcademicYearDTO> GetAcademicYearListData();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetActiveAcademicYearListData")]
        List<AcademicYearDTO> GetActiveAcademicYearListData();

    }
}