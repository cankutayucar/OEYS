using OEYS.WEB.Models.Dtos.Activities;
using OEYS.WEB.Models.Dtos.Users;
using OEYS.WEB.Utilities.Responses;

namespace OEYS.WEB.Business.Services.Abstracts
{
    public interface IActivityService
    {
        Task<CustomResponse<(IEnumerable<ActivityDto>, int)>> GetActivitiesPaginationAndSearching(int pageNumber, int pageSize, string? searchValue, string? sortColumn, string? sortColumnDirection);
        Task<CustomResponse<bool>> DeleteActivity(ActivityDeleteDto dto);
        Task<CustomResponse<ActivityDto>> GetActivityDetail(int id);
        Task<CustomResponse<CustomNoResponse>> CreateActivity(ActivityCreateDto dto);
        Task<CustomResponse<ActivityUpdateDto>> GetActivityDetailForUpdate(int id);
        Task<CustomResponse<CustomNoResponse>> UpdateActivity(ActivityUpdateDto dto);
        Task<CustomResponse<(IEnumerable<ParticipiantUserActivityDto>, int)>> GetActivitiesUserPaginationAndSearching(int pageNumber, int pageSize, string activityId, string? searchValue);
        Task<CustomResponse<(IEnumerable<ApprovalActivityDto>, int)>> GetApprovalActivitiesPaginationAndSearching(int pageNumber, int pageSize);
        Task<CustomResponse<CustomNoResponse>> ApproveAndRejectActivity(ApproveAndRejectActivityDto dto);



        Task<CustomResponse<(bool, string)>> SubscribeActivity(int id);
        Task<CustomResponse<(IEnumerable<ActivityDto>, int)>> GetSubscribeActivitiesPaginationAndSearching(int pageNumber, int pageSize, string? searchValue, string? sortColumn, string? sortColumnDirection);
        Task<CustomResponse<CustomNoResponse>> LeaveActivity(int activityId);
    }
}
