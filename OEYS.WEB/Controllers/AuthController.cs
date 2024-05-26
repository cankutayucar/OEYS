using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using OEYS.WEB.Business.Services.Abstracts;
using OEYS.WEB.Models.Dtos.Users;
using System.Text;

namespace OEYS.WEB.Controllers
{
    public class AuthController([FromServices] IAuthService authService, [FromServices] IToastNotification toastNotification) : Controller
    {
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInRequest request)
        {
            var result = await authService.SignIn(request);
            if (!result.IsSuccessfull)
            {
                StringBuilder sb = new();
                foreach (var error in result.Errors) sb.AppendLine(error);
                toastNotification.AddErrorToastMessage(sb.ToString(), new ToastrOptions { Title = "" });
            }

            return HttpContext.User.IsInRole("Admin") ? RedirectToAction("Index", "Activity") : RedirectToAction("Index", "UserActivity");
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpRequest request)
        {
            var result = await authService.SignUp(request);
            if (!result.IsSuccessfull)
            {
                StringBuilder sb = new();
                foreach (var error in result.Errors) sb.AppendLine(error);
                toastNotification.AddErrorToastMessage(sb.ToString(), new ToastrOptions { Title = "" });

            }
            return RedirectToAction("SignIn");
        }

        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn");
        }
    }
}
