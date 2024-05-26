using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using OEYS.WEB.Business.Services.Abstracts;
using OEYS.WEB.Models.Contexts;
using OEYS.WEB.Models.Dtos.Users;
using OEYS.WEB.Models.Entities;
using OEYS.WEB.Utilities.Password;
using System.Security.Claims;
using OEYS.WEB.Utilities.Responses;
using OEYS.WEB.Exceptions;

namespace OEYS.WEB.Business.Services.Concretes
{
    public class AuthManager(IHttpContextAccessor httpContextAccessor) : IAuthService
    {
        public async Task<CustomResponse<CustomNoResponse>> SignIn(UserSignInRequest request)
        {
            if (DapperDatabaseConnection.DataReaderState)
            {
                string sql = string.Empty;
                sql = "SELECT * FROM Users WHERE Email = @Email";
                DapperDatabaseConnection.DataReaderBusy();

                var user = (await DapperDatabaseConnection.QueryAsync<User>(sql, new { Email = request.Email?.Trim() })).FirstOrDefault();
                if (user == null) { DapperDatabaseConnection.DataReaderReady(); return CustomResponse<CustomNoResponse>.Fail("Email Veya Şifre Yanlış"); }

                var passwordCheck = HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);
                if (!passwordCheck) { DapperDatabaseConnection.DataReaderReady(); return CustomResponse<CustomNoResponse>.Fail("Email Veya Şifre Yanlış"); }

                List<Claim> claims = new();
                claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                var sql2 = "SELECT r.* FROM Roles r JOIN UserRoles ur ON r.Id = ur.RoleId WHERE ur.UserId = @UserId";

                var role = (await DapperDatabaseConnection.QueryAsync<Role>(sql2, new { UserId = user.Id })).FirstOrDefault();
                claims.Add(new Claim(ClaimTypes.Role, role.Name));

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                var authenticationProperties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
                };

                await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

                DapperDatabaseConnection.DataReaderReady();

                return CustomResponse<CustomNoResponse>.Success();
            }
            DapperDatabaseConnection.DataReaderReady();
            throw new DatabaseException();
        }

        public async Task<CustomResponse<CustomNoResponse>> SignUp(UserSignUpRequest request)
        {
            if (request.Password != request.PasswordRepeat) return CustomResponse<CustomNoResponse>.Fail("Şifreler Uyuşmuyor");

            if (DapperDatabaseConnection.DataReaderState)
            {
                string sql = "SELECT * FROM Users WHERE Email = @Email";

                DapperDatabaseConnection.DataReaderBusy();

                var user = (await DapperDatabaseConnection.QueryAsync<User>(sql, new { Email = request.Email?.Trim() })).FirstOrDefault();
                if (user != null) { DapperDatabaseConnection.DataReaderReady(); return CustomResponse<CustomNoResponse>.Fail("Bu Email Adresi Kullanılmaktadır"); }

                User newUser = request;

                await DapperDatabaseConnection.InsertAsync(newUser);

                var user2 = (await DapperDatabaseConnection.QueryAsync<User>(sql, new { Email = request.Email?.Trim() })).FirstOrDefault();

                var role = (await DapperDatabaseConnection.QueryAsync<Role>("Select * from Roles where Name=@Name", new { Name = "Participant" })).FirstOrDefault();

                UserRole userRole = new()
                {
                    RoleId = role.Id,
                    UserId = user2.Id,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    UpdatedDate = DateTime.Now
                };

                await DapperDatabaseConnection.InsertAsync(userRole);

                DapperDatabaseConnection.DataReaderReady();

                return CustomResponse<CustomNoResponse>.Success();
            }
            DapperDatabaseConnection.DataReaderReady();
            throw new DatabaseException();
        }



    }
}
