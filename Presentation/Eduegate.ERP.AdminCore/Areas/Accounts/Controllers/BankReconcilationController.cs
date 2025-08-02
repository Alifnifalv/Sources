using DocumentFormat.OpenXml.Spreadsheet;
using Eduegate.Application.Mvc;
using Eduegate.ERP.Admin.Areas.Documents.Controllers;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Logger;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Accounting;
using System.Globalization;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Accounts.Accounting;
using StackExchange.Profiling.Internal;
namespace Eduegate.ERP.AdminCore.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class BankReconcilationController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult BankReconcilation(long bankReconciliationHeadIID = 0)
        {
            var vm = new BankReconcilationViewModel();

            if (bankReconciliationHeadIID == 0)
            {
                vm.IsEdit = false;
            }
            else
            {
                vm.BankReconciliationHeadIID = bankReconciliationHeadIID;
                vm.IsEdit = true;
                vm = FillBankReconcilationData(bankReconciliationHeadIID);
            }

            return View(vm);
        }
        [HttpGet]
        public ActionResult Upload()
        {
            return View(new UploadedFileDetailsViewModel());
        }

        private BankReconcilationViewModel FillBankReconcilationData(long bankReconciliationHeadIID)
        {
            var bankReconcilationViewModel = new BankReconcilationViewModel();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var entry = ClientFactory.AccountingServiceClient(CallContext).FillBankReconcilationData(bankReconciliationHeadIID);
            bankReconcilationViewModel.BankReconciliationHeadIID = bankReconciliationHeadIID;
            if (entry == null || !entry.BankAccountID.HasValue || entry.BankAccountID == 0)
                return bankReconcilationViewModel;
            bankReconcilationViewModel.BankReconciliationStatusID=entry.BankReconciliationStatusID;
            bankReconcilationViewModel.FromDateString = entry.FromDate.HasValue ? entry.FromDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            bankReconcilationViewModel.ToDateString = entry.ToDate.HasValue ? entry.ToDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            bankReconcilationViewModel.FromDate = entry.FromDate;
            bankReconcilationViewModel.ToDate = entry.ToDate;
            bankReconcilationViewModel.BankAccountID = entry.BankAccountID ?? 0;
            bankReconcilationViewModel.BankStatementID = entry.BankStatementID;
            bankReconcilationViewModel.LedgerOpeningBalance = entry.OpeningBalanceAccount ?? 0;
            bankReconcilationViewModel.LedgerClosingBalance = entry.ClosingBalanceAccount ?? 0;
            bankReconcilationViewModel.BankOpeningBalance = entry.OpeningBalanceBankStatement ?? 0;
            bankReconcilationViewModel.BankClosingBalance = entry.ClosingBalanceBankStatement ?? 0;

            var transactionDTO = new BankReconciliationDTO
            {
                IBAN = string.Empty,
                SDate = entry.FromDate,
                EDate = entry.ToDate,
                BankAccountID = entry.BankAccountID,
                BankReconciliationHeadIID = entry.BankReconciliationHeadIID
            };
            var ledgerData = FillLedgerData(transactionDTO);
            bankReconcilationViewModel.BankName = ledgerData?.BankName;



            //var manualEntry = (from s in entry.BankReconciliationDetailDtos
            //                   where s.BankReconciliationMatchedStatusID == null && s.ReconciliationDate != null
            //                   select new BankReconciliationManualEntry

            //                   {
            //                       AccountID = s.AccountID,
            //                       BankCreditAmount = 0,
            //                       BankDebitAmount = 0,
            //                       BankDescription = "-",
            //                       ChequeDate = "-",
            //                       ChequeNo = "-",
            //                       LedgerCreditAmount = s.Amount > 0 ? 0 : s.Amount,
            //                       LedgerDebitAmount = s.Amount > 0 ? s.Amount : 0,
            //                       Particulars = s.Remarks,
            //                       PostDate = "-",
            //                       ReconciliationCreditAmount = s.Amount > 0 ? s.Amount : 0,
            //                       ReconciliationDebitAmount = s.Amount > 0 ? 0 : s.Amount,
            //                       TransDate = s.ReconciliationDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture),

            //                       TranHeadID = 0,
            //                       TranTailID = 0,
            //                       PartyName = "-",
            //                       Reference = s.Reference
            //                   }).ToList();

            var unMatchedWithBankEntries = (from s in entry.BankReconciliationDetailDtos
                                            where s.BankReconciliationMatchedStatusID == (short?)Eduegate.Services.Contracts.Enums.BankReconciliationMatchedStatuses.MisMatchWithBank && s.ReconciliationDate != null
                                            select new BankReconciliationUnMatchedWithBankEntriesViewModel
                                            {
                                                BankDebitAmount = 0,
                                                BankCreditAmount = 0,
                                                Particulars = s.Remarks,
                                                PostDate = s.ReconciliationDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture),
                                                LedgerDebitAmount = s.Amount < 0 ? 0 : s.Amount,
                                                LedgerCreditAmount = s.Amount > 0 ? 0 : s.Amount,
                                                BankDescription = string.Empty,
                                                ReconciliationDebitAmount = s.Amount > 0 ? 0 : s.Amount,
                                                ReconciliationCreditAmount = s.Amount < 0 ? 0 : s.Amount,
                                                TranHeadID = s.TranHeadID,
                                                TranTailID = s.TranTailID,
                                                BankStatementEntryID = s.BankStatementEntryID,
                                                ChequeDate = s.ChequeDate,
                                                ChequeNo = s.ChequeNo,
                                                PartyName = s.PartyName,
                                                Reference = s.Reference
                                            }).ToList();
            var unMatchedLedgerEntriesList = (from s in entry.BankReconciliationDetailDtos
                                              where s.BankReconciliationMatchedStatusID == (short?)Eduegate.Services.Contracts.Enums.BankReconciliationMatchedStatuses.MisMatchWithLedger && s.ReconciliationDate != null
                                              select new BankReconciliationUnMatchedWithLedgerEntriesViewModel
                                              {
                                                  BankDebitAmount = s.Amount < 0 ? 0 : s.Amount,
                                                  BankCreditAmount = s.Amount > 0 ? 0 : s.Amount,

                                                  Particulars = s.Remarks,
                                                  PostDate = s.ReconciliationDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture),

                                                  LedgerDebitAmount = 0,
                                                  LedgerCreditAmount = 0,
                                                  ReconciliationDebitAmount = s.Amount > 0 ? 0 : s.Amount,
                                                  ReconciliationCreditAmount = s.Amount < 0 ? 0 : s.Amount,
                                                  ChequeDate = s.ChequeDate,
                                                  ChequeNo = s.ChequeNo,
                                                  PartyName = s.PartyName,
                                                  Reference = s.Reference
                                              }).ToList();

            bankReconcilationViewModel.BankReconciliationMatchedEntries = (from s in entry.BankReconciliationDetailDtos
                                                                           where s.BankReconciliationMatchedStatusID == (short?)Eduegate.Services.Contracts.Enums.BankReconciliationMatchedStatuses.Matched
                                                                           select new BankReconciliationMatchedEntriesViewModel
                                                                           {
                                                                               BankDebitAmount = s.Amount < 0 ? 0 : s.Amount,
                                                                               BankCreditAmount = s.Amount > 0 ? 0 : s.Amount,
                                                                               Particulars = s.Remarks,
                                                                               PostDate = s.ReconciliationDate.HasValue ? s.ReconciliationDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "-",
                                                                               LedgerDebitAmount = 0,
                                                                               LedgerCreditAmount = 0,
                                                                               ReconciliationDebitAmount = s.Amount > 0 ? 0 : s.Amount,
                                                                               ReconciliationCreditAmount = s.Amount < 0 ? 0 : s.Amount,
                                                                               ChequeDate = s.ChequeDate,
                                                                               ChequeNo = s.ChequeNo,
                                                                               PartyName = s.PartyName,
                                                                               Reference = s.Reference,
                                                                               AccountID = s.AccountID,
                                                                               BankDescription = s.Remarks,
                                                                               ReferenceGroupNo = s.ReferenceGroupNO,
                                                                               BankStatementEntryID = s.BankStatementEntryID,
                                                                               ReferenceGroupName = s.ReferenceGroupName,
                                                                               MatchedBankEntries = s.BankReconciliationMatchingEntry.Count() > 0 ? GetMatchingBankEntry(s.BankReconciliationMatchingEntry.Where(x => x.BankStatementEntryID.HasValue).ToList()) : new List<MatchedBankEntries>(),
                                                                               MatchedLedgerEntries = s.BankReconciliationMatchingEntry.Count() > 0 ? GetMatchingLedgerEntry(s.BankReconciliationMatchingEntry.Where(x => !x.BankStatementEntryID.HasValue).ToList()) : new List<MatchedLedgerEntries>(),
                                                                           }).ToList();

            bankReconcilationViewModel.BankReconciliationUnMatchedWithBankEntries = unMatchedWithBankEntries;

            bankReconcilationViewModel.BankReconciliationUnMatchedWithLedgerEntries = unMatchedLedgerEntriesList;
            var matchedLedgerEntries = bankReconcilationViewModel.BankReconciliationMatchedEntries
     .SelectMany(entry => entry.MatchedLedgerEntries)
     .ToList();
            bankReconcilationViewModel.BankReconciliationLedgerTrans = ledgerData.BankReconciliationLedgerTrans;
            bankReconcilationViewModel.BankReconciliationMatchingLedgerEntries = ledgerData.BankReconciliationLedgerTrans
                .Where(ledgerTrans => !matchedLedgerEntries.Any(matchedEntry =>
                    // matchedEntry.AccountID == ledgerTrans.AccountID &&
                    matchedEntry.Particulars.ToLower().Trim() == ledgerTrans.Narration.ToLower().Trim() &&
                    matchedEntry.Reference.ToLower().Trim() == ledgerTrans.Reference.ToLower().Trim()
                    && matchedEntry.LedgerDebitAmount == ledgerTrans.LedgerDebitAmount &&
                 matchedEntry.LedgerCreditAmount == ledgerTrans.LedgerCreditAmount
                ))
                .ToList();



            bankReconcilationViewModel.BankReconciliationUnMatchedWithLedgerEntries = unMatchedLedgerEntriesList;
          

            var bankReconciliationBankTransViewModels = FillBankStatementData(entry.BankStatementEntryDTOs);
            var matchedBankEntries = bankReconcilationViewModel.BankReconciliationMatchedEntries
            .SelectMany(entry => entry.MatchedBankEntries)
            .ToList();
            bankReconcilationViewModel.BankReconciliationBankTrans = bankReconciliationBankTransViewModels;
            bankReconcilationViewModel.BankReconciliationMatchingBankTransEntries = bankReconciliationBankTransViewModels
                .Where(bankTrans => !matchedBankEntries.Any(matchedEntry =>
                    matchedEntry.BankStatementEntryID == bankTrans.BankStatementEntryID
                    // && matchedEntry.BankDescription == bankTrans.Narration
                    ))
                .ToList();

            bankReconcilationViewModel.ContentFileID = entry.BankStatementData?.ContentFileID;
            bankReconcilationViewModel.ContentFileName = entry.BankStatementData?.ContentFileName;

            return bankReconcilationViewModel;
        }
        private List<MatchedBankEntries> GetMatchingBankEntry(List<BankReconciliationMatchingEntryDTO> matchingEntry)
        {
            var matchingEntryDTO = new List<MatchedBankEntries>();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            matchingEntryDTO = (from s in matchingEntry
                                select new MatchedBankEntries()
                                {
                                    ChequeNo = s.ChequeNo,
                                    PartyName = s.PartyName,
                                    Reference = s.Reference,
                                    BankStatementEntryID = s.BankStatementEntryID,
                                    ReferenceGroupNo = s.ReferenceGroupNO,
                                    ReferenceGroupName = s.ReferenceGroupName,
                                    BankDescription = s.Remarks,
                                    ChequeDate = s.ChequeDate.HasValue ? s.ChequeDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "-",
                                    PostDate = s.ReconciliationDate.HasValue ? s.ReconciliationDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "-",
                                    BankDebitAmount = s.Amount < 0 ? 0 : s.Amount,
                                    BankCreditAmount = s.Amount > 0 ? 0 : s.Amount,

                                }).ToList();
            return matchingEntryDTO;

        }
        private List<MatchedLedgerEntries> GetMatchingLedgerEntry(List<BankReconciliationMatchingEntryDTO> matchingEntry)
        {
            var matchingEntryDTO = new List<MatchedLedgerEntries>();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            matchingEntryDTO = (from s in matchingEntry
                                select new MatchedLedgerEntries()
                                {
                                    ChequeNo = s.ChequeNo,
                                    PartyName = s.PartyName,
                                    Reference = s.Reference,
                                    ReferenceGroupNo = s.ReferenceGroupNO,
                                    ReferenceGroupName = s.ReferenceGroupName,
                                    Particulars = s.Remarks,
                                    ChequeDate = s.ChequeDate.HasValue ? s.ChequeDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "-",
                                    TransDate = s.ReconciliationDate.HasValue ? s.ReconciliationDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "-",
                                    LedgerDebitAmount = s.Amount < 0 ? 0 : s.Amount,
                                    LedgerCreditAmount = s.Amount > 0 ? 0 : s.Amount
                                }).ToList();
            return matchingEntryDTO;

        }
        public List<BankReconciliationBankTransViewModel> FillBankStatementData(List<BankStatementEntryDTO> bankStatementEntryDTO)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var bankReconciliationBankTransViewModel = new List<BankReconciliationBankTransViewModel>();

            if (bankStatementEntryDTO.Any())
            {
                foreach (var x in bankStatementEntryDTO)
                {
                    bankReconciliationBankTransViewModel.Add(new BankReconciliationBankTransViewModel()
                    {
                        Narration = x.Description,
                        PostDate = x.PostDate.HasValue ? x.PostDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        BankCreditAmount = x.Credit,
                        BankDebitAmount = x.Debit,
                        Balance = x.Balance,
                        ChequeNo = x.ChequeNo,
                        ChequeDate = x.ChequeDate.HasValue ? x.ChequeDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        PartyName = x.PartyName,
                        Reference = x.ReferenceNo,
                        SlNO = x.SlNO,
                        BankStatementEntryID = x.BankStatementEntryIID

                    });
                }
            }
            return bankReconciliationBankTransViewModel;
        }

        private BankReconcilationViewModel FillLedgerData(BankReconciliationDTO transactionDTO)
        {
            var bankReconcilationViewModel = new BankReconcilationViewModel();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var entry = ClientFactory.AccountingServiceClient(CallContext).FillBankTransctions(transactionDTO);

            var bankReconciliationLedgerTrans = (from s in entry.BankReconciliationTransactionDtos
                                                 select new BankReconciliationLedgerTransViewModel
                                                 {
                                                     AccountID = s.AccountID,
                                                     AccountName = s.AccountName,
                                                     PostDate = s.TransDate.HasValue ? s.TransDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                                     LedgerCreditAmount = s.Credit,
                                                     LedgerDebitAmount = s.Debit,
                                                     Narration = s.Narration,
                                                     ChequeNo = s.ChequeNo,
                                                     PartyName = s.PartyName,
                                                     Reference = s.Reference,
                                                     ChequeDate = s.ChequeDate.HasValue ? s.ChequeDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                                     TranHeadID = s.TranHeadID,
                                                     TranTailID = s.TranTailID,
                                                 }).ToList();
            if (entry != null && entry.BankReconciliationTransactionDtos.Count() > 0)
            {
                bankReconcilationViewModel.LedgerOpeningBalance = entry.BankReconciliationTransactionDtos[0]?.OpeningBalance;
                bankReconcilationViewModel.LedgerClosingBalance = entry.BankReconciliationTransactionDtos[0]?.ClosingBalance;
            }
            bankReconcilationViewModel.BankAccountID = entry?.BankAccountID;
            bankReconcilationViewModel.BankName = entry?.BankName;
            bankReconcilationViewModel.BankReconciliationLedgerTrans = bankReconciliationLedgerTrans;

            return bankReconcilationViewModel;
        }

        public BankReconcilationViewModel GettingMatchedAndUnMatchedLedgerEntries(BankReconcilationViewModel bankReconcilationViewModel)
        {
            var unMatchedLedgerEntriesList = new List<BankReconciliationUnMatchedWithLedgerEntriesViewModel>();
            var matchedList = new List<BankReconciliationMatchedEntriesViewModel>();

            foreach (var bankTrans in bankReconcilationViewModel.BankReconciliationBankTrans)
            {
                bool matchFound = false;


                foreach (var ledgerTrans in bankReconcilationViewModel.BankReconciliationLedgerTrans)
                {
                    if (Math.Abs(Math.Abs(bankTrans.BankDebitAmount.Value) - Math.Abs(bankTrans.BankCreditAmount.Value)) == Math.Abs(Math.Abs(ledgerTrans.LedgerDebitAmount.Value) - Math.Abs(ledgerTrans.LedgerCreditAmount.Value))
                        && (
                       HasMatchingDigits(ledgerTrans, bankTrans) || HasCommonWords(bankTrans.Narration, ledgerTrans.Narration)))
                    //                  if (bankTrans.BankDebitAmount.HasValue
                    //&& ledgerTrans.LedgerDebitAmount.HasValue
                    //&& Math.Abs(double.Parse(bankTrans.BankDebitAmount.Value.ToString())) == 1678.24
                    //&& Math.Abs(double.Parse(ledgerTrans.LedgerCreditAmount.Value.ToString())) == 1678.24)
                    {
                        if (Math.Abs(Math.Abs(bankTrans.BankDebitAmount.Value) - Math.Abs(bankTrans.BankCreditAmount.Value)) == Math.Abs(Math.Abs(ledgerTrans.LedgerDebitAmount.Value) - Math.Abs(ledgerTrans.LedgerCreditAmount.Value)))
                        {
                            matchFound = true;
                        }

                        matchedList.Add(new BankReconciliationMatchedEntriesViewModel
                        {
                            BankDebitAmount = bankTrans.BankDebitAmount,
                            BankCreditAmount = bankTrans.BankCreditAmount,
                            Particulars = ledgerTrans.Narration,
                            BankDescription = bankTrans.Narration,
                            PostDate = bankTrans.PostDate,
                            TransDate = ledgerTrans.PostDate,
                            LedgerDebitAmount = ledgerTrans.LedgerDebitAmount,
                            LedgerCreditAmount = ledgerTrans.LedgerCreditAmount,
                            TranHeadID = ledgerTrans.TranHeadID,
                            TranTailID = ledgerTrans.TranTailID,
                            PartyName = ledgerTrans.PartyName,
                            Reference = ledgerTrans.Reference,
                            ChequeDate = ledgerTrans.ChequeDate,

                        });
                        matchFound = true;
                        break;
                    }
                }

                if (!matchFound)
                {
                    if (!string.IsNullOrEmpty(bankTrans.Narration) && !(bankTrans.BankCreditAmount == 0 && bankTrans.BankDebitAmount == 0))
                    {
                        //unMatchedLedgerEntriesList.Add(new BankReconciliationUnMatchedWithLedgerEntriesViewModel
                        //{
                        //    BankDebitAmount = bankTrans.BankDebitAmount,
                        //    BankCreditAmount = bankTrans.BankCreditAmount,

                        //    Particulars = bankTrans.Narration,
                        //    PostDate = bankTrans.PostDate,

                        //    LedgerDebitAmount = 0,
                        //    LedgerCreditAmount = 0,
                        //    ReconciliationDebitAmount = bankTrans.BankCreditAmount,
                        //    ReconciliationCreditAmount = bankTrans.BankDebitAmount,
                        //});
                    }
                }

            }
            // bankReconcilationViewModel.BankReconciliationUnMatchedWithBankEntries = GettingUnMatchedBankEntries(bankReconcilationViewModel);
            bankReconcilationViewModel.BankReconciliationMatchedEntries = matchedList;
            //bankReconcilationViewModel.BankReconciliationUnMatchedWithLedgerEntries = unMatchedLedgerEntriesList;

            return bankReconcilationViewModel;
        }
        public List<BankReconciliationUnMatchedWithBankEntriesViewModel> GettingUnMatchedBankEntries(BankReconcilationViewModel bankReconcilationViewModel)
        {
            var unMatchedBankEntriesList = new List<BankReconciliationUnMatchedWithBankEntriesViewModel>();
            foreach (var ledgerTrans in bankReconcilationViewModel.BankReconciliationLedgerTrans)
            {
                bool matchFound = false;


                foreach (var bankTrans in bankReconcilationViewModel.BankReconciliationBankTrans)
                {
                    if (Math.Abs(bankTrans.BankDebitAmount.Value) - Math.Abs(bankTrans.BankCreditAmount.Value) == Math.Abs(ledgerTrans.LedgerDebitAmount.Value) - Math.Abs(ledgerTrans.LedgerCreditAmount.Value) && (
                       HasMatchingDigits(ledgerTrans, bankTrans) || HasCommonWords(bankTrans.Narration, ledgerTrans.Narration)))
                    {
                        matchFound = true;
                        break;
                    }
                }

                if (!matchFound)
                {
                    if (!string.IsNullOrEmpty(ledgerTrans.Narration) && !(ledgerTrans.LedgerDebitAmount == 0 && ledgerTrans.LedgerCreditAmount == 0))
                    {
                        unMatchedBankEntriesList.Add(new BankReconciliationUnMatchedWithBankEntriesViewModel
                        {
                            BankDebitAmount = 0,
                            BankCreditAmount = 0,
                            Particulars = ledgerTrans.Narration,
                            PostDate = ledgerTrans.PostDate,
                            LedgerDebitAmount = ledgerTrans.LedgerDebitAmount,
                            LedgerCreditAmount = ledgerTrans.LedgerCreditAmount,
                            BankDescription = string.Empty,
                            ReconciliationDebitAmount = ledgerTrans.LedgerCreditAmount,
                            ReconciliationCreditAmount = ledgerTrans.LedgerDebitAmount,
                            TranHeadID = ledgerTrans.TranHeadID,
                            TranTailID = ledgerTrans.TranTailID,
                        });
                    }
                }
            }
            return unMatchedBankEntriesList;
        }

        public static bool HasCommonWords(string description1, string description2)
        {
            var check = false;
            if (description1 == "amount of interest on Overdraft" || description1 == "DEBIT INTEREST")
            {
                check = true;
            }
            HashSet<string> words1 = new HashSet<string>(description1
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Where(word => word.Length > 3)
                .Select(word => word.ToLower()));

            HashSet<string> words2 = new HashSet<string>(description2
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Where(word => word.Length > 3)
                .Select(word => word.ToLower()));


            return words1.Intersect(words2).Any();
        }
        bool HasMatchingDigits(BankReconciliationLedgerTransViewModel ledgerTrans, BankReconciliationBankTransViewModel bankTrans)
        {
            if (!string.IsNullOrEmpty(ledgerTrans.ChequeNo))
            {
                if (bankTrans.Narration.Contains(ledgerTrans.ChequeNo) || (!string.IsNullOrEmpty(bankTrans.ChequeNo) && bankTrans.ChequeNo.Contains(ledgerTrans.ChequeNo)))
                {
                    return true;
                }

            }
            else if (bankTrans.Narration.Contains(ledgerTrans.PartyName) || (!string.IsNullOrEmpty(bankTrans.PartyName) && bankTrans.PartyName.Contains(ledgerTrans.PartyName)))
            {
                return true;
            }
            return false;
        }



        [HttpPost]
        public async Task<ActionResult> UploadDocument()
        {
            bool isSavedSuccessfully = false;
            var imageInfoList = new List<ContentFileDTO>();

            try
            {
                var userID = CallContext.LoginID;

                foreach (var fileName in Request.Form.Files)
                {
                    var file = fileName;
                    //Save file content goes here
                    if (file != null)
                    {
                        var fileInfo = new ContentFileDTO();
                        fileInfo.ContentFileName = file.FileName;
                        fileInfo.ContentData = await ConvertToByte(file);
                        fileInfo.FilePath = "Content/ReadContentsByID";
                        imageInfoList.Add(fileInfo);
                    }
                }

                imageInfoList = ClientFactory.ContentServicesClient(CallContext).SaveFiles(imageInfoList);
                isSavedSuccessfully = true;
            }
            catch (Exception ex)
            {
                LogHelper<DocManagementController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { Success = isSavedSuccessfully, Message = (ex.Message) });
            }

            return Json(new { Success = isSavedSuccessfully, FileInfo = imageInfoList });
        }
        public static async Task<byte[]> ConvertToByte(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
        [HttpPost]
        public ActionResult DeleteUploadedDocument(string fileName)
        {
            bool isSuccessFullyDeleted = false;

            try
            {
                var userID = base.CallContext.LoginID;//Needs to fech from call context
                if (!string.IsNullOrEmpty(fileName))
                {
                    string tempFolderPath = string.Format("{0}\\{1}\\{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath").ToString(), userID, fileName);

                    if (System.IO.File.Exists(tempFolderPath))
                    {
                        System.IO.File.Delete(tempFolderPath);
                    }
                    isSuccessFullyDeleted = true;

                }
                else
                {
                    var file = string.Format("{0}\\{1}\\{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), userID);
                    if (System.IO.Directory.Exists(file))
                        System.IO.Directory.Delete(file, true);
                }
            }
            catch (Exception ex)
            {
                LogHelper<DocManagementController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { Success = isSuccessFullyDeleted, Message = (ex.Message) });
            }

            return Json(new { Success = isSuccessFullyDeleted });
        }
        [HttpPost]
        public JsonResult SaveUploadedFiles(List<UploadedFileDetailsViewModel> files)
        {
            var dtos = UploadedFileDetailsViewModel.ToDocumentVM(files,
                Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Others);

            foreach (var dto in dtos)
            {
                if (dto.ReferenceID.HasValue)
                {
                    var contentData = ClientFactory.ContentServicesClient(CallContext)
                        .ReadContentsById(dto.ReferenceID.Value);
                    dto.ContentData = contentData.ContentData;
                }
            }

            DocumentFileViewModel.FromDTO(ClientFactory.DocumentServiceClient(CallContext)
                .SaveDocuments(dtos));
            return Json(new { Success = true });
        }

        [HttpGet]
        public ActionResult Configure(long IID = 0)
        {
            return View(IID);
        }

        //public ActionResult GetDocument(long referenceID = 0, EntityTypes type = EntityTypes.Others)
        //{
        //    var document = DocumentFileViewModel.FromDTO(ClientFactory
        //        .DocumentServiceClient(CallContext).GetDocuments(referenceID, type));
        //    return Json(document.FirstOrDefault());
        //}
    }
}
