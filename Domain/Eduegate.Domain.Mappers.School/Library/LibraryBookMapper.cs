using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Library
{
    public class LibraryBookMapper : DTOEntityDynamicMapper
    {
        public static LibraryBookMapper Mapper(CallContext context)
        {
            var mapper = new LibraryBookMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LibraryBookDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LibraryBookDTO ToDTO(long IID)
        {
            var returnDTO = new LibraryBookDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.LibraryBooks.Where(x => x.LibraryBookIID == IID)
                    .Include(x => x.LibraryBookMaps)
                    .Include(x => x.LibraryBookCategory)
                    .Include(x => x.LibraryBookCategoryMaps).ThenInclude(x => x.LibraryBookCategory)
                    .AsNoTracking()
                    .FirstOrDefault();

                var listmapBooks = new List<LibraryBookMapDTO>();
                string bookMapedDetails = null;

                List<KeyValueDTO> mapDto = new List<KeyValueDTO>();
                foreach (var BookCategorie in entity.LibraryBookCategoryMaps)
                {
                    mapDto.Add(new KeyValueDTO()
                    {
                        Key = BookCategorie.BookCategoryID.ToString(),
                        Value = BookCategorie.LibraryBookCategory.BookCategoryName
                    });
                }

                returnDTO = new LibraryBookDTO()
                {
                    LibraryBookIID = entity.LibraryBookIID,
                    BookTitle = entity.BookTitle,
                    //BookNumber = entity.BookNumber,
                    ISBNNumber = entity.ISBNNumber,
                    Publisher = entity.Publisher,
                    Author = entity.Author,
                    Subject = entity.Subject,
                    BookCategories = mapDto,
                    RackNumber = entity.RackNumber,
                    Quatity = entity.Quatity,
                    BookPrice = entity.BookPrice,
                    PostPrice = entity.PostPrice,
                    Description = entity.Description,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    BookTypeID = entity.LibraryBookTypeID,
                    BookConditionID = entity.BookConditionID,
                    BookStatusID = entity.BookStatusID,
                    BookCategoryCodeID = entity.BookCategoryCodeID,
                    LibraryBookCategoryCode = entity.BookCategoryCodeID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.BookCategoryCodeID.ToString(),
                        Value = entity.LibraryBookCategory.CategoryCode
                    } : null,
                    BookCategoryName = entity.BookCategoryCodeID.HasValue ? entity.LibraryBookCategory.BookCategoryName : null,
                    BookCodeSequenceNo = entity.BookCodeSequenceNo,
                    BookCode = entity.BookCode,
                    PlaceOfPublication = entity.PlaceOfPublication,
                    Series = entity.Series,
                    Pages = entity.Pages,
                    Acc_No = entity.Acc_No,
                    Year = entity.Year,
                    Edition = entity.Edition,
                    Call_No = entity.Call_No,
                    Bill_No = entity.Bill_No,
                    IsActive = entity.IsActive,
                };

                foreach (var mapBooks in entity.LibraryBookMaps)
                {
                    var bk = new LibraryBookMapDTO()
                    {
                        Acc_No = mapBooks.Acc_No,
                    };

                    listmapBooks.Add(bk);
                    bookMapedDetails = string.Concat(bookMapedDetails, bk.Acc_No, "</br>");
                }

                returnDTO.BookDetails = bookMapedDetails;
            }

            return returnDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LibraryBookDTO;
            MutualRepository mutualRepository = new MutualRepository();
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence(); 

            //convert the dto to entity and pass to the repository.
            var entity = new LibraryBook()
            {
                LibraryBookIID = toDto.LibraryBookIID,
                BookCode = toDto.BookCode,
                BookCodeSequenceNo = toDto.BookCodeSequenceNo,
                BookCategoryCodeID = toDto.BookCategoryCodeID,
                BookTitle = toDto.BookTitle,
                //BookNumber = toDto.BookNumber,
                ISBNNumber = toDto.ISBNNumber,
                Publisher = toDto.Publisher,
                Author = toDto.Author,
                Subject = toDto.Subject,
                Series = toDto.Series,
                RackNumber = toDto.RackNumber,
                PlaceOfPublication = toDto.PlaceOfPublication,
                Quatity = toDto.Quatity,
                BookPrice = toDto.BookPrice,
                PostPrice = toDto.PostPrice,
                Description = toDto.Description,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.LibraryBookIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.LibraryBookIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.LibraryBookIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.LibraryBookIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                LibraryBookTypeID = toDto.BookTypeID,
                BookConditionID = toDto.BookConditionID,
                BookStatusID = toDto.BookStatusID,
                Pages = toDto.Pages,
                //Acc_No = toDto.Acc_No,
                Year = toDto.Year,
                Edition = toDto.Edition,
                Call_No = toDto.Call_No,
                Bill_No = toDto.Bill_No,
                IsActive = toDto.IsActive,
            };

            if (toDto.BookCategories != null)
            {
                foreach (var category in toDto.BookCategories)
                {
                    entity.LibraryBookCategoryMaps.Add(new LibraryBookCategoryMap()
                    {

                        LibraryBookID = toDto.LibraryBookIID,
                        BookCategoryID = long.Parse(category.Key)

                    });
                }
            }

            if (toDto.LibraryBookIID == 0)
            {
                for (int i = 0; i < toDto.Quatity; i++)
                {
                    try
                    {
                        sequence = mutualRepository.GetNextSequence("Library_Book_Acc_No", _context.SchoolID);

                        string accNo = sequence.LastSequence.ToString().PadLeft((int)sequence.ZeroPadding, '0');

                        entity.LibraryBookMaps.Add(new LibraryBookMap()
                        {
                            LibraryBookID = entity.LibraryBookIID,
                            Call_No = toDto.Call_No,
                            Acc_No = sequence.Prefix + accNo,
                        });
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Please generate sequence with 'Library_Book_Acc_No'");
                    }
                }
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.LibraryBooks.Add(entity);
                if (entity.LibraryBookIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    var getMapDatas = dbContext.LibraryBookMaps.Where(x => x.LibraryBookID == entity.LibraryBookIID).AsNoTracking().ToList();

                    if (getMapDatas != null && getMapDatas.Select(y => y.Call_No).FirstOrDefault() != entity.Call_No)
                    {
                        foreach (var updateMap in getMapDatas)
                        {
                            updateMap.Call_No = entity.Call_No;
                            dbContext.Entry(updateMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        //dbContext.SaveChanges();
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.LibraryBookIID));
        }

        //CategoryFetching and Datapassing
        public LibraryBookDTO GetBooKCategoryName(long bookCategoryCodeId)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new LibraryBookDTO();
                var getData = dbContext.LibraryBookCategories.Where(x => x.LibraryBookCategoryID == bookCategoryCodeId).AsNoTracking().FirstOrDefault();
                var getLast = dbContext.LibraryBooks.Where(x => x.BookCategoryCodeID == bookCategoryCodeId).AsNoTracking().ToList();
                var getMax = getLast.Max(a => a.BookCodeSequenceNo);
                var sequenceNo = getMax == null ? 1 : int.Parse(getMax.ToString()) + 1;

                dtos.BookCodeSequenceNo = sequenceNo;
                dtos.BookCategoryName = getData.BookCategoryName;
                dtos.BookCode = getMax == null ? getData.CategoryCode + ".1" : getData.CategoryCode + "." + sequenceNo;
                return dtos;
            }
        }

    }
}