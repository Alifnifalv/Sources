using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Repository
{
    public class DocumentRepository
    {
        public List<DocumentFile> GetDocuments(long? referenceID, int entityTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DocumentFiles.Where(x => x.ReferenceID == referenceID
                && x.EntityTypeID == entityTypeID).Include(x => x.Employee).Include(x => x.DocumentFileStatus).ToList();
            }
        }

        public List<DocumentFileStatus> GetDocumentFileStatuses()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DocumentFileStatuses.OrderBy(a=> a.StatusName).ToList();
            }
        }

        public bool SaveDocuments(List<DocumentFile> files)
        {
            var exit = false;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                foreach (var file in files)
                {
                    dbContext.DocumentFiles.Add(file);
                    if (file.DocumentFileIID <= 0)
                        dbContext.Entry(file).State = System.Data.Entity.EntityState.Added;
                    else
                        dbContext.Entry(file).State = System.Data.Entity.EntityState.Modified;
                }
                dbContext.SaveChanges();
                // Set exit True
                exit = true;
            }
            return exit;
        }
    }
}
