using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    public class PropertyViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Property ID")]
        public long PropertyIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Property Name")]
        public string PropertyName { get; set; }
        public string PropertyTypeName { get; set; }
        public string PropertyDescription { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Default Name")]
        public string DefaultValue { get; set; }
        public byte PropertyTypeID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.PropertyType")]
        [DisplayName("Property Type")]
        public string PropertyType { get; set; }
        public bool IsUnqiue { get; set; }
        public byte UIControlTypeID { get; set; }
        public byte UIControlValidationID { get; set; }

        public static PropertyDTO ToDTO(PropertyViewModel vm)
        {
            Mapper<PropertyViewModel, PropertyDTO>.CreateMap();
            var mapper = Mapper<PropertyViewModel, PropertyDTO>.Map(vm);
            mapper.PropertyTypeID = byte.Parse(vm.PropertyType);
            return mapper;
        }

        public static PropertyViewModel ToVM(PropertyDTO dto)
        {
            Mapper<PropertyDTO, PropertyViewModel>.CreateMap();
            var mapper = Mapper<PropertyDTO, PropertyViewModel>.Map(dto);
            mapper.PropertyType = dto.PropertyTypeID.ToString();
            return mapper;
        }
    }
}