using OEYS.WEB.Models.Contexts;

namespace OEYS.WEB.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                DapperDatabaseConnection.DataReaderReady();
                context.Response.Redirect("/Anasayfa/ErrorPage");
            }
        }
    }
}
