using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ContactFax", "Contacts.Faxs")]
    [DisplayName("Fax")]
    public class FaxViewModel: EntityPropertyMapViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("FaxType")]
        public string EntityProperty { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("Fax Code")]
        public override string Value1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("Fax Number")]
        public override string Value2 { get; set; }


        public static EntityPropertyMapDTO ToDTO(FaxViewModel vm)
        {
            Mapper<FaxViewModel, EntityPropertyMapDTO>.CreateMap();
            return Mapper<FaxViewModel, EntityPropertyMapDTO>.Map(vm);
        }

        public static FaxViewModel ToVM(EntityPropertyMapDTO dto)
        {
            Mapper<EntityPropertyMapDTO, FaxViewModel>.CreateMap();
            return Mapper<EntityPropertyMapDTO, FaxViewModel>.Map(dto);
        }

        public static List<FaxViewModel> DefaultData(List<KeyValueDTO> values)
        {
            var data = new List<FaxViewModel>();

            foreach (var value in values)
            {
                data.Add(new FaxViewModel()
                {
                    EntityProperty = value.Value,
                    EntityPropertyID = int.Parse(value.Key)
                });
            }

            return data;
        }
    }
}
