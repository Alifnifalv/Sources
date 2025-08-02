using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Framework.Extensions;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    public class ServiceProviderViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [DisplayName("Service Provider ID")]
        public int ServiceProviderID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Code")]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        public string ProviderCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Provider Name")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string ProviderName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Country")]
        [LookUp("LookUps.Country1")]
        public string Country { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("IsActive")]
        public Nullable<bool> IsActive { get; set; }

        public static ServiceProviderViewModel FromDTO(ServiceProviderDTO dto)
        {
            ServiceProviderViewModel spViewModel = new ServiceProviderViewModel();

            if (dto.IsNotNull())
            {
                spViewModel.ServiceProviderID = dto.ServiceProviderID;
                spViewModel.ProviderCode = dto.ProviderCode;
                spViewModel.ProviderName = dto.ProviderName;
                spViewModel.Country = dto.CountryID.ToString();
                spViewModel.IsActive = dto.IsActive;
                spViewModel.CreatedBy = dto.CreatedBy;
                spViewModel.CreatedDate = dto.CreatedDate;
                spViewModel.UpdatedBy = dto.UpdatedBy;
                spViewModel.UpdatedDate = dto.UpdatedDate;
                spViewModel.TimeStamps = dto.TimeStamps;
            }

            return spViewModel;
        }

        public static ServiceProviderDTO ToDTO(ServiceProviderViewModel vm)
        {
            ServiceProviderDTO spDTO = new ServiceProviderDTO();

            if (vm.IsNotNull())
            {
                spDTO.ServiceProviderID = vm.ServiceProviderID;
                spDTO.ProviderCode = vm.ProviderCode;
                spDTO.ProviderName = vm.ProviderName;
                spDTO.CountryID = !string.IsNullOrEmpty(vm.Country) ? Convert.ToInt32(vm.Country) : (int?)null;
                spDTO.IsActive = vm.IsActive;
                spDTO.CreatedBy = vm.CreatedBy;
                spDTO.CreatedDate = vm.CreatedDate;
                spDTO.UpdatedBy = vm.UpdatedBy;
                spDTO.UpdatedDate = vm.UpdatedDate;
                spDTO.TimeStamps = vm.TimeStamps;
            }

            return spDTO;
        }

        public static ServiceProviderViewModel FromSPViewModelToVM(ServiceProviderViewModel vm){

            if(vm.IsNotNull()){

                var spVM = new ServiceProviderViewModel()
                {
                    ProviderCode = vm.ProviderCode,
                    ProviderName = vm.ProviderName,
                    Country = vm.Country,
                    IsActive = vm.IsActive,
                    CreatedBy = vm.CreatedBy,
                    CreatedDate = vm.CreatedDate,
                    UpdatedBy = vm.UpdatedBy,
                    UpdatedDate = vm.UpdatedDate,
                    TimeStamps = vm.TimeStamps,
                };

                return spVM;
            }
            else{
                return new ServiceProviderViewModel();
            }
        }

    }
}
