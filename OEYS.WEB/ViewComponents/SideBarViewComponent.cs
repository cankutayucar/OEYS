using Microsoft.AspNetCore.Mvc;

namespace OEYS.WEB.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
