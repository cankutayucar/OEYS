using OEYS.WEB.Models.Entities;

namespace OEYS.WEB.Models.Dtos.Users
{
    public class ParticipiantUserActivityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }




        public static implicit operator ParticipiantUserActivityDto(User user)
        {
            return new ParticipiantUserActivityDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName
            };
        }


    }
}
