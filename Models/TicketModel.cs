using AiSupportApi.Enums;

namespace AiSupportApi.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketStatus Status { get; set; } = TicketStatus.Pendente;
    }
}
