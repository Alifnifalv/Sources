using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetPaymentDetails(List<TransactionHeadShoppingCartMap> entl)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO(); 
            var payments = new PaymentDetailsTheFort();

            if (entl.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CartID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TrackID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TrackKey" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PaymentID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InitOn" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InitIP" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InitLocation" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InitAmount" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PCustomerEmail" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PMerchantReference" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PSignatureText" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PAmount" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PTransMerchantReference" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PTransAmount" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PTransPaymentOption" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PTransResponseMessage" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PTransResponseCode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PTransCustomerIP" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PTransCustomerName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "OrderID" });

                List<long> heads = new List<long>();
                heads.AddRange(entl.Select(x=> (long)x.TransactionHeadID).ToList());

                var payment = new PaymentRepository().GetPaymentDetails(heads);

                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                         entl.Select(x=>x.ShoppingCartID).FirstOrDefault(),
                         payment.IsNotNull() ? Convert.ToString(payment.TrackID) : string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.TrackKey) : string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.CustomerID) :string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.PaymentID):string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.InitOn):string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.InitIP):string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.InitLocation):string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.InitAmount) : string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.PCustomerEmail):string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.PMerchantReference):string.Empty,payment.IsNotNull() ?Convert.ToString(payment.PSignatureText):string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.PAmount):string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.TransID):string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.PTransMerchantReference):string.Empty,payment.IsNotNull() ? Convert.ToString(payment.PTransAmount) : string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.PTransPaymentOption): string.Empty,payment.IsNotNull() ? Convert.ToString(payment.PTransResponseMessage):string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.PTransResponseCode):string.Empty,payment.IsNotNull() ? Convert.ToString(payment.PTransCustomerIP):string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.PTransCustomerName): string.Empty,
                         payment.IsNotNull() ? Convert.ToString(payment.OrderID): string.Empty

                        }
                    });
                }

            return searchDTO;
        }
    }
}
 