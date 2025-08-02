using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ProductClassMap", "CRUDModel.ViewModel.ProductClassMap")]
    [DisplayName("Product Class Map")]
    public class ProductClassMapDetailViewModel : BaseMasterViewModel
    {
        public ProductClassMapDetailViewModel()
        {
            Product = new KeyValueViewModel();
            FeeMaster = new KeyValueViewModel();
            SubjectType = new KeyValueViewModel();
            Subject = new KeyValueViewModel();
            //ProductType = new KeyValueViewModel();
            //Streams = new KeyValueViewModel();
        }
        public long ProductClassMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ClassMapProducts", "Numeric", false, "")]
        [LookUp("LookUps.ClassMapProducts")]
        [CustomDisplay("Product")]
        public KeyValueViewModel Product { get; set; }
        public long ProductID { get; set; }
        public long ProductSKUMapID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ClassMapFeeMasters", "Numeric", false, "", false)]
        [CustomDisplay("FeeMaster")]
        [LookUp("LookUps.ClassMapFeeMasters")]
        public KeyValueViewModel FeeMaster { get; set; }
        public int? FeeMasterID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SubjectType", "String", false, "GetSubjectBySubjectType(gridModel)")]
        [CustomDisplay("SubjectType")]
        [LookUp("LookUps.SubjectType")]
        public KeyValueViewModel SubjectType { get; set; }
        public int SubjectTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", false, "")]
        [CustomDisplay("Subject")]
        [LookUp("LookUps.Subject")]
        public KeyValueViewModel Subject { get; set; }
        public int SubjectID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("ProductClassType", "Numeric", false, "")]
        //[CustomDisplay("ProductClassType")]
        //[LookUp("LookUps.ProductClassType")]
        //public KeyValueViewModel ProductType { get; set; }
        //public int ProductTypeID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2, optionalAttribs: "ng-disabled='CRUDModel.ViewModel.Class.Value.Contain('12')'")]
        //[Select2("Streams", "Numeric", false, "")]
        //[CustomDisplay("Streams")]
        //[LookUp("LookUps.Streams")]
        //public KeyValueViewModel Streams { get; set; }
        //public int StreamID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.ProductClassMap[0],CRUDModel.ViewModel.ProductClassMap)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index,ModelStructure.ProductClassMap[0],CRUDModel.ViewModel.ProductClassMap)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}