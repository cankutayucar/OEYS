using OEYS.WEB.Business.Services.Abstracts;
using OEYS.WEB.Enums;
using OEYS.WEB.Exceptions;
using OEYS.WEB.Mappers;
using OEYS.WEB.Models.Contexts;
using OEYS.WEB.Models.Dtos.Activities;
using OEYS.WEB.Models.Dtos.Users;
using OEYS.WEB.Models.Entities;
using OEYS.WEB.Utilities.Responses;
using System.Buffers;

namespace OEYS.WEB.Business.Services.Concretes
{
    public class ActivityManager(IHttpContextAccessor httpContextAccessor) : IActivityService
    {
        #region admin

        public async Task<CustomResponse<(IEnumerable<ActivityDto>, int)>> GetActivitiesPaginationAndSearching(int pageNumber, int pageSize, string? searchValue, string? sortColumn, string? sortColumnDirection)
        {
            if (DapperDatabaseConnection.DataReaderState)
            {
                DapperDatabaseConnection.DataReaderBusy();
                var sql = "SELECT * FROM Activities";

                sql += !string.IsNullOrWhiteSpace(searchValue) ? $" where Name like '%{searchValue}%' and IsDeleted = 0" : $" where IsDeleted = 0";

                sql += (!(string.IsNullOrWhiteSpace(sortColumn) && !string.IsNullOrWhiteSpace(sortColumnDirection))) ? $" ORDER BY {sortColumn} {sortColumnDirection}" : "";

                var result = await DapperDatabaseConnection.QueryPagingAsync<Activity>(sql, pageNumber, pageSize);
                var data = result.Item1.ToList().Select(x => (ActivityDto)x);
                var count = result.Item2;
                DapperDatabaseConnection.DataReaderReady();

                return CustomResponse<(IEnumerable<ActivityDto>, int)>.Success((data, count));
            }

            DapperDatabaseConnection.DataReaderReady();
            throw new DatabaseException();
        }

        public async Task<CustomResponse<(IEnumerable<ParticipiantUserActivityDto>, int)>> GetActivitiesUserPaginationAndSearching(int pageNumber, int pageSize, string activityId, string? searchValue)
        {
            if (DapperDatabaseConnection.DataReaderState)
            {
                DapperDatabaseConnection.DataReaderBusy();

                var sql = $"select * from Users where Id in (select UserId from UserActivities where ActivityId = {activityId} and Subscribe = {(int)ActivitySubscribeEnum.approved}) and IsDeleted = 0";

                sql += !string.IsNullOrWhiteSpace(searchValue) ? $" and Name like '%{searchValue}%'" : "";

                var result = await DapperDatabaseConnection.QueryPagingAsync<User>(sql, pageNumber, pageSize);
                var data = result.Item1.ToList().Select(x => (ParticipiantUserActivityDto)x);
                var count = result.Item2;

                DapperDatabaseConnection.DataReaderReady();

                return CustomResponse<(IEnumerable<ParticipiantUserActivityDto>, int)>.Success((data, count));
            }

            DapperDatabaseConnection.DataReaderReady();
            throw new DatabaseException();
        }

        public async Task<CustomResponse<bool>> DeleteActivity(ActivityDeleteDto dto)
        {
            if (DapperDatabaseConnection.DataReaderState)
            {
                DapperDatabaseConnection.DataReaderBusy();

                var sql = $"UPDATE Activities SET IsDeleted = 1 WHERE Id = {dto.id}";

                var result = await DapperDatabaseConnection.ExecuteAsync(sql);

                DapperDatabaseConnection.DataReaderReady();

                return CustomResponse<bool>.Success(result > 0);
            }

            DapperDatabaseConnection.DataReaderReady();
            throw new DatabaseException();
        }

        public async Task<CustomResponse<ActivityDto>> GetActivityDetail(int id)
        {
            if (DapperDatabaseConnection.DataReaderState)
            {
                DapperDatabaseConnection.DataReaderBusy();

                var sql = $"SELECT * FROM Activities WHERE Id = @id";

                var result = (await DapperDatabaseConnection.QueryAsync<Activity>(sql, new { id = id })).FirstOrDefault();

                ActivityDto activityDto = result;

                var sql2 = $"SELECT * FROM UserActivities WHERE ActivityId = @ActivityId and Subscribe = 1";

                activityDto.TotalParticipiantCount = (await DapperDatabaseConnection.QueryAsync<Activity>(sql2, new { ActivityId = id })).Count();

                DapperDatabaseConnection.DataReaderReady();

                return CustomResponse<ActivityDto>.Success(activityDto);
            }

            DapperDatabaseConnection.DataReaderReady();
            throw new DatabaseException();
        }

        public async Task<CustomResponse<CustomNoResponse>> CreateActivity(ActivityCreateDto dto)
        {
            if (DapperDatabaseConnection.DataReaderState)
            {
                DapperDatabaseConnection.DataReaderBusy();

                Activity activity = dto;

                await DapperDatabaseConnection.InsertAsync(activity);

                DapperDatabaseConnection.DataReaderReady();

                return CustomResponse<CustomNoResponse>.Success();
            }

            DapperDatabaseConnection.DataReaderReady();
            throw new DatabaseException();
        }

        public async Task<CustomResponse<ActivityUpdateDto>> GetActivityDetailForUpdate(int id)
        {
            if (DapperDatabaseConnection.DataReaderState)
            {
                DapperDatabaseConnection.DataReaderBusy();

                var sql = $"SELECT * FROM Activities WHERE Id = @id";

                var result = (await DapperDatabaseConnection.QueryAsync<Activity>(sql, new { id = id })).FirstOrDefault();

                ActivityUpdateDto activityDto = result;

                DapperDatabaseConnection.DataReaderReady();

                return CustomResponse<ActivityUpdateDto>.Success(activityDto);
            }

            DapperDatabaseConnection.DataReaderReady();
            throw new DatabaseException();
        }

        public async Task<CustomResponse<CustomNoResponse>> UpdateActivity(ActivityUpdateDto dto)
        {
            if (DapperDatabaseConnection.DataReaderState)
            {
                DapperDatabaseConnection.DataReaderBusy();

                var activity = await DapperDatabaseConnection.GetData<Activity>(dto.Id);

                var newData = ObjectMapper.Mapper.Map(dto, activity);

                await DapperDatabaseConnection.UpdateAsync(newData);

                DapperDatabaseConnection.DataReaderReady();

                return CustomResponse<CustomNoResponse>.Success();
            }

            DapperDatabaseConnection.DataReaderReady();
            throw new DatabaseException();
        }


        public async Task<CustomResponse<(IEnumerable<ApprovalActivityDto>, int)>> GetApprovalActivitiesPaginationAndSearching(int pageNumber, int pageSize)
        {
            if (DapperDatabaseConnection.DataReaderState)
            {
                DapperDatabaseConnection.DataReaderBusy();

                var sql = @"select u.Id as UId, a.Id as AId, u.Name as UName, u.Surname, u.Email,a.Name as AName from Users u 
                            inner join UserActivities ua on u.Id = ua.UserId
                            inner join Activities a on ua.ActivityId = a.Id
                            where ua.Subscribe = 0 and a.IsDeleted = 0";

                var result = await DapperDatabaseConnection.QueryPagingAsync<ApprovalActivityDto>(sql, pageNumber, pageSize);
                var data = result.Item1.ToList();
                var count = result.Item2;

                DapperDatabaseConnection.DataReaderReady();

                return CustomResponse<(IEnumerable<ApprovalActivityDto>, int)>.Success((data, count));
            }

            DapperDatabaseConnection.DataReaderReady();
            throw new DatabaseException();
        }


        public async Task<CustomResponse<CustomNoResponse>> ApproveAndRejectActivity(ApproveAndRejectActivityDto dto)
        {
            if (DapperDatabaseConnection.DataReaderState)
            {
                DapperDatabaseConnection.DataReaderBusy();

                var sql = $"UPDATE UserActivities SET Subscribe = @Subscribe WHERE UserId = @UserId AND ActivityId = @ActivityId AND Subscribe = @RASubscribe";

                await DapperDatabaseConnection.ExecuteAsync(sql, new { Subscribe = dto.ApproveAndReject == 0 ? ActivitySubscribeEnum.rejected : ActivitySubscribeEnum.approved, UserId = dto.UserId, ActivityId = dto.ActivityId, RASubscribe = ActivitySubscribeEnum.pending });

                DapperDatabaseConnection.DataReaderReady();

                return CustomResponse<CustomNoResponse>.Success();
            }

            DapperDatabaseConnection.DataReaderReady();
            throw new DatabaseException();
        }


        #endregion




        #region user

        public async Task<CustomResponse<(bool, string)>> SubscribeActivity(int id)
        {
            if (DapperDatabaseConnection.DataReaderState)
            {
                DapperDatabaseConnection.DataReaderBusy();

                var userId = httpContextAccessor.HttpContext.User.Identity.Name;

                var sql = $"SELECT * FROM UserActivities WHERE UserId = @UserId AND ActivityId = @Activity";

                var userActivities = await DapperDatabaseConnection.QueryAsync<UserActivity>(sql, new { UserId = userId, Activity = id });

                List<bool> isExist = new();

                foreach (var activity in userActivities)
                {
                    if (activity != null && (activity?.Subscribe == (int)ActivitySubscribeEnum.approved || activity?.Subscribe == (int)ActivitySubscribeEnum.pending))
                    {
                        isExist.Add(true);
                    }
                }

                if(isExist.Count>0 && isExist.Contains(true))
                {
                    DapperDatabaseConnection.DataReaderReady();
                    return CustomResponse<(bool, string)>.Fail("Bu aktiviteye zaten kayıtlısınız. Kaydınız Onaylandıktan Sonra Etkinliklerim Ekranından Görünecektir");
                }

                var newActivity = Activator.CreateInstance<UserActivity>();
                newActivity.UserId = int.Parse(userId);
                newActivity.ActivityId = id;
                newActivity.Subscribe = (int)ActivitySubscribeEnum.pending;
                newActivity.CreatedDate = DateTime.Now;
                newActivity.UpdatedDate = DateTime.Now;
                newActivity.IsDeleted = false;
                newActivity.IsActive = true;

                await DapperDatabaseConnection.InsertAsync(newActivity);

                DapperDatabaseConnection.DataReaderReady();

                return CustomResponse<(bool, string)>.Success((true, "Başarılı"));
            }

            DapperDatabaseConnection.DataReaderReady();
            throw new DatabaseException();
        }

        public async Task<CustomResponse<(IEnumerable<ActivityDto>, int)>> GetSubscribeActivitiesPaginationAndSearching(int pageNumber, int pageSize, string? searchValue, string? sortColumn, string? sortColumnDirection)
        {
            if (DapperDatabaseConnection.DataReaderState)
            {
                DapperDatabaseConnection.DataReaderBusy();

                var userId = httpContextAccessor.HttpContext.User.Identity.Name;

                var sql = $"select * from Activities where Id in (select ActivityId from UserActivities where UserId = {userId} and Subscribe = 1)";

                sql += !string.IsNullOrWhiteSpace(searchValue) ? $" and Name like '%{searchValue}%' and IsDeleted = 0" : $" and IsDeleted = 0";

                sql += (!(string.IsNullOrWhiteSpace(sortColumn) && !string.IsNullOrWhiteSpace(sortColumnDirection))) ? $" ORDER BY {sortColumn} {sortColumnDirection}" : "";

                var result = await DapperDatabaseConnection.QueryPagingAsync<Activity>(sql, pageNumber, pageSize);
                var data = result.Item1.ToList().Select(x => (ActivityDto)x);
                var count = result.Item2;
                DapperDatabaseConnection.DataReaderReady();

                return CustomResponse<(IEnumerable<ActivityDto>, int)>.Success((data, count));
            }

            DapperDatabaseConnection.DataReaderReady();
            throw new DatabaseException();
        }


        public async Task<CustomResponse<CustomNoResponse>> LeaveActivity(int activityId)
        {
            if (DapperDatabaseConnection.DataReaderState)
            {
                DapperDatabaseConnection.DataReaderBusy();

                var userId = httpContextAccessor.HttpContext.User.Identity.Name;

                var sql = $"delete UserActivities WHERE UserId = @UserId AND ActivityId = @ActivityId AND Subscribe = @RASubscribe";

                await DapperDatabaseConnection.ExecuteAsync(sql, new { UserId = userId, ActivityId = activityId, RASubscribe = ActivitySubscribeEnum.approved });

                DapperDatabaseConnection.DataReaderReady();

                return CustomResponse<CustomNoResponse>.Success();
            }

            DapperDatabaseConnection.DataReaderReady();
            throw new DatabaseException();
        }

        #endregion
    }
}
