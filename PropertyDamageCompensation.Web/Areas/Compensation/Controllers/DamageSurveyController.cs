using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PropertyDamageCompensation.Web.Areas.Compensation.Entities;
using PropertyDamageCompensation.Web.Areas.Compensation.Models;
using PropertyDamageCompensation.Web.Data;
using PropertyDamageCompensation.Web.Exceptions;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Controllers
{
    [Authorize]
    [Area("Compensation")]
    public class DamageSurveyController : Controller
    {
        private class LoggedInEngineer
        {
            public string UserId { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
        }

        private readonly ApplicationDbContext _context;
        public DamageSurveyController(ApplicationDbContext context) { 
            _context = context;
        }
        public IActionResult Index()
        {

            //var peronalAndAppInfo=await(from personal in _context.PersonalInfo
            //                            join app in _context.Application on per)
            return View();
        }

        [Authorize(Roles = "Admin,Engineer,Applicant")]
        [HttpGet]
        public async Task<IActionResult> DamageSurveyDetails(string name, string address,
            string personalInfoId, string applicationId, string appNumber, string appDate, string building, string town, string ikar)
        {
            // var da

            ViewBag.Name = name;
            ViewBag.Address = address;
            ViewBag.AppNumber = appNumber;
            ViewBag.AppDate = appDate;
            ViewBag.Building = building;
            ViewBag.Town = town;
            ViewBag.Ikar = ikar;
            LoggedInEngineer engineer = await GetLoggedEngineer();
            ViewBag.EngineerName = engineer.Name;
            ViewBag.DamageItemDefId = new SelectList(_context.DamageItemDef, "Id", "Name");
            ViewBag.ItemDefs = await (from di in _context.DamageItemDef
                                      select new
                                      {
                                          di.Id,
                                          di.UnitPrice
                                      }).ToListAsync();
            var damageSurveyItemsViewMode =
                await (from ds in _context.DamageSurvey
                       where ds.ApplicationId == Convert.ToInt32(applicationId)
                       select new DamageSurveyItemsViewModel
                       {
                           DamageSurveyId = ds.Id,
                           Date = ds.Date,
                           ApplicationId = ds.ApplicationId,
                           PersonalInfoId = ds.PersonalInfoId,
                           EngineerId = ds.EngineerId,
                           ListOfDamageItems = (
                           from dia in ds.DamageItemAssessed
                           select new DamageItemAssessedViewModel
                           {
                               Id = dia.Id,
                               DamageItemDefId = dia.DamageItemDefId,
                               DamageItemdefName = dia.DamageItemDef.Name,
                               DamageItemDefPrice = dia.DamageItemDefPrice,
                               Qty = dia.Qty,
                               TotalAmount = dia.TotalAmount
                           }).ToList()
                       }).FirstOrDefaultAsync();

            if (damageSurveyItemsViewMode == null)
            {
                damageSurveyItemsViewMode = new DamageSurveyItemsViewModel
                {
                    Date = DateTime.Now,
                    ApplicationId = Convert.ToInt32(applicationId),
                    PersonalInfoId = Convert.ToInt32(personalInfoId),
                    EngineerId = engineer.UserId
                };
            }
            return View(damageSurveyItemsViewMode);
        }


        [Authorize(Roles ="Admin,Engineer,Applicant")]
        [HttpGet]
        public async Task<IActionResult> ManageDamageSurvey(string name, string address,
            string personalInfoId, string applicationId, string appNumber, string appDate, string building, string town, string ikar)
        {
            // var da

            ViewBag.Name = name;
            ViewBag.Address = address;
            ViewBag.AppNumber = appNumber;
            ViewBag.AppDate = appDate;
            ViewBag.Building = building;
            ViewBag.Town = town;
            ViewBag.Ikar = ikar;
            LoggedInEngineer engineer = await GetLoggedEngineer();
            ViewBag.EngineerName = engineer.Name;
            ViewBag.DamageItemDefId = new SelectList(_context.DamageItemDef, "Id", "Name");
            ViewBag.ItemDefs = await (from di in _context.DamageItemDef
                                      select new
                                      { di.Id, di.UnitPrice
                                      }).ToListAsync(); 
            var damageSurveyItemsViewMode =
                await (from ds in _context.DamageSurvey
                       where ds.ApplicationId == Convert.ToInt32(applicationId)
                       select new DamageSurveyItemsViewModel
                       {
                           DamageSurveyId = ds.Id,
                           Date = ds.Date,
                           ApplicationId = ds.ApplicationId,
                           PersonalInfoId = ds.PersonalInfoId,
                           EngineerId = ds.EngineerId,
                           ListOfDamageItems = (
                           from dia in ds.DamageItemAssessed
                           select new DamageItemAssessedViewModel
                           {
                               Id = dia.Id,
                               DamageItemDefId = dia.DamageItemDefId,
                               DamageItemdefName = dia.DamageItemDef.Name,
                               DamageItemDefPrice = dia.DamageItemDefPrice,
                               Qty = dia.Qty,
                               TotalAmount = dia.TotalAmount
                           }).ToList()
                       }).FirstOrDefaultAsync();

            if (damageSurveyItemsViewMode == null)
            {
                damageSurveyItemsViewMode = new DamageSurveyItemsViewModel
                {
                    Date = DateTime.Now,
                    ApplicationId = Convert.ToInt32(applicationId),
                    PersonalInfoId = Convert.ToInt32(personalInfoId),
                    EngineerId = engineer.UserId
                };
            }
            return View(damageSurveyItemsViewMode);
        }

        private async Task<LoggedInEngineer> GetLoggedEngineer()
        {
            string userId = "";
            var claim = User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                userId= "";
                return new LoggedInEngineer { UserId="",Name=""};
            }
            userId = claim.Value;
            var engineer=await (from u in _context.Users
                                    where u.Id == userId
                                    select new LoggedInEngineer
                                    {
                                        UserId = u.Id,
                                        Name = u.FirstName + " " + u.LastName 
                                    }).SingleOrDefaultAsync();
            return engineer;
        }
        [Authorize(Roles = "Admin,Engineer")]
        [HttpPost]
        public async Task<IActionResult> AddNewAssessedItem(DamageSurveyItemViewModel data)
        {
            if (data == null || data.ItemAssessed==null)
            {
                throw new BadRequestException("Invalid parameter datadata");
            }
            var damageSurveyId = data.DamageSurveyId;

            // test id MangeSurveyId exists, so we don't need to addd a new record to DamageSurvey entity
            var damageSurvey = await (from ds in _context.DamageSurvey
                                      where ds.Id == damageSurveyId
                                      select ds).FirstOrDefaultAsync();


             if (damageSurvey == null)
            {
                damageSurvey = new DamageSurvey
                {
                    Date = data.Date,
                    PersonalInfoId = data.PersonalInfoId,
                    ApplicationId = data.ApplicationId,
                    EngineerId = data.EngineerId
                };
                _context.DamageSurvey.Add(damageSurvey);
                await _context.SaveChangesAsync();
            }
            else
            {
                //update the current record in DamageSurvey
                damageSurvey.Date = data.Date;
                _context.DamageSurvey.Update(damageSurvey);
                await _context.SaveChangesAsync();
            };

            //  we  add DamageItemAssessed record
            var damageAssessedItem = new DamageItemAssessed
            {
                DamageSurveyId = damageSurvey.Id,
                DamageItemDefId = data.ItemAssessed.DamageItemDefId,
                DamageItemDefPrice = data.ItemAssessed.DamageItemDefPrice,
                Qty = Convert.ToInt16(data.ItemAssessed.Qty),
                TotalAmount = data.ItemAssessed.TotalAmount
            };
            _context.DamageItemAssessed.Add(damageAssessedItem);
            await _context.SaveChangesAsync();




            // render the list of DamageItemAssessedViewModel to the partial view
            var model = await (from dia in _context.DamageItemAssessed
                               where dia.DamageSurveyId == damageSurvey.Id && dia.DamageItemDef !=null
                               select new DamageItemAssessedViewModel
                               {
                                   Id = dia.Id,
                                   DamageItemDefId = dia.DamageItemDefId,
                                   DamageItemdefName = dia.DamageItemDef.Name,
                                   DamageItemDefPrice = dia.DamageItemDefPrice,
                                   Qty = dia.Qty,
                                   TotalAmount = dia.TotalAmount
                               }).ToListAsync();

            return PartialView("_DamageItemAssessedListPartialView", model);

           
        }
        [Authorize(Roles = "Admin,Engineer")]
        [HttpPost]
        public async Task<IActionResult> EditAssessedItem(DamageSurveyItemViewModel data)
        {
            if (data == null || data.ItemAssessed == null)
            {
                throw new BadRequestException("Invalid parameter datadata");
            }
            var damageSurveyId = data.DamageSurveyId;
            // test id MangeSurveyId exists, so we don't need to addd a new record to DamageSurvey entity
            var damageSurvey = await (from ds in _context.DamageSurvey
                                            where ds.Id == damageSurveyId
                                            select ds).FirstOrDefaultAsync();

            if (damageSurvey!=null)// here always we have a record for DamageSurvey.So this condition is always evaluate to true
            {
                //update the current record in DamageSurvey
                damageSurvey.Date = data.Date;
                _context.DamageSurvey.Update(damageSurvey);
                await _context.SaveChangesAsync();

               
                var damageItemAssessed = await (from dia in _context.DamageItemAssessed
                                                where data.ItemAssessed.Id == dia.Id
                                                select dia).FirstOrDefaultAsync();

                // damagesurvey record exists, so we need only to add DamageItemAssessed record
                if (damageItemAssessed != null)
                {
                    damageItemAssessed.DamageItemDefPrice = data.ItemAssessed.DamageItemDefPrice;
                    damageItemAssessed.Qty = Convert.ToInt16(data.ItemAssessed.Qty);
                    damageItemAssessed.TotalAmount = data.ItemAssessed.TotalAmount;
                }

                _context.DamageItemAssessed.Update(damageItemAssessed);
                await _context.SaveChangesAsync();
            }



            // render the list of DamageItemAssessedViewModel to the partial view
            var model = await (from dia in _context.DamageItemAssessed.AsNoTracking()
                               where dia.DamageSurveyId == damageSurveyId && dia.DamageItemDef != null
                               select new DamageItemAssessedViewModel
                               {
                                   Id = dia.Id,
                                   DamageItemDefId = dia.DamageItemDefId,
                                   DamageItemdefName = dia.DamageItemDef.Name,
                                   DamageItemDefPrice = dia.DamageItemDefPrice,
                                   Qty = dia.Qty,
                                   TotalAmount = dia.TotalAmount
                               }).ToListAsync();

            return PartialView("_DamageItemAssessedListPartialView", model);


        }
        [HttpPost]
        [Authorize(Roles = "Admin,Engineer")]
        public async Task<IActionResult> DeleteDamageAssessedItem(int itemId,int damageSurveyId)
        {
            if (itemId == 0)
            {
                throw new BadRequestException("Invalid Item Id");
            };

            var item = await (from dai in _context.DamageItemAssessed
                              where dai.Id == itemId
                              select dai).FirstOrDefaultAsync();
            if (item == null)
            {
                throw new BadRequestException("Invalid Item Id");
            }
            _context.DamageItemAssessed.Remove(item);
            await _context.SaveChangesAsync();

            // render the list of DamageItemAssessedViewModel to the partial view
            var model = await (from dia in _context.DamageItemAssessed
                               where dia.DamageSurveyId == damageSurveyId && dia.DamageItemDef != null
                               select new DamageItemAssessedViewModel
                               {
                                   Id = dia.Id,
                                   DamageItemDefId = dia.DamageItemDefId,
                                   DamageItemdefName = dia.DamageItemDef.Name,
                                   DamageItemDefPrice = dia.DamageItemDefPrice,
                                   Qty = dia.Qty,
                                   TotalAmount = dia.TotalAmount
                               }).ToListAsync();

            return PartialView("_DamageItemAssessedListPartialView", model);
        }
    }

}
