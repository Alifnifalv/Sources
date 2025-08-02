using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
using System.Data;
using Eduegate.Domain.Entity;
using Eduegate.Framework;
using Microsoft.Data.SqlClient;
using Eduegate.Domain.Entity.Supports;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Domain.Repository
{
    public class ReferenceDataRepository
    {
        private static ShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository();
        public List<Culture> GetCultureList()
        {
            var cultures = new List<Culture>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                cultures = (from culture in dbContext.Cultures
                            select culture).AsNoTracking().ToList();
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
                var countryList = (from country in dbContext.Countries select country).OrderBy(a => a.CountryName);
                //if (isActiveCurrency)
                //{
                //    countryList = countryList.Where( x=> x.IsActiveForCurrency == true).ToList();
                //}

                return countryList.AsNoTracking().ToList();
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
                                                 select currency).AsNoTracking().ToList();

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
                return dbContext.Lookups.Where(a => a.LookupType == lookupType).AsNoTracking().ToList();
            }
        }

        public List<KeyValue> GetDynamicLookUpData(string lookupType, string searchText, long loginId, byte? schoolID, int? academicYearID)
        {
            var value = new List<KeyValue>();
            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
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

                if (lookUpDataTable != null)
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
                                     select loginStatus).AsNoTracking().ToList();
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
                return dbContext.CustomerStatuses.AsNoTracking().ToList();
            }
        }

        public List<Warehouse> GetWarehouses(int? companyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (!companyID.HasValue)
                {
                    return dbContext.Warehouses.OrderBy(a => a.WarehouseName).AsNoTracking().ToList();
                }
                else
                {
                    return dbContext.Warehouses.Where(x => x.CompanyID == companyID).OrderBy(a => a.WarehouseName).AsNoTracking().ToList();
                }
                //return dbContext.Warehouses.ToList();
            }
        }

        public Warehouse GetWarehouse(long warehouseID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Warehouses.Where(a => a.WarehouseID == warehouseID).AsNoTracking().FirstOrDefault();
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
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    else
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.Warehouses.Where(x => x.WarehouseID == entity.WarehouseID).AsNoTracking().FirstOrDefault();
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
                    .Where(a => a.StatusID == 1)
                    .ToList();

                if (cultureCode.IsNotNullOrEmpty() && cultureCode != "en")
                {
                    List<BranchCultureData> branchCultures = (from catCulture in dbContext.BranchCultureDatas
                                                              join culture in dbContext.Cultures on catCulture.CultureID equals culture.CultureID
                                                              where culture.CultureCode == cultureCode
                                                              select catCulture).AsNoTracking().ToList();
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
                return dbContext.Branches.Where(a => a.StatusID == statusID && a.CompanyID == companyID).OrderBy(a => a.BranchName).AsNoTracking().ToList();
            }

        }
        public List<Branch> GetInternalBranch()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Branches.Where(a => a.IsMarketPlace == null || a.IsMarketPlace == false).OrderBy(a => a.BranchName).AsNoTracking().ToList();
            }
        }

        public List<Branch> GetInternalBranch(int statusID, int CompanyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Branches.Where(a => (a.IsMarketPlace == null || a.IsMarketPlace == false) && a.StatusID == statusID && a.CompanyID == CompanyID)
                    .OrderBy(a => a.BranchName).AsNoTracking().ToList();
            }
        }

        public List<BranchStatus> GetBranchStatus()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.BranchStatuses.OrderBy(a => a.StatusName).OrderBy(a => a.StatusName).AsNoTracking().ToList();
            }
        }

        public List<Schools> GetSchool()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Schools.OrderBy(a => a.SchoolID).AsNoTracking().ToList();
            }
        }
        public List<CompanyStatus> GetCompanyStatus()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.CompanyStatuses.OrderBy(a => a.StatusName).AsNoTracking().ToList();
            }
        }

        public List<WarehouseStatus> GetWarehouseStatus()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.WarehouseStatuses.OrderBy(a => a.StatusName).AsNoTracking().ToList();
            }
        }

        public List<BranchGroupStatus> GetBranchGroupStatus()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.BranchGroupStatuses.OrderBy(a => a.StatusName).AsNoTracking().ToList();
            }
        }

        public List<DepartmentStatus> GetDepartmentStatus()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DepartmentStatuses.OrderBy(a => a.StatusName).AsNoTracking().ToList();
            }
        }

        public Branch GetBranch(long branchID, bool requiredAdditionlInfo = true)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var branch = dbContext.Branches.Where(a => a.BranchIID == branchID)
                    .Include(i => i.ProductPriceListBranchMaps).ThenInclude(i => i.ProductPriceList)
                    .Include(i => i.BranchDocumentTypeMaps).ThenInclude(i => i.DocumentType)
                    .Include(i => i.BranchCultureDatas)
                    .AsNoTracking()
                    .FirstOrDefault();

                //if (requiredAdditionlInfo)
                //{
                //    dbContext.Entry(branch).Collection(a => a.ProductPriceListBranchMaps).Load();

                //    foreach (var price in branch.ProductPriceListBranchMaps)
                //    {
                //        dbContext.Entry(price).Reference(a => a.ProductPriceList).Load();
                //    }

                //    foreach (var doc in branch.BranchDocumentTypeMaps)
                //    {
                //        dbContext.Entry(doc).Reference(a => a.DocumentType).Load();
                //    }

                //    foreach (var doc in branch.BranchCultureDatas)
                //    {
                //        dbContext.Entry(doc).Reference(a => a.Culture).Load();
                //    }
                //}

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

                    if (!dbContext.Branches.AsNoTracking().Any(a => a.BranchIID == entity.BranchIID))
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        var priceListIds = entity.ProductPriceListBranchMaps.Select(x => x.ProductPriceListID).ToList();

                        var existDocumentIds = entity.BranchDocumentTypeMaps.Select(x => x.DocumentTypeID).ToList();

                        var existCultureIds = entity.BranchCultureDatas.Select(x => x.CultureID).ToList();

                        //TO remove old mapping data not used
                        using (dbEduegateERPContext dbContext1 = new dbEduegateERPContext())
                        {
                            dbContext1.ProductPriceListBranchMaps.RemoveRange(dbContext1.ProductPriceListBranchMaps
                            .AsNoTracking().Where(a => a.BranchID == entity.BranchIID && !priceListIds.Contains(a.ProductPriceListID)));

                            dbContext1.BranchDocumentTypeMaps.RemoveRange(dbContext1.BranchDocumentTypeMaps.AsNoTracking()
                            .Where(a => a.BranchID == entity.BranchIID && !existDocumentIds.Contains(a.DocumentTypeID)));

                            dbContext1.BranchCultureDatas.RemoveRange(dbContext1.BranchCultureDatas.AsNoTracking()
                            .Where(a => a.BranchID == entity.BranchIID && !existCultureIds.Contains(a.CultureID)));

                            dbContext1.SaveChanges();
                        }

                        foreach (var priceMap in entity.ProductPriceListBranchMaps)
                        {
                            if (priceMap.ProductPriceListBranchMapIID == 0)
                            {
                                dbContext.Entry(priceMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(priceMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }

                        foreach (var doc in entity.BranchDocumentTypeMaps)
                        {
                            if (doc.BranchDocumentTypeMapIID == 0)
                            {
                                dbContext.Entry(doc).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(doc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }

                        foreach (var culture in entity.BranchCultureDatas)
                        {
                            if (existCultureIds.Contains(culture.CultureID))
                            {
                                dbContext.Entry(culture).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContext.Entry(culture).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    dbContext.SaveChanges();

                    return GetBranch(entity.BranchIID);
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw;
            }
        }

        public List<BranchGroup> GetBranchGroup(int? CompanyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (!CompanyID.HasValue)
                {
                    return dbContext.BranchGroups.OrderBy(a => a.GroupName).AsNoTracking().ToList();
                }
                else
                {
                    return dbContext.BranchGroups.Where(x => x.CompanyID == CompanyID).OrderBy(a => a.GroupName).AsNoTracking().ToList();
                }
                //return dbContext.BranchGroups.ToList();
            }
        }

        public BranchGroup GetBranchGroup(long groupID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.BranchGroups.Where(a => a.BranchGroupIID == groupID).AsNoTracking().FirstOrDefault();
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
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    else
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.BranchGroups.Where(x => x.BranchGroupIID == entity.BranchGroupIID).AsNoTracking().FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public List<Department1> GetDepartments()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Departments1.OrderBy(a => a.DepartmentName).ToList();
            }
        }

        public Department1 GetDepartment(long ID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Departments1.Where(a => a.DepartmentID == ID).AsNoTracking().FirstOrDefault();
            }
        }

        public Department1 SaveDepartment(Department1 entity)
        {
            Department1 updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (entity.DepartmentID == 0)
                    {
                        var maxID = dbContext.Departments1.Max(a => (int?)a.DepartmentID);
                        entity.DepartmentID = maxID == null ? 1 : int.Parse(maxID.ToString()) + 1;
                    }

                    dbContext.Departments1.Add(entity);

                    if (!dbContext.Departments1.Any(a => a.DepartmentID == entity.DepartmentID))
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    else
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.Departments1.Where(x => x.DepartmentID == entity.DepartmentID).AsNoTracking().FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw;
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


                var company = dbContext.Companies.Where(a => a.CompanyID == ID)
                    .Include(i => i.Currency)
                    .AsNoTracking()
                    .FirstOrDefault();
                //if (company.IsNotNull())
                //{
                //    dbContext.Entry(company).Reference(c => c.Currency).Load();
                //}

                return company;
            }
        }


        public Company GetCompanyByCountryID(long CountryID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Companies.Where(a => a.CountryID == CountryID).AsNoTracking().FirstOrDefault();
            }
        }

        public List<Company> GetCompany()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {

                return (from a in dbContext.Companies
                        join b in dbContext.CompanyStatuses on a.StatusID equals b.CompanyStatusID
                        where b.CompanyStatusID == (int)Eduegate.Services.Contracts.Enums.CompanyStatus.Active
                        select a).Include(x => x.Currency).AsNoTracking().ToList();
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
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    else
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.Companies.Where(x => x.CompanyID == entity.CompanyID).AsNoTracking().FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw;
            }

            return updatedEntity;
        }

        public List<Gender> GetGenders()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Genders.OrderBy(a => a.Description).ToList();
            }
        }

        public List<MaritalStatus> GetMaritalStatuses()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.MaritalStatuses.OrderBy(a => a.StatusName).AsNoTracking().ToList();
            }
        }
        public List<Designation> GetDesignations()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Designations.OrderBy(a => a.DesignationName).AsNoTracking().ToList();
            }
        }
        public List<EmployeeRole> GetEmployeeRoles()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.EmployeeRoles.OrderBy(a => a.EmployeeRoleName).AsNoTracking().ToList();
            }
        }
        public List<JobType> GetJobTypes()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.JobTypes.OrderBy(a => a.JobTypeName).AsNoTracking().ToList();
            }
        }

        public List<DeliveryType> GetDeliveryTypeMaster()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DeliveryTypes.AsNoTracking().ToList();
            }
        }
        public List<PackingType> GetPackingTypeMaster()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.PackingTypes.AsNoTracking().ToList();
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

                return dbContext.ViewGridColumns.Where(x => x.ViewID == (long)view && x.IsDefault.Value).AsNoTracking().ToList();
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
                                .Where(a => a.CompanyID == companyID.Value).AsNoTracking().ToList();
                        else
                            return dbContext.DocumentTypes
                                .Include(a => a.Workflow)
                                .AsNoTracking()
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
                                .Where(a => a.ReferenceTypeID == (int)referenceType && a.CompanyID == companyID.Value)
                                .AsNoTracking()
                                .ToList();
                        else
                            return dbContext.DocumentTypes
                                .Include(a => a.Workflow)
                                .Where(a => a.ReferenceTypeID == (int)referenceType)
                                .AsNoTracking()
                                .ToList();
                }
            }
        }

        public List<DocumentType> GetDocumentTypesBySystem(string system, int? companyID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (companyID.HasValue)
                    return dbContext.DocumentTypes.
                        Include(a => a.Workflow).
                        Where(a => a.System == system && a.CompanyID == companyID).OrderBy(a => a.TransactionTypeName)
                        .AsNoTracking()
                        .ToList();
                else
                    return dbContext.DocumentTypes.
                        Include(a => a.Workflow).
                        Where(a => a.System == system).OrderBy(a => a.TransactionTypeName)
                        .AsNoTracking()
                        .ToList();
            }
        }

        public DocumentType GetDocumentTypesByHeadId(long headID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from th in dbContext.TransactionHeads
                        join dt in dbContext.DocumentTypes on th.DocumentTypeID equals dt.DocumentTypeID
                        join drt in dbContext.DocumentReferenceTypes on dt.ReferenceTypeID equals drt.ReferenceTypeID
                        where th.HeadIID == headID
                        select dt).AsNoTracking().FirstOrDefault();
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
                                                      .OrderBy(a => a.TransactionTypeName)
                                                      .AsNoTracking()
                                                      .ToList();
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
                    .Where(a => a.DocumentTypeID == documentTypeID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public List<Language> GetLanguages()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Languages.OrderBy(a => a.Language1).ToList();
            }
        }

        public List<Currency> GetCurrencies()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Currencies
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<Currency> GetCurrencies(bool isEnabled)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Currencies.Where(a => a.IsEnabled == isEnabled).OrderBy(a => a.Name)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<Country> GetCountries1()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Countries.OrderBy(a => a.CountryName)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public string GetCountryName(long countryID)
        {
            var countryName = string.Empty;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var countryList = dbContext.Countries.Where(x => x.CountryID == countryID)
                    .AsNoTracking()
                    .FirstOrDefault();
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
                return dbContext.Titles.OrderBy(a => a.TitleName)
                    .AsNoTracking()
                    .ToList();
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
                    .AsNoTracking()
                    .ToList();
            }
        }

        public bool IsMarketPlaceBranch(long branchID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Branches.Where(a => a.BranchIID == branchID && a.IsMarketPlace == true)
                    .AsNoTracking()
                    .Any();
            }
        }

        public Currency GetCurrency(int currencyID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Currencies.Where(a => a.CurrencyID == currencyID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public List<Eduegate.Domain.Entity.Models.PaymentMethod> GetPaymentMethods(int siteID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.PaymentMethodSiteMaps.Where(a => a.SiteID == siteID)
                    .AsNoTracking()
                    .Select(a => a.PaymentMethod)
                    .ToList();
            }
        }

        public Eduegate.Domain.Entity.Models.PaymentMethod GetPaymentCOD(short paymentMethodID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.PaymentMethodSiteMaps.Where(a => a.PaymentMethodID == paymentMethodID)
                    .AsNoTracking()
                    .Select(a => a.PaymentMethod)
                    .FirstOrDefault();
            }
        }

        public Eduegate.Domain.Entity.Models.PaymentMethod GetPaymentMethod(short paymentMethodID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.PaymentMethods.Where(a => a.PaymentMethodID == paymentMethodID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public List<Eduegate.Domain.Entity.Models.PaymentMethod> GetPaymentBySitePaymentGroup(int siteID, bool isCustomerVerified, bool isLocal, bool isDigitalCart, string userID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                List<Eduegate.Domain.Entity.Models.PaymentMethod> paymentMethodList = new List<Eduegate.Domain.Entity.Models.PaymentMethod>();

                var canteenID = new Domain.Setting.SettingBL().GetSettingValue<string>("CANTEEN_CATEGORY_ID");

                var cartItems = shoppingCartRepository.GetCartDetailsWithItems(userID, (int)Eduegate.Framework.Helper.Enums.ShoppingCartStatus.InProcess, null);

                bool isCanteen = cartItems?.CategoryIID == long.Parse(canteenID);

                var paymentMethodGroupList = dbContext.PaymentGroups.Where
                    (a => a.SiteID == siteID && a.IsCustomerBlocked == isCustomerVerified && a.IsLocal == isLocal && a.IsDigitalcart == /*isDigitalCart*/ (isCanteen ? true : isDigitalCart))
                    .Include(i => i.PaymentMethods)
                    //.ThenInclude(i => i.pa)
                    .AsNoTracking()
                    .Select(a => a.PaymentMethods)
                    .ToList();
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

        public List<Eduegate.Domain.Entity.Models.PaymentMethod> GetPaymentExceptions(List<long> skuIds, int areaID, int siteID, int deliveryTypeID = 0)//we have to move deliverytypeID to generic way just for kuwait release doing this
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.SKUPaymentMethodExceptionMap.Where(x => (skuIds.Contains((long)x.SKUID) && x.SiteID == siteID) || x.AreaID == areaID || x.DeliveryTypeID == deliveryTypeID)
                    .AsNoTracking()
                    .Select(a => a.PaymentMethod)
                    .ToList();
            }
        }

        public List<Eduegate.Domain.Entity.Models.PaymentMethod> GetPaymentMethods()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.PaymentMethods.OrderBy(a => a.PaymentMethod1)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<Eduegate.Domain.Entity.Models.Site> GetSites()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Sites
                    .AsNoTracking()
                    .ToList();
            }
        }

        public ReWriteUrlType GetUrlTypeByCode(string code)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var type = new ReWriteUrlType();
                var category = dbContext.Categories.Where(a => a.CategoryCode.Equals(code))
                    .AsNoTracking()
                    .FirstOrDefault();

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
                return db.DeliveryTypes.Where(x => x.DeliveryTypeID == deliveryTypeId).AsNoTracking().FirstOrDefault();
            }
        }

        public ProductPriceListBranchMap GetBranchPriceListMaps(long branchID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ProductPriceListBranchMaps.Where(a => a.BranchID == branchID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public TaxTemplate GetTaxDetails(long taxTemplateID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.TaxTemplates.Where(a => a.TaxTemplateID == taxTemplateID)
                    .AsNoTracking()
                    .FirstOrDefault();
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
                       .Include(i => i.TaxTemplate)
                       .Include(a => a.TaxType)
                       .AsNoTracking()
                       .ToList();
            }
        }

        public TaxTemplateItem GetTaxTemplateItem(int taxTemplateItemID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from doc in dbContext.TaxTemplateItems
                        where doc.TaxTemplateID == taxTemplateItemID
                        select doc)
                        .AsNoTracking()
                        .FirstOrDefault();
            }
        }

        public List<Eduegate.Domain.Entity.Models.Condition> GetConditions()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Conditions
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<TaxTemplate> GetTaxTemplates()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.TaxTemplates
                    .Include(a => a.TaxTemplateItems)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public TaxTemplate GetTaxTemplates(int ID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.TaxTemplates
                    .Include(a => a.TaxTemplateItems).ThenInclude(a => a.Account)
                    .Where(a => a.TaxTemplateID == ID)
                    .AsNoTracking()
                    .FirstOrDefault();
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
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                foreach (var item in entity.TaxTemplateItems)
                {
                    if (!dbContext.TaxTemplateItems.Any(a => a.TaxTemplateItemID == item.TaxTemplateItemID))
                    {
                        var lastID = dbContext.TaxTemplateItems.Max(a => (int?)a.TaxTemplateItemID);
                        item.TaxTemplateItemID = lastID.HasValue ? lastID.Value + 1 : 1;
                        dbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }

                dbContext.SaveChanges();
                var updatedEntity = dbContext.TaxTemplates.Where(x => x.TaxTemplateID == entity.TaxTemplateID)
                    .AsNoTracking()
                    .FirstOrDefault();

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
                    .AsNoTracking()
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
                using (SqlConnection connection = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
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

        public List<DeliveryTypes1> GetActiveDeliverySettings(long? categoryID)
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
                    .Where(x => x.StatusID.Value == 1 && x.DeliveryTypeCategoryID.Value == categoryID)
                    .OrderBy(x => x.Priority)
                    .AsNoTracking()
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
                    .Where(x => !x.DeliveryTypeID.HasValue)
                    .AsNoTracking()
                    .ToList();
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
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                else
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                dbContext.SaveChanges();
                return entity;
            }
        }

        public Supplier GetSupplierByBranchnID(long branchID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.Suppliers.Where(x => x.BranchID == branchID).AsNoTracking().FirstOrDefault();
            }
        }

        public Supplier GetSupplier(long supplierId)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.Suppliers
                    .Where(x => x.SupplierIID == supplierId).AsNoTracking().FirstOrDefault();
            }
        }

        public List<EntityScheduler> GetEntityScheduler(int schedulerTypeID, string entityID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.EntitySchedulers.Where(a => a.SchedulerTypeID == schedulerTypeID && a.EntityID == entityID).AsNoTracking().ToList();
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
                    .Where(x => x.LocationIID == locationId).AsNoTracking().FirstOrDefault();
                return location != null ? location.LocationCode : null;
            }
        }

        public string GetCustomerName(long customerID)
        {
            using (var dbContext = new dbEduegateSupportContext())
            {
                var detail = dbContext.Customers
                    .Where(x => x.CustomerIID == customerID).FirstOrDefault();
                return detail == null ? string.Empty : detail.FirstName + "-" + detail.LastName;
            }
        }

        public List<KeyValueDTO> GetLookupsByQuery(string query, string parameterKey, string parameterValue, long? loginID, byte? schoolID, int? academicYearID, List<KeyValueDTO> reportParameters = null)
        {
            var values = new List<KeyValueDTO>();

            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (query.Contains("@SchoolID", StringComparison.OrdinalIgnoreCase) && schoolID.HasValue)
                {
                    cmd.Parameters.AddWithValue("@SchoolID", schoolID);
                }
                else if (query.Contains("@SCHOOL_IDs", StringComparison.OrdinalIgnoreCase) && schoolID.HasValue)
                {
                    cmd.Parameters.AddWithValue("@SCHOOL_IDs", schoolID);
                }

                if (query.Contains("@AcademicYearID", StringComparison.OrdinalIgnoreCase) && academicYearID.HasValue)
                {
                    cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
                }

                if (query.Contains("@LoginID", StringComparison.OrdinalIgnoreCase) && loginID.HasValue)
                {
                    cmd.Parameters.AddWithValue("@LoginID", loginID);
                }

                if (reportParameters != null)
                {
                    foreach (var param in reportParameters)
                    {
                        if (param.Key.ToLower() == "schoolid")
                        {
                            param.Value = string.IsNullOrEmpty(param.Value) ? schoolID.ToString() : param.Value;
                        }
                        else if (param.Key.ToLower() == "academicyearid")
                        {
                            param.Value = string.IsNullOrEmpty(param.Value) ? academicYearID.ToString() : param.Value;
                        }

                        if (query.Contains("@" + param.Key, StringComparison.OrdinalIgnoreCase))
                        {
                            if (!string.IsNullOrEmpty(param.Value))
                            {
                                // Check if the parameter is already in the command's parameters
                                if (cmd.Parameters.Contains("@" + param.Key))
                                {
                                    // Update the value of the existing parameter
                                    cmd.Parameters["@" + param.Key].Value = param.Value;
                                }
                                else
                                {
                                    // Add the parameter if it doesn't exist
                                    cmd.Parameters.AddWithValue("@" + param.Key, param.Value);
                                }
                            }
                            else
                            {
                                // Add the parameter if it doesn't exist
                                cmd.Parameters.AddWithValue("@" + param.Key, "");
                            }
                        }
                    }
                }

                conn.Open(); // Open the connection

                try
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable); // Fill the DataTable with the results

                        foreach (DataRow row in dataTable.Rows)
                        {
                            // Create a KeyValue object using the provided parameterKey and parameterValue
                            var keyValue = new KeyValueDTO
                            {
                                Key = row[parameterKey].ToString(),    // Use the passed parameterKey to get the key
                                Value = row[parameterValue].ToString()  // Use the passed parameterValue to get the value
                            };
                            values.Add(keyValue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                    Eduegate.Logger.LogHelper<string>.Fatal($"error on GetLookupsByQuery method. Error message: {errorMessage}", ex);
                }
            }

            return values;
        }

        public List<KeyValueDTO> GetLookupsByProcedure(string procedure, string parameterKey, string parameterValue, long? loginID, byte? schoolID, int? academicYearID, List<KeyValueDTO> reportParameters = null)
        {
            var keyValues = new List<KeyValueDTO>();

            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(procedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Derive parameters for the stored procedure
                    SqlCommandBuilder.DeriveParameters(cmd);

                    if (reportParameters != null)
                    {
                        foreach (var param in reportParameters)
                        {
                            // Modify values for schoolID and academicYearID if required
                            if (param.Key.ToLower() == "schoolid")
                            {
                                param.Value = string.IsNullOrEmpty(param.Value) ? schoolID.ToString() : param.Value;
                            }
                            else if (param.Key.ToLower() == "academicyearid")
                            {
                                param.Value = string.IsNullOrEmpty(param.Value) ? academicYearID.ToString() : param.Value;
                            }

                            // Check if the derived parameters contain the current parameter
                            var matchingParam = cmd.Parameters.Cast<SqlParameter>().FirstOrDefault(p => p.ParameterName == "@" + param.Key);

                            if (matchingParam != null)
                            {
                                matchingParam.Value = param.Value;
                            }
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    try
                    {
                        DataSet dt = new DataSet();
                        adapter.Fill(dt);
                        DataTable lookUpDataTable = null;

                        if (dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
                        {
                            lookUpDataTable = dt.Tables[0];
                        }

                        if (lookUpDataTable != null)
                        {
                            foreach (DataRow row in lookUpDataTable.Rows)
                            {
                                var keyValue = new KeyValueDTO
                                {
                                    Key = row[parameterKey].ToString(),
                                    Value = row[parameterValue].ToString()
                                };
                                keyValues.Add(keyValue);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                            ? ex.InnerException?.Message : ex.Message;
                        // Handle error logging as needed
                        Eduegate.Logger.LogHelper<string>.Fatal($"error on GetLookupsByProcedure method. Error message: {errorMessage}", ex);
                    }
                }
            }

            return keyValues;
        }



    }
}