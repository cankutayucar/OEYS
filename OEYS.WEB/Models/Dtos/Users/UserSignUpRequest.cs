using OEYS.WEB.Models.Entities;
using OEYS.WEB.Utilities.Password;

namespace OEYS.WEB.Models.Dtos.Users
{
    public class UserSignUpRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }
        public string PhoneNumber { get; set; }



        public static implicit operator User(UserSignUpRequest request)
        {
            HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            return new User
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsActive = true,
                IsDeleted = false
            };
        }


    }
}
