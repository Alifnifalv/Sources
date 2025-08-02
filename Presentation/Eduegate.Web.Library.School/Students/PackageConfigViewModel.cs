using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Newtonsoft.Json;
using Eduegate.Services.Contracts.School.Students;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Students
{

    public class PackageConfigViewModel : BaseMasterViewModel
    {
        public PackageConfigViewModel()
        {
            //Academic = new KeyValueViewModel();
            Class = new List<KeyValueViewModel>();
            Student = new List<KeyValueViewModel>();
            StudentGroup = new List<KeyValueViewModel>();
            FeeStructure = new List<KeyValueViewModel>();
            CreditAccount = new KeyValueViewModel();
            IsActive = true;
        }

        public long PackageConfigIID { get; set; }

        [Required]
        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(25, ErrorMessage = "Maximum Length should be within 25!")]
        [CustomDisplay("Name")]
        public string Name { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Academic", "Numeric", false, "StructureAcademicChanges($event, $element, CRUDModel.ViewModel)", false)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel Academic { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine21 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeeStructures", "Numeric", true)]
        [LookUp("LookUps.FeeStructures")]
        [CustomDisplay("FeeStructure")]
        public List<KeyValueViewModel> FeeStructure { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", true)]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public List<KeyValueViewModel> Class { get; set; }
        public int? ClassID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Student", "String", true)]
        [CustomDisplay("Student")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Students")]
        [LookUp("LookUps.Students")]
        public List<KeyValueViewModel> Student { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("StudentGroup", "Numeric", true, "")]
        [LookUp("LookUps.StudentGroup")]
        [CustomDisplay("StudentGroup")]
        public List<KeyValueViewModel> StudentGroup { get; set; }
        public int? StudentGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("CreditNoteAccount")]
        [LookUp("LookUps.CreditNoteAccount")]
        [Select2("CreditNoteAccount", "Numeric", false, "")]
        public KeyValueViewModel CreditAccount { get; set; }

        public long? CreditAccountID { get; set; }

        [ControlType(Eduegate.Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsAutoCreditNote")]
        public bool IsAutoCreditNote { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [ControlType(Eduegate.Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool IsActive { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as PackageConfigDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<PackageConfigViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            List<KeyValueViewModel> studentList = new List<KeyValueViewModel>();
            List<KeyValueViewModel> feeStructureList = new List<KeyValueViewModel>();
            List<KeyValueViewModel> studentGroupList = new List<KeyValueViewModel>();
            List<KeyValueViewModel> ClassList = new List<KeyValueViewModel>();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<PackageConfigDTO, PackageConfigViewModel>.CreateMap();
            var feeDto = dto as PackageConfigDTO;
            var vm = Mapper<PackageConfigDTO, PackageConfigViewModel>.Map(feeDto);
            vm.PackageConfigIID = feeDto.PackageConfigIID;

            vm.Academic = string.IsNullOrEmpty(feeDto.Academic.Key) ? new KeyValueViewModel() : new KeyValueViewModel() { Key = feeDto.Academic.Key, Value = feeDto.Academic.Value };
            vm.Description = feeDto.Description;
            vm.IsActive = feeDto.IsActive;
            vm.Name = feeDto.Name;
            vm.IsAutoCreditNote = feeDto.IsAutoCreditNote.HasValue ? feeDto.IsAutoCreditNote.Value : false;
            vm.CreditAccountID = feeDto.CreditNoteAccountID.Key == null ? (long?)null : long.Parse(feeDto.CreditNoteAccountID.Key);
            vm.CreditAccount = feeDto.CreditNoteAccountID.Key == null ? new KeyValueViewModel() { Key = null, Value = null } : new KeyValueViewModel() { Key = feeDto.CreditNoteAccountID.Key.ToString(), Value = feeDto.CreditNoteAccountID.Value };
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

            if (feeDto.Student != null && feeDto.Student.Count > 0)
            {
                foreach (KeyValueDTO dtoStud in feeDto.Student)
                {
                    if (dtoStud.Key != null)
                        studentList.Add(new KeyValueViewModel { Key = dtoStud.Key, Value = dtoStud.Value }
                            );
                }
            }
            vm.Student = studentList;

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

            if (feeDto.StudentGroup != null && feeDto.StudentGroup.Count > 0)
            {
                foreach (KeyValueDTO dtoStudentGroup in feeDto.StudentGroup)
                {
                    if (dtoStudentGroup.Key != null)
                        studentGroupList.Add(new KeyValueViewModel { Key = dtoStudentGroup.Key, Value = dtoStudentGroup.Value }
                            );
                }
            }
            vm.StudentGroup = studentGroupList;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            List<KeyValueDTO> studentList = new List<KeyValueDTO>();
            List<KeyValueDTO> studentGroupList = new List<KeyValueDTO>();
            List<KeyValueDTO> feeStructureList = new List<KeyValueDTO>();
            List<KeyValueDTO> ClassList = new List<KeyValueDTO>();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<PackageConfigViewModel, PackageConfigDTO>.CreateMap();
            var dto = Mapper<PackageConfigViewModel, PackageConfigDTO>.Map(this);
            dto.IsActive = this.IsActive;
            dto.Academic = this.Academic.Key == null ? new KeyValueDTO() { Key = null, Value = null } :
                new KeyValueDTO() { Key = this.Academic.Key.ToString(), Value = this.Academic.Value };
            dto.PackageConfigIID = this.PackageConfigIID;
            dto.IsAutoCreditNote =  this.IsAutoCreditNote;
            dto.CreditNoteAccountID = this.CreditAccount.Key == null ? new KeyValueDTO() { Key = null, Value = null } 
            : new KeyValueDTO() { Key = this.CreditAccount.Key.ToString(), Value = this.CreditAccount.Value };

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

            if (this.Student != null || this.Student.Count > 0)
            {

                foreach (KeyValueViewModel vm in this.Student)
                {
                    if (vm.Key != null)
                        studentList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                    );
                }
            }

            dto.Student = studentList;
            if (this.StudentGroup != null || this.StudentGroup.Count > 0)
            {

                foreach (KeyValueViewModel vm in this.StudentGroup)
                {
                    if (vm.Key != null)
                        studentGroupList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                    );
                }
            }

            dto.StudentGroup = studentGroupList;

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

            dto.Description = this.Description;
            dto.Name = this.Name;
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<PackageConfigDTO>(jsonString);
        }

    }
}
