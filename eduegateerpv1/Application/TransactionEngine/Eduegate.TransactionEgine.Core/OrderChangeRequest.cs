using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.TransactionEngineCore.Interfaces;
using Eduegate.TransactionEngineCore.ViewModels;

namespace Eduegate.TransactionEngineCore
{
    public class OrderChangeRequest : TransactionBase, ITransactions
    {
        public OrderChangeRequest(Action<string> logError)
        {
            _logError = logError;
        }

        public DocumentReferenceTypes ReferenceTypes
        {
            get { return DocumentReferenceTypes.OrderChangeRequest; }
        }

        public void Process(TransactionHeadViewModel orderChangeRequest)
        {

            #region ORDER CHANGE REQUEST

            try
            {

                // Get parent transaction (SO)
                var salesOrder = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetTransaction((long)orderChangeRequest.ReferenceHeadID);
                _logError("sales order found");
                // Get Mission for this SO if any
                // Get Job for salesInvoice (assuming there will be one SI for the SO)
                var job = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetJobByHeadID(salesOrder.TransactionHead.HeadIID);
                var mission = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetMissionByJobID(job.JobEntryHeadIID);
                bool isDelivered = false;
                // If order not delivered (job should not have a completed mission)
                if (mission.IsNull() || (mission.JobStatusID != (int)Services.Contracts.Enums.JobStatuses.Delivered))
                {
                    // Cancel SalesOrder
                    var result = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().UpdateTransactionHead(
                        new TransactionHeadDTO()
                        {
                            HeadIID = salesOrder.TransactionHead.HeadIID,
                            TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.IntitiateReprocess,
                            DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Cancelled
                        });
                }
                else
                {
                    // Delivered
                    isDelivered = true;
                }

                var actionBasedDetailCollection = orderChangeRequest.TransactionDetails.GroupBy(od => od.Action).Select(g => g.ToList()).ToList();

                // New salesOrders
                var newSalesOrders = new List<TransactionDTO>();

                // Collection of different actions detail line
                foreach (var actionCollection in actionBasedDetailCollection)
                {
                    var newSalesOrder = new TransactionDTO();
                    newSalesOrder.TransactionDetails = new List<TransactionDetailDTO>();

                    // Iterating for detail rows under every action
                    foreach (var item in actionCollection)
                    {
                        var salesOrderDetail = salesOrder.TransactionDetails.Where(sd => sd.DetailIID == (long)item.ParentDetailID).FirstOrDefault();
                        // If quantity missmatch
                        if (item.Quantity != salesOrderDetail.Quantity)
                        {
                            var isChangeRequestDetailProcessed = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().IsChangeRequestDetailProcessed(item.DetailIID);

                            // Go ahead only if not processed
                            if (!isChangeRequestDetailProcessed)
                            {
                                salesOrderDetail.Quantity = salesOrderDetail.Quantity - item.Quantity;
                                salesOrderDetail.Amount = salesOrderDetail.Quantity * salesOrderDetail.UnitPrice;
                                salesOrderDetail.Action = item.Action; // setting change request action to new SO's line
                                salesOrderDetail.ParentDetailID = item.DetailIID;// setting change request detailId to new SO line's parent
                                newSalesOrder.TransactionDetails.Add(salesOrderDetail);
                            }
                        }
                    }
                    if (newSalesOrder.TransactionDetails.Count > 0)
                    {
                        // Create transaction head for this
                        newSalesOrder.TransactionHead = new TransactionHeadDTO();
                        newSalesOrder.TransactionHead.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapDTO>();

                        newSalesOrder.TransactionHead = salesOrder.TransactionHead;
                        newSalesOrder.TransactionHead.ReferenceHeadID = orderChangeRequest.HeadIID;
                        newSalesOrder.TransactionHead.DocumentTypeID = salesOrder.TransactionHead.DocumentTypeID;
                        newSalesOrder.TransactionHead.HeadIID = default(long);

                        // Entitlement amount must match details total
                        // picking first entitlement from parent sales order for now to add entitlement in new salse order


                        //newSalesOrder.TransactionHead.TransactionHeadEntitlementMaps.Clear();
                        //newSalesOrder.TransactionHead.TransactionHeadEntitlementMaps.Add(GetEntitlementMap(salesOrder, newSalesOrder));
                        if (salesOrder.TransactionHead.TransactionHeadEntitlementMaps.IsNotNull() && salesOrder.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                        {

                            // Create entitlement
                            var newEntitlementMap = newSalesOrder.TransactionHead.TransactionHeadEntitlementMaps.FirstOrDefault();

                            newEntitlementMap.TransactionHeadEntitlementMapID = default(long);
                            newEntitlementMap.TransactionHeadID = newSalesOrder.TransactionHead.HeadIID;
                            newEntitlementMap.EntitlementID = salesOrder.TransactionHead.TransactionHeadEntitlementMaps.FirstOrDefault().EntitlementID;
                            newEntitlementMap.EntitlementName = salesOrder.TransactionHead.TransactionHeadEntitlementMaps.FirstOrDefault().EntitlementName;
                            newEntitlementMap.Amount = newSalesOrder.TransactionDetails.Sum(d => d.Amount);

                            // Remove EntitlementMaps except first
                            newSalesOrder.TransactionHead.TransactionHeadEntitlementMaps.RemoveAll(e => e.EntitlementID != newEntitlementMap.EntitlementID);
                        }
                        newSalesOrders.Add(newSalesOrder);
                    }
                }

                // Get detail lines from sales order which are not in change request
                /*
                 * var excludedDetailLines = salesOrder.TransactionDetails.Where(d => orderChangeRequest.TransactionDetails.Any(d2 => d2.ParentDetailID != d.DetailIID)).ToList();
                if (excludedDetailLines.IsNotNull() && excludedDetailLines.Count > 0)
                {

                    var excludedSalesOrder = new TransactionDTO();
                    excludedSalesOrder.TransactionHead = new TransactionHeadDTO();
                    excludedSalesOrder.TransactionHead = salesOrder.TransactionHead;
                    excludedSalesOrder.TransactionHead.ReferenceHeadID = orderChangeRequest.HeadIID;
                    excludedSalesOrder.TransactionHead.DocumentTypeID = salesOrder.TransactionHead.DocumentTypeID;
                    excludedSalesOrder.TransactionHead.HeadIID = default(long);

                    // Create entitlement
                    var newEntitlementMap = excludedSalesOrder.TransactionHead.TransactionHeadEntitlementMaps.FirstOrDefault();

                    newEntitlementMap.TransactionHeadEntitlementMapID = default(long);
                    newEntitlementMap.TransactionHeadID = excludedSalesOrder.TransactionHead.HeadIID;
                    newEntitlementMap.EntitlementID = salesOrder.TransactionHead.TransactionHeadEntitlementMaps.FirstOrDefault().EntitlementID;
                    newEntitlementMap.EntitlementName = salesOrder.TransactionHead.TransactionHeadEntitlementMaps.FirstOrDefault().EntitlementName;
                    newEntitlementMap.Amount = excludedSalesOrder.TransactionDetails.Sum(d => d.Amount);

                    // Remove EntitlementMaps except first
                    excludedSalesOrder.TransactionHead.TransactionHeadEntitlementMaps.RemoveAll(e => e.EntitlementID != newEntitlementMap.EntitlementID);

                    excludedSalesOrder.TransactionDetails = new List<TransactionDetailDTO>();
                    foreach (var item in excludedDetailLines)
                    {
                        var isChangeRequestDetailProcessed = new TransactionServiceClient().IsChangeRequestDetailProcessed(item.DetailIID);

                        // Go ahead only if not processed
                        if (!isChangeRequestDetailProcessed)
                        {

                            excludedSalesOrder.TransactionDetails.Add(new TransactionDetailDTO
                            {
                                Quantity = item.Quantity,
                                Amount = item.Amount,
                                Action = item.Action,
                                ParentDetailID = item.ParentDetailID,
                            });
                        }

                    }
                    newSalesOrders.Add(excludedSalesOrder);
                }
                */

                // Save new sales orders
                if (newSalesOrders.Count > 0)
                {
                    List<long> resultSOList = new List<long>();
                    foreach (var order in newSalesOrders)
                    {
                        var result = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).SaveTransactions(order);
                        if (result.TransactionHead.HeadIID > 0 && result.TransactionHead.IsError == false)
                            resultSOList.Add(result.TransactionHead.HeadIID);
                    }

                    /* If all the SOs created successfully, update ChangeRequest to complete 
                       else update created SOs to fail + change request to fail
                    */
                    if (resultSOList.Count == newSalesOrders.Count)
                    {
                        var result = UpdateTransactionHead(orderChangeRequest.HeadIID, Eduegate.Framework.Enums.TransactionStatus.Complete, Services.Contracts.Enums.DocumentStatuses.Completed);
                    }
                    else
                    {
                        // Change request processing failed
                        UpdateTransactionHead(orderChangeRequest.HeadIID, Eduegate.Framework.Enums.TransactionStatus.Failed, Services.Contracts.Enums.DocumentStatuses.Draft);

                        // Make newly created created SOs to be failed
                        foreach (var item in resultSOList)
                        {
                            UpdateTransactionHead(item, Eduegate.Framework.Enums.TransactionStatus.Failed, Services.Contracts.Enums.DocumentStatuses.Draft);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateTransactionHead(orderChangeRequest, Eduegate.Framework.Enums.TransactionStatus.Failed, Services.Contracts.Enums.DocumentStatuses.Draft);
                WriteLog("Exception occured in OrderChangeRequest-Process.:" + ex.Message, ex);
                TransactionProcessingFailed(orderChangeRequest, ex.Message);
            }

            #endregion
        }

        private TransactionHeadEntitlementMapDTO GetEntitlementMap(TransactionDTO salesOrder, TransactionDTO newSalesOrder)
        {
            if (salesOrder.TransactionHead.TransactionHeadEntitlementMaps.IsNotNull() && salesOrder.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
            {
                var firstEntitlement = salesOrder.TransactionHead.TransactionHeadEntitlementMaps.FirstOrDefault();
                // Create entitlement
                //var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                var newEntitlementMap = new TransactionHeadEntitlementMapDTO();

                newEntitlementMap.TransactionHeadEntitlementMapID = default(long);
                newEntitlementMap.TransactionHeadID = newSalesOrder.TransactionHead.HeadIID;
                newEntitlementMap.EntitlementID = firstEntitlement.EntitlementID;
                newEntitlementMap.EntitlementName = firstEntitlement.EntitlementName;
                newEntitlementMap.Amount = newSalesOrder.TransactionDetails.Sum(d => d.Amount);
                return newEntitlementMap;
            }
            else
            {
                return null;
            }

        }
    }
}
