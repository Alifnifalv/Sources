using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Hub.Client;
using Eduegate.PublicAPI.Common;
using Eduegate.Services.Contracts.Communications;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.MobileAppWrapper;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using SignalRChat.Hubs;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.Payroll;
using System.Linq;
using System.Threading.Tasks;

namespace Eduegate.Public.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommunicationController : ApiControllerBase
    {
        private readonly ILogger<SchoolController> _logger;
        private readonly dbEduegateERPContext _dbContext;
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly RealtimeClient _realtimeClient;
        private readonly IHttpContextAccessor _accessor;
        private readonly IHubContext<ChatHub> _hubContext;

        public CommunicationController(ILogger<SchoolController> logger, IHttpContextAccessor context,
            dbEduegateERPContext dbContext, IBackgroundJobClient backgroundJobs,
            IServiceProvider serviceProvider, RealtimeClient realtimeClient,
            IHttpContextAccessor accessor, IHubContext<ChatHub> hubContext) : base(context)
        {
            _logger = logger;
            _dbContext = dbContext;
            _backgroundJobs = backgroundJobs;
            _realtimeClient = realtimeClient;
            _accessor = accessor;   
            _hubContext = hubContext; // Injecting the SignalR HubContext
        }
        [HttpPost]
        [Route("SaveComment")]
        public CommentDTO SaveComment(CommentDTO comment)
        {
            // Save the comment using the communication service
            var savedComment = new CommunicationService(CallContext).SaveComment(comment);

            // Broadcast the comment via SignalR after saving
            if (savedComment != null)
            {
                _hubContext.Clients.All.SendAsync("ReceiveMessage",
                    savedComment.CommentIID,
                    savedComment.FromLoginID,
                    savedComment.ToLoginID,
                    savedComment.CreatedBy,
                    savedComment.CommentText);

                // Notify clients that the parent list might have been updated
                _hubContext.Clients.All.SendAsync("ParentListUpdated", savedComment.ToLoginID);
                _hubContext.Clients.All.SendAsync("TeacherListUpdated", savedComment.ToLoginID);
            }

            return savedComment;
        }


        [HttpGet]
        [Route("GetTeacherEmailByParentLoginID")]
        public List<EmployeeDTO> GetTeacherEmailByParentLoginID()
        {
            return new CommunicationService(CallContext).GetTeacherEmailByParentLoginID();
        }
        [HttpGet]
        [Route("GetParentDetailsByTeacherLoginID")]
        public List<GuardianDTO> GetParentDetailsByTeacherLoginID()
        {
            return new CommunicationService(CallContext).GetParentListByLoginID();
        }
        


        [HttpGet]
        [Route("GetComments")]
        public List<CommentDTO> GetChats(int entityTypeID, long? fromLoginID, long? toLoginID)
        {
            var entityType = (Eduegate.Infrastructure.Enums.EntityTypes)entityTypeID;
            return new CommunicationService(CallContext).GetChats(entityType,  fromLoginID, toLoginID);
        }

        //[HttpPost]
        //[Route("MarkCommentAsRead")]
        //public IActionResult MarkCommentAsRead(long commentId)
        //{
        //    try
        //    {
        //        var isUpdated = new CommunicationService(CallContext).MarkCommentAsRead(commentId, context.LoginID);

        //        if (isUpdated)
        //        {
        //            return Ok(new { operationResult = 1 });
        //        }
        //        else
        //        {
        //            return BadRequest(new { operationResult = 0, message = "Failed to mark comment as read" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<MutualBL>.Fatal(ex.Message, ex);
        //        return StatusCode(500, new { operationResult = 0, message = "Internal server error" });
        //    }
        //}

        [HttpGet]
        [Route("GetParentsWithLatestMessageByTeacherLoginID")]
        public List<GuardianDTO> GetActiveParentListByLoginID(long teacherLoginId)
        {
            var parentList =  new CommunicationService(CallContext).GetActiveParentListByLoginID();
            _hubContext.Clients.All.SendAsync("TeacherListUpdated", teacherLoginId);

            return parentList;

        }
        [HttpGet]
        [Route("GetTeachersWhoMessagedByParentLoginID")]
        public List<EmployeeDTO> GetTeachersWhoMessagedByParentLoginID(long parentLoginID)
        {
            var teacherList = new CommunicationService(CallContext).GetTeachersWhoMessagedByParentLoginID();

            // Notify clients that the teacher list has been updated
            _hubContext.Clients.All.SendAsync("TeacherListUpdated", parentLoginID);

            return teacherList;
        }

        [HttpPost]
        [Route("MarkCommentAsRead")]
        public async Task<IActionResult> MarkCommentAsRead([FromBody] dynamic data)
        {
            int commentIID = data.commentIID;
            var comment = _dbContext.Comments.FirstOrDefault(c => c.CommentIID == commentIID);
            if (comment != null && !comment.IsRead)
            {
                comment.IsRead = true;
                comment.UpdatedDate = DateTime.Now;
                _dbContext.SaveChanges();

                // Send real-time notification with IsRead status
                await _hubContext.Clients.User(comment.FromLoginID.ToString())
                    .SendAsync("MessageRead", comment.CommentIID, comment.ToLoginID, comment.IsRead);
            }

            return Ok();
        }

        [HttpGet]
        [Route("GetParentDetailsByLoginID")]
        public GuardianDTO GetParentDetailsByLoginID(long loginID)
        {
            var ParentDetailsByLoginID = new CommunicationService(CallContext).GetParentDetailsByLoginID(loginID);

            return ParentDetailsByLoginID;

        }

    }
}
