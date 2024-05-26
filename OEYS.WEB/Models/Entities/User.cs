using Dapper.Contrib.Extensions;
using OEYS.WEB.Models.Dtos.Users;
using OEYS.WEB.Models.Entities.Common;


namespace OEYS.WEB.Models.Entities
{
    [Table("Users")]
    public class User : IEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string PhoneNumber { get; set; }

    }
}
