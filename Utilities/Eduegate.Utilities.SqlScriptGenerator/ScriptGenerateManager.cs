using Eduegate.Domain.Entity;
using Eduegate.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Eduegate.Utilities.SqlScriptGenerator
{
    public class ScriptGenerateManager
    {
        public static List<string> GetLatestUpdateScripts(DateTime dateFrom, DateTime dateTo)
        {
            var scripts = new List<string>();

            foreach (var entity in GetSynchEntities())
            {
                var newData = GetInsertedData(dateFrom, dateTo, entity);
                var updateData = GetUpdatedData(dateFrom, dateTo, entity);

                var insertSQL = ScriptGenerator.GenerateSQL.BuildInsertSQL(newData);
                var updateSQL = ScriptGenerator.GenerateSQL.BuildUpdateSQL(newData);

                scripts.Add(insertSQL);
                scripts.Add(updateSQL);
            }
            
            return scripts;
        }

        private static DataTable GetInsertedData(DateTime dateFrom, DateTime dateTo, string entityName)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var query = $"SELECT * FROM {entityName} WHERE createddate BETWEEN @dateFrom AND @dateTo";
                var parameters = new[]
                {
                    new SqlParameter("@dateFrom", dateFrom),
                    new SqlParameter("@dateTo", dateTo)
                };

                var result = dbContext.Set<DataTable>().FromSqlRaw(query, parameters)
                    .AsNoTracking()
                    .FirstOrDefault();

                return result;
            }
        }

        private static DataTable GetUpdatedData(DateTime dateFrom, DateTime dateTo, string entityName)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var query = $"SELECT * FROM {entityName} WHERE updateddate BETWEEN @dateFrom AND @dateTo";
                var parameters = new[]
                {
                    new SqlParameter("@dateFrom", dateFrom),
                    new SqlParameter("@dateTo", dateTo)
                };

                var result = dbContext.Set<DataTable>().FromSqlRaw(query, parameters)
                    .AsNoTracking()
                    .FirstOrDefault();

                return result;
            }
        }

        private static List<string> GetSynchEntities()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var synchEntities = db.SyncEntities.ToList();
                return synchEntities.Select(a => a.EntityDataSource ).ToList();
            }
        }
    }
}
