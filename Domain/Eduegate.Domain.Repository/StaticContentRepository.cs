using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Logs;
using Eduegate.Framework.Extensions;
using System.Diagnostics;
using Eduegate.Services.Contracts;
using System.Globalization;
using Eduegate.Framework.Helper;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository
{
    public class StaticContentRepository
    {
        public List<StaticContentType> GetStaticContentTypes()
        {
            List<StaticContentType> entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.StaticContentTypes.OrderBy(a => a.ContentTypeName)
                        .AsNoTracking()
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the StaticContentTypes", TrackingCode.ERP);
            }

            return entity;
        }

        public StaticContentData SaveContent(StaticContentData contentData, CallContext callContext)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (contentData.ContentDataIID == default(long))
                {
                    contentData.CreatedDate = DateTime.Now;
                    contentData.CreatedBy = Convert.ToInt32(callContext.LoginID);
                    dbContext.StaticContentDatas.Add(contentData);
                }
                else
                {
                    StaticContentData dbContent = dbContext.StaticContentDatas.Where(x => x.ContentDataIID == contentData.ContentDataIID)
                        .AsNoTracking()
                        .FirstOrDefault();
                    //updatedContent = contentData;
                    dbContent.ContentDataIID = contentData.ContentDataIID;
                    dbContent.ContentTypeID = contentData.ContentTypeID;
                    dbContent.Description = contentData.Description;
                    dbContent.ImageFilePath = contentData.ImageFilePath;
                    dbContent.SerializedJsonParameters = contentData.SerializedJsonParameters;
                    dbContent.StaticContentType = contentData.StaticContentType;
                    dbContent.Title = contentData.Title;
                    dbContent.UpdatedDate = DateTime.Now;
                    dbContent.UpdatedBy = Convert.ToInt32(callContext.LoginID);
                }

                dbContext.SaveChanges();
            }

            return contentData;
        }

        public StaticContentData GetStaticContent(long contentID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var staticContent = dbContext.StaticContentDatas.Where(x => x.ContentDataIID == contentID)
                    .AsNoTracking()
                    .FirstOrDefault();
                return staticContent;
            }

        }

        public long GetDefaultSKUID(long productID)
        {
            long skuID = 0;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var sku = dbContext.ProductSKUMaps.Where(x => x.ProductID == productID && x.Sequence == 1)
                    .AsNoTracking()
                    .FirstOrDefault();
                if (sku != null)
                {
                    skuID = sku.ProductSKUMapIID;
                }
                return skuID;
            }
        }

        public StaticContentType GetStaticContentTypes(int ContentTypeID)
        {
            dbEduegateERPContext db = new dbEduegateERPContext();
            StaticContentType staticContentType = db.StaticContentTypes.Where(x => x.ContentTypeID == ContentTypeID)
                .AsNoTracking()
                .FirstOrDefault();
            return staticContentType;
        }

        public List<StaticContentData> GetStaticContentData(Eduegate.Services.Contracts.Enums.StaticContentTypes staticPageTypes, int pageSize, int pageNumber)
        {
            dbEduegateERPContext db = new dbEduegateERPContext();
            List<StaticContentData> staticContentDatas = new List<StaticContentData>();
            if (Eduegate.Services.Contracts.Enums.StaticContentTypes.All == staticPageTypes)
            {
                staticContentDatas = db.StaticContentDatas
                .AsNoTracking()
                .OrderByDescending(x => x.ContentDataIID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            }
            else
            {
                staticContentDatas = db.StaticContentDatas.Where(x => x.ContentTypeID == (int)staticPageTypes)
                .AsNoTracking()
                .OrderByDescending(x => x.ContentDataIID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            }
            return staticContentDatas;
        }

    }
}