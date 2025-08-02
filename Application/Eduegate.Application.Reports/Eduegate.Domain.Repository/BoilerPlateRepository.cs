using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;
using System.Data.Entity;

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
                    bool isBool = db.BoilerPlates.Where(a => a.BoilerPlateID == boilerPlate.BoilerPlateID).Any();
                    if (isBool)
                    {
                        // Update
                        db.Entry(boilerPlate).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        // Insert
                        db.Entry(boilerPlate).State = System.Data.Entity.EntityState.Added;
                    }
                    db.SaveChanges();
                    updateEntity = db.BoilerPlates.Where(a => a.BoilerPlateID == boilerPlate.BoilerPlateID).FirstOrDefault();
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
                        .Where(x => x.BoilerPlateID == boilerPlateID).FirstOrDefault();

                    entity.BoilerPlateParameters = dbContext.BoilerPlateParameters
                                .Where(a => a.BoilerPlateID == boilerPlateID || a.BoilerPlateID == null).ToList();
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
                return db.BoilerPlates.OrderBy(a=> a.Name).ToList();
            }
        }

        public List<BoilerPlateParameter> GetBoilerPlateParameters(long boilerPlateID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.BoilerPlateParameters.Where(a=> a.BoilerPlateID == boilerPlateID).ToList();
            }
        }
    }
}
