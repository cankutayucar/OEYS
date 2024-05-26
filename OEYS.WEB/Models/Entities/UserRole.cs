using Dapper.Contrib.Extensions;
using OEYS.WEB.Models.Entities.Common;

namespace OEYS.WEB.Models.Entities
{
    [Table("UserRoles")]
    public class UserRole : IEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
