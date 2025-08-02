using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Entity;
using Eduegate.Services.Contracts.School.Mutual;

namespace Eduegate.Domain.Repository
{
    public class PageRenderRepository
    {
        public List<BoilerPlate> GetBoilerPlatesByPageID(long pageID)
        {
            var boilerplates = new List<BoilerPlate>();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                boilerplates = (from a in db.PageBoilerplateMaps
                                join b in db.BoilerPlates on a.BoilerplateID equals b.BoilerPlateID
                                where a.PageID == pageID
                                select b)
                                .OrderBy(a => a.BoilerPlateID)
                                .AsNoTracking()
                                .ToList();

            }
            return boilerplates;
        }

        public BoilerPlate GetBoilerPlate(long boilerPlateID)
        {
            var boilerplates = new List<BoilerPlate>();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.BoilerPlates.Where(a => a.BoilerPlateID == boilerPlateID)
                    .AsNoTracking()
                    .FirstOrDefault();

            }
        }

        public Page GetPageDetails(long pageID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var page = db.Pages.Where(a => a.PageID == pageID)
                    .Include(i => i.Site)
                    .Include(i => i.Page1)
                    .Include(i => i.PageType)
                    .AsNoTracking()
                    .FirstOrDefault();

                //db.Entry(page).Reference(a => a.Site).Load();
                //db.Entry(page).Reference(a => a.Page1).Load();
                //db.Entry(page).Reference(a => a.PageType).Load();

                return page;
            }
        }

        public Page GetPageInfo(long pageID, long? referenceID, int companyÌD)
        {
            Page page = null;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                //page = db.Pages.Where(a => a.PageID == pageID /*&& a.CompanyID == companyÌD*/)
                //    .Include(x => x.PageBoilerplateMaps)
                //    .Include(x => x.PageBoilerplateMaps.Select(a=> a.PageBoilerplateMapParameters))
                //    .Include(x => x.PageBoilerplateMaps.Select(a => a.BoilerPlate).Select( q=> q.BoilerPlateParameters))
                //    .FirstOrDefault();

                page = db.Pages.Where(a => a.PageID == pageID)
                    .Include(x => x.PageBoilerplateMaps).ThenInclude(a => a.PageBoilerplateMapParameters)
                    .Include(x => x.PageBoilerplateMaps).ThenInclude(a => a.BoilerPlate).ThenInclude(q => q.BoilerPlateParameters)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (page != null && page.PageBoilerplateMaps != null)
                {
                    foreach (var param in page.PageBoilerplateMaps)
                    {
                        param.BoilerPlate.BoilerPlateParameters = new List<BoilerPlateParameter>();
                        param.BoilerPlate.BoilerPlateParameters = db.BoilerPlateParameters
                            .Where(a => a.BoilerPlateID == param.BoilerplateID || a.BoilerPlateID == null).ToList();
                    }
                }
            }

            return page;
        }

        public List<Site> GetSites()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Sites.OrderBy(a=> a.SiteName)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<PageType> GetPageTypes()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.PageTypes
                    .OrderBy(a => a.TypeName)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<Page> GetPages(int? companyID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (companyID.HasValue)
                    return db.Pages.OrderBy(a => a.PageName).Where(a=> a.CompanyID == companyID.Value)
                        .AsNoTracking()
                        .ToList();
                else
                    return db.Pages.OrderBy(a=> a.PageName)
                        .AsNoTracking()
                        .ToList();
            }
        }

        public Page SavePage(Page entity, Nullable<long> paramReferenceID)
        {
            Page updatedEntity = null;

            try
            {
                if (entity.IsNotNull())
                {
                    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                    {
                        dbContext.Pages.Add(entity);

                        if (entity.PageID == 0)
                        {
                            var pageID = dbContext.Pages.Max(a => (long?)a.PageID);
                            entity.PageID = pageID.HasValue ? pageID.Value + 1 : 1;
                        }

                        if (!dbContext.Pages.Any(a => a.PageID == entity.PageID))
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            // Reomove boilerplate with this reference
                            var pageboilerpageIDs = entity.PageBoilerplateMaps.Select(a => a.PageBoilerplateMapIID);

                            IQueryable<PageBoilerplateMap> deletedMaps;

                            if (paramReferenceID.HasValue || paramReferenceID > 0)
                                deletedMaps = dbContext.PageBoilerplateMaps.Include(i => i.PageBoilerplateMapParameters).Where(a => a.PageID == entity.PageID
                                && a.ReferenceID == paramReferenceID
                                && !pageboilerpageIDs.Contains(a.PageBoilerplateMapIID)).AsNoTracking();
                            else
                                deletedMaps = dbContext.PageBoilerplateMaps.Include(i => i.PageBoilerplateMapParameters).Where(a => a.PageID == entity.PageID
                                && a.ReferenceID == null
                                && !pageboilerpageIDs.Contains(a.PageBoilerplateMapIID)).AsNoTracking();

                            if (deletedMaps.IsNotNull())
                                dbContext.PageBoilerplateMaps.RemoveRange(deletedMaps);

                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                            foreach (var map in entity.PageBoilerplateMaps)
                            {
                                if(map.PageBoilerplateMapIID == 0)
                                    dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                else
                                    dbContext.Entry(map).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                foreach (var param in map.PageBoilerplateMapParameters)
                                {
                                    if (param.PageBoilerplateMapParameterIID == 0)
                                        dbContext.Entry(param).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    else
                                        dbContext.Entry(param).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }
                        }

                        dbContext.SaveChanges();

                        updatedEntity = GetPageInfo(entity.PageID, paramReferenceID, (int)entity.CompanyID);
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PageRenderRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public bool DeletePageBoilerPlateMaps(List<PageBoilerplateMap> pageBoilerPlateMaps)
        {
            bool IsDeleted = false;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (pageBoilerPlateMaps.IsNotNull() && pageBoilerPlateMaps.Count > 0)
                    {
                        foreach (PageBoilerplateMap pbMap in pageBoilerPlateMaps)
                        {
                            PageBoilerplateMap pageEntity = dbContext.PageBoilerplateMaps.Where(x => x.PageBoilerplateMapIID == pbMap.PageBoilerplateMapIID)
                                .AsNoTracking()
                                .FirstOrDefault();

                            if (pbMap.PageBoilerplateMapParameters.IsNotNull() && pbMap.PageBoilerplateMapParameters.Count > 0)
                            {
                                foreach (PageBoilerplateMapParameter pbmParameter in pbMap.PageBoilerplateMapParameters)
                                {
                                    PageBoilerplateMapParameter pageParamEntity = dbContext.PageBoilerplateMapParameters.Where(y => y.PageBoilerplateMapParameterIID == pbmParameter.PageBoilerplateMapParameterIID)
                                        .AsNoTracking()
                                        .FirstOrDefault();
                                    dbContext.PageBoilerplateMapParameters.Remove(pageParamEntity);
                                }
                            }

                            dbContext.PageBoilerplateMaps.Remove(pageEntity);
                        }

                        dbContext.SaveChanges();
                    }
                }

                return IsDeleted;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PageRenderRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public string GetBoilerPlateCultureData(long boilerplateMapIID, string parameterName, int cultureID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {
                    var abc = (from b in db.PageBoilerplateMapParameters
                               join c in db.PageBoilerplateMapParameterCultureDatas on b.PageBoilerplateMapParameterIID equals c.PageBoilerplateMapParameterID into one
                               from c in one.Where(a => a.CultureID == cultureID).DefaultIfEmpty()
                               where b.ParameterName == parameterName && b.PageBoilerplateMapID == boilerplateMapIID
                               select (c.ParameterValue != null && c.ParameterValue != "") ? c.ParameterValue : b.ParameterValue).FirstOrDefault();
                    return abc;
                }
                catch (Exception) { return ""; }
            }
        }

        public PowerBiDashBoardDTO GetPowerBIDataUsingPageID(long? pageID)
        {
            var toDto = new PowerBiDashBoardDTO();

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var dat = db.Dashboards.FirstOrDefault(x => x.PageID == pageID);

                if(dat != null)
                {
                    toDto.MenuLinkID = dat.MenuLinkID;
                    toDto.PageID = dat.PageID;
                    toDto.ReportID = dat.ReportID;
                    toDto.ClientID = dat.ClientID;
                    toDto.WorkspaceID = dat.WorkspaceID;
                    toDto.Dsh_ID = dat.Dsh_ID;
                    toDto.ClientSecret = dat.ClientSecret;
                    toDto.TenantID = dat.TenantID;
                }
            }

            return toDto;
        }
    }
}
