using Eduegate.Domain.Entity.Logging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Eduegate.Domain.Entity.Logging;

namespace Eduegate.Domain.Repository.Logging
{
    public class ActivityLoggerRepository
    {
        public List<Activity> GetActivities(int activityTypeID)
        {
            try
            {
                using (EduegatedERP_LoggerContext dbContext = new EduegatedERP_LoggerContext())
                {
                    return dbContext.Activities
                        .Where(a=> a.ActivityTypeID == activityTypeID)
                        .OrderByDescending(a => a.ActivitiyIID)
                        .ToList();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<CatalogLoggerRepository>.Fatal(exception.Message.ToString(), exception);
                throw;
            }
        }

        public List<Activity> GetActivitiesByLoginID(long loginID)
        {
            try
            {
                using (EduegatedERP_LoggerContext dbContext = new EduegatedERP_LoggerContext())
                {
                    var activity = dbContext.Activities
                        .Include(a=> a.ActionStatus)
                        .Include(a => a.ActionType)
                        .Include(a => a.ActivityType)
                        .OrderByDescending(a => a.ActivitiyIID)
                        .AsNoTracking()
                        .Take(10)
                        .ToList();

                    return activity;
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<CatalogLoggerRepository>.Fatal(exception.Message.ToString(), exception);
                //throw exception;
                return null;
            }
        }

        public Activity GetActivity(long activityID)
        {
            try
            {
                using (EduegatedERP_LoggerContext dbContext = new EduegatedERP_LoggerContext())
                {
                    return dbContext.Activities
                        .Include(a => a.ActionStatus)
                        .Include(a => a.ActionType)
                        .Include(a => a.ActivityType)
                        .Where(a=> a.ActivitiyIID == activityID)
                        .FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<CatalogLoggerRepository>.Fatal(exception.Message.ToString(), exception);
                throw;
            }
        }

        public Activity GetLastActivity(long loginID, int activityTypeID, int statusID)
        {
            try
            {
                using (var dbContext = new EduegatedERP_LoggerContext())
                {
                    var activity = dbContext.Activities
                        .Where(a => a.CreatedBy == loginID && a.ActivityTypeID == activityTypeID
                        && a.ActionStatusID == statusID).OrderByDescending(a=> a.ActivitiyIID)
                        .Include(a=> a.ActionStatus)
                        .Include(a => a.ActionType)
                        .Include(a => a.ActivityType)
                        .FirstOrDefault();

                    return activity;
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<CatalogLoggerRepository>.Fatal(exception.Message.ToString(), exception);
                return null;
                //throw;
            }
        }


        public void SaveActivities(List<Activity> activities)
        {
            try
            {
                using (var dbContext = new EduegatedERP_LoggerContext())
                {
                    dbContext.Activities.AddRange(activities);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<CatalogLoggerRepository>.Fatal(exception.Message.ToString(), exception);
                //throw;
            }
        }
    }
}
