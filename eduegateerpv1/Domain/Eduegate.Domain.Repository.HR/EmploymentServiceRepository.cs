using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository.HR
{
    public class EmploymentServiceRepository
    {
        public AvailableJob GetJobOpening(Guid id)
        {
            try
            {
                using (var dbContext = new dbEduegateHRContext())
                {
                    return dbContext.DB_Availablejobs
                        .Include(a=> a.AvailableJobTags)
                        .Include(a => a.AvailableJobCultureDatas)
                        .Where(a => a.Id == id)
                        .AsNoTracking()
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
                throw ex;
            }
        }

        public AvailableJob GetJobOpening(long id)
        {
            try
            {
                using (var dbContext = new dbEduegateHRContext())
                {
                    var availbleJop = dbContext.DB_Availablejobs
                        .Where(a => a.JobIID == id)
                        .Include(a => a.AvailableJobTags)
                        .Include(a => a.AvailableJobCultureDatas)
                        .AsNoTracking()
                        .FirstOrDefault();
                    //availbleJop.AvailableJobCultureDatas = dbContext.AvailableJobCultureDatas
                    //    .Where(a => a.JobIID == availbleJop.JobIID).ToList();
                    return availbleJop;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
                throw ex;
            }
        }

        public List<AvailableJob> GetJobOpenings(string filter)
        {
            try
            {
                using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
                {
                    if(filter.IsNullOrEmpty())
                        return dbContext.DB_Availablejobs.Where(a=> a.Status.Equals("Active"))
                            .Include(i => i.AvailableJobTags)
                            .Include(i => i.AvailableJobCultureDatas)
                            .AsNoTracking()
                            .ToList();
                    else
                        return dbContext.DB_Availablejobs.Where(a=> a.Status.Equals("Active") && a.JobDescription.Contains(filter))
                            .Include(i => i.AvailableJobTags)
                            .Include(i => i.AvailableJobCultureDatas)
                            .AsNoTracking()
                            .ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
                throw ex;
            }
        }

        public AvailableJob SaveJobOpening(AvailableJob job)
        {
            try
            {
                using (var dbContext = new dbEduegateHRContext())
                {
                    dbContext.DB_Availablejobs.Add(job);

                    if (dbContext.DB_Availablejobs.Any(a => a.Id == job.Id)) dbContext.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.AvailableJobTags.RemoveRange(dbContext.AvailableJobTags.Where(a => a.JobID == job.JobIID));
                    dbContext.SaveChanges();
                    return dbContext.DB_Availablejobs.Where(a => a.Id == job.Id).FirstOrDefault(); ;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
                return null;
            }
        }

        public void SaveJobOpeningCultureData(List<AvailableJobCultureData> cultureDatas)
        {
            try
            {
                using (var dbContext = new dbEduegateHRContext())
                {
                    foreach (var cultureData in cultureDatas)
                    {
                        dbContext.AvailableJobCultureDatas.Add(cultureData);

                        if (dbContext.AvailableJobCultureDatas
                            .Any(a => a.CultureID == cultureData.CultureID && a.JobIID == cultureData.JobIID))
                        {
                            dbContext.Entry(cultureData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
            }
        }

        public void ArchiveJobProfile(List<string> applicationIDs, string status)
        {
            try
            {
                using (var dbContext = new dbEduegateHRContext())
                {
                    var datas = dbContext.DB_ApplicationForms.Where(a => applicationIDs.Contains(a.Id.ToString())).AsNoTracking().ToList();
                    foreach (var applicationForm in datas)
                    {
                        applicationForm.Status = status;// "ARCHIVE";
                    }

                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
            }
        }

        public DB_ApplicationForm GetApplicationForm(Guid ID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                return dbContext.DB_ApplicationForms.Where(x => x.Id == ID).AsNoTracking().FirstOrDefault();
            }
        }

        public AvailableJob GetAvailableJob(Guid ID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                return dbContext.DB_Availablejobs
                    .Include(a => a.AvailableJobTags)
                    .Include(a => a.AvailableJobCultureDatas)
                    .Where(x => x.Id == ID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public DB_Department GetDepartment(int id)
        {
            using (var db = new dbEduegateHRContext())
            {
                return db.DB_Departments
                    .Include(a => a.DepartmentTags)
                    .Where(x => x.DepartmentID == id).AsNoTracking().FirstOrDefault();
            }
        }

        public List<DB_Department> GetDepartments(string name)
        {
            using (var db = new dbEduegateHRContext())
            {
                return db.DB_Departments.AsNoTracking().ToList();
            }
        }

        public List<DB_Department> GetDBDepartments()
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                return dbContext.DB_Departments.AsNoTracking().ToList();
            }
        }

        public List<AvailableJobCultureData> GetAvailableJobCultureData(long jobIID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                return dbContext.AvailableJobCultureDatas
                    .Where(a=> a.JobIID == jobIID).AsNoTracking().ToList();
            }
        }
    }
}
