using OEYS.WEB.Enums;
using OEYS.WEB.Models.Entities;
using OEYS.WEB.Utilities.Extensions;

namespace OEYS.WEB.Models.Dtos.Activities
{
    public class ActivityUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ActivityTime { get; set; }
        public int ActivityType { get; set; }



        public static implicit operator ActivityUpdateDto(Activity activity)
        {
            return new ActivityUpdateDto
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                ActivityTime = activity.ActivityTime,
                ActivityType = activity.ActivityType
            };
        }


        public static implicit operator Activity(ActivityUpdateDto activityDto)
        {
            return new Activity
            {
                Id = activityDto.Id,
                Name = activityDto.Name,
                Description = activityDto.Description,
                ActivityTime = activityDto.ActivityTime,
                ActivityType = activityDto.ActivityType,
                UpdatedDate = DateTime.Now
            };
        }


    }
}
