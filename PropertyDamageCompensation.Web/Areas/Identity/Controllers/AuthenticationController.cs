using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyDamageCompensation.Web.Areas.Compensation.Models;
using PropertyDamageCompensation.Web.Areas.Identity.Models;
using PropertyDamageCompensation.Web.Data;
using PropertyDamageCompensation.Web.Email;
using System.Security.Claims;

namespace PropertyDamageCompensation.Web.Areas.Identity.Controllers;

[Area("Identity")]
public class AuthenticationController : Controller
{
    private readonly SignInManager<ApplicationUser> _sinInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly IEmailSending _emailSending;

    public AuthenticationController(SignInManager<ApplicationUser> sinInManager,
        UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,
        ApplicationDbContext context,IEmailSending emailSending)
    {
        _sinInManager = sinInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
        _emailSending = emailSending;
    }

    public async Task<IActionResult> Switch()
    {
        string userId = "";
        var claim = User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier);
        if (claim != null)
        {
            userId = claim.Value;
            var personalinfoVM =
           await (from personalinfo in _context.PersonalInfo
                  join POBtown in _context.Towns on personalinfo.PlaceOfBirthTownId
                      equals POBtown.Id
                  join LIVTown in _context.Towns on personalinfo.LivingTownId equals LIVTown.Id
                  where personalinfo.UserId == userId
                  select new PersonalInfoViewModel
                  {
                      Id = personalinfo.Id,
                      UserId = personalinfo.UserId,
                      FirstName = personalinfo.FirstName,
                      MiddleName = personalinfo.MiddleName,
                      LastName = personalinfo.LastName,
                      DateOfBirth = personalinfo.DateOfBirth,
                      PlaceOfBirthTownName = POBtown.Name,
                      PlaceOfBirthTownId = POBtown.Id,
                      RegisterId = personalinfo.RegisterId,
                      Address = personalinfo.Address,
                      Street = personalinfo.Street,
                      Phone = personalinfo.Phone,
                      LivingTownName = LIVTown.Name,
                      LivingTownId = LIVTown.Id

                  }).FirstOrDefaultAsync();
            if (personalinfoVM == null)
            {
                return RedirectToAction("Create","PersonalInfo",new {area="Compensation"});
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        return RedirectToAction("Create","PersonalInfo", new { area = "Compensation" });

    }
    public IActionResult Get()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpGet]
    public  IActionResult Login()
    {

        var model = new UserLoginViewModel();
    
        return View(model);

    }

    [AllowAnonymous]
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Login(UserLoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var result = await _sinInManager.PasswordSignInAsync(model.Email,
                    model.Password, model.RemeberMe, false);
                if (result.Succeeded)
                {
                    //send an email to the logged user, notifying successfull loging in operation
                    var message = new EmailMessage();
                    message.RecipientName = model.Email;
                    message.RecipientEmail = model.Email;
                    message.Subject = "Loging in success";
                    message.Body = "You hav successfully logged on !";
                    await _emailSending.SendEmailHotmailAsync(message);
                    return RedirectToAction("Index","Home",new {area=""});
                }
                else
                {

                    ModelState.AddModelError("", "Invalid Login Attempt");
                    model.Password = "";
                    return View();
                }
            }
            catch ( Exception e)
            {
                var re=e.Source.ToString();
                ModelState.AddModelError("", "Error occured"); ;

            }

        }
        return View();

    }

    [AllowAnonymous]
    [HttpGet ]
    public IActionResult Register()
    {
        var model = new UserRegistrationViweModel();
        return View(model);
    }


    [HttpPost]
    [AllowAnonymous]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Register(UserRegistrationViweModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.EmailAddress,
                Email = model.EmailAddress,
                IsApplicant=true

            };
            try
            {
                // check if this user already exists
                var resultUser = await _userManager.FindByNameAsync(model.EmailAddress);
                if (resultUser == null)
                {
                    var actionresult = await _userManager.CreateAsync(user, model.Password);
                    if (actionresult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Applicant");
                        await _sinInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Switch");
                    }
                    else
                        foreach (var error in actionresult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                }
                else
                {
                    ModelState.AddModelError(String.Empty, "User already exists !");

                }
            }
            catch (Exception)
            {

                return View();
            }
  
        }
        return View(); 
    }



    [HttpGet]
    [Authorize(Policy = "IsAdmin")]
    public async Task<IActionResult> ManageUserRoles(string userId)
    {
        var user=await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return View(new List<UserRolesViewModel>());
        }
        ViewData["UserId"] = user.Id;
        ViewData["UserName"] = user.FirstName + " " + user.LastName;
        var model = new List<UserRolesViewModel>();
        var roles = _roleManager.Roles.Where(role=>role.Name!="Applicant");
        foreach (IdentityRole role in roles)
        {
            var userRoleVM = new UserRolesViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                IsSelected = await _userManager.IsInRoleAsync(user, role.Name)
            };
        model.Add(userRoleVM);
        }

        return View(model);
    }


    [HttpPost]
    [Authorize(Policy = "IsAdmin")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> ManageUserRoles(string userId,List<UserRolesViewModel> model)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return View();
        }

        ViewData["UserName"] = user.FirstName + " " + user.LastName;
        var roles = await _userManager.GetRolesAsync(user);
        var result = await _userManager.RemoveFromRolesAsync(user, roles);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Unable to remove roles for this user");
            return View();

        }
        var userRolesSelected = model
            .Where(role => role.IsSelected == true)
            .Select(role => role.RoleName);

        result = await _userManager.AddToRolesAsync(user, userRolesSelected);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Unable to Add selected roles for this user");
            return View();
        }

        return RedirectToAction(nameof(ListOfOrganizationUsers));
    }

    [HttpGet]
    [Authorize(Policy = "IsAdmin")]
    public  async Task<IActionResult> ListOfOrganizationUsers()
    {

        var ListOfEmployeeUsers = await (from user in _userManager.Users
                           where user.IsApplicant == false
                           select user).ToListAsync();
        var model = new List<OrganizationUserViewModel>();
        foreach (ApplicationUser user in ListOfEmployeeUsers)
        {
            model.Add(new OrganizationUserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.Email,
                UserRoles = await GetUserRoles(user)
            });

        }

        return View(model);
    }
    private async Task<List<String>> GetUserRoles(ApplicationUser user)
    {
        return new List<string>(await _userManager.GetRolesAsync(user));
    }

    [HttpGet]
    [Authorize(Policy = "IsAdmin")]
    public async Task<IActionResult> EditOrganizationUser(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var user=await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        var model = new OrganizationUserViewModel
        {
            Id=user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        return View(model);
    }
    [HttpPost]
    [Authorize(Policy = "IsAdmin")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> EditOrganizationUser(string id,OrganizationUserViewModel model)
    {
        if (id==null || model == null ||id!=model.Id)
        {
            return NotFound();
        }
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(ListOfOrganizationUsers));
        }
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }
    }

    [AllowAnonymous]
    [HttpGet ]
    [Authorize(Policy = "IsAdmin")]
    public IActionResult RegisterOrganizationUser()
    {
        var model = new OrganizationUserRegistrationViewModel();
        return View(model);
    }


    [HttpPost]
    [AllowAnonymous]
    [AutoValidateAntiforgeryToken]
    [Authorize(Policy = "IsAdmin")]
    public async Task<IActionResult> RegisterOrganizationUser(OrganizationUserRegistrationViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                FirstName=model.FirstName,
                LastName=model.LastName,
                UserName = model.EmailAddress,
                Email = model.EmailAddress,
                IsApplicant=false

            };
            try
            {
                // check if this user already exists
                var resultUser = await _userManager.FindByNameAsync(model.EmailAddress);
                if (resultUser == null)
                {
                    var actionresult = await _userManager.CreateAsync(user, model.Password);
                    if (actionresult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Employee");
                        await _sinInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index","Home",new {area=""});
                    }
                    else
                        foreach (var error in actionresult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                }
                else
                {
                    ModelState.AddModelError(String.Empty, "User already exists !");

                }
            }
            catch (Exception)
            {

                return View();
            }
  

        }
        return View(); 
    }

    private async Task<bool> UserAlreadyExists(string userName)
    {
        var exists = await _context.Users.AnyAsync(u => u.UserName.ToLower() == userName.ToLower());

        if (exists)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}



 
