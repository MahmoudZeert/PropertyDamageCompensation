using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyDamageCompensation.Web.Areas.Compensation.Entities;
using PropertyDamageCompensation.Web.Areas.Compensation.Models;
using PropertyDamageCompensation.Web.Areas.Identity.Models;
using PropertyDamageCompensation.Web.Data;
using PropertyDamageCompensation.Web.Exceptions;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Controllers
{
    [Area("Compensation")]
    [Authorize]
    public class EndgineerDistrictController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EndgineerDistrictController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Policy = "IsChiefEngineer")]
        public async Task<IActionResult> Index()
        {
            if (_context.District == null || _context.District.Count() == 0)
            {
                throw new BadRequestException("Bad request : No districts defined! Please define districts before !!");
            }
            var engineers = await (from user in _context.Users
                                   join engineerRole in _context.UserRoles on user.Id equals engineerRole.UserId
                                   join role in _context.Roles on engineerRole.RoleId equals role.Id
                                   where user.IsApplicant == false && role.Name == "Engineer"
                                   join engineerdistrict in _context.DistrictEngineer on user.Id equals engineerdistrict.EngineerId
                                   into userDistrictGroup
                                   from ed in userDistrictGroup.DefaultIfEmpty()
                                   join district in _context.District on ed.DistrictId equals district.Id into districtGroup
                                   from dg in districtGroup.DefaultIfEmpty()
                                   group dg by new { user.Id, user.FirstName, user.LastName, user.Email } into userdistricFinaltGroup
                                   select new EngineerWithDistrictsViewModel
                                   {
                                       EmgineerId = userdistricFinaltGroup.Key.Id,
                                       EngineerName = userdistricFinaltGroup.Key.FirstName + " " + userdistricFinaltGroup.Key.LastName,
                                       EngineerEmailAddress = userdistricFinaltGroup.Key.Email,
                                       EngineerDistricts = (from d in userdistricFinaltGroup
                                                            select d.Name).ToList()
                                   }).AsNoTracking()
                                    .ToListAsync();
            if (engineers == null)
            {
                throw new BadRequestException("Bad request : No enginners defined! Please define engineer employees before !!");
            }

            return View(engineers);
        } 

        [HttpGet]
        [Authorize(Policy = "IsChiefEngineer")]
        public async Task<IActionResult> ManageEngineerDistricts(string userName,string userId)
        {
            ViewBag.UserName = userName;
            ViewBag.UserId = userId;
            var engineerDistrics = await (from district in _context.District
                                           join ed in (from de in _context.DistrictEngineer 
                                                       where de.EngineerId==userId
                                                       select de )
                                                       on district.Id equals ed.DistrictId
                                           into deGroup
                                           from deg in deGroup.DefaultIfEmpty()
                                           join eng in (from us in _context.Users 
                                                        where us.Id == userId
                                                        select us )
                                                        on deg.EngineerId equals eng.Id into dedGroup
                                           from dedf in dedGroup.DefaultIfEmpty()
                                           select new EngineerDistrictsViewModel
                                           {
                                               DistrictId = district.Id,
                                               DistrictName = district.Name,
                                               IsSelected = deg.EngineerId != null

                                           }).AsNoTracking().ToListAsync();
            return View(engineerDistrics);
        }
        [HttpPost]
        [Authorize(Policy = "IsChiefEngineer")]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> ManageEngineerDistricts(string userId,List<EngineerDistrictsViewModel>model)
        {
            if (userId==null)
            {
                throw new BadRequestException("Bad request + invalid Engineer Id ");
            }
            if (model==null)
            {
                throw new BadRequestException("Bad request we cannot proceed !! n");
            }
            var districtEngineers = await _context.DistrictEngineer
                .AsNoTracking()
                .Where(de => de.EngineerId == userId).ToListAsync();

            if (districtEngineers==null)
            {
                throw new NotFoundException("Engineer districts for this user :" + userId + " not found");
            }
            DistrictEngineer engineerDistrict ;
            foreach (EngineerDistrictsViewModel deng in model)
            {
                DistrictEngineer engDistrict = districtEngineers
                    .Find(de => de.DistrictId == deng.DistrictId);

                if (deng.IsSelected == true)
                {
                    if  (engDistrict == null)
                    {
                        engineerDistrict = new DistrictEngineer
                        {
                            DistrictId = deng.DistrictId,
                            EngineerId = userId
                        };
                        _context.DistrictEngineer.Add(engineerDistrict); 
                    };
                }
                else
                {
                    if (engDistrict != null)
                    {
                        engineerDistrict = new DistrictEngineer
                        {
                            Id = engDistrict.Id                          
                        };
                        _context.DistrictEngineer.Remove(engineerDistrict);
                    };
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
