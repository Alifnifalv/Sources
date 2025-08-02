using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MarkGrades", "CRUDModel.ViewModel")]
    [DisplayName("Mark Details")]
    public class MarkGradeViewModel : BaseMasterViewModel
    {
        public MarkGradeViewModel()
        {
            GradeTypes = new List<MarkGradeMapViewModel>() { new MarkGradeMapViewModel() };

        }

        public int MarkGradeIID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        public string  Description { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("GradeTypes")]
        public List<MarkGradeMapViewModel> GradeTypes { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as MarkGradeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<MarkGradeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<MarkGradeDTO, MarkGradeViewModel>.CreateMap();
            Mapper<MarkGradeMapDTO, MarkGradeMapViewModel>.CreateMap();
            var mpdto = dto as MarkGradeDTO;
            var vm = Mapper<MarkGradeDTO, MarkGradeViewModel>.Map(dto as MarkGradeDTO);
            //vm.Description = mpdto.Description;
            vm.GradeTypes = new List<MarkGradeMapViewModel>();
            vm.MarkGradeIID = mpdto.MarkGradeIID;
            vm.Description = mpdto.Description;
            if(mpdto.GradeTypes!=null)
            {
                foreach(var grade in mpdto.GradeTypes)
                {
                    vm.GradeTypes.Add(new MarkGradeMapViewModel()
                    {
                        MarksGradeMapIID=grade.MarksGradeMapIID,
                        MarkGradeID = grade.MarksGradeID,
                        GradeName=grade.GradeName,
                        GradeFrom=grade.GradeFrom,
                        GradeTo=grade.GradeTo,
                        IsPercentage=grade.IsPercentage,
                        Description=grade.Description

                    });

                }
            }
          
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<MarkGradeViewModel, MarkGradeDTO>.CreateMap();
            Mapper<MarkGradeMapViewModel, MarkGradeMapDTO>.CreateMap();
            var dto = Mapper<MarkGradeViewModel, MarkGradeDTO>.Map(this);
            dto.GradeTypes = new List<MarkGradeMapDTO>();
            dto.Description = this.Description;
            dto.MarkGradeIID = this.MarkGradeIID;
            foreach(var markdto in this.GradeTypes)
            {
                if (!string.IsNullOrEmpty(markdto.GradeName))
                { 
                dto.GradeTypes.Add(new MarkGradeMapDTO()
                {
                    MarksGradeMapIID = markdto.MarksGradeMapIID,
                    MarksGradeID= markdto.MarkGradeID,
                    Description=markdto.Description,
                    GradeName=markdto.GradeName,
                    GradeFrom=markdto.GradeFrom.HasValue?markdto.GradeFrom:(decimal?)null,
                    GradeTo=markdto.GradeTo.HasValue ? markdto.GradeTo : (decimal?)null,
                    IsPercentage=markdto.IsPercentage,

                });
                }
            }
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<MarkGradeDTO>(jsonString);
        }
    }
}

