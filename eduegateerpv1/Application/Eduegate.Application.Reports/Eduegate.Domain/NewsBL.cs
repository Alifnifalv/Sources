using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Domain
{
    public class NewsBL
    {
        private CallContext _callContext;
        private NewsRepository repository = new NewsRepository();

        public NewsBL(CallContext context)
        {
            _callContext = context;
        }

        public List<NewsDTO> GetNews(NewsTypes type, int pageSize, int pageNumber)
        {
            var dtos = new List<NewsDTO>();

            foreach (var entity in repository.GetNews(type, pageSize, pageNumber))
            {
                dtos.Add(FromEntity(entity));
            }
            return dtos;;
        }

        public NewsDTO GetNews(long newsID)
        {
            return FromEntity(repository.GetNews(newsID)); ;
        }

        public NewsDTO SaveNews(NewsDTO dto)
        {
            var updatedEntity = repository.SaveNews(ToEntity(dto, _callContext));
            return FromEntity(updatedEntity);
        }

        public static NewsDTO FromEntity(News entity)
        {
            return new NewsDTO()
            {
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                NewsIID = entity.NewsIID,
                UpdatedDate = entity.UpdatedDate,
                TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                NewsType = (NewsTypes)entity.NewsTypeID.Value,
                NewsContent = entity.NewsContent,
                NewsContentShort = entity.NewsContentShort,
                Date = entity.CreatedDate.HasValue ? entity.CreatedDate.Value.ToLongDateString() : null,
                ImageUrl = entity.ThumbnailUrl,
                Name = entity.Title,
                UpdatedBy = entity.UpdatedBy,
                CompanyID = entity.CompanyID
            };
        }

        public static News ToEntity(NewsDTO dto, CallContext _callContext)
        {
            var entity = new News()
            {
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                NewsIID = dto.NewsIID,
                UpdatedDate = dto.UpdatedDate,
                TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                NewsTypeID = (int)dto.NewsType,
                NewsContent = dto.NewsContent,
                NewsContentShort = dto.NewsContentShort,
                ThumbnailUrl = dto.ImageUrl,
                Title = dto.Name,
                UpdatedBy = dto.UpdatedBy,
                CompanyID = dto.CompanyID
            };

            if (entity.NewsIID == 0)
            {
                entity.CreatedBy = int.Parse(_callContext.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }
            if (entity.CompanyID == 0)
            {
                entity.CompanyID = _callContext.CompanyID;
            }

            return entity;
        }

    }
}
