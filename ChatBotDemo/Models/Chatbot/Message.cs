using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;

namespace ChatBotDemo.Models.Chatbot
{
    public class Message
    {
       
        public required MessageType SenderType { get; set; }
        public required string MessageValue { get; set; }

    }
}
