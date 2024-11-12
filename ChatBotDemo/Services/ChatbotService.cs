using ChatBotDemo.Models.Chatbot;
using ChatBotDemo.Models.Interfaces;
using System.Net.Http;
using System.Security.Permissions;
using System.Text;

namespace ChatBotDemo.Services
{
    public class ChatbotService : IChatbotService
    {
        private readonly HttpClient _httpClient;
        public ChatbotService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendPrompt(List<Message> messages)
        {
            try
            {
                var prompt = PrepareChatbotPrompt(messages);

                var data = new
                {
                    prompt = prompt,
                };
                var response = await _httpClient.PostAsJsonAsync("/chatbot-response", data);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return $"Error: {response.StatusCode} - {errorContent}";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        private string PrepareChatbotPrompt(List<Message> messages)
        {
            StringBuilder sb = new StringBuilder();
            if (messages.Count > 0)
            {
                if (messages.Count == 1)
                {
                    sb.Append(messages[0].MessageValue);
                }
                else
                {
                    sb.Append(messages[0].MessageValue);
                    for (int i = 1; i < messages.Count; i++)
                    {
                        sb.Append("</s><s>");
                        sb.Append(messages[i].MessageValue);
                    }
                }
            }
            return sb.ToString();
        }
    }
}
