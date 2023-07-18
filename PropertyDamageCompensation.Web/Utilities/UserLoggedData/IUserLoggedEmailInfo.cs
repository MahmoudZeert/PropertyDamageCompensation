using System.Security.Claims;

namespace PropertyDamageCompensation.Web.Utilities.UserLoggedData
{
    public interface IUserLoggedEmailInfo
    {
         Task GetRecipientInfo(ClaimsPrincipal user);
    }
}
