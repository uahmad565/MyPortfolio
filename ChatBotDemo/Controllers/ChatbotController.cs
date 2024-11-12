using ChatBotDemo.Models.Chatbot;
using ChatBotDemo.Models.Helpers;
using ChatBotDemo.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Caching.Distributed;

namespace ChatBotDemo.Controllers
{
    public class ChatbotController : Controller
    {
        private readonly IMemoryStorage _memoryStorage;
        private IChatbotService _chatbotService;
        public ChatbotController(IMemoryStorage memoryStorage, IChatbotService chatbotService)
        {
            this._memoryStorage = memoryStorage;
            this._chatbotService = chatbotService;
        }
        public IActionResult Index()
        {
            List<Message> messages = [
                new Message { SenderType = MessageType.Bot, MessageValue = "Hello, how can I assist you today?" },
                new Message { SenderType = MessageType.Human, MessageValue = "Can you tell me about evolution?" },
                new Message { SenderType = MessageType.Bot, MessageValue = "Yes this is Darwin's Theory. Bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" },
                new Message { SenderType = MessageType.Human, MessageValue = "Can you tell me how to reset my password?" },
                new Message { SenderType = MessageType.Bot, MessageValue = "Of course! To reset your password, please go to the login page and click on the 'Forgot Password' link. Follow the instructions sent to your email." },
                new Message { SenderType = MessageType.Human, MessageValue = "Thank you! I received the email, but I'm not sure how to proceed." },
                new Message { SenderType = MessageType.Bot, MessageValue = "No problem! Click on the link provided in the email, and it will guide you to create a new password. Make sure it's a strong password, including letters, numbers, and special characters." },
                new Message { SenderType = MessageType.Human, MessageValue = "Okay, I did that. What should I do if I forget my password again?" },
                new Message { SenderType = MessageType.Bot, MessageValue = "Great! If you forget your password in the future, you can follow the same reset process. I recommend saving your password in a secure password manager." },
            ];


            return View(messages);
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            string? currentSession = HttpContext.Session.GetString(SessionConstants.SESSION_ID);

            var list = this._memoryStorage.GetCurrentSessionMessages(currentSession);

            list.Add(new Message { SenderType = MessageType.Human, MessageValue = message });
            var chatbotResponseMessage = await _chatbotService.SendPrompt(list);
            Message response = new Message { MessageValue = chatbotResponseMessage, SenderType = MessageType.Bot };
            list.Add(response);

            this._memoryStorage.SetSessionMessages(currentSession, list);

            return PartialView("_ChatMessage", response);
        }

        [HttpGet]
        public IActionResult GetChatbotPartialView()
        {
            string? currentSession = HttpContext.Session.GetString(SessionConstants.SESSION_ID);

            var list = this._memoryStorage.GetCurrentSessionMessages(currentSession);
            if (list.Count == 0)
            {
                list.Add(new Message { SenderType = MessageType.Bot, MessageValue = "Hello, how can I assist you today?" });
                this._memoryStorage.SetSessionMessages(currentSession, list);
            }
            return PartialView("_Chatbot", list);
        }
    }
}
