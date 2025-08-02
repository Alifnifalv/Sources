using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Services;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.TransactionEgine.Accounting;
using Eduegate.TransactionEngineCore.Interfaces;
using Eduegate.TransactionEngineCore.ViewModels;

namespace Eduegate.TransactionEngineCore
{
    public class MIssionJobTransaction : TransactionBase
    {
        public MIssionJobTransaction(Action<string> logError)
        {
            _logError = logError;
        }

        private List<JobEntryHeadAccountingDTO> GetAllMissionJobEntryHeads(DocumentReferenceTypes referenceTypes, TransactionStatus transactionStatus)
        {
            return new Eduegate.TransactionEgine.ClientFactory.ClientFactory().GetAllMissionJobEntryHeads(referenceTypes, transactionStatus);

            //var _TransactionHeadViewModel = new List<TransactionHeadViewModel>();
            //var _JobEntryHeadAccountingDTO = new AccountingTransactionServiceClient().GetAllMissionJobEntryHeads(referenceTypes, transactionStatus);

            //if (_JobEntryHeadAccountingDTO == null)
            //{
            //    return null;
            //}

            //// convert TransactionDetail into TransactionDetailDTO
            //foreach (var item in _JobEntryHeadAccountingDTO)
            //{
            //    _TransactionHeadViewModel.Add(TransactionHeadViewModel.FromDTO(item.TransactionHeadDTO));
            //}

            //return _TransactionHeadViewModel;
        }

        public void StartProcess(DocumentReferenceTypes referenceTypes, TransactionStatus status)
        {
            if (!IsTransactionEnabled(referenceTypes))
            {
                return;
            }

            WriteLog("Geting MissionJob Transactions - " + referenceTypes.ToString() + " " + status.ToString());
            var _JobEntryHeadAccountingDTOs = GetAllMissionJobEntryHeads(referenceTypes, status);

            WriteLog(" MissionJob Transaction found - " + _JobEntryHeadAccountingDTOs.Count());

            try
            {
                //for each start
                foreach (var jobEntryHeadAccountingDTO in _JobEntryHeadAccountingDTOs)
                {
                    try
                    {
                        switch (referenceTypes)
                        {
                            case DocumentReferenceTypes.All:
                                break;
                            case DocumentReferenceTypes.DistributionJobs: //Driver Assignments
                                new MissionJobAssignmentAccounting(_logError).Process(jobEntryHeadAccountingDTO);
                                 break;
                            case DocumentReferenceTypes.ServiceJobs: //Driver Return job
                                new MissionJobReturnAccounting(_logError).Process(jobEntryHeadAccountingDTO);
                                break;
                            
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Eduegate.Logger.LogHelper<string>.Fatal(ex.Message.ToString(), ex);
                        WriteLog(ex.Message.ToString());
                    }
                }
                //for each end
            }
            catch (Exception ex)
            {
                //Eduegate.Logger.LogHelper<string>.Fatal(ex.Message.ToString(), ex);
                WriteLog(ex.Message.ToString());
            }
        }
    }
}
