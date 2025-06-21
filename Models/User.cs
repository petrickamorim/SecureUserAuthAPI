using System.ComponentModel.DataAnnotations;

namespace SecureUserAuthAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public string RefreshToken { get; set; }

        public DateTime TokenCreated { get; set; }

        public DateTime TokenExpires { get; set; }

    }

}


