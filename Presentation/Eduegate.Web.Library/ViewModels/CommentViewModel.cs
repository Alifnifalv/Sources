using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Web.Library.ViewModels
{
    public class CommentViewModel : BaseMasterViewModel
    {
        public CommentViewModel()
        {
            //Comments = new List<CommentViewModel>();
            IsEdit = false;
        }

        public long CommentIID { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public string CommentText { get; set; }
        public string Username { get; set; }
        public Eduegate.Infrastructure.Enums.EntityTypes EntityType { get; set; }
        public long ReferenceID { get; set; }
        public bool IsEdit { get; set; }

        public string CommentDate { get; set; }
       
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Departments")]
        [DisplayName("Department")]
        public List<KeyValueViewModel> Department { get; set; }

        public Nullable<long> DepartmentID { get; set; }


        public static CommentViewModel FromDTO(CommentDTO dto)
        {
            Mapper<CommentDTO, CommentViewModel>.CreateMap();
            var mapper = Mapper<CommentDTO, CommentViewModel>.Map(dto);

            mapper.EntityType = (Eduegate.Infrastructure.Enums.EntityTypes)(int)mapper.EntityType;
            mapper.CommentDate = mapper.CreatedDate.HasValue ? mapper.CreatedDate.Value.ToString() : null;

            return mapper;
        }

        public static CommentDTO ToDTO(CommentViewModel vm)
        {
            Mapper<CommentViewModel, CommentDTO>.CreateMap();
            var map = Mapper<CommentViewModel, CommentDTO>.Map(vm);

            map.EntityType = (Eduegate.Framework.Contracts.Common.Enums.EntityTypes)(int)vm.EntityType;

            return map;
        }
    }
}