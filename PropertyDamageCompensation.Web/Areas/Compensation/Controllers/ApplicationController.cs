using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using PropertyDamageCompensation.Web.Areas.Compensation.Entities;
using PropertyDamageCompensation.Web.Areas.Compensation.Models;
using PropertyDamageCompensation.Web.Data;
using PropertyDamageCompensation.Web.Email;
using PropertyDamageCompensation.Web.Exceptions;


namespace PropertyDamageCompensation.Web.Areas.Compensation.Controllers
{
    [Authorize]
    [Area("Compensation")]
    public class ApplicationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSending _emailSending;

        public ApplicationController(ApplicationDbContext context, IEmailSending emailSending)
        {
            _context = context;
            _emailSending = emailSending;
        }
        [Authorize(Roles = "Admin,Applicant,Engineer")]
        // GET: Compensation/Application
        public async Task<IActionResult> Index()
        {
            bool IsEmployee = HttpContext.User.IsInRole("Employee");
            var personlInfoId = await GetPersonalInfoIdOfLoggedInUser();
            string userRoleType = "";
            if (HttpContext.User.IsInRole("Engineer"))
            {
                userRoleType = "Engineer";
                ViewBag.EngineerName = User.Identity.Name;
            }
            else if (HttpContext.User.IsInRole("Admin"))
            {
                userRoleType = "Admin";
            }
            else if (HttpContext.User.IsInRole("Chief Engineer"))
            {
                userRoleType = "Chief Engineer";
            }else if (HttpContext.User.IsInRole("Applicant"))
            {
                userRoleType = "Applicant";
            }

            var applicationVM = await GetAppsFor(userRoleType,personlInfoId);

            return View(applicationVM);
                                     
        }
        private async Task<List<ApplicationViewModel>> GetAppsFor(string userRoleType,int personalInfoId)
        {
            string userId = "";
            var claim = User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                userId = claim.Value;
            }
            if (userRoleType == "Applicant" )
            {
                var applicationVMList = await (from app in _context.Application
                                               join personal in _context.PersonalInfo on app.PersonalInfoId equals personal.Id
                                               join town in _context.Towns on personal.LivingTownId equals town.Id
                                               join proptype in _context.PropertyType on app.PropertTypeId equals proptype.Id
                                               join floor in _context.Floor on app.FloorId equals floor.Id
                                               join appstate in _context.ApplicationState on app.StateId equals appstate.Id
                                               join apptown in _context.Towns on app.TownId equals apptown.Id
                                               where personal.Id == personalInfoId
                                               select new ApplicationViewModel
                                               {
                                                   Id = app.Id,
                                                   PersonalInfoId = app.PersonalInfoId,
                                                   AppDate = app.AppDate,
                                                   AppNumber = app.AppNumber,
                                                   FullName = personal.FirstName + ' ' + personal.MiddleName + ' ' + personal.LastName,
                                                   AddressStreetLivingTown = personal.Address + ' ' + personal.Street + ' ' + town.Name,
                                                   PropertTypeId = app.PropertTypeId,
                                                   PropertTypeName = proptype.Name,
                                                   Street = app.Street,
                                                   Building = app.Building,
                                                   Block = app.Block,
                                                   FloorId = app.FloorId,
                                                   FloorName = floor.Name,
                                                   Appartment = app.Appartment,
                                                   Ikar = app.Ikar,
                                                   SubIkar = app.SubIkar,
                                                   TownId = app.TownId,
                                                   TownName = apptown.Name,
                                                   StateId = app.StateId,
                                                   ApplicationState = appstate.Name
                                               }).ToListAsync();
                return applicationVMList;
            }
            if (userRoleType == "Engineer")
            {
                var applicationVMList = await (from app in _context.Application
                                               join personal in _context.PersonalInfo on app.PersonalInfoId equals personal.Id
                                               join town in _context.Towns on personal.LivingTownId equals town.Id
                                               join proptype in _context.PropertyType on app.PropertTypeId equals proptype.Id
                                               join floor in _context.Floor on app.FloorId equals floor.Id
                                               join appstate in _context.ApplicationState on app.StateId equals appstate.Id
                                               join apptown in _context.Towns on app.TownId equals apptown.Id
                                               join district in _context.District on apptown.DistrictId equals district.Id
                                               join engDistrict in (from de in _context.DistrictEngineer
                                                                    where de.EngineerId == userId
                                                                    select de) on district.Id equals engDistrict.DistrictId
                                               where HttpContext.User.IsInRole("Engineer")
                                               select new ApplicationViewModel
                                               {
                                                   Id = app.Id,
                                                   PersonalInfoId = app.PersonalInfoId,
                                                   AppDate = app.AppDate,
                                                   AppNumber = app.AppNumber,
                                                   FullName = personal.FirstName + ' ' + personal.MiddleName + ' ' + personal.LastName,
                                                   AddressStreetLivingTown = personal.Address + ' ' + personal.Street + ' ' + town.Name,
                                                   PropertTypeId = app.PropertTypeId,
                                                   PropertTypeName = proptype.Name,
                                                   Street = app.Street,
                                                   Building = app.Building,
                                                   Block = app.Block,
                                                   FloorId = app.FloorId,
                                                   FloorName = floor.Name,
                                                   Appartment = app.Appartment,
                                                   Ikar = app.Ikar,
                                                   SubIkar = app.SubIkar,
                                                   TownId = app.TownId,
                                                   TownName = apptown.Name,
                                                   StateId = app.StateId,
                                                   ApplicationState = appstate.Name
                                               }).ToListAsync();
                return applicationVMList;
            }
            if (userRoleType == "Admin")
            {
                var applicationVMList = await (from app in _context.Application
                                               join personal in _context.PersonalInfo on app.PersonalInfoId equals personal.Id
                                               join town in _context.Towns on personal.LivingTownId equals town.Id
                                               join proptype in _context.PropertyType on app.PropertTypeId equals proptype.Id
                                               join floor in _context.Floor on app.FloorId equals floor.Id
                                               join appstate in _context.ApplicationState on app.StateId equals appstate.Id
                                               join apptown in _context.Towns on app.TownId equals apptown.Id
                                               select new ApplicationViewModel
                                               {
                                                   Id = app.Id,
                                                   PersonalInfoId = app.PersonalInfoId,
                                                   AppDate = app.AppDate,
                                                   AppNumber = app.AppNumber,
                                                   FullName = personal.FirstName + ' ' + personal.MiddleName + ' ' + personal.LastName,
                                                   AddressStreetLivingTown = personal.Address + ' ' + personal.Street + ' ' + town.Name,
                                                   PropertTypeId = app.PropertTypeId,
                                                   PropertTypeName = proptype.Name,
                                                   Street = app.Street,
                                                   Building = app.Building,
                                                   Block = app.Block,
                                                   FloorId = app.FloorId,
                                                   FloorName = floor.Name,
                                                   Appartment = app.Appartment,
                                                   Ikar = app.Ikar,
                                                   SubIkar = app.SubIkar,
                                                   TownId = app.TownId,
                                                   TownName = apptown.Name,
                                                   StateId = app.StateId,
                                                   ApplicationState = appstate.Name
                                               }).ToListAsync();
                return applicationVMList;
            }
            return new List<ApplicationViewModel>();
        }



        [Authorize(Roles = "Admin,Applicant,Engineer")]
        // GET: Compensation/ApplicationViewModel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Application == null)
            {
                throw new BadRequestException($"Application with this Id : {id} is InvalidOr Some thing wrong in the server !"); ;
            }

            var applicationViewModel = await _context.Application
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationViewModel == null)
            {
                throw new NotFoundException($"Application with this Id {id} is not found !");
            }

            return View(applicationViewModel);
        }

        [Authorize(Roles = "Admin,Applicant")]
        // GET: Compensation/Application/Create
        public   async Task<IActionResult> Create()
        {
            Dictionary<string, object> dictionary =await  GetPersonalInfoForDisplay();
         
            ViewData["PersonalId"] =dictionary["PersonalId"];
            ViewData["FullName"] = dictionary["FullName"]; ;
            ViewData["DateOfBirth"] = dictionary["DateOfBirth"]; 
            ViewData["Phone"] = dictionary["Phone"]; ;
            ViewData["AddressStreetLivingTown"] = dictionary["AddressStreetLivingTown"]; 
            ViewData["TownId"] = new SelectList(_context.Towns, "Id", "Name");
            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyType, "Id", "Name");
            ViewData["StateId"] = new SelectList(_context.ApplicationState, "Id", "Name");
            ViewData["FloorId"] = new SelectList(_context.Floor, "Id", "Name");
            return View();
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
        private async Task<Dictionary<string, object>> GetPersonalInfoForDisplay()
        {
            Dictionary<string, object> dictionary = new () ;
            string userId = "";
            var claim = User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                userId = claim.Value;
            }
            var personalInfo = await(from person in _context.PersonalInfo

                                     join town in _context.Towns
                                     on person.LivingTownId equals town.Id
                                     where person.UserId == userId
                                     select new
                                     {
                                         PersonalId = person.Id,
                                         DateOfBirth = person.DateOfBirth.ToString("dd/MM/yyyy"),
                                         person.Phone,
                                         FullName = person.FirstName + ' ' + person.MiddleName + ' ' + person.LastName,
                                         AddressStreetLivingTown = town.Name + " , " + person.Address + " , Street : " + person.Street,
                                     }).FirstOrDefaultAsync();
            if (personalInfo != null)
            {
                dictionary["PersonalId"] = personalInfo.PersonalId;
                dictionary["FullName"] = personalInfo.FullName;
                dictionary["DateOfBirth"] = personalInfo.DateOfBirth;
                dictionary["Phone"] = personalInfo.Phone;
                dictionary["AddressStreetLivingTown"] = personalInfo.AddressStreetLivingTown;

            }
            else
            {
                dictionary["PersonalId"] = 0;
                dictionary["FullName"] = "";
                dictionary["DateOfBirth"] = "";
                dictionary["Phone"] = "";
                dictionary["AddressStreetLivingTown"] = "";
            }
            return dictionary;
        }
        private static string GetAppNumber()
        {
            return "DSGDGG";
        }
        // POST: Compensation/Application/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Applicant")]
        public async Task<IActionResult> Create([Bind("PersonalInfoId,AppDate,AppNumber,FullName,AddressStreetLivingTown,PropertTypeId,Street,Building,Block,FloorId,Appartment,Ikar,SubIkar,TownId,StateId")] ApplicationViewModel applicationViewModel)
        {
            if (ModelState.IsValid)
            {
                    var stateIdDefault=await (from sd in _context.ApplicationState
                                           where sd.IsDefault==true
                                           select sd.Id
                                           ).SingleOrDefaultAsync();


                    var application = new PropertyDamageCompensation.Domain.Entities.Application
                    {
                        PersonalInfoId = applicationViewModel.PersonalInfoId,
                        AppDate = applicationViewModel.AppDate,
                        AppNumber = GetAppNumber(),
                        PropertTypeId = applicationViewModel.PropertTypeId,
                        Street = applicationViewModel.Street,
                        Building = applicationViewModel.Building,
                        Block = applicationViewModel.Block,
                        FloorId = applicationViewModel.FloorId,
                        Appartment = applicationViewModel.Appartment,
                        Ikar = applicationViewModel.Ikar,
                        SubIkar = applicationViewModel.SubIkar,
                        TownId = applicationViewModel.TownId,
                        StateId = stateIdDefault
                    };
                    _context.Add(application);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
            }

            Dictionary<string, object> dictionary = await GetPersonalInfoForDisplay();

            ViewData["PersonalId"] = dictionary["PersonalId"];
            ViewData["FullName"] = dictionary["FullName"]; ;
            ViewData["DateOfBirth"] = dictionary["DateOfBirth"];
            ViewData["Phone"] = dictionary["Phone"]; ;
            ViewData["AddressStreetLivingTown"] = dictionary["AddressStreetLivingTown"];

            ViewData["TownId"] = new SelectList(_context.Towns, "Id", "Name");
            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyType, "Id", "Name");
            ViewData["StateId"] = new SelectList(_context.ApplicationState, "Id", "Name");
            ViewData["FloorId"] = new SelectList(_context.Floor, "Id", "Name");

            return View(applicationViewModel);
        }
        [Authorize(Roles = "Admin,Applicant")]
        [HttpGet]
        // GET: Compensation/Application/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }
            Dictionary<string, object> dictionary = await GetPersonalInfoForDisplay();

            ViewData["PersonalId"] = dictionary["PersonalId"];
            ViewData["FullName"] = dictionary["FullName"]; ;
            ViewData["DateOfBirth"] = dictionary["DateOfBirth"];
            ViewData["Phone"] = dictionary["Phone"]; ;
            ViewData["AddressStreetLivingTown"] = dictionary["AddressStreetLivingTown"];
            ViewData["TownId"] = new SelectList(_context.Towns, "Id", "Name");
            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyType, "Id", "Name");
            ViewData["StateId"] = new SelectList(_context.ApplicationState, "Id", "Name");
            ViewData["FloorId"] = new SelectList(_context.Floor, "Id", "Name");
            var applicationVM = await (from app in _context.Application
                                       where app.Id==id                                       
                                       select new ApplicationViewModel
                                       {
                                           Id = app.Id,
                                           PersonalInfoId = app.PersonalInfoId,
                                           AppDate = app.AppDate,
                                           AppNumber = app.AppNumber,
                                           PropertTypeId = app.PropertTypeId,
                                           Street = app.Street,
                                           Building = app.Building,
                                           Block = app.Block,
                                           FloorId = app.FloorId,
                                           Appartment = app.Appartment,
                                           Ikar = app.Ikar,
                                           SubIkar = app.SubIkar,
                                           TownId = app.TownId,
                                           StateId = app.StateId
                                       }).FirstOrDefaultAsync();
            if (applicationVM == null)
            {
                throw new NotFoundException($"Application with this Id {id} is not found !");
            }
            return View(applicationVM);
        }

        // POST: Compensation/Application/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Applicant")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonalInfoId,AppDate,AppNumber,PropertTypeId,Street,Building,Block,FloorId,Appartment,Ikar,SubIkar,TownId,StateId")] ApplicationViewModel applicationViewModel)
        {
            if (id != applicationViewModel.Id)
            {
                throw new BadRequestException($"Wrong Id : {id}  !"); ;
            }

            if (ModelState.IsValid)
            {
                //get the the record is being updated from the Date store
                var application = await _context.Application.FindAsync(id);
                if (application == null)
                {
                    throw new NotFoundException($" Application with this Id : {id} no longer exists");
                }
                application.AppDate = applicationViewModel.AppDate;
                application.AppNumber = applicationViewModel.AppNumber;
                application.PropertTypeId = applicationViewModel.PropertTypeId;
                application.Street = applicationViewModel.Street;
                application.Building = applicationViewModel.Building;
                application.Block = applicationViewModel.Block;
                application.FloorId = applicationViewModel.FloorId;
                application.Appartment = applicationViewModel.Appartment;
                application.Ikar = applicationViewModel.Ikar;
                application.SubIkar = applicationViewModel.SubIkar;
                application.TownId = applicationViewModel.TownId;
                application.StateId = applicationViewModel.StateId;
                _context.Update(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Applicant")]
        public async Task<IActionResult> Submit(int id )
        {

                //get the the record is being updated from the Date store
                var application = await _context.Application.FindAsync(id);
                if (application == null)
                {
                    throw new NotFoundException($" Application with this Id : {id} no longer exists");
                }
            var submittedId = await(from appState in _context.ApplicationState
                              where appState.Name == "Submitted"
                              select appState.Id).SingleAsync();
            if (submittedId == 0)
            {
                throw new NotFoundException("Submitted sate does not exists !");
            }
            application.StateId = submittedId;
            _context.Update(application);
            await _context.SaveChangesAsync();
            //send an email to the logged user, notifying successfull loging in operation
            var message = new EmailMessage();
            message.RecipientName = User.Identity.Name;
            message.RecipientEmail = User.Identity.Name;
            message.Subject =" Application Submitted Successfull";

            message.Body = @"Dear [Applicant's Name],

We are pleased to inform you that your application has been successfully submitted through our online platform. We would like to acknowledge receipt of your application regarding the damaged property.

Please be advised that our team is currently reviewing all submitted applications, and they will be examined by the relevant authorities to assess the necessary actions. We understand the importance of your application and assure you that it will receive the attention it deserves.

Should any additional information be required, we will reach out to you using the contact details provided in your application form.

We appreciate your patience during this process, and we will notify you promptly of any updates or decisions related to your application. If you have any questions or concerns, please do not hesitate to contact us at [contact information].

Thank you for choosing our services.

Sincerely,"; 

            await _emailSending.SendEmailHotmailAsync(message);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        // GET: Compensation/Application/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Application == null)
            {
                throw new BadRequestException($"Application with this Id :{id} is invalid Or Some thing wrong in the server !"); ;
            }
            var applicationVM = await (from app in _context.Application
                                       join personal in _context.PersonalInfo on app.PersonalInfoId equals personal.Id
                                       join town in _context.Towns on personal.LivingTownId equals town.Id
                                       join proptype in _context.PropertyType on app.PropertTypeId equals proptype.Id
                                       join floor in _context.Floor on app.FloorId equals floor.Id
                                       join appstate in _context.ApplicationState on app.StateId equals appstate.Id
                                       join apptown in _context.Towns on app.TownId equals apptown.Id
                                       where app.Id==id
                                       select new ApplicationViewModel
                                       {
                                           Id = app.Id,
                                           PersonalInfoId = app.PersonalInfoId,
                                           AppDate = app.AppDate,
                                           AppNumber = app.AppNumber,
                                           FullName = personal.FirstName + ' ' + personal.MiddleName + ' ' + personal.LastName,
                                           AddressStreetLivingTown = personal.Address + ' ' + personal.Street + ' ' + town.Name,
                                           PropertTypeId = app.PropertTypeId,
                                           PropertTypeName = proptype.Name,
                                           Street = app.Street,
                                           Building = app.Building,
                                           Block = app.Block,
                                           FloorId = app.FloorId,
                                           FloorName = floor.Name,
                                           Appartment = app.Appartment,
                                           Ikar = app.Ikar,
                                           SubIkar = app.SubIkar,
                                           TownId = app.TownId,
                                           TownName = apptown.Name,
                                           StateId = app.StateId,
                                           ApplicationState = appstate.Name
                                       }).FirstOrDefaultAsync();

            if (applicationVM == null)
            {
                throw new NotFoundException($" Application with this Id : {id} no longer exists");
            }

            return View(applicationVM);
        }
        [Authorize(Roles = "Admin")]
        // POST: Compensation/Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int a = 0;
            string b = "677";
            int c = 0;
            c = a + Convert.ToInt32( b);
            if (_context.Application == null)
            {
                throw new NotFoundException($" Internal Server error ");
            }
            var application = await _context.Application.FindAsync(id);
            if (application != null)
            {
                _context.Application.Remove(application);
            }
            else
            {
                throw new DbUpdateConcurrencyException();
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationViewModelExists(int id)
        {
          return _context.Application.Any(e => e.Id == id);
        }

        //Can be viewed by engineers, Chief engineer, and Admin
        [HttpGet]
        public async Task<IActionResult> SubmittedApps()
        {
            string userRoleType="";
            if (HttpContext.User.IsInRole("Engineer"))
            {
                userRoleType = "Engineer";
                ViewBag.EngineerName = User.Identity.Name;
            }
            else if (HttpContext.User.IsInRole("Admin"))
            {
                userRoleType = "Admin";
            }else if (HttpContext.User.IsInRole("Chief Engineer"))
            {
                userRoleType = "Chief Engineer";
            }
            var applicationVM = await GetSubmittedApps(userRoleType);

        
            return View(applicationVM);

        }

        private async Task<List<ApplicationViewModel>> GetSubmittedApps(string userRoleType)
        {
            string userId = "";
            var claim = User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                userId = claim.Value;
            }
            if (userRoleType=="Admin" || userRoleType=="Chief Engineer")
            {
                var applicationVMList = await (from app in _context.Application
                                           join personal in _context.PersonalInfo on app.PersonalInfoId equals personal.Id
                                           join town in _context.Towns on personal.LivingTownId equals town.Id
                                           join proptype in _context.PropertyType on app.PropertTypeId equals proptype.Id
                                           join floor in _context.Floor on app.FloorId equals floor.Id
                                           join appstate in _context.ApplicationState on app.StateId equals appstate.Id
                                           join apptown in _context.Towns on app.TownId equals apptown.Id
                                           where appstate.Id==2 // submitted
                                           select new ApplicationViewModel
                                           {
                                               Id = app.Id,
                                               PersonalInfoId = app.PersonalInfoId,
                                               AppDate = app.AppDate,
                                               AppNumber = app.AppNumber,
                                               FullName = personal.FirstName + ' ' + personal.MiddleName + ' ' + personal.LastName,
                                               AddressStreetLivingTown = personal.Address + ' ' + personal.Street + ' ' + town.Name,
                                               PropertTypeId = app.PropertTypeId,
                                               PropertTypeName = proptype.Name,
                                               Street = app.Street,
                                               Building = app.Building,
                                               Block = app.Block,
                                               FloorId = app.FloorId,
                                               FloorName = floor.Name,
                                               Appartment = app.Appartment,
                                               Ikar = app.Ikar,
                                               SubIkar = app.SubIkar,
                                               TownId = app.TownId,
                                               TownName = apptown.Name,
                                               StateId = app.StateId,
                                               ApplicationState = appstate.Name
                                           }).ToListAsync();
                return applicationVMList;
            }
            if (userRoleType == "Engineer")
            {
                var applicationVMList = await (from app in _context.Application
                                           join personal in _context.PersonalInfo on app.PersonalInfoId equals personal.Id
                                           join town in _context.Towns on personal.LivingTownId equals town.Id
                                           join proptype in _context.PropertyType on app.PropertTypeId equals proptype.Id
                                           join floor in _context.Floor on app.FloorId equals floor.Id
                                           join appstate in _context.ApplicationState on app.StateId equals appstate.Id
                                           join apptown in _context.Towns on app.TownId equals apptown.Id
                                           join district in _context.District on apptown.DistrictId equals district.Id
                                           join engDistrict in (from de in _context.DistrictEngineer
                                                                where de.EngineerId==userId
                                                                select de) on district.Id equals engDistrict.DistrictId
                                           where appstate.Id == 2 // submitted
                                                 && HttpContext.User.IsInRole("Engineer")
                                           select new ApplicationViewModel
                                           {
                                               Id = app.Id,
                                               PersonalInfoId = app.PersonalInfoId,
                                               AppDate = app.AppDate,
                                               AppNumber = app.AppNumber,
                                               FullName = personal.FirstName + ' ' + personal.MiddleName + ' ' + personal.LastName,
                                               AddressStreetLivingTown = personal.Address + ' ' + personal.Street + ' ' + town.Name,
                                               PropertTypeId = app.PropertTypeId,
                                               PropertTypeName = proptype.Name,
                                               Street = app.Street,
                                               Building = app.Building,
                                               Block = app.Block,
                                               FloorId = app.FloorId,
                                               FloorName = floor.Name,
                                               Appartment = app.Appartment,
                                               Ikar = app.Ikar,
                                               SubIkar = app.SubIkar,
                                               TownId = app.TownId,
                                               TownName = apptown.Name,
                                               StateId = app.StateId,
                                               ApplicationState = appstate.Name
                                           }).ToListAsync();
                return applicationVMList;
            }
            return new List<ApplicationViewModel>();
        }
    }
}
