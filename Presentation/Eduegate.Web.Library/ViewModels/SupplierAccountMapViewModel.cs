using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Accounting = Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Client.Factory;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierAccount", "CRUDModel.ViewModel.SupplierAccountMaps")]
    [DisplayName("Accounting settings")]

    public class SupplierAccountMapViewModel : BaseMasterViewModel
    {
        public SupplierAccountMapViewModel(EntityTypes supplierCustomer)
        {
            SupplierAccountEntitlements = new List<SupplierAccountEntitlementViewModel>() { new SupplierAccountEntitlementViewModel() };
            SupplierCustomer = supplierCustomer;
        }

        //[ControlType(Framework.Enums.ControlTypes.Select2, "ng-change='AccountCodeChange($event, $element)'")]
        //[Select2("Accounts", "Numeric", false, "AccountCodeChange")]
        //[LazyLoad("", "LookUps.Accounts", "LookUps.Accounts")]
        //[DisplayName("Account")]
        public KeyValueViewModel SupplierAccount { get; set; }
        public long? SupplierAccountId { get; set; }

        [DataPicker("AccountEntry")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("Configured Account Name")]
        public string DefaultSupplierAccountName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Configured Account Code")]
        public string DefaultSupplierAccountAlias { get; set; }

        public long? RootSupplierAccountId { get; set; }

        //[DataPicker(Framework.Enums.SearchView.AccountEntry)]
        //[ControlType(Framework.Enums.ControlTypes.DataPicker)]
        //[DisplayName("Account Name")]
        public string RootSupplierAccountName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Account Alias")]
        public string RootSupplierAccountAlias { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Entitlement Accounts")]
        public List<SupplierAccountEntitlementViewModel> SupplierAccountEntitlements { get; set; }

        public long SupplierAccountMapIID { get; set; }
        public EntityTypes SupplierCustomer { get; set; }

        public KeyValueViewModel DefaultAccountData(string SettingCode)
        {
            long AccountId = GetGLAccountIDFromSettings(SettingCode);
            var account = ClientFactory.AccountingServiceClient(null, null).GetAccount(AccountId);
            return account != null ? new KeyValueViewModel() : new KeyValueViewModel() { Key = account.AccountID.ToString(), Value = account.AccountName };
        }

        public long GetGLAccountIDFromSettings(string SettingCode)
        {
            long GLAccountId = 0;
            var SettingDTO = ClientFactory.SettingServiceClient(null).GetSettingDetail(SettingCode);
            if (SettingDTO != null)
            {
                GLAccountId = Convert.ToInt64(SettingDTO.SettingValue);
            }
            return GLAccountId;
        }

        public static List<SupplierAccountEntitlmentMapsDTO> SaveSupplierAccountMap(SupplierAccountMapViewModel supplierAccountMapViewModel, long SupplierIID, string AccountName_Name, string AccountName_Code)
        {

            List<SupplierAccountEntitlmentMapsDTO> dtoList = new List<SupplierAccountEntitlmentMapsDTO>();
            foreach (SupplierAccountEntitlementViewModel viewModel in supplierAccountMapViewModel.SupplierAccountEntitlements)
            {
                SupplierAccountEntitlmentMapsDTO dto = new SupplierAccountEntitlmentMapsDTO();
                dto.EntitlementID = viewModel.EntitlementID;
                //dto.AccountID = viewModel.AccountID;
                dto.SupplierID = SupplierIID;
                dto.SupplierAccountMapIID = viewModel.SupplierAccountMapIID;

                AccountDTO accountDTO = new AccountDTO();
                accountDTO.AccountBehavior = (Infrastructure.Enums.AccountBehavior)Convert.ToInt32(viewModel.AccountBehavior);
                accountDTO.AccountGroup = new AccountGroupDTO() { AccountGroupID = viewModel.GroupID };
                accountDTO.ParentAccount = new AccountDTO { AccountID = viewModel.ParentAccountID };
                //accountDTO.AccountName = viewModel.AccountName;
                accountDTO.AccountName = string.IsNullOrEmpty(viewModel.AccountName) ? null : viewModel.AccountName.Replace("{Name}", AccountName_Name).Replace("{Code}", AccountName_Code);
                accountDTO.Alias = viewModel.Alias;
                accountDTO.AccountID = viewModel.AccountID;

                dto.Account = accountDTO;
                dto.SupplierCustomer = supplierAccountMapViewModel.SupplierCustomer;
                dtoList.Add(dto);
            }
            //Add the base account with NULL entity ID
            SupplierAccountEntitlmentMapsDTO baseEntitlementDto = new SupplierAccountEntitlmentMapsDTO();
            baseEntitlementDto.SupplierID = SupplierIID;
            baseEntitlementDto.SupplierAccountMapIID = supplierAccountMapViewModel.SupplierAccountMapIID;

            AccountDTO baseAccountDTO = new AccountDTO();
            baseAccountDTO.AccountID = supplierAccountMapViewModel.SupplierAccountId;
            if (baseAccountDTO.ParentAccount == null) baseAccountDTO.ParentAccount = new AccountDTO();

            if (supplierAccountMapViewModel.SupplierAccount != null)
            {
                baseAccountDTO.ParentAccount.AccountID = Convert.ToInt64(supplierAccountMapViewModel.SupplierAccount.Key);
            }

            baseAccountDTO.Alias = supplierAccountMapViewModel.RootSupplierAccountAlias;
            baseAccountDTO.AccountID = supplierAccountMapViewModel.RootSupplierAccountId;
            baseAccountDTO.AccountName = string.IsNullOrEmpty(supplierAccountMapViewModel.RootSupplierAccountName) ? null 
                : supplierAccountMapViewModel.RootSupplierAccountName.Replace("{Name}", AccountName_Name).Replace("{Code}", AccountName_Code);

            baseEntitlementDto.Account = baseAccountDTO;

            baseEntitlementDto.SupplierCustomer = supplierAccountMapViewModel.SupplierCustomer;

            dtoList.Add(baseEntitlementDto);

            //List<Accounting.SupplierAccountEntitlmentMapsDTO> DTOList = new List<Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO>();
            if (supplierAccountMapViewModel.SupplierCustomer == EntityTypes.Supplier)
            {
                dtoList = ClientFactory.SupplierServiceClient(null, null).SaveSupplierAccountMaps(dtoList);
            }
            else
            {
                dtoList = ClientFactory.CustomerServiceClient(null, null).SaveCustomerAccountMaps(dtoList);
            }



            return dtoList;
        }

        public static SupplierAccountMapViewModel SetAutoGeneratedAccounts(SupplierAccountMapViewModel supplierAccountMaps, string AccountCode, string AccountNamePrefix, string AccountName_Name, string AccountName_Code)
        {
            /*
             Hierachy--
                        Deafult Account configured in Settings table
                                -- Supplier/Customer root Account in Supplier/Customer Account Maps with entitlement ID is null
                                       --Entitlement Account 
                        E.g.
                            Alias Name
                            1000  Cash Account
                            10001 Sup1 Account
                            100011 Sup1 Entitlement1 Account 
                            100012 Sup1 Entitlement2 Account
             */

            if (supplierAccountMaps.SupplierAccountMapIID == 0)
            {
                supplierAccountMaps.SupplierAccount = new SupplierAccountMapViewModel(supplierAccountMaps.SupplierCustomer).DefaultAccountData(AccountCode);
                long baseAccountId = Convert.ToInt64(supplierAccountMaps.SupplierAccount.Key);
                if (baseAccountId != 0)
                {
                    string Entitlment = string.Join(",", supplierAccountMaps.SupplierAccountEntitlements.Select(p => p.EntitlementName));


                    var AccountsDTOList = ClientFactory.AccountingTransactionServiceClient(null).GetAutoGeneratedAccounts(baseAccountId, supplierAccountMaps.SupplierAccountEntitlements.Count, "Entity", Entitlment);
                    AccountDTO defaultAccount = AccountsDTOList.FirstOrDefault();
                    List<AccountDTO> ChildAccountList = new List<AccountDTO>();
                    if (defaultAccount != null)
                    {
                        if (defaultAccount != null)
                        {
                            int nStartChildID = 1;
                            string rootAccountAlias = defaultAccount.Alias.ToString() + "1";
                            string rootAccountName = AccountNamePrefix + " {Name} {Code} ";

                            for (int nLoop = 0; nLoop < supplierAccountMaps.SupplierAccountEntitlements.Count; nLoop++)
                            {
                                AccountDTO childAccount = new AccountDTO();
                                childAccount.AccountName = AccountNamePrefix + " {Name} {Code} " + supplierAccountMaps.SupplierAccountEntitlements[nLoop].EntitlementName;
                                childAccount.Alias = rootAccountAlias + nStartChildID;

                                childAccount.AccountBehavior = (Infrastructure.Enums.AccountBehavior)defaultAccount.AccountBehavior;
                                childAccount.AccountGroup = new AccountGroupDTO() { AccountGroupID = defaultAccount.AccountGroup.AccountGroupID };
                                //childAccount.ParentAccount = new Accounting.AccountDTO() { AccountID = defaultAccount.Account1 != null ? defaultAccount.Account1.AccountID : 0 };

                                ChildAccountList.Add(childAccount);
                                ++nStartChildID;
                            }
                            AccountDTO BaseAccount = new AccountDTO();
                            BaseAccount.AccountID = 0;
                            BaseAccount.ParentAccount = new AccountDTO() { AccountID = defaultAccount.AccountID };
                            //BaseAccount.ParentAccount.AccountID = defaultAccount.AccountID;
                            ChildAccountList.Add(BaseAccount);

                            supplierAccountMaps.RootSupplierAccountName = rootAccountName;
                            supplierAccountMaps.RootSupplierAccountAlias = rootAccountAlias;
                            supplierAccountMaps.SupplierAccountId = defaultAccount.AccountID;//Settings Account will be the Parent for cust/supp account

                            supplierAccountMaps.DefaultSupplierAccountName = defaultAccount.AccountName;
                            supplierAccountMaps.DefaultSupplierAccountAlias = defaultAccount.Alias;

                        }
                    }
                    if (ChildAccountList != null)
                    {
                        int nLoopIndex = 0;
                        foreach (var item in supplierAccountMaps.SupplierAccountEntitlements)
                        {
                            AccountDTO account = ChildAccountList[nLoopIndex];
                            item.AccountName = account.AccountName;
                            item.Alias = account.Alias;
                            //item.ParentAccountID = account.ParentAccount.AccountID;
                            item.GroupID = account.AccountGroup.AccountGroupID.Value;
                            item.AccountBehavior = ((int)account.AccountBehavior).ToString();

                            item.AccountID = 0;

                            nLoopIndex++;
                        }
                    }

                    //var AccountsDTOList = new AccountingTransactionServiceClient(null).GetAutoGeneratedAccounts(baseAccountId, supplierAccountMaps.SupplierAccountEntitlements.Count, "Entity", Entitlment);
                    //if (AccountsDTOList != null)
                    //{
                    //    int nLoopIndex = 0;
                    //    foreach (var item in supplierAccountMaps.SupplierAccountEntitlements)
                    //    {
                    //        Accounting.AccountDTO account = AccountsDTOList[nLoopIndex];
                    //        item.AccountName = account.AccountName;
                    //        item.Alias = account.Alias;
                    //        item.ParentAccountID = account.ParentAccount.AccountID;
                    //        item.GroupID = account.AccountGroup.AccountGroupID;
                    //        item.AccountBehavior = ((int)account.AccountBehavior).ToString();

                    //        item.AccountID = 0;

                    //        nLoopIndex++;
                    //    }
                    //}
                }
            }
            return supplierAccountMaps;
        }

        public static SupplierAccountMapViewModel
            SetSupplierAccountMapsViewModel(SupplierAccountMapDTO dto, EntityTypes SupplierCustomer)
        {
            SupplierAccountMapViewModel vm = new SupplierAccountMapViewModel(SupplierCustomer);
            vm.SupplierAccountEntitlements = new List<SupplierAccountEntitlementViewModel>();
            foreach (SupplierAccountEntitlmentMapsDTO item in dto.SupplierAccountEntitlements)
            {
                if (item.EntitlementID == null)
                {
                    //vm.SupplierAccount = new KeyValueViewModel() { Key = item.Account.AccountID.ToString(), Value = item.Account.AccountName };
                    // vm.SupplierAccountId = item.Account.AccountID;
                    vm.SupplierAccountMapIID = item.SupplierAccountMapIID;

                    if (item.Account != null)
                    {
                        vm.RootSupplierAccountName = item.Account.AccountName;
                        vm.RootSupplierAccountAlias = item.Account.Alias;
                        vm.RootSupplierAccountId = item.Account.AccountID;

                        var parentAccount = ClientFactory.AccountingServiceClient(null).GetAccount(item.Account.ParentAccount.AccountID.Value);

                        vm.DefaultSupplierAccountName = parentAccount.AccountName;
                        vm.DefaultSupplierAccountAlias = parentAccount.Alias;

                        vm.SupplierAccountId = item.Account.ParentAccount.AccountID;
                        vm.SupplierAccount = new KeyValueViewModel() { Key = item.Account.ParentAccount.AccountID.ToString(), Value = item.Account.ParentAccount.AccountName };
                    }
                }
                else
                {
                    SupplierAccountEntitlementViewModel viewModel = new SupplierAccountEntitlementViewModel();
                    viewModel.AccountID = item.Account.AccountID;
                    viewModel.AccountName = item.Account.AccountName;
                    viewModel.Alias = item.Account.Alias;
                    viewModel.EntitlementID = item.EntitlementID;
                    viewModel.EntitlementName = item.EntitlementName;
                    viewModel.GroupID = item.Account.AccountGroup.AccountGroupID;
                    viewModel.ParentAccountID = item.Account.ParentAccount != null ? item.Account.ParentAccount.AccountID : (long?)null;
                    viewModel.SupplierAccountMapIID = item.SupplierAccountMapIID;
                    viewModel.SupplierID = item.SupplierID;


                    vm.SupplierAccountEntitlements.Add(viewModel);
                }

            }
            return vm;
        }

        public static SupplierAccountMapViewModel MergeEntitlements(SupplierAccountMapViewModel supplierAccountMapViewModel)
        {
            List<KeyValueDTO> entitlements = new List<KeyValueDTO>();
            if (supplierAccountMapViewModel.SupplierCustomer == EntityTypes.Supplier)
            {
                entitlements = ClientFactory.MutualServiceClient(null).GetEntityTypeEntitlementByEntityType(EntityTypes.Supplier);
            }
            else
            {
                entitlements = ClientFactory.MutualServiceClient(null).GetEntityTypeEntitlementByEntityType(EntityTypes.Customer);
            }

            if (supplierAccountMapViewModel == null)
            {
                supplierAccountMapViewModel = new SupplierAccountMapViewModel(EntityTypes.Supplier);
            }
            if (supplierAccountMapViewModel.SupplierAccountEntitlements == null)
            {
                supplierAccountMapViewModel.SupplierAccountEntitlements = new List<SupplierAccountEntitlementViewModel>();
            }
            foreach (KeyValueDTO keyValue in entitlements)
            {
                byte entitlementId = Convert.ToByte(keyValue.Key);
                var item = supplierAccountMapViewModel.SupplierAccountEntitlements.Where(b => b.EntitlementID == entitlementId).FirstOrDefault();
                if (item == null)
                {
                    supplierAccountMapViewModel.SupplierAccountEntitlements.Add(new SupplierAccountEntitlementViewModel
                    { EntitlementID = entitlementId, EntitlementName = keyValue.Value });
                }
            }
            return supplierAccountMapViewModel;
        }
    }
}
