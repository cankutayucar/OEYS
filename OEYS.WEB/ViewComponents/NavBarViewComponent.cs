using Microsoft.AspNetCore.Mvc;

namespace OEYS.WEB.ViewComponents
{
    public class NavBarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View();
        }
    }
}
