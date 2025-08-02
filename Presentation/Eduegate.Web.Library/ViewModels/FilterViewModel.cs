using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eduegate.Services.Contracts.Metadata;

namespace Eduegate.Web.Library.ViewModels
{
    public class FilterViewModel
    {
        public Eduegate.Infrastructure.Enums.SearchView View  {get;set;}
        public string ViewName { get; set; }

        public List<FilterColumnViewModel> Columns { get; set; }
        public List<FilterUserValueDTO> UserValues { get; set; }

        public string JsonModel
        {
            get
            {
                return JsonConvert.SerializeObject(FilterValueViewModel.FromModel(this.View, Columns, UserValues));
            }
        }
    }
}