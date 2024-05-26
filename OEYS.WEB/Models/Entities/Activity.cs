using Dapper.Contrib.Extensions;
using OEYS.WEB.Enums;
using OEYS.WEB.Models.Entities.Common;


namespace OEYS.WEB.Models.Entities
{
    [Table("Activities")]
    public class Activity : IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ActivityTime { get; set; }
        public int ActivityType { get; set; }
    }
}
