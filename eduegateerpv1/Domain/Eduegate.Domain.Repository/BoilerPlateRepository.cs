using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;
using Eduegate.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository
{
    public class BoilerPlateRepository
    {
        public BoilerPlate SaveBoilerPlate(BoilerPlate boilerPlate)
        {
            BoilerPlate updateEntity = null;
            try
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    bool isBool = db.BoilerPlates.Where(a => a.BoilerPlateID == boilerPlate.BoilerPlateID).AsNoTracking().Any();
                    if (isBool)
                    {
                        // Update
                        db.Entry(boilerPlate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    else
                    {
                        // Insert
                        db.Entry(boilerPlate).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    db.SaveChanges();
                    updateEntity = db.BoilerPlates.Where(a => a.BoilerPlateID == boilerPlate.BoilerPlateID).AsNoTracking().FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<BoilerPlateRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }



            return updateEntity;
        }

        public BoilerPlate GetBoilerPlate(long boilerPlateID)
        {
            BoilerPlate entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.BoilerPlates
                        .Include(x=> x.BoilerPlateParameters)
                        .Where(x => x.BoilerPlateID == boilerPlateID).AsNoTracking().FirstOrDefault();

                    entity.BoilerPlateParameters = dbContext.BoilerPlateParameters
                                .Where(a => a.BoilerPlateID == boilerPlateID || a.BoilerPlateID == null).AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the boiler plates", TrackingCode.ERP);
            }

            return entity;
        }
    
        public List<PageBoilerplateReport> GetBoilerPlateReports(long? boilerPlateID, long? pageBoilerPlateMapIID)
        {
            List <PageBoilerplateReport> entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var pageBoilerplateMaps = dbContext.PageBoilerplateMaps                        
                        .Where(x => x.BoilerplateID == boilerPlateID && x.PageBoilerplateMapIID== pageBoilerPlateMapIID).AsNoTracking().FirstOrDefault();
                    if(pageBoilerplateMaps != null)
                    entity = dbContext.PageBoilerplateReports
                                .Where(a => a.BoilerPlateID == boilerPlateID || a.PageID == pageBoilerplateMaps.PageID).AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the boiler plates", TrackingCode.ERP);
            }

            return entity;
        }

        public List<BoilerPlate> GetBoilerplates()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.BoilerPlates.OrderBy(a=> a.Name).AsNoTracking().ToList();
            }
        }

        public List<BoilerPlateParameter> GetBoilerPlateParameters(long boilerPlateID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.BoilerPlateParameters.Where(a => a.BoilerPlateID == boilerPlateID).AsNoTracking().ToList();
            }
        }
    }
}
