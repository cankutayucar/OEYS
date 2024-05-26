

using Dapper.Contrib.Extensions;

namespace OEYS.WEB.Models.Entities.Common
{
    public abstract class IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
