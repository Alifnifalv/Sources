using Eduegate.Services.Contracts.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class TaxDetailsViewModel : BaseMasterViewModel
    {
        //TODO: Need to check if this is required
        //[IgnoreMap]
        public List<TaxViewModel> Taxes { get; set; }

        public TaxDetailsViewModel()
        {
            Taxes = new List<TaxViewModel>();
        }

        public static TaxDetailsViewModel ToVM(List<TaxDetailsDTO> dtos, List<TaxViewModel> existingVM)
        {
            var taxVM = new TaxDetailsViewModel()
            {
                Taxes = new List<TaxViewModel>()
            };

            foreach (var dto in dtos)
            {
                var exist = existingVM == null ? null : existingVM.FirstOrDefault(a => a.TaxTypeID == dto.TaxTypeID);

                if(exist == null)
                {
                    taxVM.Taxes.Add(ToVM(dto));                    
                }
                else
                {
                    taxVM.Taxes.Add(exist);
                }                
            }

            return taxVM;
        }

        public static TaxViewModel ToVM(TaxDetailsDTO dto)
        {
            return new TaxViewModel()
            {
                TaxTypeID = dto.TaxTypeID,
                TaxAmount = dto.Amount,
                TaxPercentage = dto.Percentage,
                TaxTemplateID = dto.TaxTemplateID,
                TaxTemplateItemID = dto.TaxTemplateItemID,
                TaxName = dto.TaxName,
                TaxID = dto.TaxID,
                IsFixedPercentage = dto.Amount.HasValue || dto.Percentage.HasValue ? true : false,
                HasTaxInclusive = dto.HasTaxInclusive,
                InclusiveTaxAmount = dto.InclusiveTaxAmount,
                ExclusiveTaxAmount = dto.ExclusiveTaxAmount
            };
        }

        public static TaxDetailsDTO ToDTO(TaxViewModel vm)
        {
            return new TaxDetailsDTO()
            {
                TaxTypeID = vm.TaxTypeID,
                Amount = vm.TaxAmount,
                Percentage = vm.TaxPercentage,
                TaxTemplateID = vm.TaxTemplateID,
                TaxTemplateItemID = vm.TaxTemplateItemID,
                TaxName = vm.TaxName,
                TaxID = vm.TaxID,
                HasTaxInclusive = vm.HasTaxInclusive,
                InclusiveTaxAmount = vm.InclusiveTaxAmount,
                ExclusiveTaxAmount = vm.ExclusiveTaxAmount
            };
        }

        public static List<TaxDetailsDTO> ToDTO(List<TaxViewModel> vms)
        {
            var taxes = new List<TaxDetailsDTO>();

            foreach (var vm in vms)
            {
                taxes.Add(ToDTO(vm));
            }

            return taxes;
        }

    }
}