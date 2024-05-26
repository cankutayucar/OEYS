using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OEYS.WEB.Business.Services.Abstracts;
using OEYS.WEB.Enums;
using OEYS.WEB.Models.Contexts;
using OEYS.WEB.Models.Dtos.Activities;
using OEYS.WEB.Models.Entities;
using OEYS.WEB.Utilities.Extensions;

namespace OEYS.WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ActivityController(IActivityService activityService) : Controller
    {

        #region adnmin
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAllActivities()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var result = await activityService.GetActivitiesPaginationAndSearching(skip, pageSize, searchValue, sortColumn, sortColumnDirection);
            recordsTotal = result.Data.Item2;

            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result.Data.Item1 };
            return Ok(jsonData);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteActivity(ActivityDeleteDto request)
        {
            var result = await activityService.DeleteActivity(request);
            return Json(new { success = result.IsSuccessfull });
        }

        [HttpPost]
        public async Task<JsonResult> Detail(int id)
        {
            var result = await activityService.GetActivityDetail(id);
            return Json(result.Data);
        }

        [HttpPost]
        public async Task<JsonResult> Create(ActivityCreateDto request)
        {
            var result = await activityService.CreateActivity(request);
            return Json(new { success = result.IsSuccessfull });
        }

        [HttpGet]
        public async Task<JsonResult> GetActivities()
        {
            var activities = EnumHelper.GetEnumDisplayNames<ActivityEnum>();
            return Json(activities);
        }

        [HttpGet]
        public async Task<JsonResult> GetActivityDetailForUpdate(int id)
        {
            var result = await activityService.GetActivityDetailForUpdate(id);
            var activities = EnumHelper.GetEnumDisplayNames<ActivityEnum>();
            return Json(new { activity = result.Data, activityTypes = activities });
        }

        [HttpPost]
        public async Task<JsonResult> Update(ActivityUpdateDto request)
        {
            var result = await activityService.UpdateActivity(request);
            return Json(new { success = result.IsSuccessfull });
        }

        [HttpPost]
        public async Task<IActionResult> GetUsersForActivity()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var activityId = Request.Form["ActivityId"].FirstOrDefault();

            var result = await activityService.GetActivitiesUserPaginationAndSearching(skip, pageSize, activityId, searchValue);
            recordsTotal = result.Data.Item2;

            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result.Data.Item1 };
            return Ok(jsonData);
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> ApproveActivityListForUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetApprovalActivitiesPaginationAndSearching()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var result = await activityService.GetApprovalActivitiesPaginationAndSearching(skip, pageSize);
            recordsTotal = result.Data.Item2;

            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result.Data.Item1 };
            return Ok(jsonData);
        }

        [HttpPost]
        public async Task<JsonResult> ApproveAndRejectActivity(ApproveAndRejectActivityDto request)
        {
            var result = await activityService.ApproveAndRejectActivity(request);
            return Json(new { success = result.IsSuccessfull });
        }

    }
}
