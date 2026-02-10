namespace BombRush.DTO.User
{
    public class InfoResponse
    {
        public long? userId { get; set; }
        public required string displayName { get; set; }
        public required string username { get; set; }
        public required string email { get; set; }
    }
}
