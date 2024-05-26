using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OEYS.WEB.Business.Services.Abstracts;
using System.Text;

namespace OEYS.WEB.Controllers
{
    [Authorize(Roles = "Admin,Participant")]
    public class UserActivityController(IActivityService activityService) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> Subscribe(int id)
        {
            var result = await activityService.SubscribeActivity(id);
            if (!result.IsSuccessfull)
            {
                StringBuilder sb = new();
                foreach (var item in result.Errors)
                {
                    sb.AppendLine(item);
                }
                return Json(new { success = result.IsSuccessfull, message = sb.ToString() });
            }
            return Json(new { success = result.IsSuccessfull });
        }



        [HttpGet]
        public IActionResult GetAllSubscribeActivities()
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

            var result = await activityService.GetSubscribeActivitiesPaginationAndSearching(skip, pageSize, searchValue, sortColumn, sortColumnDirection);
            recordsTotal = result.Data.Item2;

            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = result.Data.Item1 };
            return Ok(jsonData);
        }

        [HttpPost]
        public async Task<JsonResult> LeaveActivity(int id)
        {
            var result = await activityService.LeaveActivity(id);

            return Json(new { success = result.IsSuccessfull });
        }

    }
}
