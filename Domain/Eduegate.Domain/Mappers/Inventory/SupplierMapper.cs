using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Contents;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Vendor;
using Microsoft.CodeAnalysis;

namespace Eduegate.Domain.Mappers.Inventory
{
    public class SupplierMapper : DTOEntityDynamicMapper
    {
        public static SupplierMapper Mapper(CallContext context)
        {
            var mapper = new SupplierMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SupplierDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }


        public SupplierDTO ToDTO(long IID)
        {
            var dto = new SupplierDTO();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var entity = new SupplierRepository().GetSupplier(IID);

                if (entity.IsNotNull())
                {
                    dto = new SupplierDTO()
                    {
                        SupplierIID = entity.SupplierIID,
                        SupplierCode = entity.SupplierCode,
                        LoginID = entity.LoginID,
                        TitleID = entity.TitleID.HasValue ? (short?)short.Parse(entity.TitleID.ToString()) : null,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        MiddleName = entity.MiddleName,
                        StatusID = entity.StatusID,
                        VendorNickName = entity.VendorNickName,
                        CompanyLocation = entity.CompanyLocation,
                        CreatedBy = entity.CreatedBy,
                        CreatedDate = entity.CreatedDate,
                        UpdatedDate = entity.UpdateDate,
                        IsMarketPlace = entity.IsMarketPlace,
                        BranchID = entity.BranchID,
                        ReturnMethodID = entity.ReturnMethodID,
                        ReceivingMethodID = entity.ReceivingMethodID,
                        ReceivingMethodName = entity.ReceivingMethod.IsNotNull() ? entity.ReceivingMethod.ReceivingMethodName : null,
                        ReturnMethodName = entity.ReturnMethod.IsNotNull() ? entity.ReturnMethod.ReturnMethodName : null,
                        CompanyID = entity.CompanyID,
                        Login = LoginMapper.Mapper(_context).ToDTO(entity.Login),
                        Profit = entity.Profit,
                        SupplierEmail = entity.SupplierEmail,
                        TelephoneNumber = entity.Telephone,
                        CommunicationAddress = entity.CommunicationAddress,
                        PhysicalAddress = entity.PhysicalAddress,
                        WebsiteURL = entity.WebsiteURL,
                        ClientContactInformation = entity.ClientContactInformation,
                        ClientProjectDetails = entity.ClientProjectDetails,
                        NamesOfClients = entity.NamesOfClients,
                        PrevContractScopeOfWork = entity.PrevContractScopeOfWork,
                        PrevValueOfContracts = entity.PrevValueOfContracts,
                        PrevContractDuration = entity.PrevContractDuration,
                        Declaration = entity.SupplierIID != 0 ? true : false,
                    };

                    dto.Contacts = new List<ContactDTO>();

                    dto.BusinessDetail = new BusinessDetailDTO()
                    {
                        VendorCR = entity.VendorCR,
                        TINNumber = entity.TINNumber,
                        BusinessTypeID = entity.BusinessTypeID,
                        BusinessType = entity.BusinessTypeID.HasValue ? new KeyValueDTO()
                        {
                            Key = entity.BusinessTypeID.ToString(),
                            Value = entity.BusinessType.BusinessType.ToString()
                        }
                        : new KeyValueDTO(),
                        YearEstablished = entity.YearEstablished,

                        CRStartDate = entity.CRStartDate,
                        CRExpiry = entity.CRExpiry,

                        CRStartDateString = entity.CRStartDate.HasValue ? entity.CRStartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        CRExpiryDateString = entity.CRExpiry.HasValue ? entity.CRExpiry.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,

                        TaxJurisdictionCountryID = entity.TaxJurisdictionCountryID,
                        TaxJurisdictionCountry = entity.TaxJurisdictionCountryID.HasValue ? new KeyValueDTO()
                        {
                            Key = entity.TaxJurisdictionCountryID.ToString(),
                            Value = entity.TaxJurisdictionCountry.CountryName.ToString()
                        }
                        : new KeyValueDTO(),
                        DUNSNumber = entity.DUNSNumber,
                        LicenseNumber = entity.LicenseNumber,

                        LicenseStartDateString = entity.LicenseStartDate.HasValue ? entity.LicenseStartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        LicenseExpiryDateString = entity.LicenseExpiryDate.HasValue ? entity.LicenseExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,

                        LicenseStartDate = entity.LicenseStartDate,
                        LicenseExpiryDate = entity.LicenseExpiryDate,
                        EstIDNumber = entity.EstIDNumber,

                        EstFirstIssueDateString = entity.EstFirstIssueDate.HasValue ? entity.EstFirstIssueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        EstExpiryDateString = entity.EstExpiryDate.HasValue ? entity.EstExpiryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,

                        EstFirstIssueDate = entity.EstFirstIssueDate,
                        EstExpiryDate = entity.EstExpiryDate,
                    };

                    dto.BankAccounts = new BankAccountDTO()
                    {
                        BankName = entity.BankName,
                        BankAddress = entity.BankAddress,
                        AccountNumber = entity.AccountNumber,
                        IBAN = entity.IBAN,
                        SwiftCode = entity.SWIFT_BIC_Code,
                        IsCreditReference = entity.IsCreditReference,
                        PaymentMaxNoOfDaysAllowed = entity.PaymentMaxNoOfDaysAllowed,
                    };

                    dto.ExternalSettings = new ExternalSettingsDTO()
                    {
                        ProductorServiceDescription = entity.ProductorServiceDescription,
                        PricingInformation = entity.PricingInformation,
                        LeadTimeDays = entity.LeadTimeDays,
                        MinOrderQty = entity.MinOrderQty,
                        Warranty_GuaranteeInfo = entity.Warranty_GuaranteeInfo,
                    };

                    var contents = entity.SupplierContentIDs.FirstOrDefault(f => f.SupplierID == entity.SupplierIID);

                    dto.Document = new DocumentViewDTO()
                    {
                        SupplierID = contents?.SupplierID,
                        LetterConfirmationFromBank = contents?.LetterConfirmationFromBank,
                        LatestAuditedFinancialStatements = contents?.LatestAuditedFinancialStatements,
                        LiabilityInsurance = contents?.LiabilityInsurance,
                        WorkersCompensationInsurance = contents?.WorkersCompensationInsurance,
                        PrdctCategories = contents?.PrdctCategories,
                        ISO9001 = contents?.ISO9001,
                        OtherRelevantISOCertifications = contents?.OtherRelevantISOCertifications,
                        ISO14001 = contents?.ISO14001,
                        OtherEnviStandards = contents?.OtherEnviStandards,
                        SA8000 = contents?.SA8000,
                        OtherSocialRespoStandards = contents?.OtherSocialRespoStandards,
                        OHSAS18001 = contents?.OHSAS18001,
                        OtherRelevantHealthSafetyStandards = contents?.OtherRelevantHealthSafetyStandards,
                        BusinessRegistration = contents?.BusinessRegistration,
                        TaxIdentificationNumber = contents?.TaxIdentificationNumber,
                        DUNSNumberUpload = contents?.DUNSNumberUpload,
                        TradeLicense = contents?.TradeLicense,
                        EstablishmentLicense = contents?.EstablishmentLicense,
                    };

                }

            return dto;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SupplierDTO;

            if (toDto.Declaration == false)
            {
                throw new Exception("Please read and tick the declaration to save !");
            }

            //Date Validations 
            if (toDto.BusinessDetail.CRExpiry.HasValue)
            {
                if (toDto.BusinessDetail.CRExpiry.Value.Date < toDto.BusinessDetail.CRStartDate.Value.Date)
                {
                    throw new Exception("CR Start Date must be earlier than CR Expiry Date!");
                }
            }           
            if (toDto.BusinessDetail.LicenseExpiryDate.HasValue)
            {
                if (toDto.BusinessDetail.LicenseExpiryDate.Value.Date < toDto.BusinessDetail.LicenseStartDate.Value.Date)
                {
                    throw new Exception("License Start Date must be earlier than License Expiry Date!");
                }
            } 
            if (toDto.BusinessDetail.EstExpiryDate.HasValue)
            {
                if (toDto.BusinessDetail.EstExpiryDate.Value.Date < toDto.BusinessDetail.EstFirstIssueDate.Value.Date)
                {
                    throw new Exception("First Issue Date must be earlier than Expiry Date!");
                }
            }

            //if condition for new supplier create
            if(toDto.SupplierIID == 0)
            {
                var regDto = new VendorRegisterDTO();

                regDto.VendorCr = toDto.BusinessDetail.VendorCR;
                regDto.Email = toDto.SupplierEmail;
                regDto.Password = toDto.BusinessDetail.VendorCR;
                regDto.FirstName = toDto.FirstName;
                regDto.TelephoneNo = toDto.TelephoneNumber;

                var createSupplier = new SupplierBL(_context).RegisterVendor(regDto);

                if(createSupplier.IsError == true)
                {
                    throw new Exception(createSupplier.ReturnMessage);
                }
                else
                {
                    toDto.SupplierIID = (long)createSupplier.SupplierIID;
                    toDto.SupplierCode = createSupplier.SupplierCode;
                    toDto.LoginID = createSupplier.LoginID;
                }
            }

            var toSave = ToEntity(toDto);

            return GetEntity(toSave.SupplierIID);
        }

        public Supplier ToEntity(SupplierDTO supplier)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var entity = new Supplier();

            if (supplier.IsNotNull())
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    //Clear old content data re-enter new
                    var existContents = dbContext.SupplierContentIDs.Where(r => r.SupplierID == supplier.SupplierIID).ToList();

                    if (existContents.Any())
                    {
                        dbContext.SupplierContentIDs.RemoveRange(existContents);
                        dbContext.SaveChanges();
                    }

                    entity = new Supplier()
                    {
                        SupplierIID = supplier.SupplierIID,
                        SupplierCode = supplier.SupplierCode,
                        LoginID = supplier.LoginID,
                        FirstName = supplier.FirstName,
                        VendorNickName = supplier?.VendorNickName,
                        CommunicationAddress = supplier?.CommunicationAddress,
                        PhysicalAddress = supplier?.PhysicalAddress,
                        Telephone = supplier?.TelephoneNumber,
                        SupplierEmail = supplier?.SupplierEmail,
                        WebsiteURL = supplier?.WebsiteURL,
                        CreatedBy = supplier.SupplierIID == 0 ? (int?)_context.LoginID : supplier.CreatedBy,
                        CreatedDate = supplier.SupplierIID == 0 ? DateTime.Now : supplier.CreatedDate,
                        UpdateDate = DateTime.Now,
                        UpdatedBy = (int?)_context.LoginID,
                        StatusID = supplier?.StatusID,
                        SupplierAddress = supplier?.SupplierAddress,

                        //Business Details
                        VendorCR = supplier.BusinessDetail?.VendorCR,
                        TINNumber = supplier.BusinessDetail?.TINNumber,
                        BusinessTypeID = supplier.BusinessDetail?.BusinessType?.Key != null ? long.Parse(supplier.BusinessDetail.BusinessType.Key) : null,
                        YearEstablished = supplier.BusinessDetail?.YearEstablished,

                        CRStartDate = string.IsNullOrEmpty(supplier.BusinessDetail?.CRStartDateString) ? (DateTime?)null : DateTime.ParseExact(supplier.BusinessDetail?.CRStartDateString, dateFormat, CultureInfo.InvariantCulture),
                        CRExpiry = string.IsNullOrEmpty(supplier.BusinessDetail?.CRExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(supplier.BusinessDetail?.CRExpiryDateString, dateFormat, CultureInfo.InvariantCulture),

                        TaxJurisdictionCountryID = supplier.BusinessDetail?.TaxJurisdictionCountry?.Key != null ? int.Parse(supplier.BusinessDetail.TaxJurisdictionCountry.Key) : null,
                        DUNSNumber = supplier.BusinessDetail?.DUNSNumber,
                        LicenseNumber = supplier.BusinessDetail?.LicenseNumber,

                        LicenseStartDate = string.IsNullOrEmpty(supplier.BusinessDetail?.LicenseStartDateString) ? (DateTime?)null : DateTime.ParseExact(supplier.BusinessDetail?.LicenseStartDateString, dateFormat, CultureInfo.InvariantCulture),
                        LicenseExpiryDate = string.IsNullOrEmpty(supplier.BusinessDetail?.LicenseExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(supplier.BusinessDetail?.LicenseExpiryDateString, dateFormat, CultureInfo.InvariantCulture),

                        EstIDNumber = supplier.BusinessDetail?.EstIDNumber,
                        EstFirstIssueDate = string.IsNullOrEmpty(supplier.BusinessDetail?.EstFirstIssueDateString) ? (DateTime?)null : DateTime.ParseExact(supplier.BusinessDetail?.EstFirstIssueDateString, dateFormat, CultureInfo.InvariantCulture),
                        EstExpiryDate = string.IsNullOrEmpty(supplier.BusinessDetail?.EstExpiryDateString) ? (DateTime?)null : DateTime.ParseExact(supplier.BusinessDetail?.EstExpiryDateString, dateFormat, CultureInfo.InvariantCulture),

                        //Financial Information
                        BankName = supplier.BankAccounts?.BankName,
                        BankAddress = supplier.BankAccounts?.BankAddress,
                        AccountNumber = supplier.BankAccounts?.AccountNumber,
                        IBAN = supplier.BankAccounts?.IBAN,
                        SWIFT_BIC_Code = supplier.BankAccounts?.SwiftCode,
                        IsCreditReference = supplier.BankAccounts?.IsCreditReference == true ? true : false,
                        PaymentMaxNoOfDaysAllowed = supplier.BankAccounts?.PaymentMaxNoOfDaysAllowed,

                        //Product/Service Information
                        ProductorServiceDescription = supplier?.ExternalSettings?.ProductorServiceDescription,
                        PricingInformation = supplier?.ExternalSettings?.PricingInformation,
                        LeadTimeDays = supplier?.ExternalSettings?.LeadTimeDays,
                        MinOrderQty = supplier?.ExternalSettings?.MinOrderQty,
                        Warranty_GuaranteeInfo = supplier?.ExternalSettings?.Warranty_GuaranteeInfo,

                        //Reference and Past performance
                        NamesOfClients = supplier?.NamesOfClients,
                        ClientContactInformation = supplier?.ClientContactInformation,
                        ClientProjectDetails = supplier?.ClientProjectDetails,
                        PrevContractScopeOfWork = supplier?.PrevContractScopeOfWork,
                        PrevValueOfContracts = supplier?.PrevValueOfContracts,
                        PrevContractDuration = supplier?.PrevContractDuration,
                    };

                    //Attachments
                    entity.SupplierContentIDs.Add(new SupplierContentIDs()
                    {
                        SupplierID = entity.SupplierIID,
                        LetterConfirmationFromBank = supplier.Document?.LetterConfirmationFromBank,
                        LatestAuditedFinancialStatements = supplier.Document?.LatestAuditedFinancialStatements,
                        LiabilityInsurance = supplier.Document?.LiabilityInsurance,
                        WorkersCompensationInsurance = supplier.Document?.WorkersCompensationInsurance,
                        PrdctCategories = supplier.Document?.PrdctCategories,
                        ISO9001 = supplier.Document?.ISO9001,
                        OtherRelevantISOCertifications = supplier.Document?.OtherRelevantISOCertifications,
                        ISO14001 = supplier.Document?.ISO14001,
                        OtherEnviStandards = supplier.Document?.OtherEnviStandards,
                        SA8000 = supplier.Document?.SA8000,
                        OtherSocialRespoStandards = supplier.Document?.OtherSocialRespoStandards,
                        OHSAS18001 = supplier?.Document?.OHSAS18001,
                        OtherRelevantHealthSafetyStandards = supplier.Document.OtherRelevantHealthSafetyStandards,
                        BusinessRegistration = supplier.Document.BusinessRegistration,
                        TaxIdentificationNumber = supplier.Document.TaxIdentificationNumber,
                        DUNSNumberUpload = supplier.Document.DUNSNumberUpload,
                        TradeLicense = supplier.Document.TradeLicense,
                        EstablishmentLicense = supplier.Document.EstablishmentLicense,
                    });

                    if (entity.SupplierIID == 0)
                    {
                        dbContext.Suppliers.Add(entity);
                    }
                    else
                    {
                        foreach (var content in entity.SupplierContentIDs)
                        {
                            dbContext.Entry(content).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    dbContext.SaveChanges();
                }

            }
            return entity;
        }
    }
}
