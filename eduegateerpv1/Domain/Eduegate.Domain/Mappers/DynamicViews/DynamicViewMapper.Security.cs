using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetClaimDetails(Claim claim)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (claim.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ClaimIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ClaimName" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            claim.ClaimIID,claim.ClaimName
                        }
                });

            }
            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetClaimDetails(ClaimSet claimSet)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (claimSet.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ClaimSetIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ClaimSetName" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            claimSet.ClaimSetIID,claimSet.ClaimSetName
                        }
                });

            }


            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO LoginDetails(Login login)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (login.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "LoginIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "LoginUserID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "LoginEmailID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "LastLoginDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "StatusID" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            login.LoginIID,login.LoginUserID, login.LoginEmailID, login.LastLoginDate, login.StatusID
                        }
                });

            }


            return searchDTO;
        }
    }
}
