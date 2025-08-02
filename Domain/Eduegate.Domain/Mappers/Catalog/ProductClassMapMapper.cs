using Newtonsoft.Json;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Linq;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Entity;
using System.Data.Entity;
using Eduegate.Domain.Entity.School.Models.School;

namespace Eduegate.Domain.Mappers.Catalog
{
    public class ProductClassMapMapper : DTOEntityDynamicMapper
    {

        public static ProductClassMapMapper Mapper(CallContext context)
        {
            var mapper = new ProductClassMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ProductClassMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ProductClassMapDTO;
            var entity = new ProductClassMap();

            using (var dbContext = new dbEduegateERPContext())
            {
                if (toDto.ProductClassMapIID == 0)
                {
                    //duplicate validation
                    var oldData = dbContext.ProductClassMaps.Where(x => x.ClassID == toDto.ClassID && x.AcademicYearID == toDto.AcademicYearID).AsNoTracking().ToList();

                    if (oldData.Count > 0)
                    {
                        throw new Exception("The Same Class already exists for this academic, Please try with different or use edit option");
                    }
                }

                var IIDs = toDto.ProductClassMaps
                .Select(a => a.ProductClassMapIID).ToList();

                //delete maps
                var entities = dbContext.ProductClassMaps.Where(x =>
                    x.ClassID == toDto.ClassID && x.AcademicYearID == toDto.AcademicYearID &&
                    !IIDs.Contains(x.ProductClassMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.ProductClassMaps.RemoveRange(entities);

                dbContext.SaveChanges();
            }

            foreach (var prdtClsMap in toDto.ProductClassMaps)
            {
                entity = new ProductClassMap()
                {
                    ProductClassMapIID = prdtClsMap.ProductClassMapIID,
                    ProductID = prdtClsMap.ProductID,
                    ProductSKUMapID = prdtClsMap.ProductSKUMapID,
                    //ProductClassTypeID = prdtClsMap.ProductTypeID,
                    ClassID = toDto.ClassID,
                    FeeMasterID = prdtClsMap.FeeMasterID,
                    AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : _context.AcademicYearID,
                    IsActive = toDto.IsActive,
                    //StreamID = prdtClsMap.StreamID,
                    SubjectID = prdtClsMap.SubjectID,
                    CreatedBy = toDto.ProductClassMapIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.ProductClassMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.ProductClassMapIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                    UpdatedDate = toDto.ProductClassMapIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                };

                using (var dbContext = new dbEduegateERPContext())
                {
                    dbContext.ProductClassMaps.Add(entity);

                    if (entity.ProductClassMapIID == 0)
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

            return ToDTOString(ToDTO(Convert.ToInt64(entity.ProductClassMapIID)));

        }

        private ProductClassMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.ProductClassMaps.Where(x => x.ProductClassMapIID == IID).AsNoTracking().FirstOrDefault();

                var mapEntity = dbContext.ProductClassMaps.Where(x => x.ClassID == entity.ClassID && x.AcademicYearID == entity.AcademicYearID).AsNoTracking().ToList();

                var classes = dbContext.Classes.Where(x => x.ClassID == entity.ClassID).AsNoTracking().FirstOrDefault();

                var academicyear = dbContext.AcademicYears.Where(x => x.AcademicYearID == entity.AcademicYearID).AsNoTracking().FirstOrDefault();

                var prdtClassMap = new ProductClassMapDTO()
                {
                    ProductClassMapIID = entity.ProductClassMapIID,
                    ClassID = entity.ClassID,
                    ClassName = classes.ClassDescription,
                    AcademicYearID = entity.AcademicYearID,
                    AcademicYearName = academicyear.Description + " (" + academicyear.AcademicYearCode + ")",
                    IsActive = entity.IsActive,
                };


                foreach (var map in mapEntity)
                {
                    using (var _sContext = new dbEduegateSchoolContext())
                    {
                        var prdct = dbContext.Products.Where(x => x.ProductIID == map.ProductID).AsNoTracking().FirstOrDefault();
                        //var feemaster = dbContext.Fee.Where(x => x.AcademicYearID == entity.AcademicYearID).AsNoTracking().FirstOrDefault();
                        //var prodctType = dbContext.ProductClassTypes.Where(x => x.ProductClassTypeIID == map.ProductClassTypeID).AsNoTracking().FirstOrDefault();
                        var feeMaster = _sContext.FeeMasters.Where(x => x.FeeMasterID == map.FeeMasterID).AsNoTracking().FirstOrDefault();
                        var streams = _sContext.Streams.Where(x => x.StreamID == map.StreamID).AsNoTracking().FirstOrDefault();

                        var subject = _sContext.Subjects.Where(x => x.SubjectID == map.SubjectID).AsNoTracking().FirstOrDefault();
                        var subjectType = subject != null ? _sContext.SubjectTypes.Where(x => x.SubjectTypeID == subject.SubjectTypeID).AsNoTracking().FirstOrDefault() : null;

                        prdtClassMap.ProductClassMaps.Add(new ProductClassMapDetailDTO()
                        {
                            ProductClassMapIID = map.ProductClassMapIID,
                            ProductID = map.ProductID,
                            ProductSKUMapID = map.ProductSKUMapID,
                            ProductName = prdct.ProductName,
                            FeeMasterID = map.FeeMasterID,
                            FeeMasterName = feeMaster?.Description,
                            //ProductTypeID = map.ProductClassTypeID,
                            //ProductTypeName = prodctType.ProductClassTypeName,
                            StreamID = map.StreamID,
                            StreamName = streams?.Description,
                            SubjectID = map.SubjectID,
                            SubjectName = subject?.SubjectName,
                            SubjectTypeName = subjectType?.TypeName,
                        });
                    }
                }

                return prdtClassMap;
                //return null;
            }
        }

    }
}