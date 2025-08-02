using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels.Payroll;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "EmployeeDetails", "CRUDModel.ViewModel.Employee")]
    [DisplayName("Supplier")]
    public class EmployeeDetailViewModel : BaseMasterViewModel
    {
        [DataPicker("Employee")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [CustomDisplay("EmployeeID")]
        public long? EmployeeIID { get; set; }
        public string TitleID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
  
        public string LastName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("EmployeeCode")]
        public string EmployeeCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("EmployeeName")]
        public string EmployeeName { get; set; }


        public static EmployeeViewModel FromDTO(EmployeeDTO dto)
        {
            return new EmployeeViewModel()
            {
                EmployeeIID = dto.EmployeeIID,
                EmployeeName = dto.FirstName + " " + dto.MiddleName + " " + dto.LastName,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                EmployeeCode = dto.EmployeeCode,
            };
        }

        public static EmployeeDTO ToDTO(EmployeeViewModel vm)
        {
            return new EmployeeDTO()
            {
                EmployeeIID = vm.EmployeeIID,
            };
        }
    }
}