using ChatBotDemo.Models.Chatbot;

namespace ChatBotDemo.Models.Interfaces
{
    public interface IChatbotService
    {
        public Task<string> SendPrompt(List<Message> messages);
    }
}
