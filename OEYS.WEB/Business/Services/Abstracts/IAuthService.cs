using OEYS.WEB.Models.Dtos.Users;
using OEYS.WEB.Utilities.Responses;

namespace OEYS.WEB.Business.Services.Abstracts
{
    public interface IAuthService
    {
        Task<CustomResponse<CustomNoResponse>> SignIn(UserSignInRequest request);

        Task<CustomResponse<CustomNoResponse>> SignUp(UserSignUpRequest request);
    }
}
