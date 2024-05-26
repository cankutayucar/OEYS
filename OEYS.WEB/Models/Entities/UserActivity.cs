using Dapper.Contrib.Extensions;
using OEYS.WEB.Models.Entities.Common;

namespace OEYS.WEB.Models.Entities
{
    [Table("UserActivities")]
    public class UserActivity : IEntity
    {
        public int UserId { get; set; }
        public int ActivityId { get; set; }
        public int Subscribe { get; set; }
    }
}
