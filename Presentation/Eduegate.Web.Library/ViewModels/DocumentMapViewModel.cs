using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "DocumentMaps", "CRUDModel.ViewModel.DocumentMap.DocumentMaps")]
    [DisplayName("")]
    public class DocumentMapViewModel : BaseMasterViewModel
    {
        public DocumentMapViewModel()
        {
            DocumentType = new KeyValueViewModel();
        }


        public long DocumentTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [LookUp("LookUps.DocumentTypes")]
        [Select2("DocumentType", "String", false, "", false, "ng-click=LoadDocumentLookup('DocumentTypes','Stock')")]
        [DisplayName("Document Type")]
        public KeyValueViewModel DocumentType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='InsertGridRow($index, ModelStructure.SchedulerInfo.Schedulers[0], CRUDModel.ViewModel.DocumentMap.DocumentMaps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='RemoveGridRow($index, ModelStructure.SchedulerInfo.Schedulers[0], CRUDModel.ViewModel.DocumentMap.DocumentMaps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        public Nullable<int> DocumentTypeMapID { get; set; }

        public static List<DocumentMapViewModel> FromDTO(List<DocumentTypeTypeDTO> dtos)
        {
            var vm = new List<DocumentMapViewModel>();

            if (dtos != null && dtos.Count > 0)
            {
                foreach (var dto in dtos)
                {
                    if (dto.DocumentTypeID == 0) continue;

                    vm.Add(new DocumentMapViewModel()
                    {
                        DocumentTypeID = (int)dto.DocumentTypeID,
                        DocumentTypeMapID = dto.DocumentTypeMapID,
                        DocumentType = new KeyValueViewModel() { Key = (dto.DocumentTypeMapID).ToString(), Value = dto.DocumentTypeMapName.ToString() },
                        CreatedBy = dto.CreatedBy,
                        CreatedDate = dto.CreatedDate,
                        UpdatedBy = dto.UpdatedBy,
                        UpdatedDate = dto.UpdatedDate,
                        TimeStamps = dto.TimeStamps,
                    });
                }
            }

            else
            {
                vm.Add(new DocumentMapViewModel());
            }

            return vm;
        }

        public static List<DocumentTypeTypeDTO> ToDTO(List<DocumentMapViewModel> vms,long documentTypeId)
        {
            var dto = new List<DocumentTypeTypeDTO>();

            if (vms != null && vms.Count > 0)
            {
                foreach (var vm in vms)
                {
                    if (vm.DocumentTypeID == 0) continue;

                    dto.Add(new DocumentTypeTypeDTO()
                    {
                        DocumentTypeID = (int)documentTypeId,
                        DocumentTypeMapID = Convert.ToInt32(vm.DocumentType.Key),
                        CreatedBy = vm.CreatedBy,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = vm.UpdatedDate,
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
