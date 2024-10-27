using ChatBotDemo.Models.Chatbot;

namespace ChatBotDemo.Models.Interfaces
{
    public interface IMemoryStorage
    {
        /// <summary>
        /// Also initialize session with empty list if not previously set.
        /// </summary>
        public List<Message> GetCurrentSessionMessages(string sessionId);
        public void SetSessionMessages(string sessionId, List<Message> messages);
    }
}
