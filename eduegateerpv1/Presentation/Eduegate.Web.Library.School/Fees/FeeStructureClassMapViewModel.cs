using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "FeeMasterClass", "CRUDModel.ViewModel")]
    [DisplayName("Fee Details")]
    public class FeeStructureClassMapViewModel : BaseMasterViewModel
    {
        public FeeStructureClassMapViewModel()
        {
            //Academic = new KeyValueViewModel();
            //Class = new List<KeyValueViewModel>();
            //FeeStructure = new List<KeyValueViewModel>();
            IsActive = true;
        }

        public long ClassFeeStructureMapIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Academic", "Numeric", false, "StructureAcademicChanges($event, $element, CRUDModel.ViewModel)", false)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel Academic { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", true)]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public List<KeyValueViewModel> Class { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine21 { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeeStructures", "Numeric", true)]
        [LookUp("LookUps.FeeStructure")]
        [CustomDisplay("FeeStructure")]
        public List<KeyValueViewModel> FeeStructure { get; set; }

        public List<KeyValueViewModel> FeeStructureClassList { get; set; }

        [ControlType(Eduegate.Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool IsActive { get; set; }
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FeeStructureClassMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeStructureClassMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {

            List<KeyValueViewModel> feeStructureList = new List<KeyValueViewModel>();
            List<KeyValueViewModel> ClassList = new List<KeyValueViewModel>();
            List<KeyValueViewModel> FeeStructureClassList = new List<KeyValueViewModel>();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<FeeStructureClassMapDTO, FeeStructureClassMapViewModel>.CreateMap();
            var feeDto = dto as FeeStructureClassMapDTO;
            var vm = Mapper<FeeStructureClassMapDTO, FeeStructureClassMapViewModel>.Map(feeDto);
            vm.ClassFeeStructureMapIID = feeDto.ClassFeeStructureMapIID;
            vm.Academic = feeDto.Academic.Key == null ? new KeyValueViewModel() { Key = null, Value = null } : new KeyValueViewModel() { Key = feeDto.Academic.Key.ToString(), Value = feeDto.Academic.Value };
            vm.IsActive = feeDto.IsActive;

            if (feeDto.FeeStructure != null && feeDto.FeeStructure.Count > 0)
            {
                foreach (KeyValueDTO dtoFee in feeDto.FeeStructure)
                {
                    if (dtoFee.Key != null)
                        feeStructureList.Add(new KeyValueViewModel { Key = dtoFee.Key, Value = dtoFee.Value }
                            );
                }
            }
            vm.FeeStructure = feeStructureList;

            if (feeDto.Class != null && feeDto.Class.Count > 0)
            {
                foreach (KeyValueDTO dtoClass in feeDto.Class)
                {
                    if (dtoClass.Key != null)
                        ClassList.Add(new KeyValueViewModel { Key = dtoClass.Key, Value = dtoClass.Value }
                            );
                }
            }
            vm.Class = ClassList;
            if (feeDto.FeeStructureClassMapList != null && feeDto.FeeStructureClassMapList.Count > 0)
            {
                foreach (FeeStructureClassMapListDTO  dtolist in feeDto.FeeStructureClassMapList)
                {

                    FeeStructureClassList.Add(new KeyValueViewModel { Key = dtolist.ClassFeeStructureMapIID.ToString(), Value = dtolist.ClassFeeStructureMapIID.ToString() }
                            );
                }
            }
            vm.FeeStructureClassList = FeeStructureClassList;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {

            List<KeyValueDTO> feeStructureList = new List<KeyValueDTO>();
            List<FeeStructureClassMapListDTO> feeStructureClassList = new List<FeeStructureClassMapListDTO>();
            List<KeyValueDTO> ClassList = new List<KeyValueDTO>();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<FeeStructureClassMapViewModel, FeeStructureClassMapDTO>.CreateMap();
            var dto = Mapper<FeeStructureClassMapViewModel, FeeStructureClassMapDTO>.Map(this);
            dto.IsActive = this.IsActive;
            dto.Academic = this.Academic.Key == null ? new KeyValueDTO() { Key = null, Value = null } : new KeyValueDTO() { Key = this.Academic.Key.ToString(), Value = this.Academic.Value };
            dto.ClassFeeStructureMapIID = this.ClassFeeStructureMapIID;

            if (this.FeeStructure != null || this.FeeStructure.Count > 0)
            {

                foreach (KeyValueViewModel vm in this.FeeStructure)
                {
                    if (vm.Key != null)
                        feeStructureList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                    );
                }
            }

            dto.FeeStructure = feeStructureList;


            if (this.Class != null || this.Class.Count > 0)
            {

                foreach (KeyValueViewModel vm in this.Class)
                {
                    if (vm.Key != null)
                        ClassList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                    );
                }
            }

            dto.Class = ClassList;


            if (this.FeeStructureClassList != null && this.FeeStructureClassList.Count > 0)
            {

                foreach (KeyValueViewModel vm in this.FeeStructureClassList)
                {
                    if (vm.Key != null)
                        feeStructureClassList.Add(new FeeStructureClassMapListDTO { ClassFeeStructureMapIID = long.Parse(vm.Key) }
                    );
                }
            }

            dto.FeeStructureClassMapList = feeStructureClassList;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeStructureClassMapDTO>(jsonString);
        }

    }
}