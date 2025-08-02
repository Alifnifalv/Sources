using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "FeePayment", "CRUDModel.ViewModel")]
    [DisplayName("Student Fee Due Details")]
    public class FeePaymentViewModel : BaseMasterViewModel
    {
        public FeePaymentViewModel()
        {
            FeeTypes = new List<FeePaymentFeeTypeViewModel>() { new FeePaymentFeeTypeViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [DisplayName("Student")]
        public long? StudentID { get; set; }
        public string Student { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public string ClassName { get; set; }

        public string SectionName { get; set; }

        public string SchoolName { get; set; }

        public string AcademicYear { get; set; }

        public decimal? TotalAmount { get; set; }

        public decimal? OldTotalAmount { get; set; }

        public decimal? NowPaying { get; set; }

        public bool? IsSelected { get; set; }

        public int FeepaymentModeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Fee Types")]
        public List<FeePaymentFeeTypeViewModel> FeeTypes { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FeePaymentDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeePaymentViewModel>(jsonString);
        }

        public FeePaymentViewModel ToVM(FeePaymentDTO dto)
        {
            Mapper<FeePaymentDTO, FeePaymentViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var applicationDto = dto as FeePaymentDTO;
            var vm = Mapper<FeePaymentDTO, FeePaymentViewModel>.Map(applicationDto);

        
            return vm;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<FeePaymentDTO, FeePaymentViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var stDtO = dto as FeePaymentDTO;
            var vm = Mapper<FeePaymentDTO, FeePaymentViewModel>.Map(stDtO);

          
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FeePaymentViewModel, FeePaymentDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<FeePaymentViewModel, FeePaymentDTO>.Map(this);
            
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeePaymentDTO>(jsonString);
        }

    }
}