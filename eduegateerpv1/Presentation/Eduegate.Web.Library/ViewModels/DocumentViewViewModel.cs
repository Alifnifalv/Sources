using System.Collections.Generic;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    public class DocumentViewViewModel 
    {
        public DocumentViewViewModel()
        {
            Documents = new List<DocumentFileViewModel>() { new DocumentFileViewModel() };
        }

        public List<DocumentFileViewModel> Documents { get; set; }
        public string ReferenceParameterName { get { return "referenceID"; } }
        public string EntityParameterName { get { return "entityType"; } }

        public static DocumentViewDTO ToDTO(DocumentViewViewModel vm)
        {
            Mapper<DocumentViewDTO, DocumentViewViewModel>.CreateMap();
            return Mapper<DocumentViewViewModel, DocumentViewDTO>.Map(vm);
        }

        public static DocumentViewViewModel FromDTO(DocumentViewDTO dto)
        {
            Mapper<DocumentViewViewModel, DocumentViewDTO>.CreateMap();
            return Mapper<DocumentViewDTO, DocumentViewViewModel>.Map(dto);
        }
    }
}