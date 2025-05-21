using System.ComponentModel.DataAnnotations;

namespace HerreraSierraVargas_ChatBot.Models
{
    public class ChatHistorial
    {
        [Key]
        public int Id { get; set; }
        public string Prompt { get; set; }
        public string Response { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string BotName { get; set; }
    }
}
