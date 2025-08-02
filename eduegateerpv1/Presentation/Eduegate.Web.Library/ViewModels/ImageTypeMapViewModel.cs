using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Maps", "CRUDModel.ViewModel.ImageMap.Maps")]
    [DisplayName("Image Maps")]
    public class ImageTypeMapViewModel : BaseMasterViewModel
    {
        public long ImageMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "ex-small-col-width")]
        [DisplayName("Image Sequence")]
        public long SerialNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, "medium-col-width")]
        [DisplayName("Type")]
        [LookUp("LookUps.ImageTypes")]
        public string ImageType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [DisplayName("Title")]
        public string ImageTitle { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, "medium-col-width")]
        [LookUp("LookUps.ActionLinkType")]
        [DisplayName("ActionLinkType")]
        public string ActionLinkTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [DisplayName("ImageLinkParameters")]
        public string ImageLinkParameters { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [DisplayName("Target")]
        public string ImageTarget { get; set; }


        [ControlType(Framework.Enums.ControlTypes.FileUpload, "large-col-width")]
        [FileUploadInfo("Mutual/UploadImages", EduegateImageTypes.Temporary, "ImageUrl", "")]
        [DisplayName("Upload")]
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width noellipsis", "ng-click='InsertGridRow($index, ModelStructure.ImageMap.Maps[0], CRUDModel.ViewModel.ImageMap.Maps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width noellipsis", "ng-click='RemoveGridRow($index, ModelStructure.ImageMap.Maps[0], CRUDModel.ViewModel.ImageMap.Maps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
