using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;
using Eduegate.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using Eduegate.Domain.Entity;
using System.Threading.Tasks;

namespace Eduegate.Domain.Repository
{
    public class MetadataRepository
    {
        #region Search
        public async Task<View> GetViewInfo(SearchView view, string languageCode = null)
        {
            View viewDef = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    viewDef = await dbContext.Views
                        .Include(i => i.ViewActions)
                        .Include(b => b.ViewCultureDatas).ThenInclude(b => b.Culture)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.ViewID == (long)view);

                    //dbContext.Entry(viewDef).Collection(la => la.ViewColumns).Load();

                    if (viewDef != null)
                    {
                        if (languageCode != null && !languageCode.ToLower().Contains("en"))
                        {
                            var cultureData = viewDef.ViewCultureDatas.FirstOrDefault(x => x.Culture.CultureCode.ToLower().StartsWith(languageCode.ToLower()));

                            if (cultureData != null)
                            {
                                viewDef.ViewTitle = cultureData?.ViewTitle ?? viewDef.ViewTitle;
                                viewDef.ViewDescription = cultureData?.ViewDescription ?? viewDef.ViewDescription;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get column map from ViewGridColumns", TrackingCode.ERP);
            }

            return viewDef;
        }

        /// <summary>
        /// Method to get column map list for grid
        /// </summary>
        /// <param name="ViewId">ViewId</param>
        /// <returns>List<ViewGridColumn></returns>
        public List<ViewColumn> SearchColumns(SearchView view, char viewType = '\0', string languageCode = null)
        {
            List<ViewColumn> columns = new List<ViewColumn>();

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (languageCode != null && !languageCode.ToLower().Contains("en"))
                    {
                        if (viewType == 'E')
                        {
                            columns = dbContext
                                .ViewGridColumns
                                .AsNoTracking()
                                .Include(x => x.ViewColumnCultureDatas)
                                .Where(x => x.ViewID == (long)view && x.PhysicalColumnName != null && x.PhysicalColumnName != "" && x.IsDefault == true && (x.IsExcludeForExport == null || x.IsExcludeForExport == false))
                                .OrderBy(a => a.SortOrder)
                                .ToList();
                        }
                        else
                        {
                            columns = dbContext
                                .ViewGridColumns
                                .AsNoTracking()
                                .Include(x => x.ViewColumnCultureDatas).ThenInclude(x => x.Culture)
                                .Where(x => x.ViewID == (long)view && x.IsDefault == true).OrderBy(a => a.SortOrder)
                                .ToList();
                        }

                        foreach (var col in columns)
                        {
                            var cultureData = col.ViewColumnCultureDatas.FirstOrDefault(x => x.Culture.CultureCode.ToLower().StartsWith(languageCode.ToLower()));

                            if (cultureData != null)
                            {
                                col.ColumnName = cultureData?.ColumnName ?? col.ColumnName;
                            }
                        }
                    }
                    else
                    {
                        if (viewType == 'E')
                        {
                            columns = dbContext
                                .ViewGridColumns
                                .AsNoTracking()
                                .Where(x => x.ViewID == (long)view && x.PhysicalColumnName != null && x.PhysicalColumnName != "" && x.IsDefault == true && (x.IsExcludeForExport == null || x.IsExcludeForExport == false))
                                .OrderBy(a => a.SortOrder)
                                .ToList();
                        }
                        else
                        {
                            columns = dbContext
                                .ViewGridColumns
                                .AsNoTracking()
                                .Where(x => x.ViewID == (long)view && x.IsDefault == true).OrderBy(a => a.SortOrder)
                                .ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get column map from ViewSummaryColumns", TrackingCode.ERP);
            }

            return columns;
        }
        #endregion

        #region Filter
        public List<FilterColumn> GetFilterMetadata(SearchView view, string languageCode = null)
        {
            var columns = new List<FilterColumn>();

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    columns = dbContext.FilterColumns
                            .Include(x => x.FilterColumnCultureDatas).ThenInclude(x => x.Culture)
                            .Where(x => x.ViewID == (long)view)
                            .OrderBy(a => a.SequenceNo)
                            .ToList();

                    foreach (var col in columns)
                    {
                        if (languageCode != null && !languageCode.ToLower().Contains("en"))
                        {
                            var cultureData = col.FilterColumnCultureDatas.FirstOrDefault(x => x.Culture.CultureCode.ToLower().StartsWith(languageCode.ToLower()));

                            if (cultureData != null)
                            {
                                col.ColumnCaption = cultureData?.ColumnCaption ?? col.ColumnCaption;
                            }
                        }

                        col.FilterColumnConditionMaps = dbContext.FilterColumnConditionMaps
                            .Where(x => x.DataTypeID == col.DataTypeID || x.FilterColumnID == col.FilterColumnID)
                            .AsNoTracking()
                            .ToList();

                        foreach (var maps in col.FilterColumnConditionMaps)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get column map from ViewSummaryColumns", TrackingCode.ERP);
            }

            return columns;
        }

        public List<FilterColumnUserValue> GetUserFilterMetadata(SearchView view, long? loginID, int companyID = 0)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    return dbContext.FilterColumnUserValues.Where(x => x.ViewID == (long)view && x.LoginID == (loginID == 0 ? (long?)null : loginID) && x.CompanyID == companyID).AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get column map from ViewSummaryColumns", TrackingCode.ERP);
                throw;
            }

        }

        public bool HasUserFilter(SearchView view, long? loginID, long companyID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.FilterColumnUserValues.Where(x => x.ViewID == (long)view && (x.LoginID == (loginID == 0 ? (long?)null : loginID) && x.CompanyID == companyID)).AsNoTracking().Any();
            }
        }

        public bool SaveUserFilterMetadata(SearchView view, long? loginID, List<FilterColumnUserValue> values, int companyID = 0)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.FilterColumnUserValues.RemoveRange(dbContext.FilterColumnUserValues.Where(x => x.ViewID == (long)view && x.LoginID == loginID).AsNoTracking());
                    dbContext.FilterColumnUserValues.AddRange(values);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to save SaveUserFilterMetadata", TrackingCode.ERP);
                return false;
            }

            return true;
        }
        #endregion

        #region DocumentTypes
        public List<DocumentReferenceType> GetDocumentReferenceTypes(Eduegate.Services.Contracts.Enums.Systems system)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DocumentReferenceTypes.Where(a => a.System == system.ToString()).OrderBy(a => a.InventoryTypeName).AsNoTracking().ToList();
            }
        }

        public List<DocumentType> GetDocumentType(Eduegate.Services.Contracts.Enums.Systems system, int? CompanyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DocumentTypes.Where(a => a.System == system.ToString() && a.CompanyID == CompanyID).OrderBy(a => a.TransactionTypeName).AsNoTracking().ToList();
            }
        }

        public DocumentType GetDocumentType(int documentTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DocumentTypes
                    .Include(a => a.Workflow)
                    .Where(a => a.DocumentTypeID == documentTypeID).AsNoTracking().FirstOrDefault();
            }
        }

        public DocumentType SaveDocumentType(DocumentType entity)
        {
            DocumentType updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
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
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                    if (entity.Workflow != null)
                    {
                        dbContext.Entry(entity.Workflow).State = EntityState.Unchanged;
                    }

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.DocumentTypes
                        .Include(a => a.Workflow)
                        .Where(x => x.DocumentTypeID == entity.DocumentTypeID).AsNoTracking().FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public List<DocumentTypeType> SaveDocumentMap(List<DocumentTypeType> entities)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var dbData = new MetadataRepository().GetDocumentMap(entities[0].DocumentTypeID.Value);
                    if (dbData.IsNotNull() && dbData.Count > 0)
                    {
                        foreach (var data in dbData)
                        {
                            var dtm = dbContext.DocumentTypeTypes.Where(x => x.DocumentTypeID == data.DocumentTypeID).AsNoTracking().ToList();
                            dbContext.DocumentTypeTypes.RemoveRange(dtm);
                        }

                        dbContext.SaveChanges();
                    }
                    if (entities.IsNotNull() && entities.Count > 0)
                    {
                        foreach (var entity in entities)
                        {
                            if (entity.DocumentTypeID == 0) continue;

                            if (entity.DocumentTypeTypeMapIID <= 0)
                            {
                                dbContext.DocumentTypeTypes.Add(entity);
                                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }

                        dbContext.SaveChanges();
                    }
                    return GetDocumentMap(entities[0].DocumentTypeID.Value);
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public List<DocumentTypeType> GetDocumentMap(int documentTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DocumentTypeTypes.Where(a => a.DocumentTypeID == documentTypeID).AsNoTracking().ToList();
            }
        }

        public DocumentReferenceType GetDocumentReferenceTypesByDocumentType(int documentTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from drt in dbContext.DocumentReferenceTypes
                        join dt in dbContext.DocumentTypes on drt.ReferenceTypeID equals dt.ReferenceTypeID
                        where dt.DocumentTypeID == documentTypeID
                        select drt).SingleOrDefault();
            }
        }

        #endregion

        public List<Location> GetLocations()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Locations.AsNoTracking().ToList();
            }
        }

        //public List<SubLocation> GetSubLocations()
        //{
        //    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
        //    {
        //        return dbContext.Locations.ToList();
        //    }
        //}

        public Location GetLocation(long locationID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Locations.Where(a => a.LocationIID == locationID).AsNoTracking().FirstOrDefault();
            }
        }

        public Location SaveLocation(Location entity)
        {
            Location updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.Locations.Add(entity);

                    if (dbContext.Locations.Any(x => x.LocationIID == entity.LocationIID))
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    else
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.Locations.Where(x => x.LocationIID == entity.LocationIID).AsNoTracking().FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public string GetDocumentTypeName(int documentTypeMapID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from dt in dbContext.DocumentTypes
                        where dt.DocumentTypeID == documentTypeMapID
                        select dt.TransactionTypeName).AsNoTracking().FirstOrDefault();
            }
        }


        public List<DocumentTypeTransactionNumber> SaveDocumentTypeTransactionNumber(List<DocumentTypeTransactionNumber> entities)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var id = entities[0].DocumentTypeID;
                    var existingDocumentTypeTransactionNumbers = dbContext.DocumentTypeTransactionNumbers.Where(x => x.DocumentTypeID == id);
                    foreach (var entity in entities)
                    {
                        DocumentTypeTransactionNumber existingData = null;
                        if (existingDocumentTypeTransactionNumbers != null && existingDocumentTypeTransactionNumbers.Count() > 0)
                        {
                            existingData = existingDocumentTypeTransactionNumbers.Where(x => x.Month == entity.Month && x.Year == entity.Year).AsNoTracking().FirstOrDefault();
                        }
                        if (existingData == null)//new record
                        {
                            dbContext.DocumentTypeTransactionNumbers.Add(entity);
                        }
                        else
                        {
                            existingData.LastTransactionNo = entity.LastTransactionNo;
                        }

                    }
                    dbContext.SaveChanges();
                    return GetDocumentTypeTransactionNumber(entities[0].DocumentTypeID);
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public List<DocumentTypeTransactionNumber> GetDocumentTypeTransactionNumber(long documentTypeID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    return dbContext.DocumentTypeTransactionNumbers.Where(x => x.DocumentTypeID == documentTypeID).AsNoTracking().ToList();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public DocumentType SaveNextTransactionNumberByMonthYear(int documentTypeID, int month, int year)
        {
            //This function retuns the Document Type with next Sequence number
            string sNextTransactionNumber = string.Empty;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                DocumentTypeTransactionNumber documentTypeTransactionNumber = null;
                var documentType = dbContext.DocumentTypes.Include(x => x.DocumentTypeTransactionNumbers).Where(a => a.DocumentTypeID == documentTypeID).AsNoTracking().FirstOrDefault();
                if (documentType != null)
                {

                    if (documentType.DocumentTypeTransactionNumbers != null)
                    {
                        documentTypeTransactionNumber = documentType.DocumentTypeTransactionNumbers.Where(x => x.Month == month && x.Year == year).FirstOrDefault();
                    }
                    else
                    {
                        documentType.DocumentTypeTransactionNumbers = new List<DocumentTypeTransactionNumber>();
                    }
                    if (documentTypeTransactionNumber == null)
                    {
                        documentTypeTransactionNumber = new DocumentTypeTransactionNumber { DocumentTypeID = documentTypeID, Month = month, Year = year, LastTransactionNo = 1 };
                    }
                    else
                    {
                        documentTypeTransactionNumber.LastTransactionNo = documentTypeTransactionNumber.LastTransactionNo + 1;
                    }
                    documentType.DocumentTypeTransactionNumbers.Add(documentTypeTransactionNumber);
                    dbContext.SaveChanges();
                }
                return documentType;
            }
        }

        public DocumentType GetNextTransactionNumberByMonthYear(int documentTypeID, int month, int year)
        {
            //This function retuns the Document Type with next Sequence number
            string sNextTransactionNumber = string.Empty;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                DocumentTypeTransactionNumber documentTypeTransactionNumber = null;
                var documentType = dbContext.DocumentTypes.Include(x => x.DocumentTypeTransactionNumbers).Where(a => a.DocumentTypeID == documentTypeID).AsNoTracking().FirstOrDefault();

                if (documentType != null)
                {
                    if (documentType.DocumentTypeTransactionNumbers != null)
                    {
                        documentTypeTransactionNumber = documentType.DocumentTypeTransactionNumbers.Where(x => x.Month == month && x.Year == year).FirstOrDefault();
                    }
                    else
                    {
                        documentType.DocumentTypeTransactionNumbers = new List<DocumentTypeTransactionNumber>();
                    }

                    if (documentTypeTransactionNumber == null)
                    {
                        documentTypeTransactionNumber = new DocumentTypeTransactionNumber { DocumentTypeID = documentTypeID, Month = month, Year = year, LastTransactionNo = 1 };
                    }
                    else
                    {
                        documentTypeTransactionNumber.LastTransactionNo = documentTypeTransactionNumber.LastTransactionNo + 1;
                    }

                    documentType.DocumentTypeTransactionNumbers.Add(documentTypeTransactionNumber);
                }

                return documentType;
            }
        }

        public List<DocumentType> GetDocumentTypesByReferenceType(int referenceTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DocumentTypes
                    .Include(i => i.DocumentReferenceType)
                    .Where(d => d.ReferenceTypeID == referenceTypeID)
                    .AsNoTracking().ToList();
            }
        }

    }
}