using System;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyDamageCompensation.Web.Areas.Compensation.Entities;
using PropertyDamageCompensation.Web.Areas.Compensation.Models;
using PropertyDamageCompensation.Web.Data;
using PropertyDamageCompensation.Web.Exceptions;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Controllers
{
    [Area("Compensation")]
    [Authorize]
    public class PersonalInfoController : Controller
    {
        private readonly ApplicationDbContext _context;


        public PersonalInfoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Compensation/PersonalInfo
      [Authorize(Roles ="Admin,Applicant,Engineer")]
        public async Task<IActionResult> Index()
        {
            bool IsEmployee = HttpContext.User.IsInRole("Employee");
            var personlInfoId = await GetPersonalInfoIdOfLoggedInUser();
            string userRoleType = "";
            if (HttpContext.User.IsInRole("Engineer"))
            {
                userRoleType = "Engineer";
            }
            else if (HttpContext.User.IsInRole("Admin"))
            {
                userRoleType = "Admin";
            }
            else if (HttpContext.User.IsInRole("Chief Engineer"))
            {
                userRoleType = "Chief Engineer";
            }
            else if (HttpContext.User.IsInRole("Applicant"))
            {
                userRoleType = "Applicant";
            }

            var personalinfoVM = await GetPersonalInfoFor(userRoleType, personlInfoId);

            return View( personalinfoVM);
        }
        private async Task<int> GetPersonalInfoIdOfLoggedInUser()
        {
            string userId = "";
            var claim = User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                userId = claim.Value;
            }
            var personalInfoId = await (from person in _context.PersonalInfo
                                        where person.UserId == userId
                                        select person.Id).SingleOrDefaultAsync();

            return personalInfoId;
        }
        private async Task<List<PersonalInfoViewModel>> GetPersonalInfoFor(string userRoleType, int personalInfoId)
        {
            string userId = "";
            var claim = User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                userId = claim.Value;
            }
            if (userRoleType == "Applicant")
            {
                var personalinfoVM = await
                        (from personalinfo in _context.PersonalInfo
                         join POBtown in _context.Towns on personalinfo.PlaceOfBirthTownId
                         equals POBtown.Id
                         join LIVTown in _context.Towns on personalinfo.LivingTownId equals LIVTown.Id
                         where personalinfo.Id== personalInfoId
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

                         }).ToListAsync();

                return personalinfoVM;
            };
            if (userRoleType == "Engineer")
            {
                var personalinfoVM = await (
                    from pi in _context.PersonalInfo
                    join POBtown in _context.Towns on pi.PlaceOfBirthTownId
                    equals POBtown.Id
                    join LIVTown in _context.Towns on pi.LivingTownId equals LIVTown.Id
                    where pi.Application.Any(a => a.Town != null && a.Town.District != null &&
                    a.Town.District.DistrictEngineers.Any(de => de.EngineerId == userId))
                    select new PersonalInfoViewModel
                    {
                        Id = pi.Id,
                        UserId = pi.UserId,
                        FirstName = pi.FirstName,
                        MiddleName = pi.MiddleName,
                        LastName = pi.LastName,
                        DateOfBirth = pi.DateOfBirth,
                        PlaceOfBirthTownName = POBtown.Name,
                        PlaceOfBirthTownId = POBtown.Id,
                        RegisterId = pi.RegisterId,
                        Address = pi.Address,
                        Street = pi.Street,
                        Phone = pi.Phone,
                        LivingTownName = LIVTown.Name,
                        LivingTownId = LIVTown.Id

                    }).ToListAsync();
                return personalinfoVM;
            };

            if (userRoleType == "Admin")
            {
                var personalinfoVM = await
                      (from personalinfo in _context.PersonalInfo
                       join POBtown in _context.Towns on personalinfo.PlaceOfBirthTownId
                       equals POBtown.Id
                       join LIVTown in _context.Towns on personalinfo.LivingTownId equals LIVTown.Id
                      
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

                       }).ToListAsync();

                return personalinfoVM;
            }
            return new List<PersonalInfoViewModel>();
        }
        // GET: Compensation/PersonalInfo/Details/UserId
        [Authorize(Roles ="Admin,Applicant,Engineer")]
        public async Task<IActionResult> Details1(string? userId)
        {
            if(userId == null)
            {
                throw new NotFoundException($"No details info for user : {userId}  !");

            }
            if ( _context.PersonalInfo == null)
            {
                return RedirectToAction("Create");
            }
            bool IsEmployee = HttpContext.User.IsInRole("Employee");
            var personalinfoVM = await
                (from personalinfo in _context.PersonalInfo
                 join POBtown in _context.Towns on personalinfo.PlaceOfBirthTownId
                 equals POBtown.Id
                 join LIVTown in _context.Towns on personalinfo.LivingTownId equals LIVTown.Id
                 where IsEmployee ||  personalinfo.UserId ==userId
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
                return RedirectToAction("Create");
            }
            if (!IsEmployee)
            {
                return RedirectToAction("Details", new { id = personalinfoVM.Id });
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        [Authorize(Roles = "Admin,Applicant,Engineer")]
        // GET: Compensation/PersonalInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PersonalInfo == null)
            {
                throw new  BadRequestException("No personal Info for request");
            }

            var personalinfoVM = await
                (from personalinfo in _context.PersonalInfo
                 join POBtown in _context.Towns on personalinfo.PlaceOfBirthTownId
                 equals POBtown.Id
                 join LIVTown in _context.Towns on personalinfo.LivingTownId equals LIVTown.Id
                 where personalinfo.Id == id
                 select new PersonalInfoViewModel
                 {
                     Id=personalinfo.Id ,
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
                throw new NotFoundException("No personal info for this Id : {id}");
            }

            return View(personalinfoVM);
        }
        [Authorize(Roles = "Admin,Applicant")]
        // GET: Compensation/PersonalInfo/Create
        public IActionResult Create()
        {
           // var personalVieModel = new PersonalInfoViewModel();
            ViewData["LivingTownId"] = new SelectList(_context.Towns, "Id", "Name");
            ViewData["PlaceOfBirthTownId"] = new SelectList(_context.Towns, "Id", "Name");
            return View();
        }

        // POST: Compensation/PersonalInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Applicant")]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,MiddleName,LastName,DateOfBirth,PlaceOfBirthTownId,RegisterId,Address,Street,Phone,LivingTownId")] PersonalInfoViewModel personalInfoViewModel)
        {
            if (ModelState.IsValid)
            {
                string userId = "";
                var claim = User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier);
                if (claim != null)
                {
                    userId = claim.Value;
                }

                var personalInfo = new PersonalInfo
                {
                    UserId = userId,
                    FirstName = personalInfoViewModel.FirstName,
                    MiddleName = personalInfoViewModel.MiddleName,
                    LastName = personalInfoViewModel.LastName,
                    DateOfBirth = personalInfoViewModel.DateOfBirth ,
                    PlaceOfBirthTownId=personalInfoViewModel.PlaceOfBirthTownId,
                    RegisterId=personalInfoViewModel.RegisterId,
                    Street=personalInfoViewModel.Street,
                    Address=personalInfoViewModel.Address,
                    LivingTownId=personalInfoViewModel.LivingTownId,
                    Phone=personalInfoViewModel.Phone
                };
                _context.Add(personalInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LivingTownId"] = new SelectList(_context.Towns, "Id", "Name", personalInfoViewModel.LivingTownId);
            ViewData["PlaceOfBirthTownId"] = new SelectList(_context.Towns, "Id", "Name", personalInfoViewModel.PlaceOfBirthTownId);
            return View(personalInfoViewModel);
        }

        [Authorize(Roles = "Admin,Applicant")]
        // GET: Compensation/PersonalInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PersonalInfo == null)
            {
                throw new BadRequestException("No personal Info for request: No Id or some thimg wrong on the server");
            }

            var personalInfo = await _context.PersonalInfo.FindAsync(id);
            if (personalInfo == null)
            {
                throw new NotFoundException("No personal info for this Id : {id}");
            }

            var personalinfoVM = await
                (from personalinfo in _context.PersonalInfo
                 where personalinfo.Id == id
                 select new PersonalInfoViewModel
                 {
                     Id= personalinfo.Id ,
                     UserId = personalinfo.UserId,
                     FirstName = personalinfo.FirstName,
                     MiddleName = personalinfo.MiddleName,
                     LastName = personalinfo.LastName,
                     DateOfBirth = personalinfo.DateOfBirth,
                     PlaceOfBirthTownId = personalinfo.PlaceOfBirthTownId ,
                     RegisterId = personalinfo.RegisterId,
                     Address = personalinfo.Address,
                     Street = personalinfo.Street,
                     Phone = personalinfo.Phone,
                     LivingTownId = personalinfo.LivingTownId 

                 }).FirstOrDefaultAsync();
            if (personalinfoVM == null)
            {
                throw new NotFoundException("No personal info for this Id : {id}");
            }
            ViewData["LivingTownId"] = new SelectList(_context.Towns, "Id", "Name", personalinfoVM.LivingTownId);
            ViewData["PlaceOfBirthTownId"] = new SelectList(_context.Towns, "Id", "Name", personalinfoVM.PlaceOfBirthTownId);
            return View(personalinfoVM);
        }

        // POST: Compensation/PersonalInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Applicant")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,DateOfBirth,PlaceOfBirthTownId,RegisterId,Address,Street,Phone,LivingTownId")] PersonalInfoViewModel personalInfoViewModel)
        {
            if (id != personalInfoViewModel.Id)
            {
                throw new BadRequestException("No personal info for this Id : {id}");
            }
            if (ModelState.IsValid)
            {

                var personalInfo = await _context.PersonalInfo.FindAsync(id);
                if (personalInfo == null)
                {
                   throw new NotFoundException("No personal info for this Id : {id}");
                };
                personalInfo.FirstName = personalInfoViewModel.FirstName;
                personalInfo.MiddleName = personalInfoViewModel.MiddleName;
                personalInfo.LastName = personalInfoViewModel.LastName;
                personalInfo.DateOfBirth = personalInfoViewModel.DateOfBirth;
                personalInfo.PlaceOfBirthTownId = personalInfoViewModel.PlaceOfBirthTownId;
                personalInfo.RegisterId = personalInfoViewModel.RegisterId;
                personalInfo.Address = personalInfoViewModel.Address;
                personalInfo.Street = personalInfoViewModel.Street;
                personalInfo.LivingTownId = personalInfoViewModel.LivingTownId;
                personalInfo.Phone = personalInfoViewModel.Phone;

                _context.Update(personalInfo);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details",new { id });
            }
            ViewData["LivingTownId"] = new SelectList(_context.Towns, "Id", "Name", personalInfoViewModel.LivingTownId);
            ViewData["PlaceOfBirthTownId"] = new SelectList(_context.Towns, "Id", "Name", personalInfoViewModel.PlaceOfBirthTownId);
            return View(personalInfoViewModel);
        }

        [Authorize(Roles = "Admin")]
        // GET: Compensation/PersonalInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PersonalInfo == null)
            {
                throw new NotFoundException("No personal info for this Id : {id}");
            }

            var personalinfoVM = await
                           (from personalinfo in _context.PersonalInfo
                            join POBtown in _context.Towns on personalinfo.PlaceOfBirthTownId
                            equals POBtown.Id
                            join LIVTown in _context.Towns on personalinfo.LivingTownId equals LIVTown.Id
                            where personalinfo.Id == id
                            select new PersonalInfoViewModel
                            {
                                Id=personalinfo.Id,
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
                throw new NotFoundException("No personal info for this Id : {id}");
            }

            return View(personalinfoVM);
        }
        [Authorize(Roles = "Admin")]
        // POST: Compensation/PersonalInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var personalInfo = await _context.PersonalInfo.FindAsync(id);
            if (personalInfo != null)
            {
                _context.PersonalInfo.Remove(personalInfo);
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalInfoExists(int id)
        {
          return _context.PersonalInfo.Any(e => e.Id == id);
        }
    }
}
