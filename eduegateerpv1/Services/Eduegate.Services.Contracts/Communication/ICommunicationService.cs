using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Services.Contracts.Communications
{
    public interface ICommunicationService
    {
        public CommentDTO SaveComment(CommentDTO comment);

    }
}
