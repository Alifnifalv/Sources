using Eduegate.Domain.Caches;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums.Schedulers;
using Eduegate.Services.Contracts.Metadata;
using Eduegate.Services.Contracts.Search;
using Eduegate.Services.Contracts.Warehouses;
using System;
using System.Collections.Generic;

namespace Eduegate.Domain
{
    public class MetadataBL
    {
        private CallContext _callContext { get; set; }

        public MetadataBL(CallContext context)
        {
            _callContext = context;
        }

        public List<FilterColumnDTO> GetFilterMetadata(Eduegate.Services.Contracts.Enums.SearchView view, string language = null)
        {
            var filterData = Eduegate.Framework.CacheManager.MemCacheManager<FilterMetaData>.Get("Filter_" + view.ToString() + "_" + language);
            if (filterData != null) return filterData.FilterColumns;
            var dto = new List<FilterColumnDTO>();
            switch (view)
            {

                default:

                    var filters = new MetadataRepository().GetFilterMetadata((SearchView)(long)view, language);

                    foreach (var filter in filters)
                    {
                        var conditions = new List<Services.Contracts.Enums.Conditions>();

                        foreach (var condition in filter.FilterColumnConditionMaps)
                        {
                            conditions.Add((Services.Contracts.Enums.Conditions)condition.ConidtionID);
                        }

                        dto.Add(new FilterColumnDTO()
                        {
                            ColumnCaption = filter.ColumnCaption,
                            ColumnName = filter.ColumnName,
                            ColumnType = (Eduegate.Services.Contracts.Enums.DataTypes)filter.DataTypeID,
                            DefaultValues = filter.DefaultValues,
                            IsQuickFilter = !filter.IsQuickFilter.HasValue ? false : filter.IsQuickFilter.Value,
                            FilterColumnID = filter.FilterColumnID,
                            FilterControlType = filter.UIControlTypeID.HasValue ? (Eduegate.Services.Contracts.Enums.UIControlTypes)filter.UIControlTypeID : Services.Contracts.Enums.UIControlTypes.TextBox,
                            SequenceNo = filter.SequenceNo.Value,
                            FilterConditions = conditions,
                            LookUpID = filter.LookupID,
                            IsLookupLazyLoad = filter.IsLookupLazyLoad.HasValue && filter.IsLookupLazyLoad.Value == true ? filter.IsLookupLazyLoad.Value : false,
                            Attribute1 = filter.Attribute1,
                            Attribute2 = filter.Attribute2,
                        });
                    }
                    break;
            }

            Eduegate.Framework.CacheManager.MemCacheManager<FilterMetaData>.Add(new FilterMetaData() { FilterColumns = dto, View = view }, "Filter_" + view.ToString());
            return dto;

        }

        public bool SaveUserFilterMetadata(UserFilterValueDTO dto)
        {
            var entities = new List<FilterColumnUserValue>();
            dto.LoginID = _callContext.LoginID;

            switch (dto.View)
            {
                default:
                    foreach (var value in dto.ColumnValues)
                    {
                        //delete the existing data

                        entities.Add(new FilterColumnUserValue()
                        {
                            LoginID = dto.LoginID,
                            ViewID = (long)dto.View,
                            FilterColumnID = value.FilterColumnID,
                            ConditionID = (byte)value.FilterCondition,
                            Value1 = value.Value,
                            Value2 = value.Value2,
                            CompanyID = _callContext.CompanyID.HasValue ? (int)_callContext.CompanyID.Value : default(int),
                        });
                    }
                    return new MetadataRepository().SaveUserFilterMetadata((SearchView)(long)dto.View, dto.LoginID, entities, _callContext.CompanyID.HasValue ? (int)_callContext.CompanyID.Value : 1);
                    //break;
            }
        }

        public List<FilterUserValueDTO> GetUserFilterMetadata(Eduegate.Services.Contracts.Enums.SearchView view)
        {
            var userFilter = new List<FilterUserValueDTO>();
            var filterMetadata = new MetadataRepository().GetUserFilterMetadata((SearchView)(long)view, _callContext == null ? null : _callContext.LoginID, _callContext != null && _callContext.CompanyID.HasValue ? (int)_callContext.CompanyID.Value : 1);

            foreach (var metadata in filterMetadata)
            {
                userFilter.Add(new FilterUserValueDTO()
                {
                    Condition = (Eduegate.Services.Contracts.Enums.Conditions)metadata.ConditionID,
                    LoginID = _callContext == null ? null : _callContext.LoginID,
                    FilterColumnID = metadata.FilterColumnID.Value,
                    Value1 = metadata.Value1,
                    Value2 = metadata.Value2,
                    Value3 = metadata.Value3,
                    ViewID = (Eduegate.Services.Contracts.Enums.SearchView)metadata.ViewID,
                    CompanyID = _callContext.CompanyID.HasValue ? (int)_callContext.CompanyID.Value : default(int),
                });
            }

            return userFilter;
        }

        public DocumentTypeDTO GetDocumentType(long documentTypeID, SchedulerTypes? type = null)
        {
            var metaDataRepository = new MetadataRepository();
            var typeID = Convert.ToInt32(documentTypeID);
            var documentType = Mappers.DocumentTypeMapper.Mapper(_callContext).ToDTO(metaDataRepository.GetDocumentType(typeID));

            if (documentType == null) return null;

            documentType.DocumentMaps = Mappers.DocumentTypeMapMapper.Mapper(_callContext).ToDTO(metaDataRepository.GetDocumentMap((int)documentTypeID));

            if (type != null)
            {
                documentType.Schedulers = Mappers.Schedulers.EntitySchedulerMapper.Mapper(_callContext)
                    .ToDTO(new SchedulerRepository().GetEntityScheduler((int)type, documentTypeID.ToString()));
            }

            #region Document Type -  Transaction Number Based on Type   
            if (documentType.TransactionSequenceType == null) documentType.TransactionSequenceType = 1;
            var DocumentTypeTransactionNumberMapper = Mappers.DocumentTypeTransactionNumberMapper.Mapper(_callContext);
            var entityList = new MetadataRepository().GetDocumentTypeTransactionNumber(documentTypeID);
            documentType.DocumentTypeTransactionNumbers = DocumentTypeTransactionNumberMapper.ToDTO(entityList);

            #endregion Transaction Number Based on Type


            return documentType;
        }

        public DocumentTypeDTO SaveDocumentType(DocumentTypeDTO document)
        {
            var mapper = Mappers.DocumentTypeMapper.Mapper(_callContext);
            var schedulerMapper = Mappers.Schedulers.EntitySchedulerMapper.Mapper(_callContext);
            var documentTypeMapMapper = Mappers.DocumentTypeMapMapper.Mapper(_callContext);
            var updatedEntity = new MetadataRepository().SaveDocumentType(mapper.ToEntity(document));
            var dto = mapper.ToDTO(updatedEntity);

            if (document.Schedulers != null)
            {
                dto.Schedulers = schedulerMapper.ToDTO(new SchedulerRepository().SaveEntityScheduler(schedulerMapper.ToEntity(document.Schedulers)));
            }
            if (document.DocumentMaps != null && document.DocumentMaps.Count > 0)
            {
                dto.DocumentMaps = documentTypeMapMapper.ToDTO(new MetadataRepository().SaveDocumentMap(documentTypeMapMapper.ToEntity(document.DocumentMaps)));
            }

            #region Document Type -  Transaction Number Based on Type
            var DocumentTypeTransactionNumberMapper = Mappers.DocumentTypeTransactionNumberMapper.Mapper(_callContext);
            if (document.DocumentTypeTransactionNumbers != null && document.DocumentTypeTransactionNumbers.Count > 0)
            {
                var entityList = DocumentTypeTransactionNumberMapper.ToEntity(document.DocumentTypeTransactionNumbers);
                entityList = new MetadataRepository().SaveDocumentTypeTransactionNumber(entityList);
                dto.DocumentTypeTransactionNumbers = DocumentTypeTransactionNumberMapper.ToDTO(entityList);
            }
            #endregion Transaction Number Based on Type

            return dto;
        }

        public List<Eduegate.Services.Contracts.Search.ColumnDTO> AvailableViewColumns(Eduegate.Services.Contracts.Enums.SearchView view)
        {
            return ToAvailableColumnDTO(new MetadataRepository().SearchColumns((SearchView)(int)view));
        }

        public static List<ColumnDTO> ToAvailableColumnDTO(List<ViewColumn> viewColumns)
        {
            var columns = new List<ColumnDTO>();

            foreach (var column in viewColumns)
            {
                columns.Add(new ColumnDTO()
                {
                    Header = column.ColumnName,
                    ColumnName = column.PhysicalColumnName,
                    DataType = column.DataType,
                    IsVisible = column.IsVisible.HasValue ? column.IsVisible.Value : true,
                    IsExpression = column.IsExpression.HasValue ? column.IsExpression.Value : false,
                    IsSortable = column.IsSortable.HasValue ? column.IsSortable.Value : false
                });
            }

            return columns;
        }

        public LocationDTO GetLocation(long locationID)
        {
            return Mappers.Warehouses.LocationMapper.Mapper(_callContext).ToDTO(new MetadataRepository().GetLocation(locationID));
        }

        public LocationDTO SaveLocation(LocationDTO document)
        {
            var mapper = Mappers.Warehouses.LocationMapper.Mapper(_callContext);
            var updatedEntity = new MetadataRepository().SaveLocation(mapper.ToEntity(document));
            return mapper.ToDTO(updatedEntity);
        }


        public DocumentReferenceType GetDocumentReferenceTypesByDocumentType(int documentTypeID)
        {
            return new MetadataRepository().GetDocumentReferenceTypesByDocumentType(documentTypeID);
        }
    }
}
