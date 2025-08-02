using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    public class PropertiesViewModel : BaseMasterViewModel
    {
        public long PropertyIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("PropertyName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string PropertyName { get; set; }

        public static PropertyDTO ToDTO(PropertiesViewModel vm)
        {
            Mapper<PropertiesViewModel, PropertyDTO>.CreateMap();
                var mapper = Mapper<PropertiesViewModel, PropertyDTO>.Map(vm);
            return mapper;

        }

        public static PropertiesViewModel ToVM(PropertyDTO dto)
        {
            Mapper<PropertyDTO, PropertiesViewModel>.CreateMap();
                var mapper = Mapper<PropertyDTO, PropertiesViewModel>.Map(dto);
            return mapper;
        }
    }
}