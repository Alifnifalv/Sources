using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    public class AccountAllocationViewModel : BaseMasterViewModel
    {
        public List<AccountAllocationDetailViewModel> DetailViewModel { get; set; }
        public AccountAllocationMasterViewModel MasterViewModel { get; set; }

        public AccountAllocationViewModel()
        {
            DetailViewModel = new List<AccountAllocationDetailViewModel>() { new AccountAllocationDetailViewModel() };
            MasterViewModel = new AccountAllocationMasterViewModel();
        }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AccountAllocationDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AccountAllocationViewModel>(jsonString);
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AccountAllocationDTO>(jsonString);
        }

        public override BaseMasterDTO ToDTO()
        {
            var dto = new AccountAllocationDTO()
            {
                AllocationDetails = new List<AccountAllocationDetailDTO>(),
            };

            dto.AllocationDetails.Add(new AccountAllocationDetailDTO()
            {

            });
            return dto;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            var allocationDTO = dto as AccountAllocationDTO;
            var vm = new AccountAllocationViewModel();
            return vm;
        }
    }
}
