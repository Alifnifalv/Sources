using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(long commentId, long fromLoginId, long toLoginId, int CreatedBy, string message, bool isRead)
        {
            await Clients.All.SendAsync("ReceiveMessage", commentId, fromLoginId, toLoginId, CreatedBy, message, isRead);
        }

        public async Task NotifyParentListUpdated(long teacherLoginId)
        {
            await Clients.All.SendAsync("ParentListUpdated", teacherLoginId);
        }

        public async Task NotifyTeacherListUpdated(long parentLoginId)
        {
            await Clients.All.SendAsync("TeacherListUpdated", parentLoginId);
        }

        public async Task NotifyMessageRead(int commentId, long toLoginId)
        {
            await Clients.User(toLoginId.ToString()).SendAsync("MessageRead", commentId, toLoginId);
        }
    }

}