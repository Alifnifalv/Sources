using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.Common
{
    public class ViewMapper : IDTOEntityMapper<ViewDTO, View>
    {
        private CallContext _callContext;

        public static ViewMapper Mapper(CallContext _context = null)
        {
            var mapper = new ViewMapper();
            mapper._callContext = _context;
            return mapper;
        }

        public List<ViewDTO> ToDTO(List<View> entities)
        {
            var dtos = new List<ViewDTO>();

            foreach(var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public ViewDTO ToDTO(View entity)
        {
            return new ViewDTO()
            {
                ViewID = entity.ViewID,
                ChildViewID = entity.ChildViewID,
                ChildFilterField = entity.ChildFilterField,
                ControllerName = entity.ControllerName,
                HasChild = entity.HasChild,
                IsEditable = entity.IsEditable,
                IsGenericCRUDSave = entity.IsGenericCRUDSave,
                IsMasterDetail = entity.IsMasterDetail,
                IsMultiLine = entity.IsMultiLine,
                IsReloadSummarySmartViewAlways = entity.IsReloadSummarySmartViewAlways,
                IsRowCategory = entity.IsRowCategory,
                IsRowClickForMultiSelect = entity.IsRowClickForMultiSelect,
                JsControllerName = entity.JsControllerName,
                PhysicalSchemaName = entity.PhysicalSchemaName,
                ViewFullPath = entity.ViewFullPath,
                ViewName = entity.ViewName,
                ViewTypeID = entity.ViewTypeID
            };
        }

        public View ToEntity(ViewDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
