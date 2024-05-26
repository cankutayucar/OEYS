using OEYS.WEB.Enums;
using OEYS.WEB.Models.Entities;
using OEYS.WEB.Utilities.Extensions;

namespace OEYS.WEB.Models.Dtos.Activities
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ActivityTime { get; set; }
        public string ActivityType { get; set; }
        public int TotalParticipiantCount { get; set; }




        public static implicit operator ActivityDto(Activity activity)
        {
            return new ActivityDto
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                ActivityTime = activity.ActivityTime,
                ActivityType = EnumHelper.GetEnumDisplayNameFromInt<ActivityEnum>(activity.ActivityType)
            };
        }

    }
}
