using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinBY.Domain.Entities
{
    [Serializable]
    [Table("User")]
    public class User : Entity
    {
        public User(string name)
        {
            Name = name;
        }
        public User(string name, string userName, string password, string refreshToken, DateTime refreshTokenExpiryTime)
        {
            Name = name;
            UserName = userName;
            Password = password;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }

        [Required]
        [StringLength(50)]
        public String Name { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string RefreshToken { get;  set; }
        public DateTime RefreshTokenExpiryTime { get;  set; }

    }
}
