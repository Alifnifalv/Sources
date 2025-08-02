using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Logs;
using Eduegate.Services.Contracts.Eduegates;
using System.Data.SqlClient;
using System.Data;


namespace Eduegate.Domain.Repository
{
   public class FeeDataRepository
    {
        public List<long?> GetFeeStructureID(int? classID, long? studentID, int acadamicYearID)
        {
            List<long?> feeStructureID = new List<long?>();
            using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
            using (SqlCommand cmd = new SqlCommand("schools.SPS_GETFEESTRUCTUREID", conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@classID", SqlDbType.NVarChar));
                adapter.SelectCommand.Parameters["@classID"].Value = classID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@studentID", SqlDbType.NVarChar));
                adapter.SelectCommand.Parameters["@studentID"].Value = studentID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@acadamicYearID", SqlDbType.NVarChar));
                adapter.SelectCommand.Parameters["@acadamicYearID"].Value = acadamicYearID;

                DataSet dt = new DataSet();
                adapter.Fill(dt);
                DataTable FeeStructure = null;

                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        FeeStructure = dt.Tables[0];
                    }
                }
                if (FeeStructure != null)
                {
                    foreach (DataRow row in FeeStructure.Rows)
                    {
                        
                        feeStructureID.Add((long?)(row["FeeStructureID"]));
                    }
                }

                
            }

            return feeStructureID;
        }

    }
}
