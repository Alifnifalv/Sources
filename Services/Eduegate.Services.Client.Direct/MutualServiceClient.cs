using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services;
using Eduegate.Services.Contracts.Metadata;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Services.Contracts.Accounting;
using UglyToad.PdfPig.Content;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Service.Client.Direct
{
    public class MutualServiceClient : IMutualService
    {
        MutualService service = new MutualService();

        public MutualServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public EntityTypeRelationMapDTO SaveEntityTypeRelationMaps(EntityTypeRelationDTO dto)
        {
            return service.SaveEntityTypeRelationMaps(dto);
        }

        public List<KeyValueDTO> GetEmployeeIdNameEntityTypeRelation(EntityTypeRelationDTO dto)
        {
            return service.GetEmployeeIdNameEntityTypeRelation(dto);
        }

        public List<KeyValueDTO> GetEntityPropertiesByType(int entityType)
        {
            return service.GetEntityPropertiesByType(entityType);
        }

        public List<ContactDTO> CreateEntityProperties(List<ContactDTO> contactDTOList)
        {
            return service.CreateEntityProperties(contactDTOList);
        }

        public List<KeyValueDTO> GetEntityTypeEntitlementByEntityType(EntityTypes entityType)
        {
            return service.GetEntityTypeEntitlementByEntityType(entityType);
        }

        public Services.Contracts.Search.SearchResultDTO ExecuteView(DynamicViews view, string IID1 = "", string IID2 = "")
        {
            return service.ExecuteView(view, IID1, IID2);
        }

        public string GetNextTransactionNumber(long documentTypeID)
        {
            return service.GetNextTransactionNumber(documentTypeID);
        }

        public List<KeyValueDTO> GetCustomerEntitlements(long customerID)
        {
            return service.GetCustomerEntitlements(customerID);
        }

        public GeoLocationDTO GetGeoLocation()
        {
            return service.GetGeoLocation();
        }


        #region City

        public CityDTO SaveCity(CityDTO dtoCity)
        {
            return service.SaveCity(dtoCity);
        }

        public CityDTO GetCity(long cityID)
        {
            return service.GetCity(cityID);
        }

        #endregion


        #region Zone

        public ZoneDTO SaveZone(ZoneDTO dtoZone)
        {
            return service.SaveZone(dtoZone);
        }

        public ZoneDTO GetZone(short zoneID)
        {
            return service.GetZone(zoneID);
        }

        #endregion


        #region Aread

        public AreaDTO SaveArea(AreaDTO dtoArea)
        {
            return service.SaveArea(dtoArea);
        }

        public AreaDTO GetArea(int areaID)
        {
            return service.GetArea(areaID);
        }

        public List<AreaDTO> GetAreaByCountryID(int countryID)
        {
            return service.GetAreaByCountryID(countryID);
        }

        public List<CityDTO> GetCityByCountryID(int countryID)
        {
            return service.GetCityByCountryID(countryID);
        }

        public List<AreaDTO> GetAreaByCityID(int cityID, int siteID = 0, int countryID = 0)
        {
            return service.GetAreaByCityID(cityID, siteID, countryID);
        }

        #endregion

        #region Vehicle

        public VehicleDTO SaveVehicle(VehicleDTO vehicleDetail)
        {
            return service.SaveVehicle(vehicleDetail);
        }

        public VehicleDTO GetVehicle(long vehicleID)
        {
            return service.GetVehicle(vehicleID);
        }
        #endregion

        #region Comment
        public CommentDTO SaveComment(CommentDTO comment)
        {
            return service.SaveComment(comment);
        }

        public CommentDTO GetComment(int commentID)
        {
            return service.GetComment(commentID);
        }

        public List<CommentDTO> GetComments(Eduegate.Infrastructure.Enums.EntityTypes entityTypeID, long referenceID, long departmentID)
        {
            return service.GetComments(entityTypeID, referenceID, departmentID);
        }


        public List<CommentDTO> GetChats(
            Eduegate.Infrastructure.Enums.EntityTypes entityTypeID,
            long referenceID,
            long? fromLoginID,
            long? toLoginID,
            int studentID,
            long departmentID = 0,
            int page = 0,
            int pageSize = 0)
        {
            return service.GetChats(entityTypeID, fromLoginID, toLoginID, page, pageSize, studentID);
        }

        public bool DeleteComment(int commentID)
        {
            return service.DeleteComment(commentID);
        }

        #endregion

        #region  Product Delivery Type

        public bool SaveDeliveryCharges(List<ProductDeliveryTypeDTO> dtos, long IID, bool isProduct)
        {
            return service.SaveDeliveryCharges(dtos, IID, isProduct);
        }

        public bool SaveCustomerDeliveryCharges(List<CustomerGroupDeliveryChargeDTO> dtos, int customerGroupID)
        {
            return service.SaveCustomerDeliveryCharges(dtos, customerGroupID);
        }

        public bool SaveZoneDeliveryCharges(List<ZoneDeliveryChargeDTO> dtos, short zoneID)
        {
            return service.SaveZoneDeliveryCharges(dtos, zoneID);
        }

        public bool SaveAreaDeliveryCharges(List<AreaDeliveryChargeDTO> dtos, int areaID)
        {
            return service.SaveAreaDeliveryCharges(dtos, areaID);
        }
        #endregion

        public SupplierAccountMapDTO GetAccountBySupplierID(long? supplierID)
        {
            return service.GetAccountBySupplierID(supplierID);
        }
        public List<AdditionalExpenseProvisionalAccountMapDTO> GetProvisionalAccountByAdditionalExpense(int? additionalExpenseID)
        {
            return service.GetProvisionalAccountByAdditionalExpense(additionalExpenseID);
        }
        public KeyValueDTO GetDocumentStatus(short statusID)
        {
            return service.GetDocumentStatus(statusID);
        }

        public List<KeyValueDTO> GetMainDesignation(int gdesig_code)
        {
            return service.GetMainDesignation(gdesig_code);
        }

        public List<KeyValueDTO> GetHRDesignation(int mdesig_code)
        {
            return service.GetHRDesignation(mdesig_code);
        }

        public List<KeyValueDTO> GetAllowancebyPayComp(int paycomp)
        {
            return service.GetAllowancebyPayComp(paycomp);
        }

        public List<KeyValueDTO> GetLocationByDept(int deptCode)
        {
            return service.GetLocationByDept(deptCode);
        }

        public List<KeyValueDTO> GetDocType(string fileUploadType)
        {
            return service.GetDocType(fileUploadType);
        }

        public List<KeyValueDTO> GetAgent()
        {
            return service.GetAgent();
        }

        public EntitlementMapDTO GetEntitlementMaps(long supplierID, short supplier)
        {
            return service.GetEntitlementMaps(supplierID, supplier);
        }

        public string GetNextTransactionNumberByMonthYear(TransactionNumberDTO dto)
        {
            return service.GetNextTransactionNumberByMonthYear(dto);
        }

        public AttachmentDTO SaveAttachment(AttachmentDTO comment)
        {
            return service.SaveAttachment(comment);
        }

        public AttachmentDTO GetAttachment(int attachmentID)
        {
            return service.GetAttachment(attachmentID);
        }

        public List<AttachmentDTO> GetAttachments(EntityTypes entityTypeID, long referenceID, long departmentID)
        {
            return service.GetAttachments(entityTypeID, referenceID, departmentID);
        }

        public bool DeleteAttachment(int attachmentID)
        {
            return service.DeleteAttachment(attachmentID);
        }

        public List<ScreenShortCutDTO> GetShortCuts(long screenID)
        {
            return service.GetShortCuts(screenID);
        }

        public string GetNextSequence(string sequenceType)
        {
            return service.GetNextSequence(sequenceType);
        }

        public CommunicationDTO GetMailIDDetailsFromLead(long? leadID)
        {
            return service.GetMailIDDetailsFromLead(leadID);
        }

        public CommunicationDTO GetEmailTemplateByID(int? templateID)
        {
            return service.GetEmailTemplateByID(templateID);
        }

        public List<AcademicYearDTO> GetAcademicYearListData()
        {
            return service.GetAcademicYearListData();
        }

        public List<AcademicYearDTO> GetActiveAcademicYearListData()
        {
            return service.GetActiveAcademicYearListData();
        }


        public List<SchoolsDTO> GetSchoolsProfileWithAcademicYear()
        {
            return service.GetSchoolsProfileWithAcademicYear();
        }

        public List<CommentDTO> GetChats(Infrastructure.Enums.EntityTypes entityTypeID, long fromLoginID, long toLoginID, long referenceID, long departmentID)
        {
            return ((IMutualService)service).GetChats(entityTypeID, fromLoginID, toLoginID, referenceID, departmentID);
        }
    }
}