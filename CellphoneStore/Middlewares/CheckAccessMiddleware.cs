using CellphoneStore.Logics;
using CellphoneStore.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CellphoneStore.Middlewares
{
    public class CheckAccessMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckAccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string ADMIN = "sa";
            string USER = "us";


            string json = httpContext.Session.GetString("user");
            User userX = null;
            if (!string.IsNullOrEmpty(json))
            {
                userX = JsonConvert.DeserializeObject<User>(json);
            }

            string path = httpContext.Request.Path;
            
            if (path.StartsWith("/Admin") == true || path.StartsWith("/admin") == true)
            {
                if (userX != null)
                {
                    if (userX.Role.Equals(ADMIN))
                    {
                        //Cho request đi qua
                        await _next(httpContext);
                    }
                    else if (userX.Role.Equals(USER))
                    {
                        //Chuyển hướng sang đăng nhập
                        httpContext.Response.Redirect("/Authentication/Authentications?code=403");
                    }
                }
                else
                {
                    //Chuyển hướng sang đăng nhập
                    httpContext.Response.Redirect("/Authentication/Authentications?code=401");
                }
                //Console.WriteLine("CheckAcessMiddleware: Cấm truy cập");
                //await Task.Run(
                //  async () =>
                //  {
                //      string html = $"<h1>{path}</h1>";
                //      httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                //      await httpContext.Response.WriteAsync(html);
                //  }
                //);    
            }
            else
            {
                // Thiết lập Header cho HttpResponse
                httpContext.Response.Headers.Add("throughCheckAcessMiddleware", new[] { DateTime.Now.ToString() });
                // Chuyển Middleware tiếp theo trong pipeline
                await _next(httpContext);

            }
        }
    }
}
