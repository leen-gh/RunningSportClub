using System.Security.Claims;

namespace WebApplication3
{
    public static class PrincipalExtentions
    {
        //extention is a method that without modifing the orginal class
       
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
