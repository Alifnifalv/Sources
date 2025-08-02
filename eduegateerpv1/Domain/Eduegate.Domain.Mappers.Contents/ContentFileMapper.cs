using Eduegate.Domain.Entity.Contents;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Contents;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.Contents
{
    public class ContentFileMapper : DTOEntityDynamicMapper
    {
        public static ContentFileMapper Mapper(CallContext context)
        {
            var mapper = new ContentFileMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ContentFileDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public ContentFileDTO ReadContentsById(long IID)
        {
            return ToDTO(IID);
        }

        public ContentFileDTO GetSaveFile(Eduegate.Services.Contracts.Contents.Enums.ContentType contentType, long referenceID)
        {
            return null;
        }

        private ContentFileDTO ToDTO(long IID)
        {
            using (dbContentContext dbContext = new dbContentContext())
            {
                var entity = dbContext.ContentFiles
                    .Include(x => x.ContentType)
                    //.Include(x => x.GalleryAttachmentMaps)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.ContentFileIID  == IID);

                if (entity != null)
                {
                    return new ContentFileDTO()
                    {
                        ContentFileIID = entity.ContentFileIID,
                        ContentData = entity.ContentData,
                        ContentFileName = entity.ContentFileName,
                        ContentTypeID = entity.ContentTypeID,
                        ReferenceID = entity.ReferenceID,
                        IsCompressed = entity.IsCompressed,
                        //CreatedBy = entity.ContentFileIID == 0 ? (int)_context.LoginID : entity.CreatedBy,
                        //UpdatedBy = entity.ContentFileIID > 0 ? (int)_context.LoginID : entity.UpdatedBy,
                        //CreatedDate = entity.ContentFileIID == 0 ? DateTime.Now : entity.CreatedDate,
                        //UpdatedDate = entity.ContentFileIID > 0 ? DateTime.Now : entity.UpdatedDate,
                        ////TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        public ContentFileDTO SaveEntity(ContentFileDTO dto)
        {
            var entity = SaveContentFile(dto);

            return ToDTO(entity.ContentFileIID);
        }

        public List<ContentFileDTO> SaveEntity(List<ContentFileDTO> dtos)
        {
            var newDtos = new List<ContentFileDTO>();
            foreach (var dto in dtos)
            {
                var entity = SaveContentFile(dto);
                newDtos.Add(ToDTO(entity.ContentFileIID));
            }

            return newDtos;
        }

        public long DeleteEntity(long contentID)
        {
            try
            {
                using (dbContentContext dbContext = new dbContentContext())
                {
                    var data = dbContext.ContentFiles.AsNoTracking().FirstOrDefault(x => x.ContentFileIID == contentID);

                    dbContext.ContentFiles.Remove(data);
                    dbContext.SaveChanges();
                }
            }
            catch
            {
                return contentID;
            }
            return contentID;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var entity = SaveContentFile(dto);
            return ToDTOString(ToDTO(entity.ContentFileIID));
        }

        public Eduegate.Domain.Entity.Contents.ContentFile SaveContentFile(BaseMasterDTO dto)
        {
            var toDto = dto as ContentFileDTO;
            var entity = new Eduegate.Domain.Entity.Contents.ContentFile();

            try
            {   
                //convert the dto to entity and pass to the repository.
                entity = new Eduegate.Domain.Entity.Contents.ContentFile()
                {
                    ContentFileIID = toDto.ContentFileIID,
                    ContentData = toDto.ContentData,
                    ContentFileName = toDto.ContentFileName,
                    ContentTypeID = toDto.ContentTypeID,
                    ReferenceID = toDto.ReferenceID,
                    IsCompressed = toDto.IsCompressed,
                    CreatedBy = toDto.ContentFileIID > 0 ? (int)_context?.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.ContentFileIID > 0 ? (int)_context?.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.ContentFileIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.ContentFileIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                using (dbContentContext dbContext = new dbContentContext())
                {
                    //dbContext.ContentFiles.Add(entity);
                    if (entity.ContentFileIID == 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }

                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
            }

            return entity;
        }

        public List<ContentFileDTO> SaveBulkContentFiles(List<ContentFileDTO> fileDTOs)
        {
            var contentFileDto = new List<ContentFileDTO>();
            var entityList = new List<Eduegate.Domain.Entity.Contents.ContentFile>();
            var ids = new List<long?>();
            ids = fileDTOs.Select(x => x.ReferenceID).ToList();

            try
            {
                using (dbContentContext dbContext = new dbContentContext())
                {
                    foreach (var toDto in fileDTOs)
                    {
                        //convert the dto to entity and pass to the repository.
                        var entity = new Eduegate.Domain.Entity.Contents.ContentFile()
                        {
                            ContentFileIID = toDto.ContentFileIID,
                            ContentData = toDto.ContentData,
                            ContentFileName = toDto.ContentFileName,
                            ContentTypeID = toDto.ContentTypeID,
                            ReferenceID = toDto.ReferenceID,
                            //CreatedBy = toDto.ContentFileIID == 0 ? (int)_context.LoginID : toDto.CreatedBy,
                            //UpdatedBy = toDto.ContentFileIID > 0 ? (int)_context.LoginID : toDto.UpdatedBy,
                            //CreatedDate = toDto.ContentFileIID == 0 ? DateTime.Now : toDto.CreatedDate,
                            //UpdatedDate = toDto.ContentFileIID > 0 ? DateTime.Now : toDto.UpdatedDate,
                            //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                        };

                        dbContext.ContentFiles.Add(entity);

                        if (entity.ContentFileIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        entityList.Add(entity);
                    }
                    dbContext.SaveChanges();

                    contentFileDto = (from ss in dbContext.ContentFiles
                                      where ids.Contains(ss.ReferenceID)
                                      select new ContentFileDTO() { ContentFileIID = ss.ContentFileIID, ReferenceID = ss.ReferenceID }).ToList();
                }
            }
            //TODO: Need to handle the exception
            //catch (DbEntityValidationException ex)
            //{

            //    foreach (var eve in ex.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
            }

            return contentFileDto;
        }

        public List<ContentFileDTO> GetContentFileList(List<ContentFileDTO> fileDTOs)
        {

            var contentFileDto = new List<ContentFileDTO>();
            var ids = new List<long>();

            try
            {
                using (dbContentContext dbContext = new dbContentContext())
                {
                    ids = fileDTOs.Select(x => x.ContentFileIID).ToList();

                    contentFileDto = (from ss in dbContext.ContentFiles
                                      where ids.Contains(ss.ContentFileIID)
                                      select new ContentFileDTO()
                                      {
                                          ContentFileIID = ss.ContentFileIID,
                                          ReferenceID = ss.ReferenceID,
                                          ContentData = ss.ContentData,
                                          ContentFileName = ss.ContentFileName
                                      }).AsNoTracking().ToList();

                }
            }
            //TODO: Need to handle the exception
            //catch (DbEntityValidationException ex)
            //{

            //    foreach (var eve in ex.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
            }

            return contentFileDto;
        }

        public async Task<ContentFileDTO> ReadContentsByIdAsync(long IID)
        {
            return await ToDTOasync(IID);
        }

        private async Task<ContentFileDTO> ToDTOasync(long IID)
        {
            using (var dbContext = new dbContentContext())
            {
                var entity = await dbContext.ContentFiles
                    .Include(x => x.ContentType)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ContentFileIID == IID);

                if (entity != null)
                {
                    return new ContentFileDTO()
                    {
                        ContentFileIID = entity.ContentFileIID,
                        ContentData = entity.ContentData,
                        ContentFileName = entity.ContentFileName,
                        ContentTypeID = entity.ContentTypeID,
                        ReferenceID = entity.ReferenceID,
                        //CreatedBy = entity.ContentFileIID == 0 ? (int)_context.LoginID : entity.CreatedBy,
                        //UpdatedBy = entity.ContentFileIID > 0 ? (int)_context.LoginID : entity.UpdatedBy,
                        //CreatedDate = entity.ContentFileIID == 0 ? DateTime.Now : entity.CreatedDate,
                        //UpdatedDate = entity.ContentFileIID > 0 ? DateTime.Now : entity.UpdatedDate,
                        ////TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    };
                }
                else
                {
                    return null;
                }
            }
        }

    }
}