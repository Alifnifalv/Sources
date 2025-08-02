using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Metadata;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Service.Client
{
    public class MutualServiceClient : BaseClient, IMutualService
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.Mutual_SERVICE_NAME);

        public MutualServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public EntityTypeRelationMapDTO SaveEntityTypeRelationMaps(EntityTypeRelationDTO dto)
        {
            return JsonConvert.DeserializeObject<EntityTypeRelationMapDTO>(ServiceHelper.HttpPostRequest<EntityTypeRelationDTO>(Service + "SaveEntityTypeRelationMaps", dto, _callContext));
        }


        public List<KeyValueDTO> GetEmployeeIdNameEntityTypeRelation(EntityTypeRelationDTO dto)
        {
            return JsonConvert.DeserializeObject<List<KeyValueDTO>>(ServiceHelper.HttpPostRequest<EntityTypeRelationDTO>(Service + "GetEmployeeIdNameEntityTypeRelation", dto, _callContext));
        }

        public List<KeyValueDTO> GetEntityPropertiesByType(int entityType)
        {
            var uri = string.Format("{0}/GetEntityPropertiesByType?entityType={1}", Service, entityType);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext, _logger);
        }

        public List<ContactDTO> CreateEntityProperties(List<ContactDTO> contactDTOList)
        {
            var result = ServiceHelper.HttpPostRequest(Service + "CreateEntityProperties", contactDTOList, _callContext);
            return JsonConvert.DeserializeObject<List<ContactDTO>>(result);
        }

        public List<KeyValueDTO> GetEntityTypeEntitlementByEntityType(EntityTypes entityType)
        {
            var uri = string.Format("{0}/GetEntityTypeEntitlementByEntityType?entityType={1}", Service, entityType);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext, _logger);
        }

        public Services.Contracts.Search.SearchResultDTO ExecuteView(DynamicViews view, string IID1 = "", string IID2 = "")
        {
            var uri = string.Format("{0}/ExecuteView?view={1}&IID1={2}&IID2={3}", Service, view, IID1, IID2);
            return ServiceHelper.HttpGetRequest<Services.Contracts.Search.SearchResultDTO>(uri, _callContext, _logger);
        }

        public string GetNextTransactionNumber(long documentTypeID)
        {
            var uri = string.Format("{0}/GetNextTransactionNumber?documentTypeID={1}", Service, documentTypeID);
            return ServiceHelper.HttpGetRequest<string>(uri, _callContext, _logger);
        }

        public List<KeyValueDTO> GetCustomerEntitlements(long customerID)
        {
            var uri = string.Format("{0}/GetCustomerEntitlements?customerID={1}", Service, customerID);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext, _logger);
        }

        public GeoLocationDTO GetGeoLocation()
        {
            var uri = string.Format("{0}/GetGeoLocation", Service);
            return ServiceHelper.HttpGetRequest<GeoLocationDTO>(uri, _callContext, _logger);
        }


        #region City

        public CityDTO SaveCity(CityDTO dtoCity)
        {
            var uri = Service + "SaveCity";
            return ServiceHelper.HttpPostGetRequest<CityDTO>(uri, dtoCity, _callContext);
        }

        public CityDTO GetCity(long cityID)
        {
            var uri = Service + "GetCity?cityID=" + cityID;
            return ServiceHelper.HttpGetRequest<CityDTO>(uri, _callContext);
        }

        #endregion


        #region Zone

        public ZoneDTO SaveZone(ZoneDTO dtoZone)
        {
            var uri = Service + "SaveZone";
            return ServiceHelper.HttpPostGetRequest<ZoneDTO>(uri, dtoZone, _callContext);
        }

        public ZoneDTO GetZone(short zoneID)
        {
            var uri = Service + "GetZone?zoneID=" + zoneID;
            return ServiceHelper.HttpGetRequest<ZoneDTO>(uri, _callContext);
        }

        #endregion


        #region Aread

        public AreaDTO SaveArea(AreaDTO dtoArea)
        {
            var uri = Service + "SaveArea";
            return ServiceHelper.HttpPostGetRequest<AreaDTO>(uri, dtoArea, _callContext);
        }

        public AreaDTO GetArea(int areaID)
        {
            var uri = Service + "GetArea?areaID=" + areaID;
            return ServiceHelper.HttpGetRequest<AreaDTO>(uri, _callContext);
        }

        public List<AreaDTO> GetAreaByCountryID(int countryID)
        {
            var uri = Service + "GetAreaByCountryID?countryID=" + countryID;
            return ServiceHelper.HttpGetRequest<List<AreaDTO>>(uri, _callContext);
        }

        public List<CityDTO> GetCityByCountryID(int countryID)
        {
            var uri = Service + "GetCityByCountryID?countryID=" + countryID;
            return ServiceHelper.HttpGetRequest<List<CityDTO>>(uri, _callContext);
        }

        public List<AreaDTO> GetAreaByCityID(int cityID, int siteID = 0, int countryID = 0)
        {
            var uri = Service + "GetAreaByCityID?cityID=" + cityID + "&siteID=" + siteID + "&countryID=" + countryID;
            return ServiceHelper.HttpGetRequest<List<AreaDTO>>(uri, _callContext);
        }

        #endregion

        #region Vehicle

        public VehicleDTO SaveVehicle(VehicleDTO vehicleDetail)
        {
            var uri = Service + "SaveVehicle";
            return ServiceHelper.HttpPostGetRequest<VehicleDTO>(uri, vehicleDetail, _callContext);
        }

        public VehicleDTO GetVehicle(long vehicleID)
        {
            var uri = Service + "GetVehicle?vehicleID=" + vehicleID;
            return ServiceHelper.HttpGetRequest<VehicleDTO>(uri, _callContext);
        }
        #endregion

        #region Comment
        public CommentDTO SaveComment(CommentDTO comment)
        {
            var uri = Service + "SaveComment";
            return ServiceHelper.HttpPostGetRequest<CommentDTO>(uri, comment, _callContext);
        }

        public CommentDTO GetComment(int commentID)
        {
            var uri = string.Format("{0}GetComment?commentID={1}", Service, commentID);
            return ServiceHelper.HttpGetRequest<CommentDTO>(uri, _callContext);
        }

        public List<CommentDTO> GetComments(Eduegate.Infrastructure.Enums.EntityTypes entityTypeID, long referenceID, long departmentID)
        {
            var uri = string.Format("{0}GetComments?entityTypeID={1}&referenceID={2}&departmentID={3}", Service, entityTypeID, referenceID, departmentID);
            return ServiceHelper.HttpGetRequest<List<CommentDTO>>(uri, _callContext);
        }

        public bool DeleteComment(int commentID)
        {
            var uri = string.Format("{0}DeleteComment?commentID={1}", Service, commentID);
            return ServiceHelper.HttpGetRequest<bool>(uri, _callContext);
        }

        #endregion

        public SupplierAccountMapDTO GetAccountBySupplierID(long? supplierID)
        {
            var uri = Service + "GetAccountBySupplierID?supplierID=" + supplierID;
            return ServiceHelper.HttpGetRequest<SupplierAccountMapDTO>(uri, _callContext);
        }
        public List<AdditionalExpenseProvisionalAccountMapDTO> GetProvisionalAccountByAdditionalExpense(int? additionalExpenseID)
        {
            var uri = Service + "GetProvisionalAccountByAdditionalExpense?additionalExpenseID=" + additionalExpenseID;
            return ServiceHelper.HttpGetRequest< List<AdditionalExpenseProvisionalAccountMapDTO>>(uri, _callContext);
        }

        public AttachmentDTO SaveAttachment(AttachmentDTO attachment)
        {
            var uri = Service + "SaveAttachment";
            return ServiceHelper.HttpPostGetRequest<AttachmentDTO>(uri, attachment, _callContext);
        }

        public AttachmentDTO GetAttachment(int attachmentID)
        {
            var uri = string.Format("{0}GetAttachment?attachmentID={1}", Service, attachmentID);
            return ServiceHelper.HttpGetRequest<AttachmentDTO>(uri, _callContext);
        }

        public List<AttachmentDTO> GetAttachments(EntityTypes entityTypeID, long referenceID, long departmentID)
        {
            var uri = string.Format("{0}GetAttachments?entityTypeID={1}&referenceID={2}&departmentID={3}", Service, entityTypeID, referenceID, departmentID);
            return ServiceHelper.HttpGetRequest<List<AttachmentDTO>>(uri, _callContext);
        }

        public bool DeleteAttachment(int attachmentID)
        {
            var uri = string.Format("{0}DeleteAttachment?attachmentID={1}", Service, attachmentID);
            return ServiceHelper.HttpGetRequest<bool>(uri, _callContext);
        }

        #region  Product Delivery Type

        public bool SaveDeliveryCharges(List<ProductDeliveryTypeDTO> dtos, long IID, bool isProduct)
        {
            var result = ServiceHelper.HttpPostRequest<List<ProductDeliveryTypeDTO>>(string.Concat(Service + "SaveDeliveryCharges?IID=" + IID + "&isProduct=" + isProduct), dtos, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }
        public bool SaveCustomerDeliveryCharges(List<CustomerGroupDeliveryChargeDTO> dtos, int customerGroupID)
        {
            var result = ServiceHelper.HttpPostRequest<List<CustomerGroupDeliveryChargeDTO>>(Service + "SaveCustomerDeliveryCharges?customerGroupID=" + customerGroupID, dtos, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }
        public bool SaveZoneDeliveryCharges(List<ZoneDeliveryChargeDTO> dtos, short zoneID)
        {
            var result = ServiceHelper.HttpPostRequest<List<ZoneDeliveryChargeDTO>>(Service + "SaveZoneDeliveryCharges?zoneID=" + zoneID, dtos, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }
        public bool SaveAreaDeliveryCharges(List<AreaDeliveryChargeDTO> dtos, int areaID)
        {
            var result = ServiceHelper.HttpPostRequest<List<AreaDeliveryChargeDTO>>(Service + "SaveAreaDeliveryCharges?areaID=" + areaID, dtos, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }
        #endregion


        public KeyValueDTO GetDocumentStatus(short statusID)
        {
            var uri = string.Format("{0}/GetDocumentStatus?statusID={1}", Service, statusID);
            return ServiceHelper.HttpGetRequest<KeyValueDTO>(uri, _callContext, _logger);
        }

        public List<KeyValueDTO> GetMainDesignation(int gdesig_code)
        {
            var uri = string.Format("{0}/GetMainDesignation?gdesig_code={1}", Service, gdesig_code);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext, _logger);
        }
        public List<KeyValueDTO> GetHRDesignation(int mdesig_code)
        {
            var uri = string.Format("{0}/GetHRDesignation?mdesig_code={1}", Service, mdesig_code);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext, _logger);
        }
        public List<KeyValueDTO> GetAllowancebyPayComp(int paycomp)
        {
            var uri = string.Format("{0}/GetAllowancebyPayComp?paycomp={1}", Service, paycomp);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext, _logger);
        }

        public List<KeyValueDTO> GetLocationByDept(int deptCode)
        {
            var uri = string.Format("{0}/GetLocationByDept?deptCode={1}", Service, deptCode);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext, _logger);
        }

        public List<KeyValueDTO> GetDocType(string fileUploadType)
        {
            var uri = string.Format("{0}/GetDocType?fileUploadType={1}", Service, fileUploadType);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext, _logger);
        }

        public List<KeyValueDTO> GetAgent()
        {
            var uri = string.Format("{0}/GetAgent", Service);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext, _logger);
        }

        public EntitlementMapDTO GetEntitlementMaps(long supplierID, short supplier)
        {
            var uri = string.Format("{0}/GetEntitlementMaps?supplierID={1}&supplier={2}", Service, supplierID, supplier);
            return ServiceHelper.HttpGetRequest<EntitlementMapDTO>(uri, _callContext, _logger);
        }
        public string GetNextTransactionNumberByMonthYear(TransactionNumberDTO dto)
        {
            var result = ServiceHelper.HttpPostRequest<TransactionNumberDTO>(Service + "GetNextTransactionNumberByMonthYear", dto, _callContext);
            return JsonConvert.DeserializeObject<string>(result);
        }

        public List<ScreenShortCutDTO> GetShortCuts(long screenID)
        {
            return ServiceHelper.HttpGetRequest<List<ScreenShortCutDTO>>(Service + "GetShortCuts?screenID=" + screenID.ToString(), _callContext);
        }

        public string GetNextSequence(string sequenceType)
        {
            return ServiceHelper.HttpGetRequest(Service + "GetNextSequence?sequenceType=" + sequenceType, _callContext);
        }

        public CommunicationDTO GetMailIDDetailsFromLead(long? leadID)
        {
            throw new NotImplementedException();
        }

        public CommunicationDTO GetEmailTemplateByID(int? templateID)
        {
            throw new NotImplementedException();
        }

        public List<AcademicYearDTO> GetAcademicYearListData()
        {
            throw new NotImplementedException();
        }

        public List<AcademicYearDTO> GetActiveAcademicYearListData()
        {
            throw new NotImplementedException();
        }

        public List<SchoolsDTO> GetSchoolsProfileWithAcademicYear()
        {
            throw new NotImplementedException();
        }

        public List<CommentDTO> GetChats(Infrastructure.Enums.EntityTypes entityTypeID, long fromLoginID, long toLoginID, long referenceID, long departmentID)
        {
            throw new NotImplementedException();
        }
    }
}