using Eduegate.Domain.Entity.Logging;
using Eduegate.Domain.Entity.Logging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Eduegate.Domain.Repository.Logging
{
   public class CatalogLoggerRepository
    {
       public void SyncLogger(CatalogLogger catalogLogger)
       {
           try
           {
               using (EduegatedERP_LoggerContext dbContext = new EduegatedERP_LoggerContext())
               {
                   dbContext.CatalogLoggers.Add(catalogLogger);
                   dbContext.SaveChanges();
               }
           }
           catch (Exception exception)
           {
                Eduegate.Logger.LogHelper<CatalogLoggerRepository>.Fatal(exception.Message.ToString(), exception);
               throw;
           }
       }

       public bool syncLoggerSKUIDExists(CatalogLogger catalogLogger)
       {
           try
           {
               using (EduegatedERP_LoggerContext dbContext = new EduegatedERP_LoggerContext())
               {
                   var result = (from l in dbContext.CatalogLoggers
                                 where (l.ProductSKUMapID == catalogLogger.ProductSKUMapID) && (l.OperationTypeID == catalogLogger.OperationTypeID)
                                 select l).FirstOrDefault();
                   if (result != null) { return true; } else { return false; }
               }
           }
           catch (Exception exception)
           {
                Eduegate.Logger.LogHelper<CatalogLoggerRepository>.Fatal(exception.Message.ToString(), exception);
               return false;
           }
       }
    }
}
