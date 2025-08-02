using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    public class ProductFamilyViewModel : BaseMasterViewModel
    {
        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Product Family ID")]
        public long ProductFamilyIID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("FamilyName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string FamilyName { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1{ get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LookUp("LookUps.PropertyType")]
        [CustomDisplay("PropertyType")]
        [Select2("SelectedPropertyTypes", "Numeric", true)]
        public List<KeyValueViewModel> SelectedPropertyTypes { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.ProductFamilyType")]
        //[DisplayName("Family Type")]
        //public string ProductFamilyType { get; set; }

        public int ProductFamilyTypeID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LookUp("LookUps.Property")]
        [CustomDisplay("Properties")]
        [Select2("SelectedProperties", "Numeric", true)]
        public List<KeyValueViewModel> SelectedProperties { get; set; }

        public static List<ProductFamilyViewModel> FromDTO(List<ProductFamilyDTO> dtos)
        {
            var vms = new List<ProductFamilyViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(FromDTO(dto));
            }

            return vms;
        }

        public static ProductFamilyViewModel FromDTO(ProductFamilyDTO dto)
        {
            Mapper<ProductFamilyDTO, ProductFamilyViewModel>.CreateMap();
            var mapper = Mapper<ProductFamilyDTO, ProductFamilyViewModel>.Map(dto);
            //mapper.ProductFamilyType = dto.ProductFamilyTypeID.ToString();
            mapper.SelectedPropertyTypes = new List<KeyValueViewModel>();
            if (dto.PropertyTypes != null)
            {
                foreach (var PropertyType in dto.PropertyTypes)
                {
                    mapper.SelectedPropertyTypes.Add(new KeyValueViewModel() { Key = PropertyType.PropertyTypeID.ToString(), Value = PropertyType.PropertyTypeName });
                }
            }
            mapper.SelectedProperties = new List<KeyValueViewModel>();

            if (dto.Properties != null)
            {
                foreach (var property in dto.Properties)
                {
                    mapper.SelectedProperties.Add(new KeyValueViewModel() { Key = property.PropertyIID.ToString(), Value = property.PropertyName });
                }
            }

            return mapper;
        }

        public static List<ProductFamilyDTO> ToDTO(List<ProductFamilyViewModel> vms)
        {
            var dtos = new List<ProductFamilyDTO>();

            foreach (var vm in vms)
            {
                dtos.Add(ToDTO(vm));
            }

            return dtos;
        }

        public static ProductFamilyDTO ToDTO(ProductFamilyViewModel vm)
        {
            Mapper<ProductFamilyViewModel, ProductFamilyDTO>.CreateMap();
            var mapper = Mapper<ProductFamilyViewModel, ProductFamilyDTO>.Map(vm);
            //mapper.ProductFamilyTypeID = string.IsNullOrEmpty(vm.ProductFamilyType) ? 1 : int.Parse(vm.ProductFamilyType);
            mapper.ProductFamilyTypeID = 1;
            mapper.PropertyTypes = new List<PropertyTypeDTO>();
            if (vm.SelectedPropertyTypes != null)
            {
                foreach (var PropertyType in vm.SelectedPropertyTypes)
                {
                    mapper.PropertyTypes.Add(new PropertyTypeDTO() { PropertyTypeID = Convert.ToByte(PropertyType.Key), PropertyTypeName = PropertyType.Value });
                }
            }
            mapper.Properties = new List<PropertyDTO>();

            if (vm.SelectedProperties != null)
            {
                foreach (var property in vm.SelectedProperties)
                {
                    mapper.Properties.Add(new PropertyDTO() { PropertyIID = long.Parse(property.Key), PropertyName = property.Value });
                }
            }

            return mapper;
        }
    }
}