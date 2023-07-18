using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace PropertyDamageCompensation.Web.Utilities.UserLoggedData
{
    public class UseLoggedInInfoSerice : IUserLoggedEmailInfo
    {
        async Task IUserLoggedEmailInfo.GetRecipientInfo(ClaimsPrincipal user)
        {
            //string userId = "";
            //var claim = user.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier);
            //if (claim != null)
            //{
            //    userId = claim.Value;
            //}


            //return personalInfoId;
        }
    }
}






