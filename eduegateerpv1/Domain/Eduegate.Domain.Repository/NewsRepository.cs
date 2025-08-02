using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;
using Eduegate.Services.Contracts.Enums;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository
{
    public class NewsRepository
    {
        public News GetNews(long newsID)
        {
            News entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.News.Where(x => x.NewsIID == (long)newsID)
                        .AsNoTracking()
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the Vouchers", TrackingCode.ERP);
            }

            return entity;
        }

        public List<News> GetNews(NewsTypes type, int pageSize, int pageNumber)
        {
            List<News> entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (type == NewsTypes.All)
                    {
                        entity = dbContext.News
                            .OrderByDescending(x => x.NewsIID).Skip((pageNumber - 1) * pageSize).Take(pageSize)
                            .AsNoTracking()
                            .ToList();
                    }
                    else
                    {
                        entity = dbContext.News.Where(x => x.NewsTypeID == (int)type)
                            .OrderByDescending(x => x.NewsIID).Skip((pageNumber - 1) * pageSize).Take(pageSize)
                            .AsNoTracking()
                            .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the Vouchers", TrackingCode.ERP);
            }

            return entity;
        }

        public List<NewsType> GetNewsTypes()
        {
            List<NewsType> entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.NewsTypes.OrderBy(a=> a.NewsTypeName)
                        .AsNoTracking()
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the NewsTypes", TrackingCode.ERP);
            }

            return entity;
        }

        public News SaveNews(News voucher)
        {
            News updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.News.Add(voucher);

                    if (voucher.NewsIID == 0)
                        dbContext.Entry(voucher).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    else
                        dbContext.Entry(voucher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.News.Where(x => x.NewsIID == voucher.NewsIID)
                        .AsNoTracking()
                        .FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }
    }
}
