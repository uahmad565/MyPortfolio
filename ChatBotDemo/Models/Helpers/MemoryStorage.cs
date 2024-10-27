using ChatBotDemo.Models.Chatbot;
using ChatBotDemo.Models.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace ChatBotDemo.Models.Helpers
{
    public class MemoryStorage: IMemoryStorage
    {
        private readonly IDistributedCache _cache;
        public MemoryStorage(IDistributedCache cache)
        {
            this._cache = cache;
        }
        public List<Message> GetCurrentSessionMessages(string sessionId)
        {
            List<Message> list = new List<Message>();
            string? cacheKeysJson = this._cache.GetString(sessionId);
            if (cacheKeysJson == null)
            {
                SetSessionMessages(sessionId, new List<Message>());
            }
            else
            {
                list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Message>>(cacheKeysJson)?? new List<Message>();
            }
            return list;
        }

        public void SetSessionMessages(string sessionId, List<Message> messages)
        {
            this._cache.SetString(sessionId, Newtonsoft.Json.JsonConvert.SerializeObject(messages));
        }
    }
}
