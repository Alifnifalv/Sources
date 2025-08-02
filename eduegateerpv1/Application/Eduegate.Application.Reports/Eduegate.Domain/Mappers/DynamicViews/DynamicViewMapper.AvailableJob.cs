using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.HR;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetAvailableJobs(JobOpeningDTO dto)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (dto.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobTitle" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DepartmentName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobDescription" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          dto.Id,dto.JobTitle,dto.DepartmentName,dto.JobDescription.Replace("\n","\\n").Replace("\t","\\t"),dto.CreatedDate
                        }
                });
            }


            return searchDTO;
        }
    }
}
