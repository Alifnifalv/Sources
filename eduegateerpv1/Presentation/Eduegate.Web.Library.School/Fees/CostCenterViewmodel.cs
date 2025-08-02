using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Fees
{
    public class CostCenterViewmodel : BaseMasterViewModel
    {
        public CostCenterViewmodel()
        {
            IsAffect_A = false;
            IsAffect_E = false;
            IsAffect_L = false;
            IsAffect_I = false;
            IsAffect_C = false;
            IsFixed = false;
            IsActive = true;
            IncomeAccounts = new List<KeyValueViewModel>();
            AssetAccounts = new List<KeyValueViewModel>();
            EquityAccounts = new List<KeyValueViewModel>();
            ExpensesAccounts = new List<KeyValueViewModel>();
            LiabilitiesAccounts = new List<KeyValueViewModel>();            
        }
        // [Required]
        // [ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Cost Center ID")]
        public int CostCenterID { get; set; }

        public long CostCenterAccountMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [CustomDisplay("CostCenterCode")]
        public string CostCenterCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("CostCenterName")]
        public string CostCenterName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsFixedCost")]
        public bool? IsFixed { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsAffect_Assets")]
        public bool? IsAffect_A { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Asset Accounts")]
        [Select2("AssetAccounts", "Numeric", true, optionalAttribute1: "ng-disabled=!CRUDModel.ViewModel.IsAffect_A")]
        [LookUp("LookUps.AssetAccounts")]
        public List<KeyValueViewModel> AssetAccounts { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsAffect_Expenses")]
        public bool? IsAffect_E { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Expenses Accounts")]
        [Select2("ExpensesAccounts", "Numeric", true, optionalAttribute1: "ng-disabled=!CRUDModel.ViewModel.IsAffect_E")]
        [LookUp("LookUps.ExpensesAccounts")]
        public List<KeyValueViewModel> ExpensesAccounts { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsAffect_Liabilities")]
        public bool? IsAffect_L { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Liabilities Accounts")]
        [Select2("LiabilitiesAccounts", "Numeric", true, optionalAttribute1: "ng-disabled=!CRUDModel.ViewModel.IsAffect_L")]
        [LookUp("LookUps.LiabilitiesAccounts")]
        public List<KeyValueViewModel> LiabilitiesAccounts { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsAffect_Income")]
        public bool? IsAffect_I { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Income Accounts")]
        [Select2("IncomeAccounts", "Numeric", true, optionalAttribute1: "ng-disabled=!CRUDModel.ViewModel.IsAffect_I")]
        [LookUp("LookUps.IncomeAccounts")]
        public List<KeyValueViewModel> IncomeAccounts { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsAffect_Equity")]
        public bool? IsAffect_C { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Equity Accounts")]
        [Select2("EquityAccounts", "Numeric", true, optionalAttribute1: "ng-disabled=!CRUDModel.ViewModel.IsAffect_C")]
        [LookUp("LookUps.EquityAccounts")]
        public List<KeyValueViewModel> EquityAccounts { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine8 { get; set; }

       

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as CostCenterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CostCenterViewmodel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<CostCenterDTO, CostCenterViewmodel>.CreateMap();
          
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var stDtO = dto as CostCenterDTO;
            var vm = Mapper<CostCenterDTO, CostCenterViewmodel>.Map(stDtO);

            vm.CostCenterID = stDtO.CostCenterID;
            vm.IsFixed = stDtO.IsFixed;
            vm.IsAffect_A = stDtO.IsAffect_A;
            vm.IsAffect_C = stDtO.IsAffect_C;
            vm.IsAffect_E = stDtO.IsAffect_E;
            vm.IsAffect_I = stDtO.IsAffect_I;
            vm.IsAffect_L = stDtO.IsAffect_L;
            vm.AssetAccounts = new List<KeyValueViewModel>();
            vm.EquityAccounts = new List<KeyValueViewModel>();
            vm.ExpensesAccounts = new List<KeyValueViewModel>();
            vm.IncomeAccounts = new List<KeyValueViewModel>();
            vm.LiabilitiesAccounts = new List<KeyValueViewModel>();
            foreach (var cst in stDtO.CostCenterAccountMap)
            {
                if (cst.IsAffect_A == true)
                {
                    vm.AssetAccounts.Add(new KeyValueViewModel()
                    {
                        Key = cst.AccountID.ToString(),
                        Value=cst.AccountName
                    }) ;
                }
                if (cst.IsAffect_C== true)
                {
                    vm.EquityAccounts.Add(new KeyValueViewModel()
                    {
                        Key = cst.AccountID.ToString(),
                        Value = cst.AccountName
                    });
                }
                if (cst.IsAffect_L == true)
                {
                    vm.LiabilitiesAccounts.Add(new KeyValueViewModel()
                    {
                        Key = cst.AccountID.ToString(),
                        Value = cst.AccountName
                    });
                }
                if (cst.IsAffect_E == true)
                {
                    vm.ExpensesAccounts.Add(new KeyValueViewModel()
                    {
                        Key = cst.AccountID.ToString(),
                        Value = cst.AccountName
                    });
                }
                if (cst.IsAffect_I == true)
                {
                    vm.IncomeAccounts.Add(new KeyValueViewModel()
                    {
                        Key = cst.AccountID.ToString(),
                        Value = cst.AccountName
                    });
                }
            }
            return vm;
           
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<CostCenterViewmodel, CostCenterDTO>.CreateMap();
            var dto = Mapper<CostCenterViewmodel, CostCenterDTO>.Map(this);

            dto.CostCenterAccountMap = new List<CostCenterAccountMapDTO>();
            dto.CostCenterID = this.CostCenterID;
            dto.IsFixed = this.IsFixed;
            if (this.IsAffect_A == true)
            {
                foreach (var asset in this.AssetAccounts)
                {
                    dto.CostCenterAccountMap.Add(new CostCenterAccountMapDTO()
                    {
                        CostCenterAccountMapIID = this.CostCenterAccountMapID,
                        IsAffect_A = this.IsAffect_A,
                        AccountID = long.Parse(asset.Key),
                    });
                }
            }
            if (this.IsAffect_E == true)
            {
                foreach (var expense in this.ExpensesAccounts)
                {
                    dto.CostCenterAccountMap.Add(new CostCenterAccountMapDTO()
                    {
                        CostCenterAccountMapIID = this.CostCenterAccountMapID,
                        IsAffect_E = this.IsAffect_E,
                        AccountID = long.Parse(expense.Key),
                    });
                }
            }
            if (this.IsAffect_I == true)
            {
                foreach (var income in this.IncomeAccounts)
                {
                    dto.CostCenterAccountMap.Add(new CostCenterAccountMapDTO()
                    {
                        CostCenterAccountMapIID = this.CostCenterAccountMapID,
                        IsAffect_I = this.IsAffect_I,
                        AccountID = long.Parse(income.Key),
                    });
                }
            }
            if (this.IsAffect_L == true)
            {
                foreach (var liab in this.LiabilitiesAccounts)
                {
                    dto.CostCenterAccountMap.Add(new CostCenterAccountMapDTO()
                    {
                        CostCenterAccountMapIID = this.CostCenterAccountMapID,
                        IsAffect_L = this.IsAffect_L,
                        AccountID = long.Parse(liab.Key),
                    });
                }
            }
            if (this.IsAffect_C == true)
            {
                foreach (var equa in this.EquityAccounts)
                {
                    dto.CostCenterAccountMap.Add(new CostCenterAccountMapDTO()
                    {
                        CostCenterAccountMapIID = this.CostCenterAccountMapID,
                        IsAffect_C = this.IsAffect_C,
                        AccountID = long.Parse(equa.Key),
                    });
                }
            }
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<CostCenterDTO>(jsonString);
        }
    }
}
