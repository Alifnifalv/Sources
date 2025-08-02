using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Inventory;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class UpdateInventoryViewModel : BaseMasterViewModel
    {
        public long ProductSKUMapID { get; set; }
        public long? Batch { get; set; }
        public long? BranchID { get; set; }
        public decimal? Quantity { get; set; }
        public long? HeadID { get; set; }
        public decimal? Fraction { get; set; }
        public decimal? CostPrice { get; set; }

        public static List<ProductInventoryDTO> ToDTO(List<UpdateInventoryViewModel> vms, CallContext context)
        {
            var dtos = new List<ProductInventoryDTO>();

            foreach(var vm in vms)
            {
                dtos.Add(ToDTO(vm, context));
            }

            return dtos;
        }

        public static ProductInventoryDTO ToDTO(UpdateInventoryViewModel vm, CallContext context)
        {
            return new ProductInventoryDTO()
            {
                Batch = vm.Batch,
                HeadID = vm.HeadID,
                BranchID = vm.BranchID,
                ProductSKUMapID = vm.ProductSKUMapID,
                Quantity = vm.Quantity,
                CostPrice = vm.CostPrice,
                CompanyID = context.CompanyID,
                Fraction = vm.Fraction,
            };
        }

        public static UpdateInventoryViewModel ToVM(ProductInventoryDTO dto)
        {
            return new UpdateInventoryViewModel()
            {
                Batch = dto.Batch,
                HeadID = dto.HeadID,
                BranchID = dto.BranchID,
                ProductSKUMapID = dto.ProductSKUMapID,
                Quantity = dto.Quantity,
                CostPrice = dto.CostPrice,
                Fraction = dto.Fraction,
            };
        }

        public static List<UpdateInventoryViewModel> ToVM(List<ProductInventoryDTO> dtos)
        {
            var vms = new List<UpdateInventoryViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(ToVM(dto));
            }

            return vms;
        }
    }
}
