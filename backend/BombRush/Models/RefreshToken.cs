using System.ComponentModel.DataAnnotations;

namespace BombRush.Models
{
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Expires { get; set; }
        public bool Revoked { get; set; }

        public required long UserId { get; set; }
        public User User { get; set; }
    }
}
