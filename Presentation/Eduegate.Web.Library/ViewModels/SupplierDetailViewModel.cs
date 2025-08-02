using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierDtails", "CRUDModel.ViewModel.Supplier")]
    [DisplayName("Supplier")]
    public class SupplierDetailViewModel : BaseMasterViewModel
    {
        [DataPicker("Supplier")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("Supplier ID")]
        public long SupplierIID { get; set; }
        public string TitleID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
  
        public string LastName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Supllier Name")]
        public string SupplierName { get; set; }

        public static SupplierViewModel FromDTO(SupplierDTO dto)
        {
            return new SupplierViewModel()
            {
                SupplierIID = dto.SupplierIID,
                FirstName = dto.FirstName,
                TitleID = (dto.TitleID.HasValue ? dto.TitleID.Value : 0).ToString(),
                TimeStamps = dto.TimeStamps,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                SupplierName = dto.FirstName + ' ' + dto.MiddleName + ' ' + dto.LastName
            };
        }

        public static SupplierDTO ToDTO(SupplierViewModel vm)
        {
            return new SupplierDTO()
            {
                SupplierIID = vm.SupplierIID,
            };
        }
    }
}
