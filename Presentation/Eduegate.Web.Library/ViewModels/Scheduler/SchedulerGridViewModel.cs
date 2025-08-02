using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Enums.Schedulers;
using Eduegate.Services.Contracts.Schedulers;

namespace Eduegate.Web.Library.ViewModels.Scheduler
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Scheduling", "CRUDModel.ViewModel.SchedulerInfo.Schedulers")]
    [DisplayName("")]
    public class SchedulerGridViewModel :BaseMasterViewModel
    {
        public SchedulerGridViewModel()
        {
            SchedulerEntityType = new KeyValueViewModel();
            EntityValue = new KeyValueViewModel();
        }

        public long EntitySchedulerIID { get; set; }
        public SchedulerTypes SchedulerType { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [DisplayName("Types")]
        [Select2("Account", "String", false, "OnSchedulerTypeChangeSelect2($select,$index, gridModel)")]
        [LookUp("LookUps.SchedulerType")]
        public KeyValueViewModel SchedulerEntityType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [LookUp("LookUps.EntityType")]
        [Select2("EntityType", "String", false)]
        [DisplayName("Entity")]
        public KeyValueViewModel EntityValue { get; set; }
        public string EntityID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='InsertGridRow($index, ModelStructure.SchedulerInfo.Schedulers[0], CRUDModel.ViewModel.SchedulerInfo.Schedulers)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='RemoveGridRow($index, ModelStructure.SchedulerInfo.Schedulers[0], CRUDModel.ViewModel.SchedulerInfo.Schedulers)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        public static List<SchedulerGridViewModel> FromDTO(List<SchedulerDTO> dtos)
        {
            var vm = new List<SchedulerGridViewModel>();

            if (dtos != null && dtos.Count > 0)
            {
                foreach (var dto in dtos)
                {
                    vm.Add(new SchedulerGridViewModel()
                    {
                        //EntitySchedulerIID = dto.EntitySchedulerIID,
                        SchedulerType = dto.SchedulerType,
                        SchedulerEntityType = new KeyValueViewModel() { Key = ((int)dto.SchedulerEntityType).ToString(), Value = dto.SchedulerEntityType.ToString() },
                        EntityValue = new KeyValueViewModel() { Key = dto.EntityValue, Value = dto.EntityValue },
                        EntityID = dto.EntityID,
                        TimeStamps = dto.TimeStamps,
                        CreatedBy = dto.CreatedBy,
                        CreatedDate = dto.CreatedDate,
                        UpdatedBy = dto.UpdatedBy,
                        UpdatedDate = dto.UpdatedDate,
                    });
                }
            }

            else
            {
                vm.Add(new SchedulerGridViewModel());
            }

            return vm;
        }

        public static List<SchedulerDTO> ToDTO(List<SchedulerGridViewModel> vms, SchedulerTypes type, string entityID)
        {
            var dto = new List<SchedulerDTO>();

            if (vms != null && vms.Count > 0)
            {
                foreach (var vm in vms)
                {
                    if (vm != null && !string.IsNullOrEmpty(vm.SchedulerEntityType.Key))
                    {
                        dto.Add(new SchedulerDTO()
                        {
                            EntitySchedulerIID = vm.EntitySchedulerIID,
                            SchedulerType = type,
                            SchedulerEntityType = (SchedulerEntityTypes)Enum.Parse(typeof(SchedulerEntityTypes), vm.SchedulerEntityType.Key),
                            EntityValue = vm.EntityValue.Key,
                            EntityID = entityID,
                            TimeStamps = vm.TimeStamps,
                            CreatedBy = vm.CreatedBy,
                            CreatedDate = vm.CreatedDate,
                            UpdatedBy = vm.UpdatedBy,
                            UpdatedDate = vm.UpdatedDate
                        });
                    }
                    else
                    {
                        return null;
                    }
                }
                return dto;
            }
            else
            {
                return null;
            }
        }
    }
}
