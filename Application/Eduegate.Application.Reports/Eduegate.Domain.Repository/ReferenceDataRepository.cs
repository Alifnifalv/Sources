using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Domain.Entity.Models.Inventory;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using Eduegate.Domain.Entity;
using Eduegate.Framework;

namespace Eduegate.Domain.Repository
{
    public class ReferenceDataRepository
    {
        public List<Culture> GetCultureList()
        {
            var cultures = new List<Culture>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                cultures = (from culture in dbContext.Cultures
                            select culture).ToList();
            }
            return cultures;

        }

        //Get List of country master details
        public List<Country> GetCountries(bool isActiveCurrency)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                //var countryList = (from currency in dbContext.Countries
                //                   where currency.ConversionRate != null                             
                //                   orderby currency.CurrencyCode
                //                   select currency).ToList();
                var countryList = (from country in dbContext.Countries select country).OrderBy(a=> a.CountryName);
                //if (isActiveCurrency)
                //{
                //    countryList = countryList.Where( x=> x.IsActiveForCurrency == true).ToList();
                //}

                return countryList.ToList();
            }
        }

        //Get List of passport issue country details
        public List<CountryMasterDTO> GetPassportIssueCountryMasters()
        {
            List<CountryMasterDTO> passportIssueCountryDetails = new List<CountryMasterDTO>();
            CountryMasterDTO passportIssueCountryDTO = null;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var passportCountryMasterList = (from currency in dbContext.CountryMasters
                                                 select currency).ToList();

                foreach (var countryMaster in passportCountryMasterList)
                {
                    passportIssueCountryDTO = new CountryMasterDTO();

                    passportIssueCountryDTO.CountryID = countryMaster.CountryID;
                    passportIssueCountryDTO.CountryCode = countryMaster.CountryCode;
                    passportIssueCountryDTO.CountryNameEn = countryMaster.CountryNameEn;
                    passportIssueCountryDTO.NoofDecimals = countryMaster.NoofDecimals;
                    passportIssueCountryDTO.Active = countryMaster.Active;
                    passportIssueCountryDTO.BaseCurrency = countryMaster.BaseCurrency;
                    passportIssueCountryDTO.ConversionRate = countryMaster.ConversionRate;
                    passportIssueCountryDTO.CountryNameAr = countryMaster.CountryNameAr;
                    passportIssueCountryDTO.DataFeedDateTime = countryMaster.DataFeedDateTime;
                    passportIssueCountryDTO.Operation = countryMaster.Operation;
                    passportIssueCountryDTO.RefGroupID = countryMaster.RefGroupID;

                    passportIssueCountryDetails.Add(passportIssueCountryDTO);
                }
            }

            return passportIssueCountryDetails;
        }

        //Get List of shipping address details
        public List<CheckOutDTO> GetShippingAddressMasters()
        {
            List<CheckOutDTO> shippingAddressDTOList = new List<CheckOutDTO>();
            CheckOutDTO shippingAddressDTO = null;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                shippingAddressDTO = new CheckOutDTO();
                shippingAddressDTO.ShippingAddressIID = 598;
                shippingAddressDTO.ShippingAddressName = "Madiwala";
                shippingAddressDTOList.Add(shippingAddressDTO);
            }

            return shippingAddressDTOList;
        }

        //Get List of billing address details
        public List<CheckOutDTO> GetBillingAddressMasters()
        {
            List<CheckOutDTO> billingAddressDTOList = new List<CheckOutDTO>();
            CheckOutDTO billingAddressDTO = null;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                billingAddressDTO = new CheckOutDTO();
                billingAddressDTO.BillingAddressIID = 599;
                billingAddressDTO.BillingAddressName = "M.G.Road";
                billingAddressDTOList.Add(billingAddressDTO);
            }

            return billingAddressDTOList;
        }

        public List<Lookup> GetDBLookUpData(string lookupType)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Lookups.Where(a=> a.LookupType == lookupType).ToList();
            }
        }

        public List<KeyValue> GetDynamicLookUpData(string lookupType, string searchText, long loginId,byte? schoolID,int? academicYearID )
        {
            var value = new List<KeyValue>();
            using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
            using (SqlCommand cmd = new SqlCommand("setting.spcGetDynamicLookUp", conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@lookupName", SqlDbType.NVarChar));
                adapter.SelectCommand.Parameters["@lookupName"].Value = lookupType;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@searchText", SqlDbType.NVarChar));
                adapter.SelectCommand.Parameters["@searchText"].Value = searchText;
               
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@LoginID", SqlDbType.BigInt));
                adapter.SelectCommand.Parameters["@LoginID"].Value = loginId;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@SchoolID", SqlDbType.TinyInt));
                adapter.SelectCommand.Parameters["@SchoolID"].Value = schoolID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@AcademicYearID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@AcademicYearID"].Value = academicYearID;

                DataSet dt = new DataSet();
                adapter.Fill(dt);
                DataTable lookUpDataTable = null;

                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        lookUpDataTable = dt.Tables[0];
                    }
                }

                if(lookUpDataTable != null)
                {
                    foreach (DataRow row in lookUpDataTable.Rows)
                    {
                        value.Add(new KeyValue() { Key = row["key"].ToString(), Value = row["value"].ToString() });
                    }
                }                
            }

            return value;
        }

        public List<LoginStatus> GetLoginStatus()
        {
            var loginStatuses = new List<LoginStatus>();

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    loginStatuses = (from loginStatus in dbContext.LoginStatuses
                                     select loginStatus).ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the login status details", TrackingCode.ERP);
            }

            return loginStatuses;

        }

        public List<Eduegate.Domain.Entity.Models.CustomerStatus> GetCustomerStatus()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.CustomerStatuses.ToList();
            }
        }

        public List<Warehouse> GetWarehouses(int? companyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (!companyID.HasValue)
                {
                    return dbContext.Warehouses.OrderBy(a=> a.WarehouseName).ToList();
                }
                else
                {
                    return dbContext.Warehouses.Where(x => x.CompanyID == companyID).OrderBy(a => a.WarehouseName).ToList();
                }
                //return dbContext.Warehouses.ToList();
            }
        }

        public Warehouse GetWarehouse(long warehouseID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Warehouses.Where(a => a.WarehouseID == warehouseID).FirstOrDefault();
            }
        }

        public Warehouse SaveWarehouse(Warehouse entity)
        {
            Warehouse updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.Warehouses.Add(entity);

                    if (!dbContext.Warehouses.Any(a => a.WarehouseID == entity.WarehouseID))
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    else
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.Warehouses.Where(x => x.WarehouseID == entity.WarehouseID).FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public List<Branch> GetBranch(string cultureCode)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var branches = dbContext.Branches
                    .Where(a=> a.StatusID == 1)
                    .ToList();

                if (cultureCode.IsNotNullOrEmpty() && cultureCode != "en")
                {
                    List<BranchCultureData> branchCultures = (from catCulture in dbContext.BranchCultureDatas
                                                        join culture in dbContext.Cultures on catCulture.CultureID equals culture.CultureID
                                                        where culture.CultureCode == cultureCode
                                                        select catCulture).ToList();
                    if (branchCultures.Any())
                    {
                        foreach (Branch cat in branches)
                        {
                            var result = branchCultures.Where(x => x.BranchID == cat.BranchIID).FirstOrDefault();
                            if (result.IsNotNull())
                                cat.BranchName = result.BranchName;
                        }
                    }
                }

                return branches;
            }
        }

        public List<Branch> GetBranch(int statusID, int companyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Branches.Where(a => a.StatusID == statusID && a.CompanyID == companyID).OrderBy(a=> a.BranchName).ToList();
            }

        }
        public List<Branch> GetInternalBranch()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Branches.Where(a => a.IsMarketPlace == null || a.IsMarketPlace == false).OrderBy(a=> a.BranchName).ToList();
            }
        }

        public List<Branch> GetInternalBranch(int statusID, int CompanyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Branches.Where(a => (a.IsMarketPlace == null || a.IsMarketPlace == false) && a.StatusID == statusID && a.CompanyID == CompanyID)
                    .OrderBy(a=> a.BranchName).ToList();
            }
        }

        public List<BranchStatus> GetBranchStatus()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.BranchStatuses.OrderBy(a=> a.StatusName).OrderBy(a=> a.StatusName).ToList();
            }
        }

        public List<Schools> GetSchool()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Schools.OrderBy(a => a.SchoolID).ToList();
            }
        }
        public List<CompanyStatus> GetCompanyStatus()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.CompanyStatuses.OrderBy(a=> a.StatusName).ToList();
            }
        }

        public List<WarehouseStatus> GetWarehouseStatus()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.WarehouseStatuses.OrderBy(a=> a.StatusName).ToList();
            }
        }

        public List<BranchGroupStatus> GetBranchGroupStatus()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.BranchGroupStatuses.OrderBy(a=> a.StatusName).ToList();
            }
        }

        public List<DepartmentStatus> GetDepartmentStatus()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DepartmentStatuses.OrderBy(a=> a.StatusName).ToList();
            }
        }

        public Branch GetBranch(long branchID, bool requiredAdditionlInfo = true)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var branch = dbContext.Branches.Where(a => a.BranchIID == branchID).FirstOrDefault();

                if (requiredAdditionlInfo)
                {
                    dbContext.Entry(branch).Collection(a => a.ProductPriceListBranchMaps).Load();

                    foreach (var price in branch.ProductPriceListBranchMaps)
                    {
                        dbContext.Entry(price).Reference(a => a.ProductPriceList).Load();
                    }

                    foreach (var doc in branch.BranchDocumentTypeMaps)
                    {
                        dbContext.Entry(doc).Reference(a => a.DocumentType).Load();
                    }

                    foreach (var doc in branch.BranchCultureDatas)
                    {
                        dbContext.Entry(doc).Reference(a => a.Culture).Load();
                    }
                }

                return branch;
            }
        }

        public Branch SaveBranch(Branch entity)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.Branches.Add(entity);

                    if (!dbContext.Branches.Any(a => a.BranchIID == entity.BranchIID))
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    else
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                    dbContext.ProductPriceListBranchMaps.RemoveRange(dbContext.ProductPriceListBranchMaps.Where(a => a.BranchID == entity.BranchIID));

                    foreach (var priceMap in entity.ProductPriceListBranchMaps)
                    {
                        dbContext.Entry(priceMap).State = System.Data.Entity.EntityState.Added;
                    }

                    dbContext.BranchDocumentTypeMaps.RemoveRange(dbContext.BranchDocumentTypeMaps.Where(a => a.BranchID == entity.BranchIID));

                    foreach (var doc in entity.BranchDocumentTypeMaps)
                    {
                        dbContext.Entry(doc).State = System.Data.Entity.EntityState.Added;
                    }

                    dbContext.BranchCultureDatas.RemoveRange(dbContext.BranchCultureDatas.Where(a => a.BranchID == entity.BranchIID));

                    foreach (var culture in entity.BranchCultureDatas)
                    {
                        dbContext.Entry(culture).State = System.Data.Entity.EntityState.Added;
                    }

                    dbContext.SaveChanges();
                    return GetBranch(entity.BranchIID);
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public List<BranchGroup> GetBranchGroup(int? CompanyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (!CompanyID.HasValue)
                {
                    return dbContext.BranchGroups.OrderBy(a => a.GroupName).ToList();
                }
                else
                {
                    return dbContext.BranchGroups.Where(x => x.CompanyID == CompanyID).OrderBy(a => a.GroupName).ToList();
                }
                //return dbContext.BranchGroups.ToList();
            }
        }

        public BranchGroup GetBranchGroup(long groupID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.BranchGroups.Where(a => a.BranchGroupIID == groupID).FirstOrDefault();
            }
        }

        public BranchGroup SaveBranchGroup(BranchGroup entity)
        {
            BranchGroup updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.BranchGroups.Add(entity);

                    if (!dbContext.BranchGroups.Any(a => a.BranchGroupIID == entity.BranchGroupIID))
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    else
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.BranchGroups.Where(x => x.BranchGroupIID == entity.BranchGroupIID).FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public List<Department> GetDepartments()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Departments.OrderBy(a=> a.DepartmentName).ToList();
            }
        }

        public Department GetDepartment(long ID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Departments.Where(a => a.DepartmentID == ID).FirstOrDefault();
            }
        }

        public Department SaveDepartment(Department entity)
        {
            Department updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if(entity.DepartmentID == 0)
                    {
                        var maxID = dbContext.Departments.Max(a => a.DepartmentID);
                        entity.DepartmentID = maxID == null ? 1 : long.Parse(maxID.ToString()) + 1;
                    }

                    dbContext.Departments.Add(entity);

                    if (!dbContext.Departments.Any(a => a.DepartmentID == entity.DepartmentID))
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    else
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.Departments.Where(x => x.DepartmentID == entity.DepartmentID).FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public Company GetCompany(long ID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {

                //var company = (from c in dbContext.Companies
                //               join cur in dbContext.Currencies on c.BaseCurrencyID equals cur.CurrencyID
                //               where c.CompanyID == ID
                //               select c)
                //              .Include(c => c.Currency).SingleOrDefault();


                var company = dbContext.Companies.Where(a => a.CompanyID == ID).FirstOrDefault();
                if (company.IsNotNull())
                {
                    dbContext.Entry(company).Reference(c => c.Currency).Load();
                }

                return company;
            }
        }


        public Company GetCompanyByCountryID(long CountryID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Companies.Where(a => a.CountryID == CountryID).FirstOrDefault();
            }
        }

        public List<Company> GetCompany()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {

                return (from a in dbContext.Companies
                        join b in dbContext.CompanyStatuses on a.StatusID equals b.CompanyStatusID
                        where b.CompanyStatusID == (int)Eduegate.Services.Contracts.Enums.CompanyStatus.Active
                        select a).Include(x=> x.Currency).ToList();
            }
        }

        public Company SaveCompany(Company entity)
        {
            Company updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.Companies.Add(entity);

                    if (!dbContext.Companies.Any(a => a.CompanyID == entity.CompanyID))
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    else
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.Companies.Where(x => x.CompanyID == entity.CompanyID).FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public List<Gender> GetGenders()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Genders.OrderBy(a=> a.Description).ToList();
            }
        }

        public List<MaritalStatus> GetMaritalStatuses()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.MaritalStatuses.OrderBy(a=> a.StatusName).ToList();
            }
        }
        public List<Designation> GetDesignations()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Designations.OrderBy(a=> a.DesignationName).ToList();
            }
        }
        public List<EmployeeRole> GetEmployeeRoles()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.EmployeeRoles.OrderBy(a=> a.EmployeeRoleName).ToList();
            }
        }
        public List<JobType> GetJobTypes()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.JobTypes.OrderBy(a=> a.JobTypeName).ToList();
            }
        }

        public List<DeliveryType> GetDeliveryTypeMaster()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DeliveryTypes.ToList();
            }
        }
        public List<PackingType> GetPackingTypeMaster()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.PackingTypes.ToList();
            }
        }
        public List<ViewColumn> SelectedViewColumns(Eduegate.Infrastructure.Enums.SearchView view, long loginID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                //var joined2 = from viewColumns in dbContext.ViewGridColumns
                //              join userViews in dbContext.UserViewColumnMaps
                //              on viewColumns.ViewColumnID equals userViews.ViewColumnID
                //              join userViewCol in dbContext.UserViews 
                //              on viewColumns
                //              where viewColumns.ViewID.Equals((long)view) && 
                //              select new { Name = p.Name, TypeID = pType.TypeID };

                return dbContext.ViewGridColumns.Where(x => x.ViewID == (long)view && x.IsDefault.Value).ToList();
            }
        }

        public List<DocumentType> GetDocumentTypesByID(DocumentReferenceTypes referenceType, int? companyID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                switch (referenceType)
                {
                    case DocumentReferenceTypes.All:
                        if (companyID.HasValue)
                            return dbContext.DocumentTypes
                                .Include(a => a.Workflow)
                                .Where(a => a.CompanyID == companyID.Value).ToList();
                        else
                            return dbContext.DocumentTypes
                                .Include(a => a.Workflow)
                                .ToList();
                    case DocumentReferenceTypes.WarehouseOperations:

                        if (companyID.HasValue)
                            return dbContext.DocumentTypes
                                .Include(a => a.Workflow)
                                .Where(a => (a.ReferenceTypeID == (int)DocumentReferenceTypes.InboundOperations
                                    || a.ReferenceTypeID == (int)DocumentReferenceTypes.OutboundOperations
                                    || a.ReferenceTypeID == (int)DocumentReferenceTypes.FailedOperations
                                    || a.ReferenceTypeID == (int)DocumentReferenceTypes.MarketplaceOperations
                                   ) && a.CompanyID == companyID.Value).ToList();
                        else
                            return dbContext.DocumentTypes
                                .Include(a => a.Workflow)
                                .Where(a => a.ReferenceTypeID == (int)DocumentReferenceTypes.InboundOperations
                                || a.ReferenceTypeID == (int)DocumentReferenceTypes.OutboundOperations
                                || a.ReferenceTypeID == (int)DocumentReferenceTypes.FailedOperations
                                || a.ReferenceTypeID == (int)DocumentReferenceTypes.MarketplaceOperations
                               ).ToList();
                    default:
                        if (companyID.HasValue)
                            return dbContext.DocumentTypes
                                .Include(a => a.Workflow)
                                .Where(a => a.ReferenceTypeID == (int)referenceType && a.CompanyID == companyID.Value).ToList();
                        else
                            return dbContext.DocumentTypes
                                .Include(a => a.Workflow)
                                .Where(a => a.ReferenceTypeID == (int)referenceType).ToList();
                }
            }
        }

        public List<DocumentType> GetDocumentTypesBySystem(string system, int? companyID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (companyID.HasValue)
                    return dbContext.DocumentTypes.
                        Include(a=> a.Workflow).
                        Where(a => a.System == system && a.CompanyID == companyID).OrderBy(a=> a.TransactionTypeName).ToList();
                else
                    return dbContext.DocumentTypes.
                        Include(a => a.Workflow).
                        Where(a => a.System == system).OrderBy(a => a.TransactionTypeName).ToList();
            }
        }

        public DocumentType GetDocumentTypesByHeadId(long headID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from th in dbContext.TransactionHeads                      
                        join dt in dbContext.DocumentTypes on th.DocumentTypeID equals dt.DocumentTypeID
                        join drt in dbContext.DocumentReferenceTypes on   dt.ReferenceTypeID equals drt.ReferenceTypeID
                        where th.HeadIID == headID
                        select dt).SingleOrDefault();
            }
        }

        public List<DocumentType> GetDocumentTypesByReferenceAndBranch(int typeID, long branchID, long? companyID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (companyID.HasValue)
                    if (branchID == 0)
                    {
                        return (from dt in dbContext.DocumentTypes
                                where dt.ReferenceTypeID == typeID && dt.CompanyID == companyID.Value
                                select dt)
                                                      .Include(a => a.Workflow)
                                                      .OrderBy(a => a.TransactionTypeName).ToList();
                    }
                    else
                    {
                        return (from dt in dbContext.DocumentTypes
                                join branchMap in dbContext.BranchDocumentTypeMaps on dt.DocumentTypeID equals branchMap.DocumentTypeID
                                where dt.ReferenceTypeID == typeID && branchMap.BranchID == branchID && dt.CompanyID == companyID.Value
                                select dt)
                                .Include(a => a.Workflow)
                                .OrderBy(a => a.TransactionTypeName).ToList();
                    }
                else
                    return (from dt in dbContext.DocumentTypes
                            join branchMap in dbContext.BranchDocumentTypeMaps on dt.DocumentTypeID equals branchMap.DocumentTypeID
                            where dt.ReferenceTypeID == typeID && branchMap.BranchID == branchID
                            select dt)
                            .Include(a => a.Workflow)
                            .OrderBy(a => a.TransactionTypeName).ToList();
            }
        }

        public DocumentType GetDocumentType(int documentTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DocumentTypes
                    .Include(a => a.Workflow)
                    .Where(a => a.DocumentTypeID == documentTypeID).FirstOrDefault();
            }
        }

        public List<Language> GetLanguages()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Languages.OrderBy(a=> a.Language1).ToList();
            }
        }

        public List<Currency> GetCurrencies()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Currencies.ToList();
            }
        }

        public List<Currency> GetCurrencies(bool isEnabled)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Currencies.Where(a => a.IsEnabled == isEnabled).OrderBy(a=> a.Name).ToList();
            }
        }

        public List<Country> GetCountries1()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Countries.OrderBy(a=> a.CountryName).ToList();
            }
        }

        public string GetCountryName(long countryID)
        {
            var countryName = string.Empty;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var countryList = dbContext.Countries.Where(x => x.CountryID == countryID).FirstOrDefault();
                if (countryList != null)
                {
                    countryName = countryList.CountryName;
                }
            }

            return countryName;
        }

        public List<Title> GetTitle()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Titles.OrderBy(a => a.TitleName).ToList();
            }
        }

        public List<Branch> GetMarketPlaceBranch(bool isMarketPlace, int CompanyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Branches
                    // commnete
                    //.Where(x => x.IsMarketPlace == isMarketPlace && x.CompanyID == CompanyID)
                    .OrderBy(x => x.BranchName)
                    .ToList();
            }
        }

        public bool IsMarketPlaceBranch(long branchID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Branches.Where(a => a.BranchIID == branchID && a.IsMarketPlace == true).Any();
            }
        }

        public Currency GetCurrency(int currencyID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Currencies.Where(a => a.CurrencyID == currencyID).FirstOrDefault();
            }
        }

        public List<Eduegate.Domain.Entity.Models.PaymentMethod> GetPaymentMethods(int siteID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.PaymentMethodSiteMaps.Where(a => a.SiteID == siteID).Select(a => a.PaymentMethod).ToList();
            }
        }

        public Eduegate.Domain.Entity.Models.PaymentMethod GetPaymentCOD(short paymentMethodID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.PaymentMethodSiteMaps.Where(a => a.PaymentMethodID == paymentMethodID).Select(a => a.PaymentMethod).FirstOrDefault();
            }
        }

        public Eduegate.Domain.Entity.Models.PaymentMethod GetPaymentMethod(short paymentMethodID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.PaymentMethods.Where(a => a.PaymentMethodID == paymentMethodID).FirstOrDefault();
            }
        }

        public List<Eduegate.Domain.Entity.Models.PaymentMethod> GetPaymentBySitePaymentGroup(int siteID, bool isCustomerVerified, bool isLocal, bool isDigitalCart)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                List<Eduegate.Domain.Entity.Models.PaymentMethod> paymentMethodList = new List<Eduegate.Domain.Entity.Models.PaymentMethod>();
                var paymentMethodGroupList = dbContext.PaymentGroups.Where
                    (a => a.SiteID == siteID && a.IsCustomerBlocked == isCustomerVerified && a.IsLocal == isLocal && a.IsDigitalcart == isDigitalCart)
                    .Select(a => a.PaymentMethods).ToList();
                foreach (var payment in paymentMethodGroupList)
                {
                    foreach (var item in payment)
                    {
                        paymentMethodList.Add(item);
                    }
                }
                return (from p in paymentMethodList
                        join m in dbContext.PaymentMethodSiteMaps.ToList() on p.PaymentMethodID equals m.PaymentMethodID
                        where m.SiteID == siteID
                        select p).ToList();
            }
        }

        public List<Eduegate.Domain.Entity.Models.PaymentMethod> GetPaymentExceptions(List<long> skuIds,int areaID,int siteID,int deliveryTypeID=0)//we have to move deliverytypeID to generic way just for kuwait release doing this
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.SKUPaymentMethodExceptionMap.Where(x => (skuIds.Contains((long)x.SKUID) && x.SiteID == siteID) || x.AreaID == areaID || x.DeliveryTypeID == deliveryTypeID).Select(a => a.PaymentMethod).ToList();
            }
        }

        public List<Eduegate.Domain.Entity.Models.PaymentMethod> GetPaymentMethods()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.PaymentMethods.OrderBy(a => a.PaymentMethod1).ToList();
            }
        }

        public List<Eduegate.Domain.Entity.Models.Site> GetSites()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Sites.ToList();
            }
        }       

        public ReWriteUrlType GetUrlTypeByCode(string code)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var type = new ReWriteUrlType();
                var category = dbContext.Categories.Where(a => a.CategoryCode.Equals(code)).FirstOrDefault();

                if (category != null)
                {
                    type.IID = category.CategoryIID;
                    type.UrlType = (int)Eduegate.Services.Contracts.UrlReWriter.UrlType.Category;
                    type.LevelNo = category.ParentCategoryID.HasValue ? 1 : 0;
                }
                else
                {
                    var product = dbContext.ProductSKUMaps.Where(a => a.ProductSKUCode.Equals(code)).FirstOrDefault();

                    if (product != null)
                    {
                        type.IID = product.ProductSKUMapIID;
                        type.UrlType = (int)Eduegate.Services.Contracts.UrlReWriter.UrlType.Product;
                    }
                    else
                    {
                        var brand = dbContext.Brands.Where(a => a.BrandCode.Equals(code)).FirstOrDefault();

                        if (brand != null)
                        {
                            type.IID = brand.BrandIID;
                            type.UrlType = (int)Eduegate.Services.Contracts.UrlReWriter.UrlType.Brand;
                        }
                    }
                }

                return type;
            }
        }

        public DeliveryType GetDeliveryType(short deliveryTypeId)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.DeliveryTypes.Where(x => x.DeliveryTypeID == deliveryTypeId).FirstOrDefault();
            }
        }

        public ProductPriceListBranchMap GetBranchPriceListMaps(long branchID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ProductPriceListBranchMaps.Where(a => a.BranchID == branchID).FirstOrDefault();
            }
        }

        public TaxTemplate GetTaxDetails(long taxTemplateID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.TaxTemplates.Where(a => a.TaxTemplateID == taxTemplateID).FirstOrDefault();
            }
        }

        public List<TaxTemplateItem> GetTaxDetailsByDocumentTypeID(long documentID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from doc in dbContext.DocumentTypes
                       join tax in dbContext.TaxTemplates on doc.TaxTemplateID equals tax.TaxTemplateID
                       join taxItem in dbContext.TaxTemplateItems on tax.TaxTemplateID equals taxItem.TaxTemplateID
                       where doc.DocumentTypeID == documentID
                       select taxItem)
                       .Include("TaxTemplate")
                       .Include(a=> a.TaxType).ToList();
            }
        }

        public TaxTemplateItem GetTaxTemplateItem(int taxTemplateItemID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from doc in dbContext.TaxTemplateItems
                        where doc.TaxTemplateID == taxTemplateItemID
                        select doc).FirstOrDefault();
            }
        }

        public List<Eduegate.Domain.Entity.Models.Condition> GetConditions()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Conditions.ToList();
            }
        }

        public List<TaxTemplate> GetTaxTemplates()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.TaxTemplates
                    .Include(a=> a.TaxTemplateItems)
                    .ToList();
            }
        }

        public TaxTemplate GetTaxTemplates(int ID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.TaxTemplates
                    .Include(a => a.TaxTemplateItems)
                    .Where(a=> a.TaxTemplateID == ID).FirstOrDefault();
            }
        }

        public TaxTemplate SaveTaxTemplate(TaxTemplate entity)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                dbContext.TaxTemplates.Add(entity);

                if (!dbContext.TaxTemplates.Any(a => a.TaxTemplateID == entity.TaxTemplateID))
                {
                    var lastID = dbContext.TaxTemplates.Max(a => (int?)a.TaxTemplateID);
                    entity.TaxTemplateID = lastID.HasValue ? lastID.Value + 1 : 1;
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                }
                else
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                foreach(var item in entity.TaxTemplateItems)
                {
                    if (!dbContext.TaxTemplateItems.Any(a => a.TaxTemplateItemID == item.TaxTemplateItemID))
                    {
                        var lastID = dbContext.TaxTemplateItems.Max(a =>  (int?)a.TaxTemplateItemID);
                        item.TaxTemplateItemID = lastID.HasValue ? lastID.Value + 1 : 1;
                        dbContext.Entry(item).State = System.Data.Entity.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                }

                dbContext.SaveChanges();
                var updatedEntity = dbContext.TaxTemplates.Where(x => x.TaxTemplateID == entity.TaxTemplateID).FirstOrDefault();

                return updatedEntity;
            }
        }

        public List<TaxType> GetTaxTypes()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.TaxTypes
                    .ToList();
            }
        }

        public List<UserDeviceMap> GetRegisteredUserDevice(long loginID)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.UserDeviceMaps
                    .Where(x => x.LoginID == loginID && x.IsActive == true)
                    .ToList();
            }
        }

        public List<Branch> GetPhysicalBranches(string cultureCode)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var branches = dbContext.Branches
                    .Where(a => (!a.IsVirtual.HasValue || !a.IsVirtual.Value))
                    .ToList();

                if (cultureCode.IsNotNullOrEmpty() && cultureCode != "en")
                {
                    var branchCultures = (from catCulture in dbContext.BranchCultureDatas
                                          join culture in dbContext.Cultures
                                          on catCulture.CultureID equals culture.CultureID
                                          where culture.CultureCode == cultureCode
                                          select catCulture)
                                        .ToList();
                    if (branchCultures.Any())
                    {
                        foreach (Branch cat in branches)
                        {
                            var result = branchCultures.Where(x => x.BranchID == cat.BranchIID).FirstOrDefault();
                            if (result.IsNotNull())
                                cat.BranchName = result.BranchName;
                        }
                    }
                }

                return branches;
            }
        }

        public List<DeliveryTypeTimeSlotMap> GetOccupiedTimeSlots(DateTime? transactionDate)
        {
            var slot = new List<DeliveryTypeTimeSlotMap>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
                {
                    var cmd = new SqlCommand("select * from inventory.DeliveryOccupiedTimeSlotView where TransactionDate=Cast('"
                            + transactionDate.Value.ToLongDateString() + "' as date)", connection);
                    var adapter = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    adapter.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            slot.Add(new DeliveryTypeTimeSlotMap()
                            {
                                DeliveryTypeID = int.Parse(row["DeliveryTypeID"].ToString()),
                                DeliveryTypeTimeSlotMapIID = int.Parse(row["DeliveryTimeslotID"].ToString()),
                                NoOfCutOffOrder = int.Parse(row["NumberOfOrders"].ToString()),
                            });
                        }
                    }

                    return slot;
                }
            }
            catch
            {
                return slot;
            }
        }

        public List<DeliveryTypes1> GetActiveDeliverySettings()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.DeliveryTypes1
                    .Include(x => x.DeliveryTypeTimeSlotMaps)
                    .Include(x => x.ProductDeliveryTypeMaps)
                    .Include(x => x.CustomerGroupDeliveryTypeMaps)
                    .Include(x => x.DeliveryTypeAllowedAreaMaps)
                    .Include(x => x.DeliveryTypeCultureDatas)
                    .Include(x => x.DeliveryTypeCutOffSlots)
                    .Where(x => x.StatusID.Value == 1)
                    .OrderBy(x => x.Priority)
                    .ToList();
            }
        }

        public List<DeliveryTypeCutOffSlot> GetCutOffDeliveryTimeSlots()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.DeliveryTypeCutOffSlots
                    .ToList();
            }
        }

        public List<DeliveryTypeTimeSlotMap> GetDefaultDeliverySlots()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.DeliveryTypeTimeSlotMaps
                    .Where(x => !x.DeliveryTypeID.HasValue).ToList();
            }
        }

        public DocumentType SaveDocumentType(DocumentType entity)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                // get maximum ID from DocumentTypes
                if (entity.DocumentTypeID <= 0)
                {
                    var documentTypeId = dbContext.DocumentTypes.Max(x => (int?)x.DocumentTypeID);
                    documentTypeId = (documentTypeId.HasValue ? documentTypeId : 0) + 1;
                    entity.DocumentTypeID = documentTypeId.Value;
                }

                dbContext.DocumentTypes.Add(entity);

                if (entity.DocumentTypeID > 0 && dbContext.DocumentTypes.Any(x => x.DocumentTypeID == entity.DocumentTypeID))
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                else
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                dbContext.SaveChanges();
                return entity;
            }
        }

        public Supplier GetSupplierByBranchnID(long branchID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.Suppliers.FirstOrDefault(x => x.BranchID == branchID);
            }
        }

        public Supplier GetSupplier(long supplierId)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.Suppliers
                    .FirstOrDefault(x => x.SupplierIID == supplierId);
            }
        }

        public List<EntityScheduler> GetEntityScheduler(int schedulerTypeID, string entityID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.EntitySchedulers.Where(a => a.SchedulerTypeID == schedulerTypeID && a.EntityID == entityID).ToList();
            }
        }

        public decimal GetCurrencyPrice(CallContext context)
        {
            //var db = new dbSkienERPContext();
            //var ConvertedPrice = context == null || context.CurrencyCode == null ? 1 : (from c in _dbContext.Countries
            //                      where c.CurrencyCode == context.CurrencyCode && c.ConversionRate != null
            //                      select c.ConversionRate).FirstOrDefault();
            //return Convert.ToDecimal(ConvertedPrice);
            return 1;
        }

        public string GetAreaLocationName(int locationId)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var location = dbContext.Locations
                    .FirstOrDefault(x => x.LocationIID == locationId);
                return location != null ? location.LocationCode : null;
            }
        }

    }
}
