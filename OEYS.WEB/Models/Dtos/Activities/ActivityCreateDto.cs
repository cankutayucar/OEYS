using OEYS.WEB.Models.Entities;

namespace OEYS.WEB.Models.Dtos.Activities
{
    public class ActivityCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ActivityTime { get; set; }
        public int ActivityType { get; set; }





        public static implicit operator Activity(ActivityCreateDto dto)
        {
            return new Activity
            {
                Name = dto.Name,
                Description = dto.Description,
                ActivityTime = dto.ActivityTime,
                ActivityType = dto.ActivityType,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };
        }



    }
}
