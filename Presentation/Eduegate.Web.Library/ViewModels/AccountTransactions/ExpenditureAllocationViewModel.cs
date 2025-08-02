using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounts;
using Eduegate.Services.Contracts.Accounts.Accounting;
using Eduegate.Services.Contracts.Accounts.MonthlyClosing;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.School.Fees;
using Eduegate.Web.Library.ViewModels.AccountTransactions.MonthlyClosing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ExpenditureAllocation", "CRUDModel.ViewModel")]
    [DisplayName("Expenditure Allocation")]
    public class ExpenditureAllocationViewModel : BaseMasterViewModel
    {
        public ExpenditureAllocationViewModel()
        {
            ExpenditureAllocTransactions = new List<ExpenditureAllocationTransactionsViewModel>() { new ExpenditureAllocationTransactionsViewModel() };
            //ExpenditureAllocTransactionAlloc = new List<ExpenditureAllocationTransactionsAllocViewModel>() { new ExpenditureAllocationTransactionsAllocViewModel() };
        }
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("CostCenter", "Numeric", false)]
        [CustomDisplay("CostCenter")]
        [LookUp("LookUps.CostCenterDetails")]
        public KeyValueViewModel CostCenter { get; set; }
        public int? CostCenterID { get; set; }

        [DataPicker("AccountTransactionSearch", invoker: "CRUDController", runtimeFilter: "'CostCenterID='+CRUDModel.ViewModel.CostCenter.Key")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker, "onecol-header-left", attribs: "ng-disabled=CRUDModel.ViewModel.CostCenter.Key")]
        [CustomDisplay("Transaction Number")]

        public string ReferenceTransactionNo { get; set; }
        public string ReferenceTransactionHeaderID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Account Transactions Details")]
        public List<ExpenditureAllocationTransactionsViewModel> ExpenditureAllocTransactions { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[CustomDisplay("Expenditure Allocations")]
        //public List<ExpenditureAllocationTransactionsAllocViewModel> ExpenditureAllocTransactionAlloc { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TransactionAllocationHeadDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ExpenditureAllocationViewModel>(jsonString);
        }
        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<TransactionAllocationHeadDTO>(jsonString);
        }
        public override BaseMasterDTO ToDTO()
        {
            Mapper<TransactionAllocationHeadDTO, ExpenditureAllocationViewModel>.CreateMap();
            Mapper<TransactionAllocationDetailDTO, ExpenditureAllocationTransactionsAllocViewModel>.CreateMap();
            var dto = Mapper<ExpenditureAllocationViewModel, TransactionAllocationHeadDTO>.Map(this);

            dto.TransactionAllocationDetailDTO = new List<TransactionAllocationDetailDTO>();
            foreach (var accdat in this.ExpenditureAllocTransactions)
            {
                var accountTransactionID = accdat.AccountTransactionHeadID;
               
                foreach (var dat in accdat.ExpenditureAllocTransactionAlloc)
                {
                    if (dat.Account != null && dat.Account.Key != null)
                    {
                        dto.TransactionAllocationDetailDTO.Add(new TransactionAllocationDetailDTO()
                        {
                            AccountTransactionHeadID = accountTransactionID,
                            AccountID = long.Parse(dat.Account.Key),
                            Remarks = dat.Remarks,
                            Amount = dat.DebitAmount.HasValue && dat.DebitAmount != 0 ? dat.DebitAmount : -1 * dat.CreditAmount,
                            BudgetID=accdat.BudgetID,                            
                            //DepartmentID = accdat.DepartmentID,
                            //Ref_SlNo = accdat.Ref_SlNo,
                            //SL_AccountID = accdat.SL_AccountID,
                            //Ref_SlNo = accdat.Ref_SlNo,

                            //GL_AccountID = accdat.GL_AccountID,

                            CostCenterID = this.CostCenter != null && !string.IsNullOrEmpty(this.CostCenter.Key) && int.Parse(this.CostCenter.Key) != 0 ? int.Parse(this.CostCenter.Key) : null
                        });
                    }
                }
            }
            return dto;
        }
        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            //Mapper<ExpenditureAllocationViewModel, TransactionAllocationHeadDTO>.CreateMap();
            //Mapper<ExpenditureAllocation, TransactionAllocationHeadDTO>.CreateMap();
            //var dto = Mapper<ExpenditureAllocationViewModel, TransactionAllocationHeadDTO>.Map(this);
            var expdto = dto as TransactionAllocationHeadDTO;

            var vm = new ExpenditureAllocationViewModel();
            var costCenter = expdto.TransactionAllocationDetailDTO.Select(x => x.CostCenter).FirstOrDefault();
            if (costCenter != null)
            {
                vm.CostCenterID = int.Parse(costCenter.Key);
                vm.CostCenter = new KeyValueViewModel() { Key = costCenter.Key, Value = costCenter.Value };
            }
            //vm.ExpenditureAllocTransactionAlloc = new List<ExpenditureAllocationTransactionsAllocViewModel>();
            //foreach (var dat in expdto.TransactionAllocationDetailDTO)
            //{
            //    if (dat.Account != null && dat.Account.Key != null)
            //    {
            //        vm.ExpenditureAllocTransactionAlloc.Add(new ExpenditureAllocationTransactionsAllocViewModel()
            //        {
            //            AccountTransactionHeadID = dat.AccountTransactionHeadID,
            //            AccountTransactionDetailIID = dat.TransactionAllocationDetailIID,
            //            DebitAmount = dat.Amount > 0 ? dat.Amount : 0,
            //            CreditAmount = dat.Amount < 0 ? -1 * dat.Amount : 0,
            //            AccountID = long.Parse(dat.Account.Key),
            //            Account = dat.Account != null && dat.Account.Key != null ? new KeyValueViewModel() { Key = dat.Account.Key, Value = dat.Account.Value } : null,
            //            Remarks = dat.Remarks,
            //            Amount = dat.Amount,
                       

            //        });

            //    }
            //}
            return vm;
        }
    }
}
