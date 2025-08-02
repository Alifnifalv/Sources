using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetJobEntryDetails(JobEntryHead jobEntry)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (jobEntry.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobEntryHeadIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobNumber" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobStartDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobEndDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Remarks" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionTypeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Priority" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Basket" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "StatusName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobOperationStatus" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerID" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          jobEntry.JobEntryHeadIID,jobEntry.JobNumber,
                          jobEntry.JobStartDate.IsNotNull() ?  Convert.ToDateTime(jobEntry.JobStartDate).ToString("dd/MM/yyyy"): null,
                          jobEntry.TransactionHead.IsNotNull() && jobEntry.TransactionHead.DeliveryDate.IsNotNull() ? Convert.ToDateTime(jobEntry.TransactionHead.DeliveryDate).ToString("dd/MM/yyyy"): null,
                          jobEntry.Remarks,
                          jobEntry.DocumentType1.IsNotNull() ? jobEntry.DocumentType1.TransactionTypeName : string.Empty,
                          jobEntry.Priority.IsNotNull() ? jobEntry.Priority.Description : string.Empty,
                          jobEntry.Basket.IsNotNull() ? jobEntry.Basket.Description : string.Empty,
                          jobEntry.JobStatus.IsNotNull() ? jobEntry.JobStatus.StatusName : string.Empty,
                          jobEntry.JobOperationStatus.IsNotNull() ? jobEntry.JobOperationStatus.Description : string.Empty,
                          jobEntry.CreatedDate.IsNotNull() ? Convert.ToDateTime(jobEntry.CreatedDate).ToString("dd/MM/yyyy"): null,
                          jobEntry.UpdatedDate.IsNotNull() ? Convert.ToDateTime(jobEntry.UpdatedDate).ToString("dd/MM/yyyy"): null,
                          jobEntry.TransactionHead.IsNotNull() && jobEntry.TransactionHead.CustomerID > 0 ? jobEntry.TransactionHead.CustomerID : 0,
                        }
                });
            }


            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetJobEntryDetail(List<JobEntryHead > jobEntry)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (jobEntry.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobEntryHeadIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobNumber" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionHeadID" });

                foreach (var job in jobEntry)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            job.JobEntryHeadIID,job.JobNumber,job.TransactionHead.TransactionNo
                        }
                    });
                }
            }


            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetOrdersByCartDetails(List<TransactionHeadShoppingCartMap> thscm, List<TransactionHead> thead)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();
            
            if (thscm.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ShoppingCartID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionHeadID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "JobStatus" });
                // searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryStatus" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                foreach (var order in thead)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          thscm.Select(x=>x.ShoppingCartID).FirstOrDefault(),order.HeadIID,order.TransactionNo,order.CustomerID,
                          order.JobStatusID != null ?order.JobStatus.StatusName:"-",order.CreatedDate,order.UpdatedDate
                        }
                    });
                }
            }


            return searchDTO;
        }
        
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetComments(List<Comment> cmnt)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (cmnt.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Comment1" });
                foreach (var cmn in cmnt)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                         cmn.Comment1
                        }
                    });
                }
            }
            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetTransactionComments(JobEntryHead job, List<Comment> cmnt)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (job.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Comment1" });
                foreach (var cmn in cmnt)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                         cmn.Comment1
                        }
                    });
                }
            }
            return searchDTO;
        }
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetMissionInvoiceDetails(List<TransactionHead> transaction)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (transaction.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionHeadID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TransactionTypeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DeliveryDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Description" });

                foreach (var invoice in transaction)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            invoice.HeadIID,invoice.TransactionNo,invoice.DocumentType.TransactionTypeName,
                            Convert.ToDateTime(invoice.DeliveryDate).ToString("dd/MM/yyyy")
                            ,invoice.Description
                        }
                    });
                }
            }

            return searchDTO;
        }



        public Eduegate.Services.Contracts.Search.SearchResultDTO GetPayfortDetails(PaymentDetailsTheFort Details)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (Details.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TrackID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TrackKey" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Email" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InitON" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InitIP" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ResponseMessage" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Status" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PaymentMode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PaymentID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InitAmount" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PCurrency" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PTransCurrency" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "InitLocation" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "AuthorizationCode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ResponseCode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ExpiryDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CardNo" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          Details.TrackID, Details.TrackKey,Details.CustomerID,Details.PCustomerEmail,
                          Details.InitOn.IsNotNull() ? Convert.ToDateTime( Details.InitOn).ToString("dd/MM/yyyy"): null,
                          Details.InitIP,
                          Details.PTransResponseMessage.IsNotNull() ? Details.PTransResponseMessage: null,
                          Details.InitStatus.IsNotNull() ? Details.InitStatus: null,
                          Details.PTransPaymentOption.IsNotNull() ? Details.PTransPaymentOption: null,
                          Details.PaymentID,Details.InitAmount,Details.PCurrency,Details.PTransCurrency,Details.InitLocation,
                          Details.PTransAuthorizationCode.IsNotNull() ? Details.PTransAuthorizationCode.FirstOrDefault() : 0,
                          Details.PTransResponseCode.IsNotNull() ? (int)Details.PTransResponseCode : 0,
                          Details.PTransExpiryDate.IsNotNull() ? Details.PTransExpiryDate.ToString(): null,
                          Details.PTransCardNumber.IsNotNull() ? Details.PTransCardNumber : null,

                        }
                });
            }


            return searchDTO;
        }

        
    }
}