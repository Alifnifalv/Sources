using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Inventory;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class StockVerificationViewModel : BaseMasterViewModel
    {
        public StockVerificationViewModel()
        {
            MasterViewModel = new StockVerificationMasterViewModel();
            DetailViewModel = new List<StockVerificationDetailViewModel>() { new StockVerificationDetailViewModel() };
        }

        public StockVerificationMasterViewModel MasterViewModel { get; set; }
        public List<StockVerificationDetailViewModel> DetailViewModel { get; set; }

        public static StockVerificationViewModel FromDTOtoVM(StockVerificationDTO dto)
        {
            var vm = new StockVerificationViewModel();
            vm.DetailViewModel = new List<StockVerificationDetailViewModel>();

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (dto != null)
            {
                vm.MasterViewModel.EmployeeID = dto.EmployeeID;
                vm.MasterViewModel.HeadIID = dto.HeadIID;
                vm.MasterViewModel.TransactionDate = dto.TransactionDate.HasValue ? dto.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                vm.MasterViewModel.BranchID = dto.BranchID;
                vm.MasterViewModel.TransactionNo = dto.TransactionNo;
                vm.MasterViewModel.CurrentStatusID = dto.CurrentStatusID;
                vm.MasterViewModel.Branch = new KeyValueViewModel { Key = dto.BranchID.ToString(), Value = dto.Branch.Value };
                vm.MasterViewModel.DocumentStatus = new KeyValueViewModel { Key = dto.TransactionStatusID.ToString(), Value = dto.TransactionStatus.Value };
                vm.MasterViewModel.Employee = new KeyValueViewModel { Key = dto.Employee.Key, Value = dto.Employee.Value };
                vm.MasterViewModel.PostedComments = dto.PostedComments;
                if (dto.StockVerificationMap.Count > 0)
                {
                    foreach(var detailDat in dto.StockVerificationMap)
                    {
                        vm.DetailViewModel.Add(new StockVerificationDetailViewModel()
                        {
                            HeadID = detailDat.HeadID,
                            DetailIID = detailDat.DetailIID,
                            ProductID = detailDat.ProductID,
                            SKUID = new KeyValueViewModel { Key = detailDat.ProductSKUMapID.ToString(), Value = detailDat.ProductSKU.Value },
                            ProductSKU = new KeyValueViewModel { Key = detailDat.ProductSKUMapID.ToString(), Value = detailDat.ProductSKU.Value },
                            Description = detailDat.Description,
                            AvailableQuantity = detailDat.AvailableQuantity,
                            PhysicalQuantity = detailDat.PhysicalQuantity,
                            Remark = detailDat.Remark,
                    });
                    }
                }
            }
            return vm;
        }


        public static StockVerificationDTO FromVmToDTO(StockVerificationViewModel vm)
        {

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var mainDTO = new StockVerificationDTO();
            mainDTO.HeadIID = vm.MasterViewModel.HeadIID;
            mainDTO.TransactionNo = vm.MasterViewModel.TransactionNo;
            mainDTO.CurrentStatusID = vm.MasterViewModel.CurrentStatusID;
            mainDTO.BranchID = vm.MasterViewModel.Branch != null && !string.IsNullOrEmpty(vm.MasterViewModel.Branch.Key) ? long.Parse(vm.MasterViewModel.Branch.Key) : (long?)null;
            mainDTO.EmployeeID = vm.MasterViewModel.Employee != null && !string.IsNullOrEmpty(vm.MasterViewModel.Employee.Key) ? long.Parse(vm.MasterViewModel.Employee.Key) : (long?)null;
            mainDTO.TransactionStatusID = vm.MasterViewModel.DocumentStatus != null && !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? byte.Parse(vm.MasterViewModel.DocumentStatus.Key) : (byte?)null;
            mainDTO.TransactionDate = string.IsNullOrEmpty(vm.MasterViewModel.TransactionDate) ? (DateTime?)null : DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture);
            mainDTO.PostedComments = vm.MasterViewModel.PostedComments;
            
            mainDTO.Branch = new KeyValueDTO()
                {
                    Value = vm.MasterViewModel.Branch.Value,
                    Key = vm.MasterViewModel.Branch.Key.ToString()
                };

            mainDTO.Employee = new KeyValueDTO()
            {
                Value = vm.MasterViewModel.Employee.Value,
                Key = vm.MasterViewModel.Employee.Key.ToString()
            };

            mainDTO.TransactionStatus = new KeyValueDTO()
            {
                Value = vm.MasterViewModel.DocumentStatus.Value,
                Key = vm.MasterViewModel.DocumentStatus.Key.ToString()
            };

            mainDTO.StockVerificationMap = new List<StockVerificationMapDTO>();

            foreach (var mapData in vm.DetailViewModel)
            {
                if (mapData.SKUID != null && mapData.SKUID.Key != null)
                {
                    if (!mainDTO.StockVerificationMap.Any(x => x.ProductSKUMapID == long.Parse(mapData.SKUID.Key)))
                    {
                        mainDTO.StockVerificationMap.Add(new StockVerificationMapDTO()
                        {
                            HeadID = mapData.HeadID,
                            DetailIID = mapData.DetailIID,
                            ProductID = mapData.ProductID,
                            ProductSKUMapID = long.Parse(mapData.SKUID.Key),
                            ProductSKU = new KeyValueDTO()
                            {
                                Value = mapData.SKUID.Value,
                                Key = mapData.SKUID.Key.ToString()
                            },
                            Description = mapData.Description,
                            PhysicalQuantity = mapData.PhysicalQuantity,
                            Remark = mapData.Remark,
                            AvailableQuantity = mapData.AvailableQuantity,
                        });
                    }
                }
            }

            return mainDTO;
        }
    }
}
