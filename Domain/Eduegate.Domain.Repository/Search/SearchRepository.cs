using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;
using Eduegate.Framework.Extensions;
using Eduegate.Infrastructure.Enums;
using Eduegate.Domain.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository.Search
{
    public class SearchRepository
    {
       public List<UserView> GetUserViews(SearchView view, long? userID, int companyID)
       {
           List<UserView> entity = new List<UserView>();

           try
           {
               using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
               {
                   //entity = dbContext.UserViews.Where(x => x.Vie  wID == (long)view && x.LoginID == userID).ToList();
               }
           }
           catch (Exception ex)
           {

               SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get column map from ViewGridColumns", TrackingCode.ERP);
           }

           return entity;
       }

        public async Task<Tuple<long, DataTable>> SearchData(SearchView view, long totalRecords, int currentPage, int pageSize, string orderby, long loginId, string runtimeFilter, int companyID,char viewType, byte? schoolID, int? academicYearID)
        {
            DataTable userViewGridDataList = null;
            var orderByType = "DESC";
            if (!orderby.IsNullOrEmpty())
            {
                var orderByTypes = orderby.Split(' ');

                if (orderByTypes.Length == 2)
                {
                    orderby = orderByTypes[0];
                    orderByType = orderByTypes[1];
                }
                else
                {
                    var orderByDataType = GetOrderByDataType((long)view, orderby);
                    switch (orderByDataType)
                    {
                        case "DateTime":
                        case "Date":
                            orderByType = "DESC";
                            break;
                        default:
                            orderByType = "ASC";
                            break;
                    }
                }
            }

            /** using ado.net **/
            //using (SqlConnection conn = new SqlConnection("Data Source=168.187.102.242,1434;Initial Catalog=dbBlink;Persist Security Info=True;User ID=sa;Password=Blink2014;MultipleActiveResultSets=True"))
            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            using (SqlCommand cmd = new SqlCommand("setting.GetGridViewData", conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@ViewId", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@ViewId"].Value = (int)view;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@CurrentPage", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@CurrentPage"].Value = currentPage;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@PageSize"].Value = pageSize;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@OrderBy", SqlDbType.VarChar));
                adapter.SelectCommand.Parameters["@OrderBy"].Value = orderby;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@OrderByType", SqlDbType.VarChar));
                adapter.SelectCommand.Parameters["@OrderByType"].Value = orderByType;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@LoginID", SqlDbType.VarChar));
                adapter.SelectCommand.Parameters["@LoginID"].Value = loginId;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@RuntimeFilter", SqlDbType.VarChar));
                adapter.SelectCommand.Parameters["@RuntimeFilter"].Value = !string.IsNullOrEmpty(runtimeFilter) ? runtimeFilter : string.Empty;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@CompanyID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@CompanyID"].Value = companyID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@ViewType", SqlDbType.Char));
                adapter.SelectCommand.Parameters["@ViewType"].Value = viewType;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@SchoolID", SqlDbType.TinyInt));
                adapter.SelectCommand.Parameters["@SchoolID"].Value = schoolID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@AcademicYearID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@AcademicYearID"].Value = academicYearID;

                //fill the data table - no need to explicitly call `conn.Open()` - 
                //the SqlDataAdapter automatically does this (and closes the connection, too)

                //DataSet dt = new DataSet();
                //adapter.Fill(dt);

                //if (dt.Tables.Count > 0)
                //{
                //    if (dt.Tables[0].Rows.Count > 0)
                //    {
                //        userViewGridDataList = dt.Tables[0];
                //    }

                //    totalRecords = long.Parse(dt.Tables[1].Rows[0][0].ToString());
                //}

                try
                {
                    conn.Open();
                    cmd.CommandTimeout = 0;
                    var ds = new DataSet();
                    await Task.Run(() => adapter.Fill(ds));
                    userViewGridDataList = ds.Tables[0];
                    totalRecords = ds.Tables.Count > 1 ? long.Parse(ds.Tables[1].Rows[0][0].ToString()) : 1;
                }
                catch (Exception exception)
                {
                    Eduegate.Logger.LogHelper<SearchRepository>.Fatal(exception.Message, exception);
                }

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            /** using ado.net **/


            //return userViewGridDataList;
            return Tuple.Create(totalRecords, userViewGridDataList);
        }

       private string GetOrderByDataType(long ViewID, string orderByColumn)
       {
           using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
           {
               var col = dbContext.ViewGridColumns.Where(y => y.ViewID == ViewID && y.PhysicalColumnName == orderByColumn).AsNoTracking().Select(x => x.DataType).FirstOrDefault();

               if (col != null)
               {
                   return col.ToString();
               }
               else
               {
                   return null;
               }
           }
       }

        /** sample method **/
        /*public List<ClassName> MethodName(int Param)
        {
            try
            {
                //method code
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get Wallet transaction details", TrackingCode.Entity);
            }

        }*/
    }
}
