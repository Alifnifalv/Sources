using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.Common;
using Eduegate.Services.Contracts.School.Fees;
using System.Linq;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CampusTransfer", "CRUDModel.ViewModel")]
    [DisplayName("Campus Transfer")]
    public class CampusTransferViewModel : BaseMasterViewModel
    {
        public CampusTransferViewModel()
        {
            FeeTypes = new List<CampusTransferFeeTypeViewModel>() { new CampusTransferFeeTypeViewModel() };
        }

        public long CampusTransferIID { get; set; }
        public byte? SchoolID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("TransferDate")]
        public string TransferDateString { get; set; }
        public DateTime? TransferDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Student")]
        [Select2("Student", "Numeric", false, "StudentChanges($event, $element, CRUDModel.ViewModel)")]
        //[LookUp("LookUps.Student")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Student")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("AcademicYear")]
        public string AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("AdmissionNumber")]
        public string AdmissionNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Class")]
        public string Class { get; set; }
        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Section")]
        public string Section { get; set; }
        public int? SectionID { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; } 

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='SchoolChanges($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("ToTransferCampus")]
        [LookUp("LookUps.School")]
        public string ToSchool { get; set; }
        public byte? ToSchoolID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ToClasses", "String", false, "")]
        [LookUp("LookUps.ToClasses")]
        [CustomDisplay("ToClass")]
        public KeyValueViewModel ToClass { get; set; }
        public int? ToClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ToSections", "Numeric", false, "")]
        [LookUp("LookUps.ToSections")]
        [CustomDisplay("ToSection")]
        public KeyValueViewModel ToSection { get; set; }
        public int? ToSectionID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ToAcademicYear", "Numeric", false, "")]
        [CustomDisplay("ToAcademicYear")]
        [LookUp("LookUps.AcademicYear")]
        public KeyValueViewModel ToAcademicYear { get; set; }
        public int? ToAcademicYearID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "currSign htmllabelinfo-curr")]
        [CustomDisplay("Tuition fee total due from campus")]
        public decimal TutionFeeTotalFromCampus { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "currSign htmllabelinfo-curr")]
        [CustomDisplay("Tuition fee total due to campus")]
        public decimal TutionFeeTotalToCampus { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "currSign htmllabelinfo-curr")]
        [CustomDisplay("Transport fee total due from campus")]
        public decimal TransportFeeTotalFromCampus { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "currSign htmllabelinfo-curr")]
        [CustomDisplay("Transport fee total due to campus")]
        public decimal TransportFeeTotalToCampus { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "currSign htmllabelinfo-curr")]
        [CustomDisplay("Other fee total due from campus")]
        public decimal OtherFeeTotalFromCampus { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "currSign htmllabelinfo-curr")]
        [CustomDisplay("Other fee total due to campus")]
        public decimal OtherFeeTotalToCampus { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HiddenWithLabel)]
        [DisplayName("")]
        public string space { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, attribs: "ng-click='FillFeeDues($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("Fill Fee Due")]
        public string FillFeeDue { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid, "alignright")]
        [CustomDisplay("FeeTypes")]
        public List<CampusTransferFeeTypeViewModel> FeeTypes { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as CampusTransferDTO);
        }


        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CampusTransferViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<CampusTransferDTO, CampusTransferViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var campDto = dto as CampusTransferDTO;
            var vm = Mapper<CampusTransferDTO, CampusTransferViewModel>.Map(campDto);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            vm.TransferDateString = campDto.TransferDate.HasValue ? campDto.TransferDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.ToClass = new KeyValueViewModel() { Key = campDto.ToClass.Key, Value = campDto.ToClass.Value };
            vm.ToSection = new KeyValueViewModel() { Key = campDto.ToSection.Key, Value = campDto.ToSection.Value };           
            vm.ToSchool = campDto.ToSchoolID.HasValue ? campDto.ToSchoolID.ToString() : null;            
            vm.ToAcademicYearID = campDto.ToAcademicYearID;
            vm.ToAcademicYear = new KeyValueViewModel() { Key = campDto.ToAcademicYear.Key, Value = campDto.ToAcademicYear.Value };
            vm.AcademicYear = campDto.FromAcademicyear;
            vm.Class = campDto.FromClass.Value.ToString();
            vm.Section = campDto.FromSection.Value.ToString();
            vm.Student = new KeyValueViewModel() { Key = campDto.Student.Key, Value = campDto.Student.Value };
            vm.Remarks = campDto.Remarks;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<CampusTransferViewModel, CampusTransferDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<CampusTransferViewModel, CampusTransferDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.TransferDate = string.IsNullOrEmpty(this.TransferDateString) ? (DateTime?)null : DateTime.ParseExact(this.TransferDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.StudentID = string.IsNullOrEmpty(this.Student.Key) ? 0 : int.Parse(this.Student.Key);
            dto.FromAcademicYearID = this.AcademicYearID;
            dto.FromClassID = this.ClassID;
            dto.FromSectionID = this.SectionID;
            dto.FromSchoolID = this.SchoolID;

            dto.ToSchoolID = string.IsNullOrEmpty(this.ToSchool) ? (byte?)null : byte.Parse(this.ToSchool);
            dto.ToAcademicYearID = string.IsNullOrEmpty(this.ToAcademicYear.Key) ? 0 : int.Parse(this.ToAcademicYear.Key);
            dto.ToClassID = string.IsNullOrEmpty(this.ToClass.Key) ? 0 : int.Parse(this.ToClass.Key);
            dto.ToSectionID = string.IsNullOrEmpty(this.ToSection.Key) ? 0 : int.Parse(this.ToSection.Key);
            dto.Remarks = this.Remarks;

            if(this.FeeTypes != null)
            {
                foreach (var feeTypes in this.FeeTypes)
                {
                    if (feeTypes.FeeDueFeeTypeMapsID.HasValue)
                    {
                        var monthlySplitDto = feeTypes.MontlySplitMaps
                            .Where(monthlySplit => monthlySplit.MonthID.HasValue)
                            .Select(monthlySplit => new FeeCollectionMonthlySplitDTO()
                            {
                                MonthID = monthlySplit.MonthID.Value,
                                FeeDueMonthlySplitID = monthlySplit.FeeDueMonthlySplitID,
                                ReceivableAmount = monthlySplit.ReceivableAmount,
                                PayableAmount = monthlySplit.PayableAmount,
                            })
                            .ToList();

                        dto.FeeTypeMap.Add(new Services.Contracts.School.Fees.FeeCollectionFeeTypeDTO()
                        {
                            FeeMasterID = feeTypes.FeeMasterID,
                            FeePeriodID = feeTypes.FeePeriodID,
                            FeeDueFeeTypeMapsID = feeTypes.FeeDueFeeTypeMapsID,
                            ReceivableAmount = feeTypes.ReceivableAmount,
                            PayableAmount = feeTypes.PayableAmount,
                            IsTransportFee = feeTypes.IsTransportFee,
                            IsTutionFee = feeTypes.IsTutionFee,
                            ToCampusDue = feeTypes.ToCampusDue,
                            FromCampusDue = feeTypes.FromCampusDue,
                            MontlySplitMaps = monthlySplitDto
                        });
                    }
                }
            }


            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<CampusTransferDTO>(jsonString);
        }

    }
}