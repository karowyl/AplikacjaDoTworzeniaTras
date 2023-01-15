using RunnerApp.DTOs;
using RunnerApp.Entities;
using RunnerApp.Helpers;

namespace RunnerApp.Interfaces
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void RemoveMessage(Message message);
        Task<Message> GetMessageId(int id);
        Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername);
        Task<bool> SaveAllAsync();
    }
}
