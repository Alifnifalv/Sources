using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierAccountEntitlementsMap", "CRUDModel.ViewModel.SupplierAccountMaps.SupplierAccountEntitlements")]
    [DisplayName("")]
    public class SupplierAccountEntitlementViewModel : BaseMasterViewModel
    {

       
        public Nullable<byte> EntitlementID { get; set; }

        

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Entitlement")]
        public string EntitlementName { get; set; }

        [DataPicker("AccountEntry")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("AccountName")]
        public string AccountName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Alias")]
        public string Alias { get; set; }

        //[DataPicker(Framework.Enums.SearchView.AccountEntry)]
        //[ControlType(Framework.Enums.ControlTypes.DataPicker)]
        //[DisplayName("Account")]
        //public long AccountEntryIID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("AccountMapIID")]
        public long SupplierAccountMapIID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("SupplierID")]
        public long SupplierID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("AccountID")]
        public long? AccountID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        public long? ParentAccountID { get; set; }
        public int? GroupID { get; set; }
        public string AccountBehavior { get; set; }


        public static EntitlementMapDTO ToDTO(SupplierAccountEntitlementViewModel vm)
        {
            Mapper<SupplierAccountEntitlementViewModel, EntitlementMapDTO>.CreateMap();
            return Mapper<SupplierAccountEntitlementViewModel, EntitlementMapDTO>.Map(vm);
        }

        public static SupplierAccountEntitlementViewModel ToVM(EntitlementMapDTO dto)
        {
            Mapper<EntitlementMapDTO, SupplierAccountEntitlementViewModel>.CreateMap();
            return Mapper<EntitlementMapDTO, SupplierAccountEntitlementViewModel>.Map(dto);
        }

        public static List<SupplierAccountEntitlementViewModel> DefaultData(List<KeyValueDTO> values)
        {
            var data = new List<SupplierAccountEntitlementViewModel>();

            foreach (var value in values)
            {
                data.Add(new SupplierAccountEntitlementViewModel()
                {
                    EntitlementName = value.Value,
                    EntitlementID = byte.Parse(value.Key)
                });
            }

            return data;
        }

    }
}
