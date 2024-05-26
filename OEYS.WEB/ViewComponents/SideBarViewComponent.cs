using Microsoft.AspNetCore.Mvc;
using OEYS.WEB.Models.Contexts;
using OEYS.WEB.Models.Entities;

namespace OEYS.WEB.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await DapperDatabaseConnection.GetData<User>(int.Parse(HttpContext.User.Identity.Name));


            return View(user);
        }
    }
}
