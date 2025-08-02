using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Supports;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Repository.Payroll;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Payroll;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Entity.Supports.Models;

namespace Eduegate.Domain.Mappers
{
    public class TicketMapper : IDTOEntityMapper<TicketDTO, Ticket>
    {
        private CallContext _context;

        public static TicketMapper Mapper(CallContext context)
        {
            var mapper = new TicketMapper();
            mapper._context = context;
            return mapper;
        }

        public TicketDTO ToDTO(Ticket entity)
        {
            TicketDTO ticketDTO = new TicketDTO();
            ticketDTO.TicketProductSKUs = new List<TicketProductDTO>();

            if (entity.IsNotNull())
            {
                ticketDTO.TicketIID = entity.TicketIID;
                ticketDTO.TicketNo = entity.TicketNo;
                ticketDTO.DocumentTypeID = entity.DocumentTypeID;
                ticketDTO.Subject = entity.Subject;
                ticketDTO.Description = entity.Description;
                ticketDTO.Description2 = entity.Description2;
                ticketDTO.Source = entity.Source;
                ticketDTO.PriorityID = entity.PriorityID;
                ticketDTO.ActionID = entity.ActionID;
                ticketDTO.TicketStatusID = entity.TicketStatusID;
                ticketDTO.TicketStatus = entity.TicketStatusID.IsNotNull() ? new SupportRepository().GetTicketStatusByStatusID(Convert.ToInt32(entity.TicketStatusID)).StatusName : string.Empty;
                //ticketDTO.AssingedEmployeeID = entity.AssingedEmployeeID;
                //ticketDTO.AssignedEmployee = entity.AssingedEmployeeID.IsNotNull() ? (employeeRepository.GetEmployee(Convert.ToInt32(entity.AssingedEmployeeID)).IsNotNull() ? employeeRepository.GetEmployee(Convert.ToInt32(entity.AssingedEmployeeID)).EmployeeName : string.Empty) : string.Empty;
                ticketDTO.ManagerEmployeeID = entity.ManagerEmployeeID;
                ticketDTO.ManagerEmployee = entity.ManagerEmployeeID.IsNotNull() ? (new EmployeeBL(_context).GetEmployee(Convert.ToInt32(entity.ManagerEmployeeID)).IsNotNull() ? new EmployeeBL(_context).GetEmployee(Convert.ToInt32(entity.ManagerEmployeeID)).EmployeeName : string.Empty) : string.Empty;
                ticketDTO.CustomerID = entity.CustomerID;
                ticketDTO.CustomerName = entity.CustomerID.IsNotNull() ? new ReferenceDataRepository().GetCustomerName(entity.CustomerID.Value) : string.Empty;
                ticketDTO.SupplierID = entity.SupplierID;
                ticketDTO.EmployeeID = entity.EmployeeID;
                ticketDTO.DueDateFrom = entity.DueDateFrom;
                ticketDTO.DueDateTo = entity.DueDateTo;
                ticketDTO.HeadID = entity.HeadID;
                ticketDTO.CustomerNotification = entity.CustomerNotification;
                ticketDTO.CreatedBy = entity.CreatedBy;
                ticketDTO.CreatedDate = entity.CreatedDate;
                ticketDTO.UpdatedBy = entity.UpdatedBy;
                ticketDTO.UpdatedDate = entity.UpdatedDate;
                //ticketDTO.TimeStamps = entity.TimeStamps.IsNotNull() ? Convert.ToBase64String(entity.TimeStamps) : null;

                if (entity.TicketProductMaps.IsNotNull() && entity.TicketProductMaps.Count > 0)
                {
                    TicketProductDTO tpDTO = null;

                    //Get Order History details to get Unit Price of Item
                    //var orderDetail = new TransactionBL(_context).GetOrderHistoryDetails("0", 0, 0, orderID: Convert.ToInt64(entity.HeadID), withCurrencyConversion: false).FirstOrDefault();


                    foreach (TicketProductMap tpMap in entity.TicketProductMaps)
                    {
                        tpDTO = new TicketProductDTO();

                        tpDTO.TicketProductMapIID = tpMap.TicketProductMapIID;
                        tpDTO.ProductID = Convert.ToInt32(tpMap.ProductID);
                        tpDTO.SKUID = Convert.ToInt64(tpMap.ProductSKUMapID);
                        tpDTO.SKUName = tpMap.ProductSKUMapID.HasValue ? new ProductDetailBL(_context).GetProductAndSKUNameByID((long)tpMap.ProductSKUMapID.Value) : string.Empty;
                        tpDTO.ReasonID = Convert.ToInt32(tpMap.ReasonID);
                        tpDTO.Narration = tpMap.Narration;
                        tpDTO.TicketID = Convert.ToInt32(tpMap.TicketID);
                        tpDTO.Quantity = tpMap.Quantity.IsNotNull() ? tpMap.Quantity : null;
                        tpDTO.TicketID = Convert.ToInt32(tpMap.TicketID);
                        tpDTO.CreatedBy = tpMap.CreatedBy;
                        tpDTO.CreatedDate = tpMap.CreatedDate;
                        tpDTO.UpdatedBy = tpMap.UpdatedBy;
                        tpDTO.UpdatedDate = tpMap.UpdatedDate;
                        //tpDTO.TimeStamps = tpMap.TimeStamps.IsNotNull() ? Convert.ToBase64String(tpMap.TimeStamps) : null;
                        tpDTO.UnitPrice = null;//orderDetail.IsNotNull() && orderDetail.OrderDetails.Any(d => d.ProductSKUMapID == tpMap.ProductSKUMapID) ? orderDetail.OrderDetails.Where(o => o.ProductSKUMapID == tpMap.ProductSKUMapID).FirstOrDefault().UnitPrice : null; // where to get unit price if not connected to Order
                        tpDTO.ProductImageUrl = null;// orderDetail.IsNotNull() && orderDetail.OrderDetails.Any(d => d.ProductSKUMapID == tpMap.ProductSKUMapID) ? orderDetail.OrderDetails.Where(o => o.ProductSKUMapID == tpMap.ProductSKUMapID).FirstOrDefault().ProductImageUrl : null;

                        ticketDTO.TicketProductSKUs.Add(tpDTO);
                    }
                }


                if (entity.TicketActionDetailMaps.IsNotDefault() && entity.TicketActionDetailMaps.Count > 0)
                {
                    ticketDTO.TicketActionDetail = new Services.Contracts.TicketActionDetailMapsDTO();

                    // to be changed if ticket can have multiple detail maps
                    var actionDetailEntity = entity.TicketActionDetailMaps.FirstOrDefault();


                    ticketDTO.TicketActionDetail.TicketActionDetailIID = actionDetailEntity.TicketActionDetailIID;
                    ticketDTO.TicketActionDetail.TicketID = actionDetailEntity.TicketID;
                    ticketDTO.TicketActionDetail.RefundTypeID = actionDetailEntity.RefundTypeID;

                    ticketDTO.TicketActionDetail.RefundAmount = actionDetailEntity.RefundAmount;
                    ticketDTO.TicketActionDetail.Reason = actionDetailEntity.Reason;
                    ticketDTO.TicketActionDetail.Remark = actionDetailEntity.Remark;
                    ticketDTO.TicketActionDetail.ReturnNumber = actionDetailEntity.ReturnNumber;
                    ticketDTO.TicketActionDetail.GiveItemTo = actionDetailEntity.GiveItemTo;
                    ticketDTO.TicketActionDetail.IssueType = actionDetailEntity.IssueType;
                    ticketDTO.TicketActionDetail.AssignedEmployee = new KeyValueDTO();
                    ticketDTO.TicketActionDetail.AssignedEmployee.Key = actionDetailEntity.AssignedEmployee.IsNotNull() ? actionDetailEntity.AssignedEmployee.ToString() : null;
                    ticketDTO.TicketActionDetail.AssignedEmployee.Value = actionDetailEntity.AssignedEmployee.IsNotNull() ? new EmployeeBL(_context).GetEmployee(Convert.ToInt32(actionDetailEntity.AssignedEmployee)).EmployeeName : null;
                    ticketDTO.TicketActionDetail.CreatedBy = actionDetailEntity.CreatedBy;
                    ticketDTO.TicketActionDetail.UpdatedBy = actionDetailEntity.UpdatedBy;
                    ticketDTO.TicketActionDetail.CreatedDate = actionDetailEntity.CreatedDate;
                    ticketDTO.TicketActionDetail.UpdatedDate = actionDetailEntity.UpdatedDate;


                    // Get sub action name using actionId
                    Nullable<int> subActionId = null;
                    switch ((Services.Contracts.Enums.TicketActions)Enum.Parse(typeof(Services.Contracts.Enums.TicketActions), ticketDTO.ActionID.ToString()))
                    {
                        case Services.Contracts.Enums.TicketActions.Refund:
                            subActionId = actionDetailEntity.RefundTypeID;
                            break;
                        case Services.Contracts.Enums.TicketActions.CollectItem:
                            subActionId = actionDetailEntity.GiveItemTo;
                            break;
                        case Services.Contracts.Enums.TicketActions.DirectReplacement:
                            subActionId = actionDetailEntity.GiveItemTo;
                            break;
                        case Services.Contracts.Enums.TicketActions.DigitalCard:
                            subActionId = actionDetailEntity.IssueType;
                            break;
                    }

                    ticketDTO.TicketActionDetail.SubActionName = subActionId.IsNotNull() ? new SupportRepository().GetTicketActionByActionID(Convert.ToInt32(subActionId)).ActionName : null;

                    // Map action-detail-detail-map
                    if (actionDetailEntity.TicketActionDetailDetailMaps.IsNotNull() && actionDetailEntity.TicketActionDetailDetailMaps.Count > 0)
                    {
                        ticketDTO.TicketActionDetail.TicketActionDetailDetailMaps = new List<Services.Contracts.TicketActionDetailDetailMapDTO>();
                        foreach (var map in actionDetailEntity.TicketActionDetailDetailMaps)
                        {
                            ticketDTO.TicketActionDetail.TicketActionDetailDetailMaps.Add(new Services.Contracts.TicketActionDetailDetailMapDTO
                            {
                                TicketActionDetailDetailMapIID = map.TicketActionDetailDetailMapIID,
                                Notify = map.Notify,
                                TicketActionDetailMapID = map.TicketActionDetailMapID,
                                CreatedBy = map.CreatedBy,
                                UpdatedBy = map.UpdatedBy,
                                CreatedDate = map.CreatedDate,
                                UpdatedDate = map.UpdatedDate,
                            });
                        }
                    }

                }
            }
            return ticketDTO;
        }

        public Ticket ToEntity(TicketDTO dto)
        {
            Ticket ticket = new Ticket();
            ticket.TicketProductMaps = new List<TicketProductMap>();
            SupportRepository supportRepository = new SupportRepository();

            if (dto.IsNotNull())
            {
                ticket.TicketIID = dto.TicketIID;
                ticket.TicketNo = dto.TicketNo;
                ticket.DocumentTypeID = dto.DocumentTypeID;
                ticket.Subject = dto.Subject;
                ticket.Description = dto.Description;
                ticket.Description2 = dto.Description2;
                ticket.Source = dto.Source;
                ticket.PriorityID = dto.PriorityID;
                ticket.ActionID = dto.ActionID;
                ticket.TicketStatusID = dto.TicketStatusID;
                //ticket.AssingedEmployeeID = dto.AssingedEmployeeID;
                ticket.ManagerEmployeeID = dto.ManagerEmployeeID;
                ticket.CustomerID = dto.CustomerID;
                ticket.SupplierID = dto.SupplierID;
                ticket.EmployeeID = dto.EmployeeID;
                ticket.DueDateFrom = dto.DueDateFrom;
                ticket.DueDateTo = dto.DueDateTo;
                ticket.HeadID = dto.HeadID;
                ticket.CustomerNotification = dto.CustomerNotification;
                ticket.CompanyID = (int)_context.CompanyID;
                //if(dto.CreatedBy.IsNotNull() && dto.CreatedBy != 0)
                //{ 
                ticket.CreatedBy = dto.CreatedBy.HasValue ? dto.CreatedBy : _context.LoginID.HasValue ? (int)_context.LoginID : (int?)null;
                //}
                ticket.CreatedDate = dto.TicketIID > 0 ? dto.CreatedDate : DateTime.Now;
                ticket.UpdatedBy = dto.TicketIID > 0 ? (int)_context.LoginID : (int?)null;
                ticket.UpdatedDate = dto.TicketIID > 0 ? DateTime.Now : (DateTime?)null;
                //ticket.TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null;

                // Get selected products under this ticket
                if (dto.TicketProductSKUs.IsNotNull() && dto.TicketProductSKUs.Count > 0)
                {
                    TicketProductMap tpMap = null;

                    foreach (TicketProductDTO tpDTO in dto.TicketProductSKUs)
                    {
                        tpMap = new TicketProductMap();

                        if (tpDTO.SKUID > 0)
                        {
                            tpMap.TicketProductMapIID = tpDTO.TicketProductMapIID;
                            tpMap.ProductID = tpDTO.ProductID > 0 ? tpDTO.ProductID : new ProductDetailBL(_context).GetProductIDfromSKU(tpDTO.SKUID);
                            tpMap.ProductSKUMapID = tpDTO.SKUID;
                            tpMap.ReasonID = Convert.ToInt16(tpDTO.ReasonID);
                            tpMap.Narration = tpDTO.Narration;
                            tpMap.TicketID = tpDTO.TicketID;
                            tpMap.Quantity = tpDTO.Quantity.IsNotNull() ? tpDTO.Quantity : null;
                            tpMap.CreatedBy = tpDTO.TicketProductMapIID > 0 ? tpDTO.CreatedBy : (int)_context.LoginID;
                            tpMap.CreatedDate = tpDTO.TicketProductMapIID > 0 ? tpDTO.CreatedDate : DateTime.Now;
                            tpMap.UpdatedBy = tpDTO.TicketProductMapIID > 0 ? (int)_context.LoginID : tpDTO.UpdatedBy;
                            tpMap.UpdatedDate = tpDTO.TicketProductMapIID > 0 ? DateTime.Now : tpDTO.UpdatedDate;
                            //tpMap.TimeStamps = tpDTO.TimeStamps.IsNotNull() ? Convert.FromBase64String(tpDTO.TimeStamps) : null;

                            ticket.TicketProductMaps.Add(tpMap);
                        }
                    }
                }

                // Get Ticket action details
                if (dto.TicketActionDetail.IsNotNull())
                {
                    ticket.TicketActionDetailMaps = new List<TicketActionDetailMap>();
                    var ticketActionDetailMap = new TicketActionDetailMap();

                    ticketActionDetailMap.TicketActionDetailIID = dto.TicketActionDetail.TicketActionDetailIID;
                    ticketActionDetailMap.TicketID = dto.TicketIID;
                    //ticketActionDetailMap.AssignedEmployee = dto.TicketActionDetail.AssignedEmployee.IsNotNull() ? Convert.ToInt64(dto.TicketActionDetail.AssignedEmployee.Key) : (long?)null;
                    ticketActionDetailMap.GiveItemTo = dto.TicketActionDetail.GiveItemTo;
                    ticketActionDetailMap.IssueType = dto.TicketActionDetail.IssueType;
                    ticketActionDetailMap.Reason = dto.TicketActionDetail.Reason;
                    ticketActionDetailMap.RefundAmount = dto.TicketActionDetail.RefundAmount;
                    ticketActionDetailMap.RefundTypeID = dto.TicketActionDetail.RefundTypeID;
                    ticketActionDetailMap.Remark = dto.TicketActionDetail.Remark;
                    ticketActionDetailMap.ReturnNumber = dto.TicketActionDetail.ReturnNumber;
                    ticketActionDetailMap.CreatedBy = dto.TicketActionDetail.TicketActionDetailIID > 0 ? dto.TicketActionDetail.CreatedBy : (int)_context.LoginID;
                    ticketActionDetailMap.CreatedDate = dto.TicketActionDetail.TicketActionDetailIID > 0 ? dto.TicketActionDetail.CreatedDate : DateTime.Now;
                    ticketActionDetailMap.UpdatedBy = dto.TicketActionDetail.TicketActionDetailIID > 0 ? (int)_context.LoginID : dto.UpdatedBy;
                    ticketActionDetailMap.UpdatedDate = dto.TicketActionDetail.TicketActionDetailIID > 0 ? DateTime.Now : dto.TicketActionDetail.UpdatedDate;
                    //ticketActionDetailMap.Timestamps = dto.TicketActionDetail.Timestamps.IsNotNull() ? Convert.FromBase64String(dto.TicketActionDetail.Timestamps) : null;


                    // Get ticket action detail detail map if action is arrangment.
                    if (ticket.ActionID == (int)Services.Contracts.Enums.TicketActions.Arrangement && dto.TicketActionDetail.TicketActionDetailDetailMaps.IsNotNull() && dto.TicketActionDetail.TicketActionDetailDetailMaps.Count > 0)
                    {
                        ticketActionDetailMap.TicketActionDetailDetailMaps = new List<TicketActionDetailDetailMap>();
                        foreach (var item in dto.TicketActionDetail.TicketActionDetailDetailMaps)
                        {
                            ticketActionDetailMap.TicketActionDetailDetailMaps.Add(new TicketActionDetailDetailMap
                            {
                                TicketActionDetailDetailMapIID = item.TicketActionDetailDetailMapIID,
                                TicketActionDetailMapID = dto.TicketActionDetail.TicketActionDetailIID,
                                Notify = item.Notify,

                                CreatedBy = item.TicketActionDetailDetailMapIID > 0 ? item.CreatedBy : (int)_context.LoginID,
                                CreatedDate = item.TicketActionDetailDetailMapIID > 0 ? item.CreatedDate : DateTime.Now,
                                UpdatedBy = item.TicketActionDetailDetailMapIID > 0 ? (int)_context.LoginID : item.UpdatedBy,
                                UpdatedDate = item.TicketActionDetailDetailMapIID > 0 ? DateTime.Now : item.UpdatedDate,
                                //Timestamps = item.Timestamps.IsNotNull() ? Convert.FromBase64String(item.Timestamps) : null,
                            });

                        }
                    }
                    ticket.TicketActionDetailMaps.Add(ticketActionDetailMap);
                }
            }
            return ticket;
        }
    }
}