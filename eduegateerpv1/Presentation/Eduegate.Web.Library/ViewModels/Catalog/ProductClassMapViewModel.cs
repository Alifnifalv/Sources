using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.School.Students;
using Eduegate.Web.Library.ViewModels.Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
    public class ProductClassMapViewModel : BaseMasterViewModel
    {
        public ProductClassMapViewModel()
        {
            Class = new KeyValueViewModel();
            ProductClassMap = new List<ProductClassMapDetailViewModel> { new ProductClassMapDetailViewModel() };
            //Product = new KeyValueViewModel();
            //FeeMaster = new KeyValueViewModel();
        }

        public long ProductClassMapIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Classes", "Numeric", false, "")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel Class { get; set; }
        public int? ClassID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("ProductClassMap")]
        public List<ProductClassMapDetailViewModel> ProductClassMap { get; set; }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ProductClassMapViewModel, ProductClassMapDTO>.CreateMap();
            var dto = Mapper<ProductClassMapViewModel, ProductClassMapDTO>.Map(this);
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();

            dto.ProductClassMapIID = this.ProductClassMapIID;
            dto.ClassID = string.IsNullOrEmpty(this.Class.Key) ? (int?)null : int.Parse(this.Class.Key);
            dto.AcademicYearID = string.IsNullOrEmpty(this.AcademicYear.Key) ? (int?)null : int.Parse(this.AcademicYear.Key);
            IsActive = this.IsActive;

            dto.ProductClassMaps = new List<ProductClassMapDetailDTO>();

            foreach (var prdtClsMap in this.ProductClassMap)
            {
                if (prdtClsMap.Product.Key != null)
                {
                    dto.ProductClassMaps.Add(new ProductClassMapDetailDTO()
                    {
                        ProductClassMapIID = prdtClsMap.ProductClassMapID,
                        ProductID = string.IsNullOrEmpty(prdtClsMap.Product.Key) ? (long?)null : int.Parse(prdtClsMap.Product.Key),
                        FeeMasterID = string.IsNullOrEmpty(prdtClsMap.FeeMaster.Key) ? (int?)null : int.Parse(prdtClsMap.FeeMaster.Key),
                        SubjectID = string.IsNullOrEmpty(prdtClsMap.Subject.Key) ? (int?)null : int.Parse(prdtClsMap.Subject.Key),
                        //ProductTypeID = string.IsNullOrEmpty(prdtClsMap.ProductType.Key) ? (int?)null : int.Parse(prdtClsMap.ProductType.Key),
                        //StreamID = string.IsNullOrEmpty(prdtClsMap.Streams.Key) ? (int?)null : int.Parse(prdtClsMap.Streams.Key),
                    });
                }
            }

            return dto;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ProductClassMapDTO, ProductClassMapViewModel>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var mapDTO = dto as ProductClassMapDTO;
            var vm = Mapper<ProductClassMapDTO, ProductClassMapViewModel>.Map(mapDTO);
            var PrctClsDto = dto as ProductClassMapDTO;

            vm.Class = mapDTO.ClassID.HasValue ? new KeyValueViewModel() { Key = mapDTO.ClassID.ToString(), Value = mapDTO.ClassName } : new KeyValueViewModel();
            vm.AcademicYear = mapDTO.AcademicYearID.HasValue ? new KeyValueViewModel() { Key = mapDTO.AcademicYearID.ToString(), Value = mapDTO.AcademicYearName } : new KeyValueViewModel();
            vm.IsActive = mapDTO.IsActive;

            vm.ProductClassMap = new List<ProductClassMapDetailViewModel>();

            foreach (var prctClassMap in mapDTO.ProductClassMaps)
            {
                if (prctClassMap.ProductID.HasValue)
                {
                    vm.ProductClassMap.Add(new ProductClassMapDetailViewModel()
                    {
                        ProductClassMapID = prctClassMap.ProductClassMapIID,
                        Product = prctClassMap.ProductID.HasValue ? new KeyValueViewModel() { Key = prctClassMap.ProductID.ToString(), Value = prctClassMap.ProductName } : new KeyValueViewModel(),
                        FeeMaster = prctClassMap.FeeMasterID.HasValue ? new KeyValueViewModel() { Key = prctClassMap.FeeMasterID.ToString(), Value = prctClassMap.FeeMasterName } : new KeyValueViewModel(),
                        //ProductType = prctClassMap.ProductTypeID.HasValue ? new KeyValueViewModel() { Key = prctClassMap.ProductTypeID.ToString(), Value = prctClassMap.ProductTypeName } : new KeyValueViewModel(),
                        //Streams = prctClassMap.StreamID.HasValue ? new KeyValueViewModel() { Key = prctClassMap.StreamID.ToString(), Value = prctClassMap.StreamName } : new KeyValueViewModel(),
                        Subject = prctClassMap.SubjectID.HasValue ? new KeyValueViewModel() { Key = prctClassMap.SubjectID.ToString(), Value = prctClassMap.SubjectName } : new KeyValueViewModel(),
                        SubjectType = prctClassMap.SubjectID.HasValue ? new KeyValueViewModel() { Key = prctClassMap.SubjectTypeName.ToString(), Value = prctClassMap.SubjectTypeName } : new KeyValueViewModel(),
                    });
                }
            }


            return vm;
        }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ProductClassMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ProductClassMapViewModel>(jsonString);
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ProductClassMapDTO>(jsonString);
        }
    }
}