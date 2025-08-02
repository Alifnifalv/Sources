using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Logging;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository.Logging
{
    public class DataHistoryRepository
    {
        public DataTable GetDataHistory(int EntityID, int IID, string FieldName)
        {
            DataTable dtDataHistory = new DataTable();
            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetBlinkConnectionString()))
            using (SqlCommand cmd = new SqlCommand("setting.spcGetDataHistory", conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@EntityID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@EntityID"].Value = EntityID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@RecordIID", SqlDbType.BigInt));
                adapter.SelectCommand.Parameters["@RecordIID"].Value = IID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@TrackingCol", SqlDbType.VarChar));
                adapter.SelectCommand.Parameters["@TrackingCol"].Value = FieldName;

                adapter.Fill(dtDataHistory);
            }
            return dtDataHistory;
        }

        public DataHistoryEntity GetDataHistoryEntity(int EntityID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.DataHistoryEntities.Where(x => x.DataHistoryEntityID == EntityID).AsNoTracking().SingleOrDefault();
                return entity;
            }
        }
    }
}
