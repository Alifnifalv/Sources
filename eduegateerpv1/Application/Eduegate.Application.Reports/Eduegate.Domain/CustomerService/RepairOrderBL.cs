using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Search;
using Eduegate.Framework.Extensions;
using System.Data;
using Eduegate.Services.Contracts.CustomerService;

namespace Eduegate.Domain.CustomerService
{
    public class RepairOrderBL
    {
        private Eduegate.Framework.CallContext _callContext { get; set; }

        public RepairOrderBL(Eduegate.Framework.CallContext context)
        {
            _callContext = context;
        }

        public RepairOrderDTO GetRepaidOrder(int orderNo)
        {
            var mapper = Mappers.CustomerService.ROHeadMapper.Mapper(_callContext);
            var dto = mapper.ToDTO(new RepairOrderRepository().GetRepairOrders(orderNo), new RepairOrderRepository().GetRepairOrderDetails(orderNo),new RepairOrderRepository().GetRepairOrderDefects(orderNo));
            return dto;
        }

        public RepairVehicleDTO GetVehcileDetails(string chasisNo, string registrationNo)
        {
            AS_VEHICLE vehicle;

            if (!string.IsNullOrEmpty(chasisNo))
                vehicle = new RepairOrderRepository().GetVehcileInfoByChasisNo(chasisNo);
            else
                vehicle = new RepairOrderRepository().GetVehcileInfoByRegistrationNo(chasisNo);

            return ROVehicleMapper.Mapper(_callContext).ToDTO(vehicle);
        }

        public SearchResultDTO GetCustomers(int currentPage, int pageSize, string orderBy)
        {
            var dto = new SearchResultDTO();
            dto.Rows = new List<DataRowDTO>();
            dto.Columns = new List<ColumnDTO>();

            var viewInfo = new Eduegate.Domain.Repository.MetadataRepository().GetViewInfo(SearchView.RepairOrderCustomer);

            if (viewInfo.IsNotNull())
            {
                viewInfo.ViewColumns = new List<ViewColumn>();
                dto.Columns = GetMetadata((SearchView)(long)SearchView.RepairOrderCustomer);
            }

            var repository = new RepairOrderRepository();
            currentPage = currentPage == 0 ? 1 : currentPage;

            var datas = repository.GetRepairOrderCustomers(((currentPage - 1) * pageSize), pageSize, orderBy);
            var dataTable = new DataTable();
            dto.TotalRecords = repository.GetRepairOrderCustomerCount();
            dto.PageSize = pageSize;
            dto.CurrentPage = currentPage;

            // passing row data
            foreach (var item in datas)
            {
                var cells = new DataCellListDTO();
                cells.Add("green");
                // DB null should be handled                
                cells.Add(item.CUSCODE);
                cells.Add(item.CUSNAME);
                cells.Add(item.EMAIL);
                cells.Add(item.MOBILENO);
                cells.Add(item.CIVILID);
                cells.Add(item.MOBILE);
                cells.Add(item.CONTPER);
                cells.Add(item.CONTTEL1);
                cells.Add(item.FSTNAM);
                cells.Add(item.LSTNAM);
                dto.Rows.Add(new Eduegate.Services.Contracts.Commons.DataRowDTO() { DataCells = cells });
            }

            //convert this table to DTO
            return dto;
        }

        public SearchResultDTO GetRepaidOrders(int currentPage, int pageSize, string orderBy)
        {
            var dto = new SearchResultDTO();
            dto.Rows = new List<DataRowDTO>();
            dto.Columns = new List<ColumnDTO>();

            var viewInfo = new Eduegate.Domain.Repository.MetadataRepository().GetViewInfo(SearchView.RepairOrder);

            if (viewInfo.IsNotNull())
            {
                viewInfo.ViewColumns = new List<ViewColumn>();
                dto.Columns = GetMetadata((SearchView)(long)SearchView.RepairOrder);
            }

            var repository = new RepairOrderRepository();
            currentPage = currentPage == 0 ? 1 : currentPage;

            var datas = repository.GetRepairOrders(((currentPage - 1) * pageSize), pageSize, orderBy);
            var dataTable = new DataTable();
            dto.TotalRecords = repository.GetRepairOrderCount();
            dto.PageSize = pageSize;
            dto.CurrentPage = currentPage;

            // passing row data
            foreach (var item in datas)
            {
                var cells = new DataCellListDTO();

                // DB null should be handled
                cells.Add("green");
                cells.Add(item.RONO);
                cells.Add(item.DOCDATE);
                cells.Add(item.CHASSISNO);
                cells.Add(item.CUSCODE);
                cells.Add(item.PROMISED);
                cells.Add(item.ACTUALDEL);
                cells.Add(item.CONTPER);
                cells.Add(item.CONTTEL1);
                cells.Add(item.VEHINBY);
                dto.Rows.Add(new Eduegate.Services.Contracts.Commons.DataRowDTO() { DataCells = cells });
            }

            //convert this table to DTO
            return dto;
        }

        public SearchResultDTO GetRepairOrderSummary()
        {
            var dto = new SearchResultDTO();
            dto.Rows = new List<DataRowDTO>();
            dto.Columns = new List<ColumnDTO>();

            var viewInfo = new Eduegate.Domain.Repository.MetadataRepository().GetViewInfo(SearchView.RepairOrderSummary);

            if (viewInfo.IsNotNull())
            {
                viewInfo.ViewColumns = new List<ViewColumn>();
                dto.Columns = GetMetadata((SearchView)(long)SearchView.RepairOrderSummary);
            }

            var repository = new RepairOrderRepository();
            var TotalOrder = repository.GetRepairOrders(DateTime.MinValue, DateTime.MaxValue);
            var TodaysOrder = repository.GetRepairOrders(DateTime.Now, DateTime.Now);
            DateTime mondayOfLastWeek = DateTime.Now.AddDays(6);
            var LastWeekOrders = repository.GetRepairOrders(mondayOfLastWeek, DateTime.Now);
            // passing row data
            dto.Rows.Add(new Eduegate.Services.Contracts.Commons.DataRowDTO()
            {
                DataCells = new DataCellListDTO()
                {
                    TotalOrder,
                    TodaysOrder,
                    LastWeekOrders
                }
            });

            //convert this table to DTO
            return dto;
        }

        private static List<ColumnDTO> GetMetadata(SearchView view, List<KeyValueDTO> sortColumns = null)
        {
            var columns = new List<ColumnDTO>();
            var viewColumns = new Eduegate.Domain.Repository.MetadataRepository().SearchColumns(view);

            foreach (var column in viewColumns)
            {
                columns.Add(new ColumnDTO()
                {
                    Header = column.ColumnName,
                    ColumnName = column.PhysicalColumnName,
                    DataType = column.DataType,
                    IsExpression = column.IsExpression.HasValue ? column.IsExpression.Value : false,
                    Expression = column.Expression,
                    IsVisible = column.IsVisible.HasValue ? column.IsVisible.Value : true
                });

                if (sortColumns != null && column.IsSortable.HasValue && column.IsSortable.Value)
                {
                    sortColumns.Add(new KeyValueDTO() { Key = column.PhysicalColumnName, Value = column.ColumnName });
                }
            }

            return columns;
        }

        public RepairOrderDTO SaveRepairOrder(RepairOrderDTO repairOrder)
        {
            var repository = new RepairOrderRepository();
            var mapper = Mappers.CustomerService.ROHeadMapper.Mapper(_callContext);
            var mapperDetail = Mappers.CustomerService.RODetailMapper.Mapper(_callContext);
            var mapperDefect = Mappers.CustomerService.RODamageMapper.Mapper(_callContext);
            var entity = mapper.ToEntity(repairOrder);
            var insertedEntity = repository.SaveRepairOrder(entity);

            if (insertedEntity.IsNotNull())
            {
                var vehicleDTO = new RepairVehicleDTO();
                vehicleDTO.KTNO = repairOrder.KTNO;
                vehicleDTO.DESCRIPTION = repairOrder.VehicleDescription;
                vehicleDTO.REGISTRATION = repairOrder.RegitrationDate;
                vehicleDTO.MAIN_VEHTYPE = repairOrder.BillVehicleType;
                vehicleDTO.WARRANTYKM = repairOrder.WarrantyKMs;
                vehicleDTO.LASTKM = repairOrder.LastServiceKMs;
                vehicleDTO.CHASSISNO = repairOrder.CHASSISNO;
                vehicleDTO.CUSCODE = repairOrder.CUSCODE;

                var vehicleMapper = Mappers.CustomerService.ROVehicleMapper.Mapper(_callContext).ToEntity(vehicleDTO);

                var vehicleEntity = repository.SaveVehicle(vehicleMapper);

                var repairDetaillist = new List<RepairDetailDTO>();

                foreach (var detail in repairOrder.Details)
                {
                    var detailEntity = mapperDetail.ToEntity(detail);
                    detailEntity.RONO = insertedEntity.RONO;
                    repairDetaillist.Add(mapperDetail.ToDTO(detailEntity));
                }

                var defectList = new List<RepairDefectDTO>();


                foreach (var defect in repairOrder.Defects)
                {
                    var defectEntity = mapperDefect.ToEntity(defect);
                    defectEntity.RONO = insertedEntity.RONO;
                    defectList.Add(mapperDefect.ToDTO(defectEntity));
                }
                //defectList = mapperDefect.ToEntity(repairOrder.Defects);

                var insertedDetails = repository.SaveRepairOrderDetails(mapperDetail.ToEntity(repairDetaillist));

                var insertedDefects = repository.SaveDefect(mapperDefect.ToEntity(defectList));
                return mapper.ToDTO(insertedEntity, insertedDetails, insertedDefects);
            }
            else
                return null;

            //return repairOrder;
        }
    }
}
