using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "StudentFeeDue", "CRUDModel.ViewModel")]
    [DisplayName("Student Fee Due Details")]
    public class FeeDueGenerationViewModel : BaseMasterViewModel
    {
      
        public FeeDueGenerationViewModel()
        {
            StudentClass = new KeyValueViewModel();
            Section = new KeyValueViewModel();
            Student = new List<KeyValueViewModel>();
            FeePeriod = new List<KeyValueViewModel>();            
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            InvoiceDateString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
            FeeDueGenerationDateString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
            Academic = new KeyValueViewModel();
            FeeMaster = new List<KeyValueViewModel>();
        }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Id")]
        public long StudentFeeDueIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false, "AcademicYearChanges($event, $element, CRUDModel.ViewModel)", false)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel Academic { get; set; }

        public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("InvoiceDate")]
        public string InvoiceDateString { get; set; }

        public System.DateTime? InvoiceDate { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("FeeDueDate")]
        public string FeeDueGenerationDateString { get; set; }

        public System.DateTime? FeeDueGenerationDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine01 { get; set; }

       
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false, "GetStudentsByDropDowns($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Section", "Numeric", false, "GetStudentsByDropDowns($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public KeyValueViewModel Section { get; set; }
        public int? SectionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SchoolWiseStudents", "Numeric", true)]
        [LookUp("LookUps.SchoolWiseStudents")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Students")]
        [CustomDisplay("Student")]
        public List<KeyValueViewModel> Student { get; set; }
        public long? StudentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeeMaster", "Numeric", true, "ChangeFeeMaster($event, $element, CRUDModel.ViewModel)", false)]
        [LookUp("LookUps.FeeMaster")]
        [CustomDisplay("FeeMaster")]
        public List<KeyValueViewModel> FeeMaster { get; set; }
        public int? FeeMasterID { get; set; }

        public bool? IsEnableFeePeriod { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FeePeriod", "Numeric", true, "")]
        [LookUp("LookUps.FeePeriod")]
        [CustomDisplay("FeePeriod")]
        public List<KeyValueViewModel> FeePeriod { get; set; }
        public long? FeePeriodId { get; set; }
        public bool? IsEnableAmount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=!CRUDModel.ViewModel.IsEnableAmount")]
        [CustomDisplay("Amount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button)]
        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=SaveStudentFeeDue()")]
        [CustomDisplay("Generate")]
        public string GenerateButton { get; set; }
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentFeeDueDTO);
        }
        public override BaseMasterDTO ToDTO()
        {
            Mapper<FeeDueGenerationViewModel, StudentFeeDueDTO>.CreateMap();
            Mapper<KeyValueViewModel, FeeDueFeeTypeMapDTO>.CreateMap();

            var dto = Mapper<FeeDueGenerationViewModel, StudentFeeDueDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            List<KeyValueDTO> feePerioList = new List<KeyValueDTO>();
            List<KeyValueDTO> studentList = new List<KeyValueDTO>();
            List<KeyValueDTO> feeMasterList = new List<KeyValueDTO>();

            foreach (KeyValueViewModel vm in this.FeePeriod)
            {
                feePerioList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                    );
            }
            dto.FeePeriod = feePerioList;
            foreach (KeyValueViewModel vm in this.Student)
            {
                studentList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                    );
            }
            dto.Student = studentList;
            foreach (KeyValueViewModel vm in this.FeeMaster)
            {
                feeMasterList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                    );
            }
            dto.FeeMaster = feeMasterList;
            dto.ClassId = string.IsNullOrEmpty(this.StudentClass.Key) ? (int?)null : int.Parse(this.StudentClass.Key);
            dto.SectionId = string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);
            dto.DueDate = string.IsNullOrEmpty(this.FeeDueGenerationDateString) ? (DateTime?)null : DateTime.ParseExact(this.FeeDueGenerationDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.InvoiceDate = string.IsNullOrEmpty(this.InvoiceDateString) ? (DateTime?)null : DateTime.ParseExact(this.InvoiceDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.FeeMasterAmount = this.Amount.HasValue ? this.Amount : 0;
            dto.Remarks = this.Remarks;

            return dto;
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeDueGenerationViewModel>(jsonString);
        }


    }
}
