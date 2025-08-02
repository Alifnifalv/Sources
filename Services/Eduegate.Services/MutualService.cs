using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Metadata;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Domain.Mappers;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Domain.Mappers.School.Mutual;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Services
{
    public class MutualService : BaseService, IMutualService
    {
        public EntityTypeRelationMapDTO SaveEntityTypeRelationMaps(EntityTypeRelationDTO dto)
        {
            return new MutualBL(CallContext).SaveEntityTypeRelationMaps(dto);
        }

        public List<KeyValueDTO> GetEmployeeIdNameEntityTypeRelation(EntityTypeRelationDTO dto)
        {
            return new MutualBL(CallContext).GetEmployeeIdNameEntityTypeRelation(dto);
        }

        public List<KeyValueDTO> GetEntityPropertiesByType(int entityType)
        {
            try
            {
                return new MutualBL(CallContext).GetEntityPropertiesByType(entityType);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ContactDTO> CreateEntityProperties(List<ContactDTO> contactDTOList)
        {
            try
            {
                return new MutualBL(CallContext).CreateEntityProperties(contactDTOList);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<KeyValueDTO> GetEntityTypeEntitlementByEntityType(EntityTypes entityType)
        {
            return new MutualBL(CallContext).GetEntityTypeEntitlementByEntityType(entityType);
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO ExecuteView(DynamicViews view, string IID1 = "", string IID2 = "")
        {
            return new MutualBL(CallContext).ExecuteView(view, IID1, IID2);
        }

        public string GetNextTransactionNumber(long documentTypeID)
        {
            return new MutualBL(CallContext).GetNextTransactionNumber(documentTypeID);
        }

        public List<KeyValueDTO> GetCustomerEntitlements(long customerID)
        {
            return new MutualBL(CallContext).GetCustomerEntitlements(customerID);
        }

        public GeoLocationDTO GetGeoLocation()
        {
            return new MutualBL(CallContext).GetGeoLocation();
        }


        #region City

        public CityDTO SaveCity(CityDTO dtoCity)
        {
            return new MutualBL(CallContext).SaveCity(dtoCity);
        }

        public CityDTO GetCity(long cityID)
        {
            return new MutualBL(CallContext).GetCity(cityID);
        }

        #endregion

        #region Zone

        public ZoneDTO SaveZone(ZoneDTO dtoZone)
        {
            return new MutualBL(CallContext).SaveZone(dtoZone);
        }

        public ZoneDTO GetZone(short zoneID)
        {
            return new MutualBL(CallContext).GetZone(zoneID);
        }

        #endregion


        #region Area


        public AreaDTO SaveArea(AreaDTO dtoArea)
        {
            return new MutualBL(CallContext).SaveArea(dtoArea);
        }

        public AreaDTO GetArea(int areaID)
        {
            return new MutualBL(CallContext).GetArea(areaID);
        }

        public List<AreaDTO> GetAreaByCountryID(int countryID)
        {
            return new MutualBL(CallContext).GetAreaByCountryID(countryID);
        }

        public List<CityDTO> GetCityByCountryID(int countryID)
        {
            return new MutualBL(CallContext).GetCityByCountryID(countryID);
        }

        public List<AreaDTO> GetAreaByCityID(int cityID = 0, int siteID = 0, int countryID = 0)
        {
            return new MutualBL(CallContext).GetAreaByCityID(cityID, siteID, countryID);
        }



        #endregion

        #region Vehicle

        public VehicleDTO SaveVehicle(VehicleDTO dtoVehicle)
        {
            return new MutualBL(CallContext).SaveVehicle(dtoVehicle);
        }

        public VehicleDTO GetVehicle(long vehicleID)
        {
            return new MutualBL(CallContext).GetVehicle(vehicleID);
        }
        #endregion

        #region Comment
        public CommentDTO SaveComment(CommentDTO comment)
        {
            try
            {
                return new MutualBL(CallContext).SaveComment(comment);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public CommentDTO GetComment(int commentID)
        {
            try
            {
                return new MutualBL(CallContext).GetComment(commentID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<CommentDTO> GetComments(Eduegate.Infrastructure.Enums.EntityTypes entityType, long referenceID, long departmentID)
        {
            try
            {
                return new MutualBL(CallContext).GetComments(entityType, referenceID, departmentID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
        public List<CommentDTO> GetChats(Eduegate.Infrastructure.Enums.EntityTypes entityType, long? fromLoginID, long? toLoginID, int page, int pageSize, int studentID)
        {
            try
            {
                return new MutualBL(CallContext).GetChats(entityType, fromLoginID, toLoginID, page, pageSize, studentID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool DeleteComment(int commentID)
        {
            try
            {
                return new MutualBL(CallContext).DeleteComment(commentID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        #endregion

        #region Attachments
        public AttachmentDTO SaveAttachment(AttachmentDTO attachment)
        {
            try
            {
                return new MutualBL(CallContext).SaveAttachment(attachment);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public AttachmentDTO GetAttachment(int attachmentID)
        {
            try
            {
                return new MutualBL(CallContext).GetAttachment(attachmentID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<AttachmentDTO> GetAttachments(EntityTypes entityType, long referenceID, long departmentID)
        {
            try
            {
                return new MutualBL(CallContext).GetAttachments(entityType, referenceID, departmentID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool DeleteAttachment(int attachmentID)
        {
            try
            {
                return new MutualBL(CallContext).DeleteAttachment(attachmentID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        #endregion

        #region Delivery Charge

        public bool SaveDeliveryCharges(List<ProductDeliveryTypeDTO> dtos, long IID, bool isProduct)
        {
            try
            {
                return new MutualBL(CallContext).SaveDeliveryCharges(dtos, IID, isProduct);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool SaveCustomerDeliveryCharges(List<CustomerGroupDeliveryChargeDTO> dtos, int customerGroupID)
        {
            try
            {
                return new MutualBL(CallContext).SaveCustomerDeliveryCharges(dtos, customerGroupID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
        public bool SaveZoneDeliveryCharges(List<ZoneDeliveryChargeDTO> dtos, short zoneID)
        {
            try
            {
                return new MutualBL(CallContext).SaveZoneDeliveryCharges(dtos, zoneID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
        public bool SaveAreaDeliveryCharges(List<AreaDeliveryChargeDTO> dtos, int areaID)
        {
            try
            {
                return new MutualBL(CallContext).SaveAreaDeliveryCharges(dtos, areaID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualBL>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
        #endregion

        #region Inventory
        public SupplierAccountMapDTO GetAccountBySupplierID(long? supplierID)
        {
            return new MutualBL(CallContext).GetAccountBySupplierID(supplierID);
        }
        public List<AdditionalExpenseProvisionalAccountMapDTO> GetProvisionalAccountByAdditionalExpense(int? additionalExpenseID)
        {
            return new MutualBL(CallContext).GetProvisionalAccountByAdditionalExpense(additionalExpenseID);
        }

        #endregion

        public KeyValueDTO GetDocumentStatus(short statusID)
        {
            return new MutualBL(CallContext).GetDocumentStatus(statusID);
        }

        public List<KeyValueDTO> GetMainDesignation(int gdesig_code)
        {
            return new ReferenceDataBL(CallContext).GetMainDesignations(gdesig_code);
        }

        public List<KeyValueDTO> GetHRDesignation(int mdesig_code)
        {
            return new ReferenceDataBL(CallContext).GetHRDesignations(mdesig_code);
        }

        public List<KeyValueDTO> GetAllowancebyPayComp(int paycomp)
        {
            return new ReferenceDataBL(CallContext).GetAllowancebyPayComp(paycomp);
        }

        public List<KeyValueDTO> GetLocationByDept(int deptCode)
        {
            return new ReferenceDataBL(CallContext).GetLocationByDept(deptCode);
        }

        public List<KeyValueDTO> GetDocType(string fileUploadType)
        {
            return new ReferenceDataBL(CallContext).GetDocType(fileUploadType);
        }

        public List<KeyValueDTO> GetAgent()
        {
            return new ReferenceDataBL(CallContext).GetAgent();
        }

        public EntitlementMapDTO GetEntitlementMaps(long supplierID, short supplier)
        {
            return new MutualBL(CallContext).GetEntitlementMaps(supplierID, supplier);
        }

        public string GetNextTransactionNumberByMonthYear(TransactionNumberDTO dto)
        {
            return new MutualBL(CallContext).GetNextTransactionNumberByMonthYear(dto);
        }

        public List<ScreenShortCutDTO> GetShortCuts(long screenID)
        {
            return new MutualBL(CallContext).GetShortCuts(screenID);
        }

        public string GetNextSequence(string sequenceType)
        {
            return new MutualBL(CallContext).GetNextSequence(sequenceType);
        }

        public CommunicationDTO GetMailIDDetailsFromLead(long? leadID)
        {
            return CommunicationMapper.Mapper(CallContext).GetMailIDDetailsFromLead(leadID);
        }

        public CommunicationDTO GetEmailTemplateByID(int? templateID)
        {
            return CommunicationMapper.Mapper(CallContext).GetEmailTemplateByID(templateID);
        }

        public List<AcademicYearDTO> GetAcademicYearListData()
        {
            return AcademicYearMapper.Mapper(CallContext).GetAcademicYearListData();
        }

        public List<AcademicYearDTO> GetActiveAcademicYearListData()
        {
            return AcademicYearMapper.Mapper(CallContext).GetActiveAcademicYearListDataFromCache();
        }
        public List<SchoolsDTO> GetSchoolsProfileWithAcademicYear()
        {
            return SchoolsMapper.Mapper(CallContext).GetSchoolsProfileWithAcademicYear();
        }

        public List<CommentDTO> GetChats(Infrastructure.Enums.EntityTypes entityTypeID, long fromLoginID, long toLoginID, long referenceID, long departmentID)
        {
            throw new NotImplementedException();
        }
    }
}