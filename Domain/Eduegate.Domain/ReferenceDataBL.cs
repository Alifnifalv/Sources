using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Repository.Security;
using Eduegate.Domain.Repository.Accounts;
using Eduegate.Domain.Mappers;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Framework.Security.Secured;
using Eduegate.Services.Contracts.UrlReWriter;
using Eduegate.Domain.Security;
using Eduegate.Domain.Payroll;
using Eduegate.Domain.Repository.Workflows;
using Eduegate.Domain.Repository.HR;
using Eduegate.Services.Contracts.Enums.Common;
using Eduegate.Services.Contracts.Accounts.Taxes;

namespace Eduegate.Domain
{
    public class ReferenceDataBL
    {
        private CallContext _callContext;

        public ReferenceDataBL(CallContext context)
        {
            _callContext = context;
        }

        public List<CountryDTO> GetCountries(bool isActiveCurrency)
        {
            List<Country> countries = new ReferenceDataRepository().GetCountries(isActiveCurrency);
            List<CountryDTO> countryDTOList = new List<CountryDTO>();

            if (countries != null && countries.Count > 0)
            {
                foreach (var country in countries)
                {
                    var countryDTO = new CountryDTO();

                    countryDTO.CountryID = country.CountryID;
                    countryDTO.CountryCode = country.ThreeLetterCode;
                    countryDTO.CountryName = country.CountryName;
                    countryDTO.CurrencyID = country.CurrencyID.HasValue ? country.CurrencyID.Value : 0;
                    var currency = new CountryMasterRepository().GetCurrencyDetail(countryDTO.CurrencyID);

                    if (currency != null)
                    {
                        countryDTO.CurrencyCode = currency.AnsiCode;
                        countryDTO.CurrencyCodeDisplayText = currency.DisplayCode;
                        countryDTO.CurrencyName = currency.Name;
                        countryDTO.DecimalPlaces = currency.DecimalPrecisions;
                    }

                    //countryDTO.DataFeedDateTime = country.DataFeedDateTime != null ? Convert.ToDateTime(country.DataFeedDateTime) : System.DateTime.Now;
                    //countryDTO.ConversionRate = country.ConversionRate != null ? Convert.ToDecimal(country.ConversionRate) : 0;
                    //countryDTO.IsActiveForCurrency = country.IsActiveForCurrency;
                    //countryDTO.TelephoneCode = country.TelephoneCode;

                    countryDTOList.Add(countryDTO);
                }
            }
            return countryDTOList;
        }

        public CountryDTO GetCountryDetail(long countryID, bool isActiveCurrency)
        {
            return GetCountries(isActiveCurrency).Where(x => x.CountryID == countryID).FirstOrDefault();
        }

        public List<CountryMasterDTO> GetPassportIssueCountryMasters()
        {
            List<CountryMasterDTO> passportIssueCountryDetails = new ReferenceDataRepository().GetPassportIssueCountryMasters();

            return passportIssueCountryDetails;
        }

        public List<CheckOutDTO> GetShippingAddressMasters()
        {
            List<CheckOutDTO> shippingAddressDTOList = new ReferenceDataRepository().GetShippingAddressMasters();

            return shippingAddressDTOList;
        }

        public List<CheckOutDTO> GetBillingAddressMasters()
        {
            List<CheckOutDTO> billingAddressDTOList = new ReferenceDataRepository().GetBillingAddressMasters();

            return billingAddressDTOList;
        }

        public List<KeyValueDTO> GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes lookType, string searchText = "", int dataSize = 0, int optionalId = 0)
        {
            var keyValues = new List<KeyValueDTO>();
            var claim = Framework.CacheManager.MemCacheManager<List<string>>.Get("CLAIM_" + _callContext.LoginID.Value.ToString());

            if (claim == null)
            {
                claim = new Eduegate.Domain.Repository.Security.SecurityRepository().GetUserClaimKey(_callContext.LoginID.Value);
                Framework.CacheManager.MemCacheManager<List<string>>.Add(claim, "CLAIM_" + _callContext.LoginID.Value.ToString());
            }

            var secured = new SecuredData(claim);

            switch (lookType)
            {
                case Services.Contracts.Enums.LookUpTypes.Maids:
                    {
                        var employees = new Eduegate.Domain.Repository.Payroll.
                            EmployeeRepository().GetEmployeesByRoles((int)EmployeeRoles.Maid, searchText);

                        foreach (var employee in employees)
                        {
                            keyValues.Add(new KeyValueDTO() { Value = employee.EmployeeName, Key = employee.EmployeeName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.LeadMaids:
                    {
                        var employees = new Eduegate.Domain.Repository.Payroll.
                            EmployeeRepository().GetEmployeesByRolesAndDesignation((int)EmployeeRoles.Maid, (int)Designations.TeamLead, searchText);

                        foreach (var employee in employees)
                        {
                            keyValues.Add(new KeyValueDTO() { Value = employee.EmployeeName, Key = employee.EmployeeName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ChartMonthAndYearPeriod:
                    {
                        var startDate = new Domain.Setting.SettingBL().GetSettingValue<DateTime>("StartDate", _callContext.CompanyID.Value, DateTime.Now.Date);

                        while (startDate < DateTime.Now.Date.AddMonths(1))
                        {
                            keyValues.Add(new KeyValueDTO()
                            {
                                Value = $"{startDate.Month.ToString()}/{startDate.Year.ToString()}",
                                Key = "MONTHYEAR"
                            });
                            startDate = startDate.AddMonths(1);
                        }

                        keyValues.Reverse();
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ChartYearPeriod:
                    {
                        var startDate = new Domain.Setting.SettingBL().GetSettingValue<DateTime>("StartDate", _callContext.CompanyID.Value, DateTime.Now.Date);

                        while (startDate.Year <= DateTime.Now.Date.Year)
                        {
                            keyValues.Add(new KeyValueDTO() { Value = startDate.Year.ToString(), Key = "YEAR" });
                            startDate = startDate.AddYears(1);
                        }

                        keyValues.Reverse();
                    }
                    break; 
                case Services.Contracts.Enums.LookUpTypes.ExamGroups:
                    {
                        var values = new List<KeyValueDTO>(); ;

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ChartAcademicYears:
                    {
                        var values = new List<KeyValueDTO>(); 

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.PayableGLSubAccounts:
                    {
                        var allocationAllowed = new SettingRepository().GetSettingDetail("PAYABLEGLMAINACC");
                        var subAccounts = new AccountingRepository().GetSubAccounts(long.Parse(allocationAllowed.SettingValue), searchText);

                        foreach(var subAcc in subAccounts)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = subAcc.AccountName.ToString(), Value = subAcc.AccountName + "-" + subAcc.AccountCode  });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ReceivableGLSubAccounts:
                    {
                        var allocationAllowed = new SettingRepository().GetSettingDetail("RECEIVABLEGLMAINACC");
                        var subAccounts = new AccountingRepository().GetSubAccounts(long.Parse(allocationAllowed.SettingValue), searchText);

                        foreach (var subAcc in subAccounts)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = subAcc.AccountID.ToString(), Value = subAcc.AccountName + "-" + subAcc.AccountCode });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.TaxTypes:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var datas = new ReferenceDataRepository().GetTaxTypes();

                            foreach (var data in datas)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = data.TaxTypeID.ToString(), Value = data.Description });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                        break;
                    }                    
                case Services.Contracts.Enums.LookUpTypes.TaxTemplates:
                    {
                        {
                            var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                            if (cacheItem == null)
                            {
                                var datas = new ReferenceDataRepository().GetTaxTemplates();

                                foreach (var data in datas)
                                {
                                    keyValues.Add(new KeyValueDTO() { Key = data.TaxTemplateID.ToString(), Value = data.TemplateName });
                                }

                                Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                            }
                            else
                            {
                                keyValues = cacheItem;
                            }
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.UserSettings:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "layout", Value = "Layout" });
                        keyValues.Add(new KeyValueDTO() { Key = "theme", Value = "Theme" });                      
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Months:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "1", Value = "Jan" });
                        keyValues.Add(new KeyValueDTO() { Key = "2", Value = "Feb" });
                        keyValues.Add(new KeyValueDTO() { Key = "3", Value = "Mar" });
                        keyValues.Add(new KeyValueDTO() { Key = "4", Value = "Apr" });
                        keyValues.Add(new KeyValueDTO() { Key = "5", Value = "May" });
                        keyValues.Add(new KeyValueDTO() { Key = "6", Value = "June" });
                        keyValues.Add(new KeyValueDTO() { Key = "7", Value = "July" });
                        keyValues.Add(new KeyValueDTO() { Key = "8", Value = "Aug" });
                        keyValues.Add(new KeyValueDTO() { Key = "9", Value = "Sep" });
                        keyValues.Add(new KeyValueDTO() { Key = "10", Value = "Oct" });
                        keyValues.Add(new KeyValueDTO() { Key = "11", Value = "Nov" });
                        keyValues.Add(new KeyValueDTO() { Key = "12", Value = "Dec" });
                    }                    
                    break;
                case Services.Contracts.Enums.LookUpTypes.SequenceTypes:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "1", Value = "Regular" });
                        keyValues.Add(new KeyValueDTO() { Key = "2", Value = "Monthly" });
                        keyValues.Add(new KeyValueDTO() { Key = "3", Value = "Yearly" });
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.ActiveStatus:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "0", Value = "Inactive" });
                        keyValues.Add(new KeyValueDTO() { Key = "1", Value = "Active" });
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.IsRefundable:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "0", Value = "Non-Refundable" });
                        keyValues.Add(new KeyValueDTO() { Key = "1", Value = "Refundable" });
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.IsCollected:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "0", Value = "Not Collected" });
                        keyValues.Add(new KeyValueDTO() { Key = "1", Value = "Collected" });
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.IsShared:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "0", Value = "Non-Shared" });
                        keyValues.Add(new KeyValueDTO() { Key = "1", Value = "Shared" });
                    }
                    break;


                case Services.Contracts.Enums.LookUpTypes.FullAcademicYears:

                    {
                        var values = new List<KeyValueDTO>(); 

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }

                    break;

                case Services.Contracts.Enums.LookUpTypes.ApprovalWorkflow:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var datas = new WorkflowRepository().GetWorkflowByType(1);

                            foreach (var data in datas)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = data.WorkflowIID.ToString(), Value = data.WokflowName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ApprovalCondition:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var approvalCondition = new WorkflowRepository().GetWorkflowConditionByType("Approval");

                            foreach (var workflows in approvalCondition)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = workflows.WorkflowConditionID.ToString(), Value = workflows.ConditionName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.WorkflowCondition:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var workflowCondition = new WorkflowRepository().GetWorkflowConditionByType("Workflow");

                            foreach (var workflows in workflowCondition)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = workflows.WorkflowConditionID.ToString(), Value = workflows.ConditionName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Conditions:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var condition = new ReferenceDataRepository().GetConditions();

                            foreach (var title in condition)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = title.ConditionID.ToString(), Value = title.ConditionName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.WorkflowField:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var types = new WorkflowRepository().GetWorkflowFeilds(Convert.ToInt32(searchText));

                            foreach (var title in types)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = title.WorkflowFieldID.ToString(), Value = title.ColumnName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.WorkflowType:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var workflow = new WorkflowRepository().GetWorkflowTypes();

                            foreach (var title in workflow)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = title.WorkflowTypeID.ToString(), Value = title.WorkflowTypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Title:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var titles = new ReferenceDataRepository().GetTitle();

                            foreach (var title in titles)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = title.TitleID.ToString(), Value = title.TitleName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.BannerStatus:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new BannerRepository().GetBannerStatuses();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.BannerStatusID.ToString(), Value = entity.BannerStatusName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.BannerType:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new BannerRepository().GetBannerTypes();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.BannerTypeID.ToString(), Value = entity.BannerTypeName });
                            }
                                
                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.Classes:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value,(byte?) _callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.FilterClasses:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.AllSectionsFilter:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.Section:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.ClassTimingSets:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.LeadStatus:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.Relegion:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.AcademicYearStatus:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.AcademicYear:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.LeaveStatus:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.LeaveType:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.ApplicationStatus:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.StudentTransferRequestStatus:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.PresentStatus:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.LessonPlanStatus:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.AgendaStatus:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.LibraryTransactionType:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.SubGroup:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Nationality:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Languages:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.MainGroup:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.LedgerAccount:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.AcademicYearCode:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.BookStatus:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.BloodGroup:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.RouteType:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;


                case Services.Contracts.Enums.LookUpTypes.AcademicYearCalendarStatus:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.StudentTransferRequestReasons:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.ApplicationSubmitType:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;


                case Services.Contracts.Enums.LookUpTypes.VoucherStatus:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new VoucherRepository().GetVoucherStatuses();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.VoucherStatusID.ToString(), Value = entity.StatusName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.VoucherType:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new VoucherRepository().GetVoucherTypes();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.VoucherTypeID.ToString(), Value = entity.VoucherTypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.CustomerGroup:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new CustomerRepository().GetCustomerGroups();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.CustomerGroupIID.ToString(), Value = entity.GroupName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.NewsType:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new NewsRepository().GetNewsTypes();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.NewsTypeID.ToString(), Value = entity.NewsTypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.LoginUserStatus:
                    {
                        if (new Domain.Setting.SettingBL().GetSettingValue<bool>("IsJustCMS"))
                        {
                            keyValues.Add(new KeyValueDTO() { Key = "1", Value = "Active" });
                            keyValues.Add(new KeyValueDTO() { Key = "2", Value = "In Active" });
                        }
                        else
                        {
                            keyValues.Add(new KeyValueDTO() { Key = "1", Value = "Active" });
                            keyValues.Add(new KeyValueDTO() { Key = "2", Value = "In Active" });
                            keyValues.Add(new KeyValueDTO() { Key = "3", Value = "Need email verification" });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Roles:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new AccountRepository().GetRoles();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.RoleID.ToString(), Value = entity.RoleName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Customer:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new CustomerRepository().GetCustomers(searchText, dataSize);

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.CustomerIID.ToString(), Value = entity.FirstName + entity.MiddleName + entity.LastName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.StaticContentType:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new StaticContentRepository().GetStaticContentTypes();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.ContentTypeID.ToString(), Value = entity.ContentTypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Brands:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ProductDetailRepository().GetBrandList();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.BrandIID.ToString(), Value = entity.BrandName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.BrandStatus:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new BrandRepository().GetBrandStatusList();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.BrandStatusID.ToString(), Value = entity.StatusName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.TransactionStatus:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new OrderRepository().GetTransactionList();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.TransactionStatusID.ToString(), Value = entity.Description });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ProductFamilyType:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ProductDetailRepository().GetProductFamilyTypes();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.FamilyTypeID.ToString(), Value = entity.FamilyTypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.PropertyType:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ProductDetailRepository().GetPropertyTypes();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.PropertyTypeID.ToString(), Value = entity.PropertyTypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.StockDocumentReferenceTypes:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new MetadataRepository().GetDocumentReferenceTypes(Eduegate.Services.Contracts.Enums.Systems.Stock);

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.ReferenceTypeID.ToString(), Value = entity.InventoryTypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.AccountDocumentReferenceTypes:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new MetadataRepository().GetDocumentReferenceTypes(Eduegate.Services.Contracts.Enums.Systems.Account);

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.ReferenceTypeID.ToString(), Value = entity.InventoryTypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.WarehouseDocumentReferenceTypes:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new MetadataRepository().GetDocumentReferenceTypes(Eduegate.Services.Contracts.Enums.Systems.Warehouse);

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.ReferenceTypeID.ToString(), Value = entity.InventoryTypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.BranchGroup:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetBranchGroup(_callContext.CompanyID);

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.BranchGroupIID.ToString(), Value = entity.GroupName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Warehouse:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetWarehouses(_callContext.CompanyID);

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.WarehouseID.ToString(), Value = entity.WarehouseName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Gender:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetGenders();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.GenderID.ToString(), Value = entity.Description });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.MaritalStatus:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetMaritalStatuses();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.MaritalStatusID.ToString(), Value = entity.StatusName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Designation:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetDesignations();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.DesignationID.ToString(), Value = entity.DesignationName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.EmployeeRole:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetEmployeeRoles();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.EmployeeRoleID.ToString(), Value = entity.EmployeeRoleName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.JobType:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetJobTypes();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.JobTypeID.ToString(), Value = entity.JobTypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Branch:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString() + _callContext.LoginID.Value.ToString());

                        if (cacheItem == null ||cacheItem.Count == 0)
                        {
                            var entities = new ReferenceDataRepository().GetInternalBranch(1, _callContext.CompanyID.HasValue ? (int)_callContext.CompanyID.Value : 1); // active branches

                            foreach (var entity in entities)
                            {
                                //if (secured.HasAccess(entity.GetIID(), (Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ClaimType), Eduegate.Services.Contracts.Enums.ClaimType.Branches.ToString())))
                                {
                                    keyValues.Add(new KeyValueDTO() { Key = entity.BranchIID.ToString(), Value = entity.BranchName });
                                }
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString() + _callContext.LoginID.Value.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.AllBranch:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString() + _callContext.LoginID.Value.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetBranch(1, _callContext.CompanyID.HasValue ? (int)_callContext.CompanyID.Value : 1); // active branches

                            foreach (var entity in entities)
                            {
                                if (secured.HasAccess(entity.GetIID(), (Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ClaimType), Eduegate.Services.Contracts.Enums.ClaimType.Branches.ToString())))
                                {
                                    keyValues.Add(new KeyValueDTO() { Key = entity.BranchIID.ToString(), Value = entity.BranchName });
                                }
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString() + _callContext.LoginID.Value.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.AllocationBranch:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString() + _callContext.LoginID.Value.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetInternalBranch();
                            var allocationAllowed = new SettingRepository().GetSettingDetail("ALLOCATIOALLOWEDBRANCHES");

                            if (allocationAllowed.IsNotNull())
                            {
                                foreach (var entity in entities)
                                {
                                    if (allocationAllowed.SettingValue.IsNotNull() && allocationAllowed.SettingValue.Split(',').Contains(entity.BranchIID.ToString()) &&
                                        secured.HasAccess(entity.GetIID(), (Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ClaimType), Eduegate.Services.Contracts.Enums.ClaimType.Branches.ToString())))
                                    {
                                        keyValues.Add(new KeyValueDTO() { Key = entity.BranchIID.ToString(), Value = entity.BranchName });
                                    }
                                }

                                Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString() + _callContext.LoginID.Value.ToString());
                            }
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.BranchStatus:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetBranchStatus();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.BranchStatusID.ToString(), Value = entity.StatusName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.School:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString()+"_"+_callContext.LoginID.Value.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetSchool();
                            var secured1 = new SecuredData(new Eduegate.Domain.Repository.Security.SecurityRepository().GetUserClaimKey(_callContext.LoginID.Value, (int)Eduegate.Services.Contracts.Enums.ClaimType.School));

                            foreach (var entity in entities)
                            {
                                if (secured1.HasAccess(entity.SchoolID, (Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ClaimType), Eduegate.Services.Contracts.Enums.ClaimType.School.ToString())))
                                {
                                    keyValues.Add(new KeyValueDTO() { Key = entity.SchoolID.ToString(), Value = entity.SchoolName });
                                }
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString()+"_"+ _callContext.LoginID.Value.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.CompanyStatus:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetCompanyStatus();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.CompanyStatusID.ToString(), Value = entity.StatusName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.WarehouseStatus:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetWarehouseStatus();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.WarehouseStatusID.ToString(), Value = entity.StatusName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.BranchGroupStatus:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetBranchGroupStatus();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.BranchGroupStatusID.ToString(), Value = entity.StatusName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.DepartmentStatus:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetDepartmentStatus();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.DepartmentStatusID.ToString(), Value = entity.StatusName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Department:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetDepartments();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.DepartmentID.ToString(), Value = entity.DepartmentName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Property:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            int pageSize = Convert.ToInt32(new Domain.Setting.SettingBL().GetSettingValue<string>("MaxFetchCount").ToString());
                            var entities = new ProductDetailRepository().GetProperties(searchText, pageSize);

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.PropertyIID.ToString(), Value = entity.PropertyName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.DocumentFileStatus:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new DocumentRepository().GetDocumentFileStatuses();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.StatusName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Currency:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetCurrencies(true); //only enabled currency needs to bring for the lookup

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.CurrencyID.ToString(), Value = entity.Name });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Language:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetLanguages();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.LanguageID.ToString(), Value = entity.Language1 });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Country:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetCountries(false);

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.CountryID.ToString(), Value = entity.CountryName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.Employee:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new EmployeeBL(_callContext).GetEmployees();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.EmployeeIID.ToString(), Value = entity.EmployeeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                //case Services.Contracts.Enums.LookUpTypes.Driver:
                //    {
                //        var entities = new EmployeeBL(_callContext).GetEmployeesByRoles(8);

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.EmployeeIID.ToString(), Value = entity.EmployeeName });
                //        }
                //    }
                //    break;
                case Services.Contracts.Enums.LookUpTypes.JobStatus:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new WarehouseRepository().GetJobStatuses();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.JobStatusID.ToString(), Value = entity.StatusName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.JobActivity:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new WarehouseRepository().GetJobActivities();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.JobActivityID.ToString(), Value = entity.ActivityName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.JobPriority:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new WarehouseRepository().GetJobPriority();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.PriorityID.ToString(), Value = entity.Description });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.LocationType:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new WarehouseRepository().GetLocationType();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.LocationTypeID.ToString(), Value = entity.Description });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.JobOperationStatus:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new WarehouseRepository().GetJobOperationStatuses(optionalId);

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.JobOperationStatusID.ToString(), Value = entity.Description });
                            }


                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.MissionJobOperationStatus:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "4", Value = "Completed" });
                        keyValues.Add(new KeyValueDTO() { Key = "6", Value = "Failed" });
                    } 
                    break;
                case Services.Contracts.Enums.LookUpTypes.MissionJobStatus:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new WarehouseRepository().GetMissionJobStatuses();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.JobStatusID.ToString(), Value = entity.StatusName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Vehicle:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new MutualRepository().GetVehicles();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.VehicleIID.ToString(), Value = entity.VehicleCode });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.City:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new MutualRepository().GetCities();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.CityID.ToString(), Value = entity.CityName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Zone:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new MutualRepository().GetZones(_callContext.CompanyID);

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.ZoneID.ToString(), Value = entity.ZoneName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Area:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            List<Area> entities;

                            if (_callContext != null && _callContext.CompanyID.HasValue)
                                entities = new MutualRepository().GetAreaByCompanyID(_callContext.CompanyID.Value);
                            else
                                entities = new MutualRepository().GetAreas();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.AreaID.ToString(), Value = entity.AreaName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.VehicleType:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new MutualRepository().GetVehicleTypes();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.VehicleTypeID.ToString(), Value = entity.VehicleTypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.VehicleOwnershipType:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new MutualRepository().GetVehicleOwnershipTypes();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.VehicleOwnershipTypeID.ToString(), Value = entity.OwnershipTypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Basket:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new MutualRepository().GetBaskets();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.BasketID.ToString(), Value = entity.BasketCode });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Site:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new PageRenderRepository().GetSites();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.SiteID.ToString(), Value = entity.SiteName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.PageType:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new PageRenderRepository().GetPageTypes();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.PageTypeID.ToString(), Value = entity.TypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Country1:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ReferenceDataRepository().GetCountries1();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.CountryID.ToString(), Value = entity.CountryName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Claims:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new SecurityRepository().GetClaims();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.ClaimIID.ToString(), Value = entity.ClaimName + "(" + entity.ClaimTypeName + ")" });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ClaimSets:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new SecurityRepository().GetClaimSets();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.ClaimSetIID.ToString(), Value = entity.ClaimSetName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ClaimType:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new SecurityRepository().GetClaimTypes();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.ClaimTypeID.ToString(), Value = entity.ClaimTypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.JobSize:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new WarehouseRepository().GetJobSizes();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.JobSizeID.ToString(), Value = entity.Description });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.SupportDocumentType:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new MetadataRepository().GetDocumentType(Eduegate.Services.Contracts.Enums.Systems.Support, _callContext.CompanyID);

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.DocumentTypeID.ToString(), Value = entity.TransactionTypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Route:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new DistributionRepository().GetRoutes(_callContext.CompanyID);

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.RouteID.ToString(), Value = entity.Description });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.TicketPriority:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new SupportRepository().GetTicketPriorities();

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.TicketPriorityID.ToString(), Value = entity.PriorityName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.TicketAction:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new SupportRepository().GetSupportActions(optionalId);

                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.SupportActionID.ToString(), Value = entity.ActionName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.TicketStatus:
                    {
                        var entities = new SupportRepository().GetTicketStatuses();

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.TicketStatusID.ToString(), Value = entity.StatusName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.TicketReason:
                    {
                        var entities = new SupportRepository().GetTicketReasons();

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.TicketReasonID.ToString(), Value = entity.TicketReasonName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.JobOperationReason:
                    {
                        //var entities = new DistributionRepository().GetJobSize();

                        //foreach (var entity in entities)
                        //{
                        //    keyValues.Add(new KeyValueDTO() { Key = entity.ClaimTypeID.ToString(), Value = entity.ClaimTypeName });
                        //}
                        keyValues.Add(new KeyValueDTO() { Key = "1", Value = "Wrong Address" });
                        keyValues.Add(new KeyValueDTO() { Key = "2", Value = "Customer not available" });
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.AccountGroup:
                    {
                        var entities = new AccountingRepository().GetAccountGroups();

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.GroupID.ToString(), Value = entity.GroupName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.AccountBehavior:
                    {
                        var entities = new AccountingRepository().GetAccountBehavior();

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.AccountBehavoirID.ToString(), Value = entity.Description });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.JobStatuses:
                    {
                        var entities = new WarehouseRepository().GetJobStatusByID(1);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.JobStatusID.ToString(), Value = entity.StatusName });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.Account:
                    {
                        var entities = new AccountingRepository().GetAccounts(searchText);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.AccountID.ToString(), Value = entity.AccountName + "-" + entity.AccountCode });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Pages:
                    {
                        var entities = new PageRenderRepository().GetPages(_callContext.CompanyID);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.PageID.ToString(), Value = entity.PageName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Boilerplates:
                    {
                        var entities = new BoilerPlateRepository().GetBoilerplates();

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.BoilerPlateID.ToString(), Value = entity.Name });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.IncomeOrBalance:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "1", Value = "Balance Sheet" });
                        keyValues.Add(new KeyValueDTO() { Key = "2", Value = "Income Statement" });
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ChartRowType:
                    {
                        var entities = new AccountingRepository().GetChartRowTypes();

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.ChartRowTypeID.ToString(), Value = entity.Name });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.SchedulerType:
                    {
                        foreach (var value in Enum.GetValues(typeof(Eduegate.Services.Contracts.Enums.Schedulers.SchedulerEntityTypes)))
                        {
                            keyValues.Add(new KeyValueDTO() { Key = value.ToString(), Value = Enum.GetName(typeof(Eduegate.Services.Contracts.Enums.Schedulers.SchedulerEntityTypes), value) });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.DeliveryTypeStatus:
                    {
                        var entities = new DistributionRepository().GetDeliveryTypeStatuses();

                        foreach (var value in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = value.DeliveryTypeStatusID.ToString(), Value = value.StatusName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.BranchMarketPlace:
                    {
                        var entities = new ReferenceDataRepository().GetMarketPlaceBranch(true, Convert.ToInt32(_callContext.CompanyID));

                        foreach (var value in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = value.BranchIID.ToString(), Value = value.BranchName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.PriceList:
                    {
                        var entities = new PriceSettingsRepository().GetProductPriceLists(_callContext.CompanyID);

                        foreach (var value in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = value.ProductPriceListIID.ToString(), Value = value.PriceDescription });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ImageTypes:
                    {
                        var entities = new MutualRepository().GetImageTypes();

                        foreach (var value in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = value.TypeName, Value = value.TypeName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Supplier:
                    {
                        var entities = new SupplierRepository().GetSuppliers(searchText, dataSize, _callContext.CompanyID.IsNotNull() ? (int?)_callContext.CompanyID : default(int));

                        foreach (var value in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = Convert.ToString(value.SupplierIID), Value = string.Concat(value.FirstName, " ", value.LastName) });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.DeliveryType:
                    {
                        var entities = new DistributionRepository().GetDeliveryTypes();

                        foreach (var value in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = Convert.ToString(value.DeliveryTypeID), Value = value.DeliveryTypeName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.DistributionDocumentReferenceTypes:
                    {
                        var entities = new MetadataRepository().GetDocumentReferenceTypes(Eduegate.Services.Contracts.Enums.Systems.Distribution);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.ReferenceTypeID.ToString(), Value = entity.InventoryTypeName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ArraySize:
                    {
                        for (int i = 1; i <= 50; i++)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = i.ToString(), Value = i.ToString() });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ServiceProviders:
                    {
                        var entities = new DistributionRepository().GetServiceProviders();

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.ServiceProviderID.ToString(), Value = entity.ProviderName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.CityByCompany:
                    {
                        List<City> entities;
                        if (_callContext != null && _callContext.CompanyID.HasValue)
                            entities = new MutualRepository().GetCityByCompanyID(_callContext.CompanyID.Value);
                        else
                            entities = new MutualRepository().GetCities();

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.CityID.ToString(), Value = entity.CityName });
                        }
                    }
                    break;
                //case Services.Contracts.Enums.LookUpTypes.Manager:
                //    {
                //        var entities = new EmployeeBL(_callContext).GetEmployeesBySkus(1);

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.EmployeeIID.ToString(), Value = entity.EmployeeName });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.SHOP:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.RepairOrderRepository().GetShops();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.NO.ToString(), Value = entity.NAME });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.REPAIRCUSTOMER:
                //    {
                //        int customerCode = 0;
                //        int.TryParse(searchText, out customerCode);
                //        var entities = new Eduegate.Domain.Repository.Oracle.RepairOrderRepository().GetCustomers(customerCode);

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.CUSCODE.ToString(), Value = entity.CUSNAME });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.SYMPTOMCODE:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.RepairOrderRepository().GetSymptoms(searchText);

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.SYMCODE.ToString(), Value = entity.DESCRIPN });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.OPERATION:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.RepairOrderRepository().GetOperations(searchText);

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.OPRNCODE.ToString(), Value = entity.DESCRIPN });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.OPERATIONGROUP:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.RepairOrderRepository().GetOperationGroups(searchText);

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.OPGRPCODE.ToString(), Value = entity.DESCRIPN });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.REPAIRORDERTYPE:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.RepairOrderRepository().GetROType();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.CODE.ToString(), Value = entity.NAME });
                //        }
                //    }
                //    break;
                // Document reference Statuses
                case Services.Contracts.Enums.LookUpTypes.SALESORDERDOCUMENTREFERENCESTATUSES:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.SalesOrder);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.PURCHASERETURNDOCUMENTREFERENCESTATUSES:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.PurchaseReturn);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.SALESINVOICEDOCUMENTREFERENCESTATUSES:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.SalesInvoice);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.SALESQUOTATIONDOCUMENTREFERENCESTATUSES:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.SalesQuote);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.BRANCHTRANSFERDOCUMENTREFERENCESTATUSES:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.BranchTransfer);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.BranchTransferRequestDocumentStatus:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.BranchTransferRequest);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.PURCHASEORDERDOCUMENTREFERENCESTATUSES:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.PurchaseOrder);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.PURCHASEINVOICEDOCUMENTREFERENCESTATUSES:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.PurchaseInvoice);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.OREDERCHANGEREQUESTDOCUMENTREFERENCESTATUSES:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.OrderChangeRequest);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.JournalDocumentStatuses:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.Journal);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ReceivingMethod:
                    {
                        var entities = new SupplierRepository().GetReceivingMethodDetails();
                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.ReceivingMethodID.ToString(), Value = entity.ReceivingMethodName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ReturnMethod:
                    {
                        var entities = new SupplierRepository().GetReturnMethodDetails();
                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.ReturnMethodID.ToString(), Value = entity.ReturnMethodName });
                        }
                    }
                    break;
                //case Services.Contracts.Enums.LookUpTypes.DefectCode:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.RepairOrderRepository().GetDefectCode();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.CODE.ToString(), Value = entity.NAME });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.DefectSide:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.RepairOrderRepository().GetDefectSide();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.CODE.ToString(), Value = entity.NAME });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.Nationality:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetNationality();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.NAT_CODE.ToString(), Value = entity.NAT_NAME });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.HRDepartment:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().HRDepartment();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.DEPT_NO.ToString(), Value = entity.DEPT_NAME });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.LocationMaster:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetLocationMaster();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.LOC_CODE.ToString(), Value = entity.LOC_NAME });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.HRDesignation:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetDesigMaster();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.DESIG_CODE.ToString(), Value = entity.DESIG_NAME });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.MainDesignation:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetMainDesigMaster();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.MAIN_DESIG_CODE.ToString(), Value = entity.MAIN_DESIG_DESC });
                //        }
                //    }
                //    break;

                //case Services.Contracts.Enums.LookUpTypes.GroupDesignation:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetGroupDesigMaster();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.GROUP_CODE.ToString(), Value = entity.GROUP_DESC });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.Allowance:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetAllwanceMaster();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.ALLOW_CODE.ToString(), Value = entity.ALLOW_DESC });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.PayComp:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetPayComMaster();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.PAYCOMP_CODE.ToString(), Value = entity.PAYCOMP_NAME });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.PeriodSalary:
                //    {
                //        for (int i = 1; i <= 12; i++)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = i.ToString(), Value = i.ToString() });
                //        }
                //        //var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetPayComMaster();

                //        //foreach (var entity in entities)
                //        //{
                //        //    keyValues.Add(new KeyValueDTO() { Key = entity.PAYCOMP_CODE.ToString(), Value = entity.PAYCOMP_NAME });
                //        //}
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.ProductiveType:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetParamters("PR");

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.CODE.ToString(), Value = entity.NAME });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.EmploymentType:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetParamters("ER");

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.CODE.ToString(), Value = entity.NAME });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.RecruitmentType:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetParamters("EL");

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.CODE.ToString(), Value = entity.NAME });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.DocType:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetDocType(1);

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.ID.ToString(), Value = entity.DOCTYPE });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.ProcessStatus:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetProcessStatus();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.EMPLOYMENTREQPROCESSSTATUSID.ToString(), Value = entity.STATUSNAME });
                //        }
                //    }
                //    break;

                //case Services.Contracts.Enums.LookUpTypes.HRGender:
                //    {
                //        keyValues.Add(new KeyValueDTO { Key = "M", Value = "Male" });
                //        keyValues.Add(new KeyValueDTO { Key = "F", Value = "Female" });
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.EmployeeList:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetEmployeeList();

                //        foreach (var entity in entities)
                //        {
                //            if (string.IsNullOrEmpty(entity.F_NAME + entity.M_NAME + entity.L_NAME))
                //            {
                //                keyValues.Add(new KeyValueDTO() { Key = entity.EMPNO.ToString(), Value = entity.NAME });
                //            }
                //            else
                //            {
                //                keyValues.Add(new KeyValueDTO() { Key = entity.EMPNO.ToString(), Value = entity.F_NAME + " " + entity.M_NAME + " " + entity.L_NAME });
                //            }
                //        }
                //    }
                //    break;
                case Services.Contracts.Enums.LookUpTypes.HRMaritalStatus:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "1", Value = "Married" });
                        keyValues.Add(new KeyValueDTO() { Key = "2", Value = "Unmarried" });
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ContactStatus:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "1", Value = "Active" });
                        keyValues.Add(new KeyValueDTO() { Key = "2", Value = "In Active" });
                    }
                    break;
                //case Services.Contracts.Enums.LookUpTypes.Agents:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetAgents();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.NO.ToString(), Value = entity.NAME });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.ContractType:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetContractType("CNT");

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.CODE.ToString(), Value = entity.SHDES });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.QuotaType:
                //    {
                //        //keyValues.Add(new KeyValueDTO() { Key = "1", Value = "Jobs" });
                //        //keyValues.Add(new KeyValueDTO() { Key = "2", Value = "Non-Jobs" });

                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetQuotaType();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.QUOTA_TYPEID.ToString(), Value = entity.QUOTA_TYPE });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.VisaCompany:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetVisaCompanyList(0);

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.COMPANYSHOPLOCID.ToString(), Value = entity.PACI_NUMBER.ToString() });
                //        }
                //    }
                //    break;
                //case Services.Contracts.Enums.LookUpTypes.HRStatus:
                //    {
                //        var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetStatus();

                //        foreach (var entity in entities)
                //        {
                //            keyValues.Add(new KeyValueDTO() { Key = entity.CODE.ToString(), Value = entity.STATUSNAME });
                //        }
                //    }
                //    break;
                case Services.Contracts.Enums.LookUpTypes.ActionLinkType:
                    {
                        var entities = new MutualRepository().GetActionLinkTypes();
                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.ActionLinkTypeID.ToString(), Value = entity.ActionLinkTypeName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.PaymentMethod:
                    {
                        var entities = new ReferenceDataRepository().GetPaymentMethods();
                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.PaymentMethodID.ToString(), Value = entity.PaymentMethod1 });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.DBDepartment:
                    {
                        var entities = new EmploymentServiceRepository().GetDBDepartments();

                        foreach (var entity in entities)
                        {
                            //if (secured.HasAccess(entity.GetIID(), (Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ClaimType), Eduegate.Services.Contracts.Enums.ClaimType.Department.ToString())))
                            //{
                            keyValues.Add(new KeyValueDTO() { Key = entity.DepartmentID.ToString(), Value = entity.DepartmentName });
                        //}
                    }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.TypeOfJob:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "Full Time", Value = "Full Time" });
                        keyValues.Add(new KeyValueDTO() { Key = "Contract", Value = "Contract" });
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Category:
                    {
                        var entities = new CategoryRepository().GetParentCategoryID(1);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.CategoryIID.ToString(), Value = entity.CategoryName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ReplacementActions:
                    {
                        foreach (var item in Enum.GetValues(typeof(Framework.Helper.Enums.ReplacementActions)).Cast<Framework.Helper.Enums.ReplacementActions>() )
                        {
                            keyValues.Add(new KeyValueDTO() { Key =Convert.ToString((int)item), Value = item.ToString() });
                        }                        
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.ProductTypesName:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new ProductDetailRepository().GetProductTypes();
                            foreach(var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.ProductTypeID.ToString(), Value = entity.ProductTypeName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.JobOpeningStatus:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "ACTIVE", Value = "Active" });
                        keyValues.Add(new KeyValueDTO() { Key = "Archived", Value = "Archive" });
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.BooleanType:
                    {
                        foreach (var item in Enum.GetValues(typeof(Services.Contracts.Enums.BooleanType)).Cast<Services.Contracts.Enums.BooleanType>())
                        {
                            keyValues.Add(new KeyValueDTO() { Key = Convert.ToString((int)item), Value = item.ToString() });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.PriceListStatuses:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "1", Value = "Active" });   // True
                        keyValues.Add(new KeyValueDTO() { Key = "0", Value = "InActive" });
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.DocumentStatuses:
                    {
                        var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                        if (cacheItem == null)
                        {
                            var entities = new MutualBL(_callContext).GetDocumentStatus();
                            foreach (var entity in entities)
                            {
                                keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.StatusName });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                        }
                        else
                        {
                            keyValues = cacheItem;
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.CategorySettingsSortBy:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "", Value = "Relevance" });
                        keyValues.Add(new KeyValueDTO() { Key = "best-sellers", Value = "Best Sellers" });
                        keyValues.Add(new KeyValueDTO() { Key = "price-low", Value = "Price Low to High" });
                        keyValues.Add(new KeyValueDTO() { Key = "price-high", Value = "Price High to Low" });
                        keyValues.Add(new KeyValueDTO() { Key = "new-arrivals", Value = "New Arrivals" });
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.SelectOrNotSelect:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "true", Value = "Select" });
                        keyValues.Add(new KeyValueDTO() { Key = "false", Value = "Dont Select" });
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.MarketPlaceOrderStatuses:
                    {
                        keyValues.Add(new KeyValueDTO() { Key = "6", Value = "Accepted" });  
                        keyValues.Add(new KeyValueDTO() { Key = "7", Value = "Rejected" });
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.TreatmentGroup:
                    //{
                    //    var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                    //    if (cacheItem == null)
                    //    {
                    //        using (EduegatedERP_SalonContext dbContext = new EduegatedERP_SalonContext())
                    //        {
                    //            var datas = dbContext.TreatmentGroups.ToList().OrderBy(o => o.Description);

                    //            foreach (var entity in datas)
                    //            {
                    //                keyValues.Add(new KeyValueDTO() { Key = entity.TreatmentGroupID.ToString(), Value = entity.Description });
                    //            }
                    //        }

                    //        Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                    //    }
                    //    else
                    //    {
                    //        keyValues = cacheItem;
                    //    }
                    //}
                    break;
                case Services.Contracts.Enums.LookUpTypes.TreatmentType:
                    //{
                    //    var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                    //    if (cacheItem == null)
                    //    {
                    //        using (EduegatedERP_SalonContext dbContext = new EduegatedERP_SalonContext())
                    //        {
                    //            var datas = dbContext.TreatmentTypes.ToList().OrderBy(o => o.TreatmentName);

                    //            foreach (var entity in repository.GetAll().OrderBy(a => a.TreatmentName))
                    //            {
                    //                keyValues.Add(new KeyValueDTO() { Key = entity.TreatmentTypeID.ToString(), Value = entity.TreatmentName });
                    //            }
                    //        }

                    //        Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                    //    }
                    //    else
                    //    {
                    //        keyValues = cacheItem;
                    //    }
                    //}
                    break;
                case Services.Contracts.Enums.LookUpTypes.AvailableFor:
                    //{
                    //    var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                    //    if (cacheItem == null)
                    //    {
                    //        using (EduegatedERP_SalonContext dbContext = new EduegatedERP_SalonContext())
                    //        {
                    //            var datas = dbContext.ServiceAvailables.ToList().OrderBy(o => o.Description);

                    //            foreach (var entity in datas)
                    //            {
                    //                keyValues.Add(new KeyValueDTO() { Key = entity.ServiceAvailableID.ToString(), Value = entity.Description });
                    //            }
                    //        }

                    //        Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                    //    }
                    //    else
                    //    {
                    //        keyValues = cacheItem;
                    //    }
                    //}
                    break;
                case Services.Contracts.Enums.LookUpTypes.PricingType:
                    //{
                    //    var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                    //    if (cacheItem == null)
                    //    {
                    //        using (EduegatedERP_SalonContext dbContext = new EduegatedERP_SalonContext())
                    //        {
                    //            var datas = dbContext.PricingTypes.ToList().OrderBy(o => o.Description);

                    //            foreach (var entity in datas)
                    //            {
                    //                keyValues.Add(new KeyValueDTO() { Key = entity.PricingTypeID.ToString(), Value = entity.Description });
                    //            }
                    //        }

                    //        Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                    //    }
                    //    else
                    //    {
                    //        keyValues = cacheItem;
                    //    }
                    //}
                    break;
                case Services.Contracts.Enums.LookUpTypes.ExtraTimeType:
                    //{
                    //    var cacheItem = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("LOOKUP_" + lookType.ToString());

                    //    if (cacheItem == null)
                    //    {
                    //        using (EduegatedERP_SalonContext dbContext = new EduegatedERP_SalonContext())
                    //        {
                    //            var datas = dbContext.ExtraTimeTypes.ToList().OrderBy(o => o.Description);

                    //            foreach (var entity in datas)
                    //            {
                    //                keyValues.Add(new KeyValueDTO() { Key = entity.ExtraTimeTypeID.ToString(), Value = entity.Description });
                    //            }
                    //        }

                    //        Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(keyValues, "LOOKUP_" + lookType.ToString());
                    //    }
                    //    else
                    //    {
                    //        keyValues = cacheItem;
                    //    }
                    //}
                    break;
                case Services.Contracts.Enums.LookUpTypes.Duration:
                    {
                        for(int i= 5; i <= 60; i+=5)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = i.ToString(), Value = i.ToString() + " minuts" });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.PaymentModes:
                    {
                        var entities = new AccountTransactionRepository().GetPaymentModes();

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.PaymentModeID.ToString(), Value = entity.PaymentModeName });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.StudentStatus:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.TransportApplicationStatus:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.StudentPickupRequestStatus:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.Forms:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.MonthNames:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.BankName:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.BankShortNames:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.StopEntryStatus:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.LoanTypes:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.Quotations:

                    {
                        var values = new List<KeyValueDTO>();

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }

                    break;

                case Services.Contracts.Enums.LookUpTypes.BookType:
                    {
                        var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.TenderTypes:

                    {
                        var values = new List<KeyValueDTO>();

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }

                    break;

                case Services.Contracts.Enums.LookUpTypes.TenderStatuses:

                    {
                        var values = new List<KeyValueDTO>();

                        if (values == null || values.Count == 0)
                        {
                            values = new List<KeyValueDTO>();
                            foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                            {
                                values.Add(new KeyValueDTO()
                                {
                                    Value = lookup.Key,
                                    Key = lookup.Value
                                });
                            }

                            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
                        }

                        keyValues = values;
                    }

                    break;

                case Services.Contracts.Enums.LookUpTypes.AssetCode:
                    {
                        var entities = new FixedAssetsRepository().GetAssetCodes();

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.AssetIID.ToString(), Value = entity.AssetCode });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.AssetCategory:
                    {
                        var entities = new FixedAssetsRepository().GetAssetCategories();

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.AssetCategoryID.ToString(), Value = entity.CategoryName });
                        }
                    }
                    break;
                case Services.Contracts.Enums.LookUpTypes.AsstEntryManualDocumentStatuses:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.AssetEntry);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.AsstEntryPurchaseDocumentStatuses:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.AssetEntryPurchase);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.AssetDepreciationDocumentStatuses:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.AssetDepreciation);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.AssetDisposalDocumentStatuses:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.AssetRemoval);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.AssetTransferRequestDocumentStatuses:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.AssetTransferRequest);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.AssetTransferIssueDocumentStatuses:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.AssetTransferIssue);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;

                case Services.Contracts.Enums.LookUpTypes.AssetTransferReceiptDocumentStatuses:
                    {
                        var entities = new MutualRepository().GetDocumentStatusesByReferenceType((int)Framework.Enums.DocumentReferenceTypes.AssetTransferReceipt);

                        foreach (var entity in entities)
                        {
                            keyValues.Add(new KeyValueDTO() { Key = entity.DocumentStatusID.ToString(), Value = entity.DocumentStatus.StatusName });
                        }
                    }
                    break;
            }

            return keyValues;
        }

        public List<KeyValueDTO> GetDynamicLookUpData(DynamicLookUpType lookType, string searchText)
        {
            var values = new List<KeyValueDTO>();
            var cacheAttribute = EnumExtensions<DynamicLookUpType>.GetAttribute<Eduegate.Frameworks.Attributes.EnableCacheAttribute>(lookType);
            var enableCache = cacheAttribute != null && cacheAttribute.Enable;
            if (enableCache)
            {
                values = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get($"DynamicDataLookUp_{lookType}");
            }

            if (values == null || values.Count == 0)
            {
                values = new List<KeyValueDTO>();
                foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(),
                        enableCache ? string.Empty : searchText, _callContext.LoginID.Value, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                {
                    values.Add(new KeyValueDTO()
                    {
                        Value = lookup.Key,
                        Key = lookup.Value
                    });
                }

                if (lookType != DynamicLookUpType.AcademicYear || lookType != DynamicLookUpType.School)
                {
                    Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, $"DynamicDataLookUp_{lookType}");
                }
            }

            // if cache enabled search should happen on in memory variable
            if (enableCache && !string.IsNullOrEmpty(searchText))
            {
                values = values.Where(x => x.Value.Contains(searchText)).ToList();
            }

            return values;
        }

        public List<KeyValueDTO> GetDynamicLookUpDataForMobileApp(DynamicLookUpType lookType, string searchText,long loginID)
        {
            var values = new List<KeyValueDTO>(); // Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DynamicDataLookUp_" + lookType);

            if (values == null || values.Count == 0)
            {
                values = new List<KeyValueDTO>();
                foreach (var lookup in new ReferenceDataRepository().GetDynamicLookUpData(lookType.ToString(), searchText, loginID, (byte?)_callContext.SchoolID, _callContext.AcademicYearID))
                {
                    values.Add(new KeyValueDTO()
                    {
                        Value = lookup.Key,
                        Key = lookup.Value
                    });
                }

                Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DynamicDataLookUp_" + lookType);
            }

            return values;
        }

        public List<KeyValueDTO> GetDBLookUpData(string lookType)
        {
            var values = Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Get("DBDataLookUp_" + lookType);

            if (values == null || values.Count == 0)
            {
                values = new List<KeyValueDTO>();
                foreach (var lookup in new ReferenceDataRepository().GetDBLookUpData(lookType.ToString()))
                {
                    values.Add(new KeyValueDTO()
                    {
                        Value = lookup.LookupName,
                        Key = lookup.Value1
                    });
                }

                Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Add(values, "DBDataLookUp_" + lookType);
            }

            return values;
        }

        public List<LoginStatusDTO> GetLoginStatus()
        {
            List<LoginStatusDTO> dtoList = new List<LoginStatusDTO>();
            LoginStatusDTO dto = null;

            List<LoginStatus> entityList = new ReferenceDataRepository().GetLoginStatus();

            if (entityList.IsNotNull() && entityList.Count > 0)
            {
                foreach (LoginStatus entity in entityList)
                {
                    dto = new LoginStatusDTO();

                    dto.StatusID = entity.LoginStatusID;
                    dto.StatusDescription = entity.Description;

                    dtoList.Add(dto);
                }
            }

            return dtoList;
        }

        public List<CustomerStatusDTO> GetCustomerStatus()
        {
            List<CustomerStatus> customerStatus = new ReferenceDataRepository().GetCustomerStatus();
            List<CustomerStatusDTO> customerStatusList = new List<CustomerStatusDTO>();
            foreach (CustomerStatus status in customerStatus)
            {
                customerStatusList.Add(new CustomerStatusDTO()
                {
                    StatusID = status.CustomerStatusID,
                    StatusName = status.StatusName,

                });
            }

            return customerStatusList;
        }

        public WarehouseDTO GetWarehouse(long warehouseID)
        {
            return FromWarehouseEntity(new ReferenceDataRepository().GetWarehouse(warehouseID));
        }

        public WarehouseDTO SaveWarehouse(WarehouseDTO wareHouseDTO)
        {
            var updatedEntity = new ReferenceDataRepository().SaveWarehouse(ToWarehouseEntity(wareHouseDTO, _callContext));
            return FromWarehouseEntity(updatedEntity);
        }

        public static WarehouseDTO FromWarehouseEntity(Warehouse entity)
        {
            return new WarehouseDTO()
            {
                WareHouseID = entity.WarehouseID,
                WarehouseName = entity.WarehouseName,
                StatusID = Convert.ToByte(entity.StatusID),
                UpdatedBy = entity.UpdatedBy,
                CreatedBy = entity.CreatedBy,
                //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                CompanyID = entity.CompanyID
            };
        }

        public static Warehouse ToWarehouseEntity(WarehouseDTO dto, CallContext callContext)
        {
            var entity = new Warehouse()
            {
                UpdatedDate = DateTime.Now,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                UpdatedBy = int.Parse(callContext.LoginID.ToString()),
                WarehouseID = dto.WareHouseID,
                StatusID = dto.StatusID,
                WarehouseName = dto.WarehouseName,
                CompanyID = dto.CompanyID
            };

            if (entity.WarehouseID == 0)
            {
                entity.CreatedBy = int.Parse(callContext.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }
            if (entity.CompanyID == null)
            {

                entity.CompanyID = callContext.CompanyID;
            }
            return entity;
        }

        public BranchDTO GetBranch(long branchID)
        {
            return BranchMapper.Mapper(_callContext).ToDTO(new ReferenceDataRepository().GetBranch(branchID));
        }

        public BranchDTO SaveBranch(BranchDTO dto)
        {
            var mapper = BranchMapper.Mapper(_callContext);
            return mapper.ToDTO(new ReferenceDataRepository().SaveBranch(mapper.ToEntity(dto)));
        }

        public BranchGroupDTO GetBranchGroup(long groupID)
        {
            return FromBranchGroupEntity(new ReferenceDataRepository().GetBranchGroup(groupID));
        }

        public BranchGroupDTO SaveBranchGroup(BranchGroupDTO groupDTO)
        {
            var updatedEntity = new ReferenceDataRepository().SaveBranchGroup(ToBranchGroupEntity(groupDTO, _callContext));
            return FromBranchGroupEntity(updatedEntity);
        }

        public static BranchGroupDTO FromBranchGroupEntity(BranchGroup entity)
        {
            return new BranchGroupDTO()
            {
                BranchGroupIID = entity.BranchGroupIID,
                GroupName = entity.GroupName,
                StatusID = entity.StatusID,
                UpdatedBy = !entity.UpdatedBy.HasValue ? (int?)null : int.Parse(entity.UpdatedBy.ToString()),
                CreatedBy = !entity.CreatedBy.HasValue ? (int?)null : int.Parse(entity.CreatedBy.ToString()),
                //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                CompanyID= entity.CompanyID
            };
        }

        public static BranchGroup ToBranchGroupEntity(BranchGroupDTO dto, CallContext callContext)
        {
            var entity = new BranchGroup()
            {
                UpdatedDate = DateTime.Now,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                UpdatedBy = int.Parse(callContext.LoginID.ToString()),
                BranchGroupIID = dto.BranchGroupIID,
                StatusID = dto.StatusID,
                GroupName = dto.GroupName,
                CompanyID = dto.CompanyID != null ? dto.CompanyID : callContext.CompanyID
            };

            if (entity.BranchGroupIID == 0)
            {
                entity.CreatedBy = int.Parse(callContext.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }
            else
            {
                entity.CreatedDate = dto.CreatedDate;
                entity.CreatedBy = dto.CreatedBy;
            }

            return entity;
        }

        public CompanyDTO GetCompany(long ID)
        {
            return Mappers.CompanyMapper.Mapper(_callContext).ToDTO(new ReferenceDataRepository().GetCompany(ID));
        }

        public CompanyDTO GetCompanyByCountryID(long CountryID)
        {
            return Mappers.CompanyMapper.Mapper(_callContext).ToDTO(new ReferenceDataRepository().GetCompany(CountryID));
        }

        public List<CompanyDTO> GetCompanies()
        {
            return Mappers.CompanyMapper.Mapper(_callContext).ToDTO(new ReferenceDataRepository().GetCompany());
        }

        public CompanyDTO SaveCompany(CompanyDTO dto)
        {
            var mapper = Mappers.CompanyMapper.Mapper(_callContext);
            var updatedEntity = new ReferenceDataRepository().SaveCompany(mapper.ToEntity(dto));
            return mapper.ToDTO(updatedEntity);
        }

        public DepartmentDTO GetDepartment(long ID)
        {
            return FromDepartmentEntity(new ReferenceDataRepository().GetDepartment(ID));
        }

        public DepartmentDTO SaveDepartment(DepartmentDTO dto)
        {
            var updatedEntity = new ReferenceDataRepository().SaveDepartment(ToDepartmentEntity(dto, _callContext));
            return FromDepartmentEntity(updatedEntity);
        }

        public static DepartmentDTO FromDepartmentEntity(Department1 entity)
        {
            return new DepartmentDTO()
            {
                DepartmentID = entity.DepartmentID,
                //CompanyID = entity.CompanyID,
                DepartmentName = entity.DepartmentName,
                StatusID = Convert.ToByte(entity.StatusID),
                //UpdatedBy = !entity.CreatedBy.HasValue ? (int?)null : int.Parse(entity.UpdatedBy.ToString()),
                // CreatedBy = !entity.CreatedBy.HasValue ? (int?)null : int.Parse(entity.CreatedBy.ToString()),
                //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                // CreatedDate = entity.CreatedDate,
                //UpdatedDate = entity.UpdatedDate
            };
        }

        public static Department1 ToDepartmentEntity(DepartmentDTO dto, CallContext callContext)
        {
            var entity = new Department1()
            {
                //UpdatedDate = DateTime.Now,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                //UpdatedBy = int.Parse(callContext.LoginID.ToString()),
                DepartmentID = dto.DepartmentID,
                StatusID = dto.StatusID,
                DepartmentName = dto.DepartmentName,
                //CompanyID = dto.CompanyID != null ? dto.CompanyID : callContext.CompanyID
            };

            if (entity.DepartmentID == 0)
            {
                //entity.CreatedBy = int.Parse(callContext.LoginID.ToString());
                //entity.CreatedDate = DateTime.Now;
            }

            return entity;
        }

        public List<DeliveryTypeDTO> GetDeliveryTypeMaster()
        {
            List<DeliveryType> entities = new ReferenceDataRepository().GetDeliveryTypeMaster();
            return entities.Select(x => DeliveryTypeMapper.ToDto(x)).ToList();
        }
        public List<PackingTypeDTO> GetPackingTypeMaster()
        {
            List<PackingType> entities = new ReferenceDataRepository().GetPackingTypeMaster();
            return entities.Select(x => PackingTypeMapper.ToDto(x)).ToList();
        }

        public List<Eduegate.Services.Contracts.Search.ColumnDTO> SelectedViewColumns(Eduegate.Services.Contracts.Enums.SearchView view)
        {
            return null; // FromColumnEntity(new ReferenceDataRepository().SelectedViewColumns(ID));
        }

        public List<DocumentTypeDTO> GetDocumentTypesByID(Services.Contracts.Enums.DocumentReferenceTypes referenceType)
        {
            return DocumentTypeMapper.Mapper(_callContext).ToDTO(new ReferenceDataRepository().GetDocumentTypesByID((Eduegate.Framework.Enums.DocumentReferenceTypes)((int)referenceType), _callContext.CompanyID));
        }

        public List<DocumentTypeDTO> GetDocumentTypesBySystem(Services.Contracts.Enums.Systems system)
        {
            return DocumentTypeMapper.Mapper(_callContext).ToDTO(new ReferenceDataRepository().GetDocumentTypesBySystem(system.ToString(), _callContext.CompanyID));
        }

        public List<DocumentTypeDTO> GetDocumentTypesByReferenceAndBranch(int type, long branchID)
        {
            return DocumentTypeMapper.Mapper(_callContext).ToDTO(new ReferenceDataRepository().GetDocumentTypesByReferenceAndBranch(type, branchID, _callContext.CompanyID));
        }

        public DocumentTypeDTO GetDocumentTypesByHeadId(long headID)
        {
            return DocumentTypeMapper.Mapper(_callContext).ToDTO(new ReferenceDataRepository().GetDocumentTypesByHeadId(headID));
        }

        public KeyValueDTO GetDefaultCurrency()
        {
            var currencyID = _callContext != null && _callContext.CompanyID.HasValue ? int.Parse(new SettingRepository().GetSettingDetail("DEFAULT_CURRENCY_ID", _callContext.CompanyID.Value).SettingValue)
                : int.Parse(new SettingRepository().GetSettingDetail("DEFAULT_CURRENCY_ID").SettingValue);
            var currency = new ReferenceDataRepository().GetCurrency(currencyID);

            return new KeyValueDTO()
            {
                Key = currency.CurrencyID.ToString(),
                Value = currency.Name,
            };
        }

        public KeyValueDTO GetDefaultCurrencyWithName()
        {
            var defaultCurrency = _callContext != null && _callContext.CompanyID.HasValue ? new SettingRepository().GetSettingDetail("DEFAULT_CURRENCY_ID", _callContext.CompanyID.Value)
                : new SettingRepository().GetSettingDetail("DEFAULT_CURRENCY_ID");

            if (defaultCurrency != null)
            {
                var currency = new ReferenceDataRepository().GetCurrency(int.Parse(defaultCurrency.SettingValue));

                return new KeyValueDTO()
                {
                    Key = currency.CurrencyID.ToString(),
                    Value = currency.Name,
                };
            }
            else
            {
                return null; 
            }
        }

        public List<PaymentMethodDTO> GetPaymentMethods(int siteID)
        {
            var referenceDataRepository = new ReferenceDataRepository();
            var shoppingCratBl = new ShoppingCartBL(_callContext);
            var carts = shoppingCratBl.GetCart(_callContext);
            var contact = new AccountBL(_callContext).GetContactDetail((long)carts.ShippingAddressID);
            var detailbyLoginID = new AccountBL(_callContext).GetLoginDetailByLoginID(Convert.ToInt64(_callContext.LoginID));
            if (contact.IsNull() && carts.Products.Any(a => (a.DeliveryTypeID != (int)DeliveryTypes.Email) || (a.DeliveryTypeID != (int)DeliveryTypes.EmailInternationalDelivery)))
                carts.IsIntlCart = detailbyLoginID.RegisteredCountryID.HasValue ? !shoppingCratBl.IsInternationalCartByCountryID(detailbyLoginID.RegisteredCountryID.Value, siteID) : false;
            shoppingCratBl.UpdateCartIsInternational(carts.ShoppingCartID,carts.IsIntlCart);
            var paymentMethods = referenceDataRepository.GetPaymentBySitePaymentGroup(siteID, new CustomerBL(_callContext).CustomerVerificatonCheck(Convert.ToInt64(carts.CustomerID)), !carts.IsIntlCart, carts.IsEmailDeliveryInCart, _callContext.UserId);
            var paymentExceptions = referenceDataRepository.GetPaymentExceptions(carts.Products.Select(x => x.SKUID).ToList(), carts.ShippingAddressID.HasValue ? contact.IsNotNull() && contact.AreaID.HasValue ? contact.AreaID.Value : 0 : 0, siteID, carts.IsStorePickUpInCart ? (int)DeliveryTypes.StorePickup : (int)DeliveryTypes.None);
            paymentMethods.RemoveAll(x => paymentExceptions.Any(y => y.PaymentMethodID == x.PaymentMethodID));
           
            if (carts.Products.IsNotNull())
            {
                if (new UserServiceBL(_callContext).HasClaimAccess(1004, (long)_callContext.LoginID))
                {
                    if (!paymentMethods.Exists(x=>x.PaymentMethodID ==(int)Eduegate.Framework.Enums.PaymentMethod.COD))
                    {
                        paymentMethods.Add(referenceDataRepository.GetPaymentCOD((short)Eduegate.Framework.Enums.PaymentMethod.COD));
                    }
                }
            }
            else
            {
                return null;
            }
            return Mappers.Payments.PaymentMethodMapper.Mapper(_callContext).ToDTO(paymentMethods);
        }

        public ReWriteUrlTypeDTO GetUrlTypeByCode(string code)
        {
            var urlType = new ReferenceDataRepository().GetUrlTypeByCode(code);
            return new ReWriteUrlTypeDTO() { LevelNo = urlType.LevelNo, Url = urlType.Url, UrlType = (UrlType)Enum.Parse(typeof(UrlType), urlType.UrlType.ToString()), IID = urlType.IID };
        }


        public DeliveryTypeDTO GetDeliveryType(short deliveryTypeId)
        {
            // get the delivery type based on id
            var deliveryType = new ReferenceDataRepository().GetDeliveryType(deliveryTypeId);
            // convert from entity to dto
            var deliveryTypeDto = DeliveryTypeMapper.ToDto(deliveryType);
            return deliveryTypeDto;
        }

        public List<BranchDTO> GetMarketPlaceBranches()
        {
            var branches = new ReferenceDataRepository().GetMarketPlaceBranch(true);

            var claims = new SecurityBL(_callContext).GetClaims(_callContext.LoginID.Value, (int)Eduegate.Services.Contracts.Enums.ClaimType.Branches);

            var branchDTOs = new List<BranchDTO>();
            var mapper = Mappers.BranchMapper.Mapper(_callContext);
            var secured = new SecuredData(new Eduegate.Domain.Repository.Security.SecurityRepository().GetUserClaimKey(_callContext.LoginID.Value, (int)Eduegate.Services.Contracts.Enums.ClaimType.Branches));
            foreach (var branch in branches)
            {
                if (secured.HasAccess(branch.GetIID(), (Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ClaimType), Eduegate.Services.Contracts.Enums.ClaimType.Branches.ToString())))
                {
                    if (claims.Any(a => a.ResourceName == branch.BranchIID.ToString()))
                        branchDTOs.Add(mapper.ToDTO(branch, false));
                }
            }

            return branchDTOs;

        }

        public List<KeyValueDTO> GetMainDesignations(int gdesign_code)
        {
            var keyValues = new List<KeyValueDTO>();
            //var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetMainDesigMaster(gdesign_code);

            //foreach (var entity in entities)
            //{
            //    keyValues.Add(new KeyValueDTO() { Key = entity.MAIN_DESIG_CODE.ToString(), Value = entity.MAIN_DESIG_DESC });
            //}

            return keyValues;
        }

        public List<KeyValueDTO> GetHRDesignations(int mdesign_code)
        {
            var keyValues = new List<KeyValueDTO>();
            //var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetDesigMaster(mdesign_code);

            //foreach (var entity in entities)
            //{
            //    keyValues.Add(new KeyValueDTO() { Key = entity.DESIG_CODE.ToString(), Value = entity.DESIG_NAME });
            //}

            return keyValues;
        }

        public List<KeyValueDTO> GetAllowancebyPayComp(int paycomp)
        {
            var keyValues = new List<KeyValueDTO>();
            //var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetAllwanceMaster(paycomp);

            //foreach (var entity in entities)
            //{
            //    keyValues.Add(new KeyValueDTO() { Key = entity.ALLOW_CODE.ToString(), Value = entity.ALLOW_DESC });
            //}

            return keyValues;
        }

        public List<KeyValueDTO> GetDocType(string fileUploadType)
        {
            var keyValues = new List<KeyValueDTO>();
            //var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetDocType(fileUploadType);

            //foreach (var entity in entities)
            //{
            //    keyValues.Add(new KeyValueDTO() { Key = entity.FILEUPLOADTYPEID.ToString(), Value = entity.FILEUPLOADTYPENAME });
            //}

            return keyValues;
        }


        public List<KeyValueDTO> GetLocationByDept(int deptCode)
        {
            var keyValues = new List<KeyValueDTO>();
            //var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetLocationMaster(deptCode);

            //foreach (var entity in entities)
            //{
            //    keyValues.Add(new KeyValueDTO() { Key = entity.LOC_CODE.ToString(), Value = entity.LOC_NAME });
            //}

            return keyValues;
        }

        public bool IsReplacementorReappointmentEmpType(int empCode)
        {
            if (empCode == 3 || empCode == 4)
                return true;

            return false;
        }

        public List<KeyValueDTO> GetAgent()
        {
            var keyValues = new List<KeyValueDTO>();
            //var entities = new Eduegate.Domain.Repository.Oracle.EmploymentServiceRepository().GetAgents();

            //foreach (var entity in entities)
            //{
            //    keyValues.Add(new KeyValueDTO() { Key = entity.NO.ToString(), Value = entity.NAME });
            //}

            return keyValues;
        }

        public KeyValueDTO GetDefaultCountryWithName()
        {
            var countryID = _callContext != null && _callContext.CompanyID.HasValue ? int.Parse(new SettingRepository().GetSettingDetail("DEFAULTCOUNTRY", _callContext.CompanyID.Value).SettingValue)
                : int.Parse(new SettingRepository().GetSettingDetail("DEFAULTCOUNTRY").SettingValue);
            var country = new CountryMasterRepository().GetCountry(countryID);

            return new KeyValueDTO()
            {
                Key = country.CountryID.ToString(),
                Value = country.CountryName,
            };
        }

        public List<TaxDetailsDTO> GetTaxTemplateDetails(int documentID)
        {
            var dtos = new List<TaxDetailsDTO>();

            foreach (var taxItem in new ReferenceDataRepository().GetTaxDetailsByDocumentTypeID(documentID))
            {
                dtos.Add(new TaxDetailsDTO()
                {
                    TaxName = taxItem.TaxType.Description,
                    Amount = !taxItem.Amount.HasValue ? 0 : taxItem.Amount.Value,
                    Percentage = !taxItem.Percentage.HasValue ? 0 : taxItem.Percentage.Value,
                    TaxTypeID = taxItem.TaxTypeID,
                    TaxTemplateID = taxItem.TaxTemplateID,
                    TaxTemplateItemID = taxItem.TaxTemplateItemID,
                    HasTaxInclusive = taxItem.HasTaxInclusive.HasValue ? taxItem.HasTaxInclusive.Value : true,
                });
            }
            
            return dtos;
        }

        public TaxDetailsDTO GetTaxTemplateItem(int taxTemplateItemID)
        {
            var taxItem = new ReferenceDataRepository().GetTaxTemplateItem(taxTemplateItemID);
            var dto = new TaxDetailsDTO()
            {
                Amount = !taxItem.Amount.HasValue ? 0 : taxItem.Amount.Value,
                Percentage = !taxItem.Percentage.HasValue ? 0 : taxItem.Percentage.Value,
                TaxTypeID = taxItem.TaxTypeID,
                HasTaxInclusive = !taxItem.HasTaxInclusive.HasValue ? false : taxItem.HasTaxInclusive.Value,
            };

            return dto;
        }

        public List<CultureDataInfoDTO> GetCultureList()
        {
            var cultureDTOList = Framework.CacheManager.MemCacheManager<List<CultureDataInfoDTO>>.Get("CULTUREDATAINFO_" + _callContext.LoginID.Value.ToString());

            if (cultureDTOList == null)
            {
                cultureDTOList = new List<CultureDataInfoDTO>();
                var cultureList = new ReferenceDataRepository().GetCultureList();
                if (cultureList != null && cultureList.Count > 0)
                {
                    foreach (var culture in cultureList)
                    {
                        var cultureDTO = new CultureDataInfoDTO();
                        cultureDTO.CultureID = culture.CultureID;
                        cultureDTO.CultureName = culture.CultureName;
                        cultureDTO.CultureCode = culture.CultureCode;
                        cultureDTOList.Add(cultureDTO);
                    }
                }

                Framework.CacheManager.MemCacheManager<List<CultureDataInfoDTO>>.Add(cultureDTOList, "CULTUREDATAINFO_" + _callContext.LoginID.Value.ToString());
            }

            return cultureDTOList;
        }

        public List<BranchDTO> GetBranches()
        {
            var branches = new List<BranchDTO>();

            foreach(var branch in new ReferenceDataRepository().GetBranch(_callContext != null ? _callContext.LanguageCode : "en"))
            {
                branches.Add(new BranchDTO()
                {
                    BranchIID = branch.BranchIID,
                    BranchName = branch.BranchName,
                    BranchGroupID = branch.BranchGroupID,
                    CompanyID = branch.CompanyID
                });
            }

            return branches;
        }

        public List<KeyValueDTO> GetDynamicLookUpDataForReport(string lookupName, string searchText = "")
        {
            var keyValues = new List<KeyValueDTO>();
            switch (lookupName)
            {
                case "Class":

                    var classList = GetDynamicLookUpData(DynamicLookUpType.Classes, searchText).ToList();

                    keyValues.Add(new KeyValueDTO() { Key = "0", Value = "ALL" });

                    foreach (var entity in classList)
                    {
                        keyValues.Add(new KeyValueDTO() { Key = entity.Key, Value = entity.Value });
                    }
                    break;
                default:
                    var entities = GetDynamicLookUpData(GetDynamicLookUpType(lookupName), searchText).ToList();

                    keyValues.Add(new KeyValueDTO() { Key = "0", Value = "ALL" });

                    foreach (var entity in entities)
                    {
                        keyValues.Add(new KeyValueDTO() { Key = entity.Key, Value = entity.Value });
                    }
                    break;
            }

            return keyValues;
        }

        public DynamicLookUpType GetDynamicLookUpType(string lookupName)
        {
            // Use Enum.Parse to convert the string to the enum value
            if (Enum.TryParse(typeof(DynamicLookUpType), lookupName, true, out var result))
            {
                return (DynamicLookUpType)result;
            }
            else
            {
                // Handle cases where the string does not match any enum value
                throw new ArgumentException("Invalid lookupName", nameof(lookupName));
            }
        }

        public List<KeyValueDTO> GetLookupsByQuery(string query, string parameterKey, string parameterValue, byte? schoolID = null, int? academicYearID = null, List<KeyValueDTO> reportParameters = null)
        {
            var keyValues = new List<KeyValueDTO>();

            schoolID = schoolID.HasValue ? schoolID.Value : (byte?)_callContext.SchoolID;
            academicYearID = academicYearID.HasValue ? academicYearID.Value : _callContext.AcademicYearID;

            if (!string.IsNullOrEmpty(query))
            {
                keyValues = new ReferenceDataRepository().GetLookupsByQuery(query, parameterKey, parameterValue, _callContext.LoginID, schoolID, academicYearID, reportParameters);
            }

            return keyValues;
        }

        public List<KeyValueDTO> GetLookupsByProcedure(string procedure, string parameterKey, string parameterValue, byte? schoolID = null, int? academicYearID = null, List<KeyValueDTO> reportParameters = null)
        {
            var keyValues = new List<KeyValueDTO>();

            schoolID = schoolID.HasValue ? schoolID.Value : (byte?)_callContext.SchoolID;
            academicYearID = academicYearID.HasValue ? academicYearID.Value : _callContext.AcademicYearID;

            if (!string.IsNullOrEmpty(procedure))
            {
                keyValues = new ReferenceDataRepository().GetLookupsByProcedure(procedure, parameterKey, parameterValue, _callContext.LoginID, schoolID, academicYearID, reportParameters);
            }

            return keyValues;
        }

    }
}
