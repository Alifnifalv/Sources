using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SalaryComponent", "CRUDModel.ViewModel")]
    [DisplayName("Salary Component")]
    public class SalaryComponentViewModel : BaseMasterViewModel
    {
        public SalaryComponentViewModel()
        {
            SalaryComponentRelationsGrid = new List<SalaryComponentRelationsGridViewModel>() { new SalaryComponentRelationsGridViewModel() };
        }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Salary Component ID")]
        public int SalaryComponentID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50,ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Abbreviation")]
        public string Abbreviation { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("ComponentType")]
        [LookUp("LookUps.ComponentType")]
        public string ComponentType { get; set; }
        public byte? ComponentTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("ComponentGroup")]
        [LookUp("LookUps.SalaryComponentGroup")]
        public string SalaryComponentGroup { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("ReportHeadGroup")]
        [LookUp("LookUps.ReportHeadGroup")]
        public string ReportHeadGroup { get; set; }
        public int? ReportHeadGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("NoOfDaysApplicable")]
        public string NoOfDaysApplicable { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Relation")]
        public List<SalaryComponentRelationsGridViewModel> SalaryComponentRelationsGrid { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SalaryComponentDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SalaryComponentViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SalaryComponentDTO, SalaryComponentViewModel>.CreateMap();

            var sDto = dto as SalaryComponentDTO;
            var vm = Mapper<SalaryComponentDTO, SalaryComponentViewModel>.Map(sDto);
            vm.ComponentType = sDto.ComponentTypeID.ToString();
            vm.SalaryComponentGroup = sDto.SalaryComponentGroupID.ToString();
            vm.ReportHeadGroup = sDto.ReportHeadGroupID.ToString();
            vm.NoOfDaysApplicable = sDto.NoOfDaysApplicable.ToString();

            vm.SalaryComponentRelationsGrid = new List<SalaryComponentRelationsGridViewModel>();
            foreach (var relationMap in sDto.SalaryComponentRelationMap)
            {
                vm.SalaryComponentRelationsGrid.Add(new SalaryComponentRelationsGridViewModel()
                {
                    SalaryComponentRelationMapIID = relationMap.SalaryComponentRelationMapIID,
                    SalaryComponentID = relationMap.SalaryComponentID,
                    SalaryComponentRelationType = relationMap.RelationTypeID.ToString(),
                    RelationComponent = relationMap.RelatedComponentID.ToString(),
                    NoOfDaysApplicable = relationMap.NoOfDaysApplicable.ToString(),
                });
            }
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SalaryComponentViewModel, SalaryComponentDTO>.CreateMap();
            var dto = Mapper<SalaryComponentViewModel, SalaryComponentDTO>.Map(this);
            dto.ComponentTypeID = string.IsNullOrEmpty(this.ComponentType) ? (byte?)null : byte.Parse(this.ComponentType);
            dto.SalaryComponentGroupID = string.IsNullOrEmpty(this.SalaryComponentGroup) ? (byte?)null : byte.Parse(this.SalaryComponentGroup);
            dto.ReportHeadGroupID = string.IsNullOrEmpty(this.ReportHeadGroup) ? (int?)null : int.Parse(this.ReportHeadGroup);
            dto.NoOfDaysApplicable = string.IsNullOrEmpty(this.NoOfDaysApplicable) ? (int?)null : int.Parse(this.NoOfDaysApplicable);

            dto.SalaryComponentRelationMap = new List<SalaryComponentRelationMapDTO>();
            foreach (var dat in this.SalaryComponentRelationsGrid)
            {
                if (dat.RelationComponent != null)
                {
                    dto.SalaryComponentRelationMap.Add(new SalaryComponentRelationMapDTO()
                    { 
                        SalaryComponentRelationMapIID = dat.SalaryComponentRelationMapIID,
                        RelatedComponentID = string.IsNullOrEmpty(dat.RelationComponent) ? (int?)null : int.Parse(dat.RelationComponent),
                        RelationTypeID = string.IsNullOrEmpty(dat.SalaryComponentRelationType) ? (short?)null : short.Parse(dat.SalaryComponentRelationType),
                        NoOfDaysApplicable = string.IsNullOrEmpty(dat.NoOfDaysApplicable) ? (int?)null : int.Parse(dat.NoOfDaysApplicable),
                    });

                }
            }
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SalaryComponentDTO>(jsonString);
        }

    }

}


