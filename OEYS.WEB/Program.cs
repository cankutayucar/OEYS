using Microsoft.AspNetCore.Authentication.Cookies;
using NToastNotify;
using OEYS.WEB.Business.Services.Abstracts;
using OEYS.WEB.Business.Services.Concretes;
using OEYS.WEB.Configurations.SeedData;
using OEYS.WEB.Middlewares;
using OEYS.WEB.Models.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation().AddNToastNotifyToastr(new ToastrOptions
{
    ProgressBar = false,
    TimeOut = 3000,
    CloseButton = true,
    PositionClass = ToastPositions.TopRight
});

#region authentication
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = new PathString("/Auth/SignIn");
    options.LogoutPath = new PathString("/Auth/SignIn");
    options.AccessDeniedPath = new PathString("/Anasayfa/AccessDenied");
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;
    options.Cookie.Name = "_rxyXId";
});
#endregion

#region cors
builder.Services.AddCors();
#endregion

#region injections
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<IActivityService,ActivityManager>();
#endregion

var app = builder.Build();

DapperDatabaseConnection.DapperDatabaseConnectionConfigure(app.Services.GetRequiredService<IConfiguration>());

#region SeedData
//await MockData.AddData();
#endregion

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseMiddleware<CustomExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=SignIn}/{id?}");

app.Run();
