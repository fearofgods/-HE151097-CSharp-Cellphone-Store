using Microsoft.AspNetCore.Builder;

namespace CellphoneStore.Middlewares
{
    public static class AppExtension
    {
        public static IApplicationBuilder UseCheckAccess(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckAccessMiddleware>();
        }
    }
}
