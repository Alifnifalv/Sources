using Eduegate.Framework.Mvc.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Contracts.Support.CustomerSupport;

namespace Eduegate.Web.Library.Support.CustomerSupport
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SupportCategory", "CRUDModel.ViewModel")]
    [DisplayName("Support Category")]
    public class SupportCategoryViewModel : BaseMasterViewModel
    {
        public SupportCategoryViewModel()
        {
            IsActive = false;
        }

        public int SupportCategoryID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("CategoryName")]
        public string CategoryName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine1 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("ParentCategory")]
        [LookUp("LookUps.SupportCategories")]
        public string ParentCategory { get; set; }
        public int? ParentCategoryID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine2 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("SortOrder")]
        public int? SortOrder { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SupportCategoryDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SupportCategoryViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SupportCategoryDTO, SupportCategoryViewModel>.CreateMap();
            var catDTO = dto as SupportCategoryDTO;
            var vm = Mapper<SupportCategoryDTO, SupportCategoryViewModel>.Map(catDTO);

            vm.SupportCategoryID = catDTO.SupportCategoryID;
            vm.CategoryName = catDTO.CategoryName;
            vm.ParentCategory = catDTO.ParentCategoryID.HasValue ? catDTO.ParentCategoryID.ToString() : null;
            vm.SortOrder = catDTO.SortOrder;
            vm.IsActive = catDTO.IsActive;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SupportCategoryViewModel, SupportCategoryDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<SupportCategoryViewModel, SupportCategoryDTO>.Map(this);

            dto.SupportCategoryID = this.SupportCategoryID;
            dto.CategoryName = this.CategoryName;
            dto.ParentCategoryID = string.IsNullOrEmpty(this.ParentCategory) ? (int?)null : int.Parse(this.ParentCategory);
            dto.SortOrder = this.SortOrder;
            dto.IsActive = this.IsActive ?? false;            

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SupportCategoryDTO>(jsonString);
        }

    }
}