using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Extensions;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.PageRenderer
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "TicketDetails", "CRUDModel.ViewModel", "class='alignleft two-column-header'")]
    [DisplayName("Page Details")]
    public class PageViewModel : BaseMasterViewModel
    {
        public PageViewModel()
        {
            ParentPage = new KeyValueViewModel();
            MasterPage = new KeyValueViewModel();
            BoilerPlates = new List<BoilerPlateGridViewModel>() { new BoilerPlateGridViewModel() {
                 Paramters = new List<BoilerplateParameterViewModel>() { new BoilerplateParameterViewModel() },
                 ReferenceID = null,
            } };
            DeletedBoilerPlates = new List<BoilerPlateGridViewModel>();
            HasReferenceID = false;
        }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("PageID")]
        public long PageID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Site")]
        [LookUp("LookUps.Site")]
        public string SiteID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("PageName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string PageName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("PageType")]
        [LookUp("LookUps.PageType")]
        public string PageType { get; set; }

        public byte PageTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Title")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string Title { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("TemplateName")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string TemplateName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("PlaceHolder")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string PlaceHolder { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ParentPage", "String", false)]
        [CustomDisplay("ParentPage")]
        [LookUp("LookUps.Pages")]
        public KeyValueViewModel ParentPage { get; set; }

        public long ParentPageID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("MasterPage", "String", false)]
        [CustomDisplay("MasterPage")]
        [LookUp("LookUps.Pages")]
        public KeyValueViewModel MasterPage { get; set; }

        public long MasterPageID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<long> ReferenceID { get; set; }
        public bool HasReferenceID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid, "onecol-header-left")]
        [CustomDisplay("Boilerplates")]
        public List<BoilerPlateGridViewModel> BoilerPlates { get; set; }

        public List<BoilerPlateGridViewModel> DeletedBoilerPlates { get; set; }

        public static PageViewModel FromDTO(PageDTO dto)
        {
            PageViewModel vm = new PageViewModel();
            vm.BoilerPlates = new List<BoilerPlateGridViewModel>();

            if (dto.IsNotNull())
            {
                vm.PageID = dto.PageID;
                vm.SiteID = dto.SiteID.ToString();
                vm.PageName = dto.PageName;
                vm.PageType = dto.PageTypeID.ToString();
                vm.Title = dto.Title;
                vm.TemplateName = dto.TemplateName;
                vm.PlaceHolder = dto.PlaceHolder;
                vm.ParentPage = new KeyValueViewModel() { Key = (dto.ParentPageID.HasValue ? dto.ParentPageID.Value.ToString() : null) };
                vm.MasterPage = new KeyValueViewModel() { Key = (dto.MasterPageID.HasValue ? dto.MasterPageID.Value.ToString() : null) };
                vm.CreatedBy = dto.CreatedBy;
                vm.UpdatedBy = dto.UpdatedBy;
                vm.CreatedDate = dto.CreatedDate;
                vm.UpdatedDate = dto.UpdatedDate;
                vm.TimeStamps = dto.TimeStamps;
                vm.CompanyID = dto.CompanyID;
                vm.ReferenceID = dto.ReferenceID.IsNotNull() ? dto.ReferenceID : null ;

                if (dto.PageBoilerPlateMaps.IsNotNull() && dto.PageBoilerPlateMaps.Count > 0)
                {
                    BoilerPlateGridViewModel bpgVM = null;

                    foreach (var bpDTO in dto.PageBoilerPlateMaps)
                    {
                        bpgVM = new BoilerPlateGridViewModel();
                        bpgVM.Paramters = new List<BoilerplateParameterViewModel>();

                        bpgVM.BoilerplateMapIID = bpDTO.PageBoilerplateMapIID;
                        bpgVM.BoilerPlateID = bpDTO.BoilerplateID.Value;
                        bpgVM.BoilerPlate = new KeyValueViewModel() { Key = bpDTO.BoilerplateID.ToString(), Value = bpDTO.Name };
                        bpgVM.PageID = Convert.ToInt32(bpDTO.PageID);
                        bpgVM.ReferenceID = bpDTO.ReferenceID;
                        bpgVM.SerialNumber = Convert.ToInt16(bpDTO.SerialNumber);
                        bpgVM.CreatedBy = bpDTO.CreatedBy;
                        bpgVM.CreatedDate = bpDTO.CreatedDate;
                        bpgVM.UpdatedBy = bpDTO.UpdatedBy;
                        bpgVM.UpdatedDate = bpDTO.UpdatedDate;
                        bpgVM.TimeStamps = bpDTO.TimeStamps;
                        BoilerplateParameterViewModel bpVM = null;

                        if (bpDTO.PageBoilerPlateMapParameters.IsNotNull() && bpDTO.PageBoilerPlateMapParameters.Count > 0)
                        {
                            foreach (var bpParameter in bpDTO.PageBoilerPlateMapParameters)
                            {
                                bpVM = new BoilerplateParameterViewModel();

                                bpVM.BoilerPlateMapParameterIID = Convert.ToInt16(bpParameter.PageBoilerplateMapParameterIID);
                                bpVM.BoilerPlateMapID = Convert.ToInt32(bpParameter.PageBoilerplateMapID);
                                bpVM.ParameterName = bpParameter.ParameterName;
                                bpVM.ParameterValue = bpParameter.ParameterValue;
                                bpVM.Description = bpParameter.Description;
                                bpVM.CreatedBy = bpParameter.CreatedBy;
                                bpVM.CreatedDate = bpParameter.CreatedDate;
                                bpVM.UpdatedBy = bpParameter.UpdatedBy;
                                bpVM.UpdatedDate = bpParameter.UpdatedDate;
                                bpVM.TimeStamps = bpParameter.TimeStamps;

                                bpgVM.Paramters.Add(bpVM);
                            }                           
                        }

                        if(bpDTO.PageBoilerPlateMapParameters.IsNull())
                        {
                            bpDTO.PageBoilerPlateMapParameters = new List<PageBoilerPlateMapParameterDTO>();
                        }

                        //merge the new parameter which is missing in the mapping
                        foreach (var parameter in dto.BoilerPlates.Find(a => a.BoilerPlateID == bpDTO.BoilerplateID).BoilerPlateParameters)
                        {
                            var existsParameter = bpgVM.Paramters.Find(a => a.ParameterName == parameter.ParameterName);

                            if (existsParameter == null)
                            {
                                bpVM = new BoilerplateParameterViewModel();

                                bpVM.ParameterName = parameter.ParameterName;
                                bpVM.ParameterValue = parameter.ParameterValue;
                                bpVM.Description = parameter.Description;
                                bpVM.CreatedBy = parameter.CreatedBy;
                                bpVM.CreatedDate = parameter.CreatedDate;
                                bpVM.UpdatedBy = parameter.UpdatedBy;
                                bpVM.UpdatedDate = parameter.UpdatedDate;
                                bpVM.TimeStamps = parameter.TimeStamps;
                                bpgVM.Paramters.Add(bpVM);
                            }
                        }

                        vm.BoilerPlates.Add(bpgVM);
                    }
                }
                else
                {
                    var emptyBoilerPlateGrid = new BoilerPlateGridViewModel();
                    emptyBoilerPlateGrid.Paramters = new List<BoilerplateParameterViewModel>();
                    emptyBoilerPlateGrid.Paramters.Add(new BoilerplateParameterViewModel());
                    emptyBoilerPlateGrid.PageID = Convert.ToInt64(dto.PageTypeID);
                    vm.BoilerPlates.Add(emptyBoilerPlateGrid);
                }
            }

            return vm;
        }

        public static PageDTO ToDTO(PageViewModel vm)
        {
            PageDTO pageDTO = new PageDTO();
            pageDTO.PageBoilerPlateMaps = new List<PageBoilerPlateMapDTO>();
            pageDTO.DeletedBoilerPlates = new List<PageBoilerPlateMapDTO>();

            if (vm.IsNotNull())
            {
                pageDTO.PageID = vm.PageID;
                pageDTO.SiteID = !string.IsNullOrEmpty(vm.SiteID) ? Convert.ToInt32(vm.SiteID) : (int?)null;
                pageDTO.PageName = vm.PageName;
                pageDTO.PageTypeID = !string.IsNullOrEmpty(vm.PageType) ? Convert.ToByte(vm.PageType) : (byte?)null;
                pageDTO.Title = vm.Title;
                pageDTO.TemplateName = vm.TemplateName;
                pageDTO.PlaceHolder = vm.PlaceHolder;
                pageDTO.ParentPageID = string.IsNullOrEmpty(vm.ParentPage.Key) ? null : (long?)long.Parse(vm.ParentPage.Key);
                pageDTO.MasterPageID = string.IsNullOrEmpty(vm.MasterPage.Key) ? null : (long?)long.Parse(vm.MasterPage.Key);
                pageDTO.CreatedBy = vm.CreatedBy;
                pageDTO.UpdatedBy = vm.UpdatedBy;
                pageDTO.CreatedDate = vm.CreatedDate;
                pageDTO.UpdatedDate = vm.UpdatedDate;
                pageDTO.TimeStamps = vm.TimeStamps;
                pageDTO.CompanyID = vm.CompanyID;

                if (vm.ReferenceID > 0)
                {
                    pageDTO.ReferenceID = vm.ReferenceID;
                }
               

                if (vm.BoilerPlates.IsNotNull() && vm.BoilerPlates.Count > 0)
                {
                    PageBoilerPlateMapDTO bpDTO = null;

                    foreach(var bpgVM in vm.BoilerPlates.Where(a=> a.BoilerPlateID != 0))
                    {
                        bpDTO = new PageBoilerPlateMapDTO();
                        bpDTO.PageBoilerPlateMapParameters = new List<PageBoilerPlateMapParameterDTO>();

                        bpDTO.PageBoilerplateMapIID = bpgVM.BoilerplateMapIID;
                        bpDTO.BoilerplateID = bpgVM.BoilerPlate.IsNotNull() ? Convert.ToInt32(bpgVM.BoilerPlate.Key) : 0;
                        bpDTO.PageID = bpgVM.PageID;
                        bpDTO.ReferenceID = bpgVM.ReferenceID;
                        bpDTO.SerialNumber = bpgVM.SerialNumber;
                        //bpDTO.CreatedBy = bpgVM.CreatedBy;
                        //bpDTO.CreatedDate = bpgVM.CreatedDate;
                        //bpDTO.UpdatedBy = bpgVM.UpdatedBy;
                        //bpDTO.UpdatedDate = bpgVM.UpdatedDate;
                        //bpDTO.TimeStamps = bpgVM.TimeStamps;

                        if(bpgVM.Paramters.IsNotNull() && bpgVM.Paramters.Count > 0)
                        {
                            PageBoilerPlateMapParameterDTO bpParameter = null;

                            foreach(var bppVM in bpgVM.Paramters)
                            {
                                bpParameter = new PageBoilerPlateMapParameterDTO();

                                bpParameter.PageBoilerplateMapParameterIID = bppVM.BoilerPlateMapParameterIID;
                                bpParameter.PageBoilerplateMapID = bppVM.BoilerPlateMapID;
                                bpParameter.ParameterName = bppVM.ParameterName;
                                bpParameter.ParameterValue = bppVM.ParameterValue;
                                bpParameter.CreatedBy = bppVM.CreatedBy;
                                bpParameter.CreatedDate = bppVM.CreatedDate;
                                bpParameter.UpdatedBy = bppVM.UpdatedBy;
                                bpParameter.UpdatedDate = bppVM.UpdatedDate;
                                bpParameter.TimeStamps = bppVM.TimeStamps;

                                bpDTO.PageBoilerPlateMapParameters.Add(bpParameter);
                            }
                        }

                        pageDTO.PageBoilerPlateMaps.Add(bpDTO);

                    }
                }
            }

            return pageDTO;
        }

    }
}
