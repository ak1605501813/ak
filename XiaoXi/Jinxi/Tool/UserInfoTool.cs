using Microsoft.AspNetCore.Http;

namespace Jinxi.Tool
{
    public static class UserInfoTool
    {
        public static PrincipalUser CurrentUser(this IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor?.HttpContext?.User;
            PrincipalUser currentUser = new PrincipalUser();
            if (user != null && user.Identity.IsAuthenticated)
            {

                currentUser.User = user.FindFirst("user")?.Value;
                currentUser.Userid = user.FindFirst("UserId")?.Value;
            }
            return currentUser;
        }
    }
    public class PrincipalUser
    {
        public string User { get; set; }
        public string Userid { get; set; }
    }
}
