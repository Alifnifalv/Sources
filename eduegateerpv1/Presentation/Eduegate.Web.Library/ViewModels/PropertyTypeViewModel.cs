using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    public class PropertyTypeViewModel : BaseMasterViewModel
    {
        public byte PropertyTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("PropertyTypeName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string PropertyTypeName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Properties", "String", true, "")]
        [LazyLoad("", "Mutual/GetLazyLookUpData?lookType=Property", "LookUps.Property")]
        [CustomDisplay("Properties")]
        public List<KeyValueViewModel> Properties { get; set; }

        public int CultureID { get; set; }


        public static PropertyTypeDTO ToDTO(PropertyTypeViewModel vm)
        {
            Mapper<PropertyTypeViewModel, PropertyTypeDTO>.CreateMap();
            var mapper = Mapper<PropertyTypeViewModel, PropertyTypeDTO>.Map(vm);
            mapper.PropertyList = new List<Services.Contracts.PropertyDTO>();
            foreach (var prop in vm.Properties)
            {
                mapper.PropertyList.Add(new Services.Contracts.PropertyDTO
                {
                    PropertyIID = Convert.ToInt64(prop.Key),
                    PropertyName = prop.Value,
                });
            }
            return mapper;
        }

        public static PropertyTypeViewModel ToVM(PropertyTypeDTO dto)
        {
            Mapper<PropertyTypeDTO, PropertyTypeViewModel>.CreateMap();
            var mapper = Mapper<PropertyTypeDTO, PropertyTypeViewModel>.Map(dto);
            mapper.Properties = new List<KeyValueViewModel>();
            foreach (var prop in dto.PropertyList)
            {
                mapper.Properties.Add(new KeyValueViewModel
                {
                    Key = prop.PropertyIID.ToString() ,
                    Value = prop.PropertyName,
                });
            }
            return mapper;
        }
    }
}