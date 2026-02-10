namespace BombRush.Models
{
    public class MessageHistory
    {
        public long Id { get; set; }
        public required string Message { get; set; }
        public DateTime Time { get; set; }

        public long RoomId { get; set; }
        public required Room Room { get; set; }
        public long SenderId { get; set; }
        public required User Sender { get; set; }
    }
}
