using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Addresses", "CRUDModel.ViewModel.Addresses")]
    [DisplayName("Addresses")]
    public class AddressViewModel : BaseMasterViewModel
    {
        public long StudentMiscDetailsIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, Attributes = "ng-change='GuardianAddressAsCurrentAddress($event, $element,CRUDModel.ViewModel)'")]
        [CustomDisplay("GuardianAddressisCurrentAddress")]
        public bool? IsCurrentAddresIsGuardian { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextArea)]
        //[DisplayName("Current Address")]
        //public string CurrentAddress { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("PermanentAddressisCurrentAddress")]
        public bool? IsPermenentAddresIsCurrent { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextArea)]
        //[DisplayName("Permenent Address")]
        //public string PermenentAddress { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine11 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [CustomDisplay("BuildingNo")]
        public string PermenentBuildingNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [CustomDisplay("Flat/UnitNo")]
        public string PermenentFlatNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [CustomDisplay("StreetNo")]
        public string PermenentStreetNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(25, ErrorMessage = "Maximum Length should be within 25!")]
        [CustomDisplay("StreetName")]
        public string PermenentStreetName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [CustomDisplay("Zone/LocationNo")]
        public string PermenentLocationNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(25, ErrorMessage = "Maximum Length should be within 25!")]
        [CustomDisplay("Zone/LocationName")]
        public string PermenentLocationName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [CustomDisplay("ZipNo")]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        public string PermenentZipNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [CustomDisplay("PostBoxNo")]
        [RegularExpression(@"^[0-9*#+]+$", ErrorMessage = "Use  digits only")]
        public string PermenentPostBoxNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(25, ErrorMessage = "Maximum Length should be within 25!")]
        [CustomDisplay("City")]
        public string PermenentCity { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Country")]
        [LookUp("LookUps.Countries")]
        public string PermenentCountry { get; set; }
        public int? PermenentCountryID { get; set; }
    }
}
