using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetTicketDetails(Ticket ticket)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (ticket.IsNotNull())
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TicketID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TicketNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Priority" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Status" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Title" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Problem" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ActionNeeded" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Createdby" });
                //searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Owner" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CustomerID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Updatedby" });

                var priority = new SupportRepository().GetTicketPriority(ticket.PriorityID.IsNotNull() ? (int)ticket.PriorityID : 0);
                var TicketStatus = new SupportRepository().GetTicketStatus(ticket.TicketStatusID.IsNotNull() ? (int)ticket.TicketStatusID : 0);
                var created = ticket.CreatedBy.IsNotNull() && ticket.CreatedBy.Value > 0 ? new Repository.Security.SecurityRepository().GetLogin(long.Parse(ticket.CreatedBy.Value.ToString())) : null;
                var updated = ticket.UpdatedBy.IsNotNull() && ticket.UpdatedBy.Value > 0 ? new Repository.Security.SecurityRepository().GetLogin(long.Parse(ticket.UpdatedBy.Value.ToString())) : null;
                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          ticket.TicketIID
                          ,ticket.TicketNo,
                        priority.IsNotNull() && priority.PriorityName.IsNotNull() ? priority.PriorityName : null ,
                        TicketStatus.IsNotNull() && TicketStatus.StatusName.IsNotNull() ? TicketStatus.StatusName : null
                        ,ticket.Subject.IsNotNull() ? ticket.Subject : null,
                          ticket.Description.Replace("\n", " "), ticket.Description2.IsNotNull() ? ticket.Description2.Replace("\n", " ") : null,created.IsNotNull() ? created.LoginEmailID : null ,ticket.CreatedDate.IsNotNull() ? ticket.CreatedDate : null
                          ,ticket.UpdatedDate.IsNotNull() ? ticket.UpdatedDate : null,ticket.CustomerID.IsNotNull() ? ticket.CustomerID : null,updated.IsNotNull() ? updated.LoginEmailID : null
                        }
                });
            }


            return searchDTO;
        }
    }
}
