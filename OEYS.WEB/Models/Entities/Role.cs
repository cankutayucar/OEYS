using Dapper.Contrib.Extensions;
using OEYS.WEB.Models.Entities.Common;

namespace OEYS.WEB.Models.Entities
{
    [Table("Roles")]
    public class Role : IEntity
    {
        public string Name { get; set; }
    }
}
