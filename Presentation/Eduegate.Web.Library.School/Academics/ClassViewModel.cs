using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Class", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class ClassViewModel : BaseMasterViewModel
    {
        public ClassViewModel()
        {
            WorkFlowList = new List<ClassWorkFlowListViewModel>() { new ClassWorkFlowListViewModel() };
            IsActive = false;
        }

        public int  ClassID { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage ="Maximum Length should be 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        public string  ClassDescription { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("CostCenter")]
        [Select2("CostCenterDetails", "Numeric", false)]
        [LookUp("LookUps.CostCenterDetails")]
        public KeyValueViewModel CostCenter { get; set; }
        public int? CostCenterID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("OrderNumber")]
        public int ORDERNO { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("ClassGroup")]
        [Select2("ClassGroup", "Numeric", true)]
        [LookUp("LookUps.ClassGroup")]
        public List<KeyValueViewModel> GroupDescription { get; set; }
        public long? ClassGroupID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Workflow")]
        public List<ClassWorkFlowListViewModel> WorkFlowList { get; set; }

        public string Code { get; set; }

        public byte? ShiftID { get; set; }

        public bool? IsVisible { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ClassDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ClassDTO, ClassViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var sDto = dto as ClassDTO;
            var vm = Mapper<ClassDTO, ClassViewModel>.Map(dto as ClassDTO);

            vm.CostCenter = sDto.CostCenterID.HasValue? new KeyValueViewModel()
            {
                Key = sDto.CostCenterID.ToString(),
                Value = sDto.CostCenter.Value
            } : new KeyValueViewModel();

            vm.WorkFlowList = new List<ClassWorkFlowListViewModel>();
            foreach (var workflowdMap in sDto.WorkFlowListDTO)
            {
                vm.WorkFlowList.Add(new ClassWorkFlowListViewModel()
                {
                    ClassWorkFlowIID = workflowdMap.ClassWorkFlowIID,
                    ClassID = workflowdMap.ClassID,
                    WorkFlow2 = workflowdMap.WorkflowID.HasValue ? workflowdMap.WorkflowID.ToString() : null,
                    WorkFlow1 = workflowdMap.WorkflowEntityID.HasValue ? workflowdMap.WorkflowEntityID.ToString() : null,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ClassViewModel, ClassDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<ClassViewModel, ClassDTO>.Map(this);
            dto.CostCenterID = string.IsNullOrEmpty(this.CostCenter.Key) ? (int?)null : int.Parse(this.CostCenter.Key);
            List<KeyValueDTO> classGroupList = new List<KeyValueDTO>();
            if (this.GroupDescription != null)
            {
                foreach (KeyValueViewModel vm in this.GroupDescription)
                {
                    classGroupList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                        );
                }
            }

            dto.GroupDescription = this.GroupDescription != null ? classGroupList : null;            //return Mapper<ClassViewModel, ClassDTO>.Map(this);

            dto.WorkFlowListDTO = new List<ClassWorkFlowListDTO>();

            foreach (var dataList in this.WorkFlowList)
            {
                if (dataList.WorkFlow1 != null)
                {
                    dto.WorkFlowListDTO.Add(new ClassWorkFlowListDTO()
                    {
                        ClassWorkFlowIID = dataList.ClassWorkFlowIID,
                        ClassID = this.ClassID,
                        WorkflowID = string.IsNullOrEmpty(dataList.WorkFlow2) ? (int?)null : int.Parse(dataList.WorkFlow2),
                        WorkflowEntityID = string.IsNullOrEmpty(dataList.WorkFlow1) ? (int?)null : int.Parse(dataList.WorkFlow1),
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassDTO>(jsonString);
        }

    }
}