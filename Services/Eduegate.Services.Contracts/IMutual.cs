using System.Collections.Generic;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Search;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Metadata;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMutual" in both code and config file together.
    public interface IMutualService
    {
        EntityTypeRelationMapDTO SaveEntityTypeRelationMaps(EntityTypeRelationDTO dto);

        List<KeyValueDTO> GetEmployeeIdNameEntityTypeRelation(EntityTypeRelationDTO dto);

        List<KeyValueDTO> GetEntityPropertiesByType(int entityType);

        List<ContactDTO> CreateEntityProperties(List<ContactDTO> contactDTOList);

        List<KeyValueDTO> GetEntityTypeEntitlementByEntityType(EntityTypes entityType);

        SearchResultDTO ExecuteView(DynamicViews view, string IID1 = "", string IID2 = "");

        string GetNextTransactionNumber(long documentTypeID);

        List<KeyValueDTO> GetCustomerEntitlements(long customerID);  // GetCustomer Entitlements

        GeoLocationDTO GetGeoLocation();


        #region City

        CityDTO SaveCity(CityDTO dtoCity);

        CityDTO GetCity(long cityID);

        List<CityDTO> GetCityByCountryID(int countryID);

        #endregion


        #region Zone

        ZoneDTO SaveZone(ZoneDTO dtoZone);

        ZoneDTO GetZone(short zoneID);

        #endregion


        #region Area

        AreaDTO SaveArea(AreaDTO dtoArea);

        AreaDTO GetArea(int areaID);


        List<AreaDTO> GetAreaByCountryID(int countryID);

        List<AreaDTO> GetAreaByCityID(int cityID, int siteID, int countryID);



        #endregion

        #region Vehicle


        VehicleDTO SaveVehicle(VehicleDTO vehicleDetail);

        VehicleDTO GetVehicle(long vehicleID);

        #endregion



        #region Comment

        CommentDTO SaveComment(CommentDTO comment);

        CommentDTO GetComment(int commentID);

        List<CommentDTO> GetComments(Eduegate.Infrastructure.Enums.EntityTypes entityTypeID, long referenceID, long departmentID);

        List<CommentDTO> GetChats(Eduegate.Infrastructure.Enums.EntityTypes entityTypeID, long fromLoginID, long toLoginID, long referenceID, long departmentID );

        bool DeleteComment(int commentID);
        #endregion

        SupplierAccountMapDTO GetAccountBySupplierID(long? supplierID);

        List<AdditionalExpenseProvisionalAccountMapDTO> GetProvisionalAccountByAdditionalExpense(int? additionalExpenseID);


        AttachmentDTO SaveAttachment(AttachmentDTO comment);

        AttachmentDTO GetAttachment(int attachmentID);

        List<AttachmentDTO> GetAttachments(EntityTypes entityTypeID, long referenceID, long departmentID);

        bool DeleteAttachment(int attachmentID);

        #region Product Delivery Type

        bool SaveDeliveryCharges(List<ProductDeliveryTypeDTO> dtos, long IID, bool isProduct);

        #endregion
        bool SaveCustomerDeliveryCharges(List<CustomerGroupDeliveryChargeDTO> dtos, int customerGroupID);

        bool SaveZoneDeliveryCharges(List<ZoneDeliveryChargeDTO> dtos, short zoneID);

        bool SaveAreaDeliveryCharges(List<AreaDeliveryChargeDTO> dtos, int areaID);

        KeyValueDTO GetDocumentStatus(short statusID);

        List<KeyValueDTO> GetMainDesignation(int gdesig_code);

        List<KeyValueDTO> GetHRDesignation(int mdesig_code);

        List<KeyValueDTO> GetAllowancebyPayComp(int paycomp);

        List<KeyValueDTO> GetLocationByDept(int deptCode);

        List<KeyValueDTO> GetDocType(string fileUploadType);

        List<KeyValueDTO> GetAgent();

        EntitlementMapDTO GetEntitlementMaps(long supplierID, short supplier);

        string GetNextTransactionNumberByMonthYear(TransactionNumberDTO dto);

        List<ScreenShortCutDTO> GetShortCuts(long screenID);

        string GetNextSequence(string sequenceType);

        CommunicationDTO GetMailIDDetailsFromLead(long? leadID);

        CommunicationDTO GetEmailTemplateByID(int? templateID);

        List<AcademicYearDTO> GetAcademicYearListData();

        List<AcademicYearDTO> GetActiveAcademicYearListData();

        List<SchoolsDTO> GetSchoolsProfileWithAcademicYear();


    }
}