using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "DocumentNos", "CRUDModel.ViewModel.DocumentNumber.DocumentNos")]
    [DisplayName("DocumentNo")]
    public class DocumentNosViewModel : BaseMasterViewModel
    {
        public DocumentNosViewModel()
        {
          
        }
        public int DocumentTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(4, ErrorMessage = "Maximum Length should be within 4!")]
        [DisplayName("Year")]
        public int? Year { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Months")]
        [DisplayName("Month")]
        public string Month { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("LastTransactionNo")]
        public long? LastTransactionNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.DocumentNumber.DocumentNos[0], CRUDModel.ViewModel.DocumentNumber.DocumentNos)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.DocumentNumber.DocumentNos[0], CRUDModel.ViewModel.DocumentNumber.DocumentNos)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        public static List<DocumentNosViewModel> FromDTO(List<DocumentTypeTransactionNumberDTO> dtos)
        {
            var vm = new List<DocumentNosViewModel>();

            if (dtos != null && dtos.Count > 0)
            {
                foreach (var dto in dtos)
                {
                    if (dto.DocumentTypeID == 0) continue;

                    vm.Add(new DocumentNosViewModel()
                    {
                        DocumentTypeID = dto.DocumentTypeID,
                        Month = dto.Month.ToString(),//new KeyValueViewModel { Key = dto.Month, Value = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dto.Month) },
                        Year = dto.Year,
                        LastTransactionNo = dto.LastTransactionNo,
                    });
                }
            }

            else
            {
                vm.Add(new DocumentNosViewModel());
            }

            return vm;
        }

        public static List<DocumentTypeTransactionNumberDTO> ToDTO(List<DocumentNosViewModel> vms, long documentTypeId)
        {
            var dto = new List<DocumentTypeTransactionNumberDTO>();

            if (vms != null && vms.Count > 0)
            {
                foreach (var vm in vms)
                {
                    if (vm.DocumentTypeID == 0) continue;

                    dto.Add(new DocumentTypeTransactionNumberDTO()
                    {
                        DocumentTypeID = (int)documentTypeId,
                        Month = Convert.ToInt32( vm.Month),
                        Year = vm.Year,
                        LastTransactionNo = vm.LastTransactionNo,
                    });
                }
                return dto;
            }
            else
            {
                return null;
            }
        }
    }
}
