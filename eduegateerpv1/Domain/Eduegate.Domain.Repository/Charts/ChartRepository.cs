using Newtonsoft.Json;
using Eduegate.Domain.Entity.Models.Settings;
using Eduegate.Framework.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Eduegate.Domain.Repository.ValueObjects;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository.Charts
{
    public class ChartRepository
    {
        public List<ChartMetadata> GetAllChartMetadatas()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ChartMetadatas.AsNoTracking().ToList();
            }
        }

        public ChartMetadata GetChartMetadata(int chartID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ChartMetadatas.Where(a=> a.ChartMetadataID == chartID).AsNoTracking().FirstOrDefault();  
            }
        }

        public DashBoardChartDTO GetChartData(int chartID, string parameter,int? schoolID, int? academicYearID, int? loginID)
        {
            var jsonData = new List<string>();
            var jsonRelationData = new List<string>();
            var columnHeaders = new List<string>();

            var dto = new DashBoardChartDTO();

            var metaData = GetChartMetadata(chartID);

            if (metaData == null) return null;

            if (metaData.ChartPhysicalEntiy.IsNotNullOrEmpty())
            {
                using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
                using (var cmd = new SqlCommand(string.Empty, conn))
                {
                    var adapter = new SqlDataAdapter(cmd);

                    if (metaData.ChartType.Equals("View"))
                    {
                        adapter.SelectCommand.CommandType = CommandType.Text;
                        cmd.CommandText = "select * from " + metaData.ChartPhysicalEntiy;
                    }
                    else
                    {
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Filter", parameter);
                        cmd.Parameters.AddWithValue("@SchoolID", schoolID);
                        cmd.Parameters.AddWithValue("@LoginID", loginID);
                        cmd.Parameters.AddWithValue("@AcademicYearID", academicYearID);
                        cmd.CommandText = metaData.ChartPhysicalEntiy;
                    }

                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            jsonData.Add(JsonConvert.SerializeObject(dataRow.ItemArray));
                        }

                        // Get column headers
                        foreach (DataColumn column in ds.Tables[0].Columns)
                        {
                            columnHeaders.Add(column.ColumnName);
                        }
                        if(ds.Tables.Count>1)
                        {
                            foreach (DataRow dataRow in ds.Tables[1].Rows)
                            {
                                jsonRelationData.Add(JsonConvert.SerializeObject(dataRow.ItemArray));
                            }
                        }
                    }
                }

                dto.ColumnHeaders = columnHeaders;

                dto.ColumnDatas = jsonData;

                dto.ColumnRelations = jsonRelationData;
            }

            return dto;
        }

        public ListWidgetDetail GetListData(string entityType, string listType, string entityId)
        {
            var widgetDetail = new ListWidgetDetail();
            using (var conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            using (var cmd = new SqlCommand(string.Empty, conn))
            {
                var adapter = new SqlDataAdapter(cmd);

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EntityType", entityType);
                cmd.Parameters.AddWithValue("@ListType", listType);
                cmd.Parameters.AddWithValue("@EntityId", entityId);
                cmd.CommandText = "cms.spGetListData";

                var ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables.Count == 2)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        foreach (var item in dataRow.ItemArray)
                        {
                            widgetDetail.Columns.Add(item.ToString());
                        }
                    }
                }

                if (ds.Tables.Count == 2)
                {
                    foreach (DataRow dataRow in ds.Tables[1].Rows)
                    {
                        widgetDetail.Datas.Add(dataRow.ItemArray);
                    }
                }
            }

            return widgetDetail;
        }

    }
}
