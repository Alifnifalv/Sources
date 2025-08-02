using System;
using System.Collections.Generic;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    public class DataFeedViewModel
    {
        public long DataFeedID { get; set; }
        public Services.Contracts.Enums.DataFeedTypes DataFeedType { get; set; }
        public int DataFeedTypeID { get; set; }
        public Eduegate.Services.Contracts.Enums.DataFeedStatus StatusID { get; set; }
        public string FeedName { get; set; }
        public string FeedFileName { get; set; }
        public string StatusName { get; set; }
        public List<DataFeedTypeViewModel> DataFeedTypes { get; set; }

        public static DataFeedViewModel FromDTO(DataFeedLogDTO logDTO, List<DataFeedTypeDTO> dataFeedTypeCollection)
        {
            var dataFeedViewModel = new DataFeedViewModel();
            if (dataFeedTypeCollection.IsNotNull())
            {
                var dataFeedTypeViewModelCollection = new List<DataFeedTypeViewModel>();
                foreach (DataFeedTypeDTO dft in dataFeedTypeCollection)
                    dataFeedTypeViewModelCollection.Add(DataFeedTypeViewModel.FromDTO(dft));

                dataFeedViewModel.DataFeedTypes = dataFeedTypeViewModelCollection;
            }

            if (logDTO.IsNotNull())
            {
                dataFeedViewModel.DataFeedID = logDTO.DataFeedLogID;
                dataFeedViewModel.FeedName = logDTO.DataFeedTypeName;
                dataFeedViewModel.StatusID = logDTO.DataFeedStatusID;
                dataFeedViewModel.StatusName = logDTO.DataFeedStatusName;
                dataFeedViewModel.FeedFileName = logDTO.FileName;
                dataFeedViewModel.DataFeedTypeID = logDTO.DataFeedTypeID;
            }

            return dataFeedViewModel;
        }

        public static DataFeedLogDTO ToDTO(DataFeedViewModel model)
        {
            DataFeedLogDTO dto = new DataFeedLogDTO();
            dto.DataFeedLogID = model.DataFeedID;
            dto.DataFeedTypeID = model.DataFeedTypeID;
            dto.FileName = model.FeedFileName;
            dto.DataFeedStatusID = model.StatusID;
            return dto;
        }

    }
}
