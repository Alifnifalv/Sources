using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "EntityTypeEntitlement", "CRUDModel.ViewModel.PriceListEntitlement.EntitlementPriceListMaps")]
    [DisplayName("")]
    public class EntitlementPriceListMapViewModel : BaseMasterViewModel
    {
        public byte EntitlementID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Entitlement")]
        public string EntitlementName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Price List")]
        public string PriceListName { get; set; }

        [DataPicker("Price")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("ProductPriceListIID")]
        public long ProductPriceListIID { get; set; }


        public long ProductPriceListCustomerMapIID { get; set; }


        public static List<EntitlementPriceListMapViewModel> DefaultData(List<KeyValueDTO> values)
        {
            var data = new List<EntitlementPriceListMapViewModel>();

            foreach (var value in values)
            {
                data.Add(new EntitlementPriceListMapViewModel()
                {
                    EntitlementName = value.Value,
                    EntitlementID = byte.Parse(value.Key)
                });
            }

            return data;
        }
    }
}
