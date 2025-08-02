using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.TransactionEgine.Accounting;
using Eduegate.TransactionEngineCore.ViewModels;
using Eduegate.TransactionEgineCore;

namespace Eduegate.TransactionEngineCore
{
    public class Transaction : TransactionBase
    {
        public Transaction(Action<string> logError)
        {
            _logError = logError;
        }

        private TransactionHeadViewModel GetTransactionByID(long headID)
        {
            var _TransactionHeadViewModel = new List<TransactionHeadViewModel>();
            var _TransactionHeadDTO = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetTransactionDetail(headID);

            if (_TransactionHeadDTO == null)
            {
                return null;
            }

            return TransactionHeadViewModel.FromDTO(_TransactionHeadDTO);
        }

        private List<TransactionHeadViewModel> GetAllTransaction(DocumentReferenceTypes referenceTypes, TransactionStatus transactionStatus)
        {
            var _TransactionHeadViewModel = new List<TransactionHeadViewModel>();
            var _TransactionHeadDTO = new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetAllTransaction(referenceTypes, transactionStatus);

            if (_TransactionHeadDTO == null)
            {
                return null;
            }

            // convert TransactionDetail into TransactionDetailDTO
            foreach (var item in _TransactionHeadDTO)
            {
                _TransactionHeadViewModel.Add(TransactionHeadViewModel.FromDTO(item));
            }

            return _TransactionHeadViewModel;
        }

        public void StartProcess(DocumentReferenceTypes referenceTypes, TransactionStatus status, long? transactionID = null)
        {
            if (!IsTransactionEnabled(referenceTypes))
            {
                return;
            }

            var lastTransactionCount = new Domain.Setting.SettingBL().GetSettingValue<int>("MaxFetchCount");

            while (true)
            {
                if (lastTransactionCount == new Domain.Setting.SettingBL().GetSettingValue<int>("MaxFetchCount"))
                {
                    lastTransactionCount = Process(referenceTypes, status, transactionID);
                }
                else
                {
                    break;
                }
            }
        }

        private int Process(DocumentReferenceTypes referenceTypes, TransactionStatus status, long? transactionID = null)
        {
            //WriteLog("Method GetAllTransaction is calling for referenceTypes= " + referenceTypes + "and status= " + status);
            //LogHelper<Transaction>.Info("GetAllTransaction called for referenceTypes" + referenceTypes + "and status= " + status);
            WriteLog("Geting Transactions - " + referenceTypes.ToString() + " " + status.ToString());
            var transactions = new List<TransactionHeadViewModel>();

            if (transactionID.HasValue)
            {
                transactions.Add(GetTransactionByID(transactionID.Value));
            }
            else
            {
                transactions = GetAllTransaction(referenceTypes, status);
            }

            WriteLog(" Transaction found - " + transactions.Count());
            var EnableAccountProcessing = new Domain.Setting.SettingBL(null).GetSettingValue<bool>("EnableAccountProcessing", false);

            try
            {
                //for each start
                foreach (var transaction in transactions)
                {
                    //should continue with next transaction
                    try
                    {
                        switch (transaction.DocumentReferenceTypeID)
                        {
                            case DocumentReferenceTypes.All:
                                break;
                            case DocumentReferenceTypes.SalesOrder:
                                new SalesOrder(_logError).Process(transaction);
                                if (EnableAccountProcessing)
                                {
                                    new SalesOrderAccounting(_logError).Process(TransactionHeadViewModel.ToAccountingModel(transaction));
                                }
                                break;
                            case DocumentReferenceTypes.PurchaseInvoice:
                                new PurchaseInvoice(_logError).Process(transaction);
                                if (EnableAccountProcessing)
                                {
                                    new PurchaseInvoiceAccounting(_logError).Process(TransactionHeadViewModel.ToAccountingModel(transaction));
                                }
                                break;
                            case DocumentReferenceTypes.SalesReturn:
                                new SalesReturn(_logError).Process(transaction);
                                if (EnableAccountProcessing)
                                {
                                    new SalesReturnAccounting(_logError).Process(TransactionHeadViewModel.ToAccountingModel(transaction));
                                }
                                break;
                            case DocumentReferenceTypes.SalesReturnRequest:
                                new SalesReturnRequest(_logError).Process(transaction);
                                break;
                            case DocumentReferenceTypes.PurchaseReturn:
                                new PurchaseReturn(_logError).Process(transaction);
                                if (EnableAccountProcessing)
                                {
                                    new PurchaseReturnAccounting(_logError).Process(TransactionHeadViewModel.ToAccountingModel(transaction));
                                }
                                break;
                            case DocumentReferenceTypes.GoodsReceivedNotes:
                                new GoodsReceivedNote(_logError).Process(transaction);
                                if (EnableAccountProcessing)
                                {
                                    new GoodsReceivedNoteAccounting(_logError).Process(TransactionHeadViewModel.ToAccountingModel(transaction));
                                }
                                break;
                            case DocumentReferenceTypes.PurchaseReturnRequest:
                                new PurchaseReturnRequest(_logError).Process(transaction);
                                break;
                            case DocumentReferenceTypes.SalesInvoice:
                                new SalesInvoice(_logError).Process(transaction);
                                if (EnableAccountProcessing)
                                {
                                    new SalesInvoiceAccounting(_logError).Process(TransactionHeadViewModel.ToAccountingModel(transaction));
                                }
                                break;
                            case DocumentReferenceTypes.PurchaseOrder:
                                new PurchaseOrder(_logError).Process(transaction);
                                break;
                            case DocumentReferenceTypes.SalesQuote:
                                break;
                            case DocumentReferenceTypes.PurchaseQuote:
                                new PurchaseQuotation(_logError).Process(transaction);
                                break;
                            case DocumentReferenceTypes.BranchTransfer:
                                new BranchTransfer(_logError).Process(transaction);
                                break;
                            case DocumentReferenceTypes.OrderChangeRequest:
                                new OrderChangeRequest(_logError).Process(transaction);
                                break;
                            case DocumentReferenceTypes.PurchaseTender:
                                new PurchaseTender(_logError).Process(transaction);
                                break;
                            default:
                                break;
                            case DocumentReferenceTypes.BundleWrap:
                                new BundleWrap(_logError).Process(transaction);
                                break;
                            case DocumentReferenceTypes.BundleUnWrap:
                                new BundleUnWrap(_logError).Process(transaction);
                                break;
                            case DocumentReferenceTypes.ServiceEntry:
                                new ServiceEntry(_logError).Process(transaction);
                                if (EnableAccountProcessing)
                                {
                                    new ServiceEntryAccounting(_logError).Process(TransactionHeadViewModel.ToAccountingModel(transaction));
                                }
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Eduegate.Logger.LogHelper<string>.Fatal(ex.Message.ToString(), ex);
                        WriteLog(ex.Message.ToString());
                        throw ex;
                    }
                }
                //for each end
            }
            catch (Exception ex)
            {
                //Eduegate.Logger.LogHelper<string>.Fatal(ex.Message.ToString(), ex);
                WriteLog(ex.Message.ToString());
                throw ex;
            }

            return transactions.Count;
        }
    }
}
