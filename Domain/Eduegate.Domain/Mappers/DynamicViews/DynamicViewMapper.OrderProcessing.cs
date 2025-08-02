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
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetOrderProcessingData(List<PaymentMethod> paymentMethods)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (paymentMethods.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PaymentMethodID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MethodName" });

                foreach (var method in paymentMethods)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            method.PaymentMethodID,method.PaymentMethod1
                        }
                    });
                }
            }


            return searchDTO;
        }
    }
}
